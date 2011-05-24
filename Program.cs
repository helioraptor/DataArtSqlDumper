using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace DataArtSqlDumper
{
    class Program
    {
        private class Table
        {
            public Table(string name)
            {
                this.name = name;
            }
            //TODO:property
            public string name;
        }
        private static List<Table> tables = new List<Table>();

        static string getScriptFromResource(string resourceName)
        {
            using (StreamReader r = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
            {
                return r.ReadToEnd();
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Out.WriteLine("usage:");
                Console.Out.WriteLine("DataArtSqlDumper.exe \"data source=local;initial catalog=Northwind;Connection Lifetime=120;Connection Timeout=60;Timeout=60;User=sa;Password=password\" \"out.txt\"" );
                return;
            }
            String connectionString = args[0];
            String outFileName = args[1];

            //TODO: generify
            const String resourceName = "DataArtSqlDumper.Resources.getUserTables.txt";
            string tableRetrievalScript = getScriptFromResource(resourceName);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(tableRetrievalScript, con))
                {
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tables.Add(new Table(dr.GetString(0)));
                        }
                    }
                }

                string setIdentityScript = getScriptFromResource("DataArtSqlDumper.Resources.setIdentity.txt");

                using (StreamWriter writer = new StreamWriter(outFileName, false, Encoding.Unicode))
                {
                    foreach (Table table in tables)
                    {
                        writer.WriteLine(String.Format(setIdentityScript, table.name, "on", "NO"));
                        String sql = "select * from [" + table.name + "]";
                        using (SqlCommand com = new SqlCommand(sql, con))
                        {
                            using (SqlDataReader dr = com.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    String strFields = "";
                                    String strValues = "";
                                    for (int i = 0; i != dr.FieldCount; i++)
                                    {
                                        if ("" != strValues)
                                            strValues += ",";
                                        if ("" != strFields)
                                            strFields += ",";
                                        string dataType = dr.GetDataTypeName(i);
                                        strFields += "[" + dr.GetName(i) + "]";
                                        if (dr.IsDBNull(i))
                                            strValues += "NULL";
                                        else
                                        {
                                            switch (dataType)
                                            {
                                                case "bit":
                                                    strValues += string.Format("{0}", dr.GetBoolean(i) ? 1 : 0);
                                                    break;
                                                case "bigint":
                                                case "smallint":
                                                case "tinyint":
                                                case "int":
                                                case "float":
                                                case "money":
                                                case "numeric":
                                                case "real":
                                                case "smallmoney":
                                                case "decimal":
                                                    strValues += string.Format("{0}", dr.GetValue(i)); //may need new CultureInfo("en-US"),
                                                    break;
                                                case "char":
                                                case "ntext":
                                                case "nchar":
                                                case "nvarchar":
                                                case "sysname":
                                                case "text":
                                                case "varchar":
                                                case "xml":
                                                    strValues += "'" + dr.GetString(i).Replace("'", "''") + "'";
                                                    break;
                                                case "uniqueidentifier":
                                                    strValues += "'" + dr.GetGuid(i).ToString() + "'";
                                                    break;
                                                case "binary":
                                                case "image":
                                                case "sql_variant":
                                                case "varbinary":
                                                    //binary data
                                                    strValues += "NULL";
                                                    break;
                                                case "timestamp":
                                                    //not updateable
                                                    strValues += "NULL";
                                                    break;
                                                case "datetime":
                                                case "smalldatetime":
                                                    strValues += "'" + string.Format("{0:yyyyMMdd HH:mm:ss}", dr.GetDateTime(i)) + "'";
                                                    break;
                                                default:
                                                    strValues += dr.GetString(i);
                                                    break;
                                            }
                                        }
                                    }
                                    String insertSql = String.Format("Insert [{0}] ({1}) values ({2})", table.name, strFields, strValues);
                                    writer.WriteLine(insertSql);
                                    Debug.WriteLine(insertSql);
                                }//for each record
                            }
                        }
                        writer.WriteLine(String.Format(setIdentityScript, table.name, "off", ""));
                    } //for each table
                }
            }
        }
    }
}
