﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataArtSqlDumper.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DataArtSqlDumper.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*tables*/
        ///Declare @t table(object_id int, level int)
        ///
        ////*objects who are not parents*/
        ///Insert @t(object_id, level) 
        ///select o.object_id, 0 from sys.objects o 
        ///where o.type = &apos;U&apos;
        ///and not exists(select 1 from sys.foreign_keys f 
        ///			   where o.object_id = f.referenced_object_id) 
        ///
        ///Declare @level int
        ///Set @level = 1
        ///
        ///WHILE(1=1)
        /// BEGIN
        ///	Insert @t(object_id,level)
        ///	select distinct f.referenced_object_id, @level
        ///	from sys.foreign_keys f
        ///    join @t t on t.object_id = f.parent_object_id
        ///	where not  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string getUserTables {
            get {
                return ResourceManager.GetString("getUserTables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to if exists(select 1 from sys.columns c where c.object_id = object_id(&apos;{0}&apos;) and is_identity = 1)
        ///	set identity_insert [{0}] {1}.
        /// </summary>
        internal static string setIdentity {
            get {
                return ResourceManager.GetString("setIdentity", resourceCulture);
            }
        }
    }
}
