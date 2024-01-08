using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Text.RegularExpressions;

namespace Blueprint41.DatastoreTemplates
{
    public static class Generator
    {
        public static GeneratorResult Execute<T>(GeneratorSettings? settings = null)
            where T : DatastoreModel<T>, new()
        {
            if (settings is null)
                throw new ArgumentNullException("settings");

            DatastoreModel model = DatastoreModel<T>.Model;

            GeneratorResult generatorResult = new GeneratorResult();

            #region Entities and Nodes

            foreach (var entity in model.Entities.Where(item => item.IsAbstract))
            {
                Domain_Data_Entity_Abstract t4 = new Domain_Data_Entity_Abstract();
                t4.Settings = settings;
                t4.DALModel = entity;
                t4.Datastore = model;
                string content = t4.TransformText();
                generatorResult.EntityResult.Add(entity.Name, content);

                Domain_Data_Node node = new Domain_Data_Node();
                node.Settings = settings;
                node.DALModel = entity;
                node.Datastore = model;
                string nodeContent = node.TransformText();
                generatorResult.NodeResult.Add(string.Format("{0}Node", entity.Name), nodeContent);

            }
            foreach (var entity in model.Entities.Where(item => !item.IsAbstract))
            {
                Domain_Data_Entity t4 = new Domain_Data_Entity();
                t4.Settings = settings;
                t4.DALModel = entity;
                t4.Datastore = model;
                string content = t4.TransformText();
                generatorResult.EntityResult.Add(entity.Name, content);

                Domain_Data_Node node = new Domain_Data_Node();
                node.Settings = settings;
                node.DALModel = entity;
                node.Datastore = model;
                string nodeContent = node.TransformText();
                generatorResult.NodeResult.Add(string.Format("{0}Node", entity.Name), nodeContent);
            }

            #endregion

            #region Relationships

            foreach (var relation in model.Relations)
            {
                Domain_Data_Entity_Relation t4 = new Domain_Data_Entity_Relation();
                t4.Settings = settings;
                t4.DALRelation = relation;
                t4.Datastore = model;
                string content = t4.TransformText();
                generatorResult.EntityResult.Add(relation.Name, content);

                Domain_Data_Relationship relationship_template = new Domain_Data_Relationship();
                relationship_template.Settings = settings;
                relationship_template.DALRelation = relation;
                relationship_template.Datastore = model;
                string relContent = relationship_template.TransformText();
                generatorResult.RelationshipResult.Add(relation.Name, relContent);
            }

            #endregion

            #region Register

            Domain_Data_Register register = new Domain_Data_Register();
            register.Settings = settings;
            register.DALModel = null;
            register.Datastore = model;
            string registerContent = register.TransformText();
            generatorResult.EntityResult.Add("_Register", registerContent);

            #endregion

            #region GraphEvents

            Domain_Data_GraphEvents ge = new Domain_Data_GraphEvents();
            ge.Settings = settings;
            ge.DALModel = null;
            ge.Datastore = model;
            string geContent = ge.TransformText();
            generatorResult.EntityResult.Add("_GraphEvents", geContent);

            #endregion

            if (!string.IsNullOrEmpty(settings.ProjectFolder))
            {
                // create Entities, Nodes and Relationship

                if (!string.IsNullOrEmpty(settings.EntitiesFolder))
                    CreateFilesFromDictionary(generatorResult.EntityResult, Path.Combine(settings.ProjectFolder, settings.EntitiesFolder));

                if (!string.IsNullOrEmpty(settings.NodesFolder))
                    CreateFilesFromDictionary(generatorResult.NodeResult, Path.Combine(settings.ProjectFolder, settings.NodesFolder));

                if (!string.IsNullOrEmpty(settings.RelationshipsFolder))
                    CreateFilesFromDictionary(generatorResult.RelationshipResult, Path.Combine(settings.ProjectFolder, settings.RelationshipsFolder));
            }

            return generatorResult;
        }

        private static void CreateFilesFromDictionary(Dictionary<string, string> dictionary, string path)
        {
            Directory.CreateDirectory(path);
            foreach (KeyValuePair<string, string> item in dictionary)
            {
                string entityPath = Path.Combine(path, item.Key + ".cs");
                using (FileStream fs = File.Create(entityPath))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(item.Value);
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        private static void RecursiveDelete(string? currentDirectory)
        {
            if (Directory.GetFiles(currentDirectory).Length == 0)
            {
                string? parentDirectory = Path.GetDirectoryName(currentDirectory);
                Directory.Delete(currentDirectory);
                RecursiveDelete(parentDirectory);
            }
        }

        private static void GetDirectories(string folder, string[] paths, int index, List<string> directories, List<string> files)
        {
            if (paths.Length - 1 == index)
            {
                string[] filesToAdd = Directory.GetFiles(folder, paths[index]);
                if (filesToAdd.Length > 0)
                {
                    directories.Add(folder);
                    files.AddRange(filesToAdd);
                }
                return;
            }

            if (paths[index].Contains("?") || paths[index].Contains("*"))
            {
                string[] searchDirectories = Directory.GetDirectories(folder, paths[index]);
                foreach (var subDirectory in searchDirectories)
                {
                    DirectoryInfo directory = new DirectoryInfo(subDirectory);
                    if (directory.Name.StartsWith("_") == false)
                        GetDirectories(subDirectory, paths, index + 1, directories, files);
                }
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(Path.Combine(folder, paths[index]));
                if (directory.Exists && directory.Name.StartsWith("_") == false)
                    GetDirectories(directory.FullName, paths, index + 1, directories, files);
            }
        }

        public static string ToCamelCase(this string value)
        {
            if (value.Length <= 2)
                return value.ToLowerInvariant();

            int count = 0;
            foreach (char character in value)
            {
                if (char.IsUpper(character))
                    count++;
                else
                    break;
            }

            if (count >= 2)
                count--;

            return string.Concat(value.Substring(0, count).ToLowerInvariant(), value.Substring(count));
        }
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToProperCase());
        }
        public static string ToProperCase(this string value)
        {
            StringBuilder retVal = new StringBuilder(32);

            if (!string.IsNullOrEmpty(value))
            {
                string strTrimmed = value.Trim();

                if (!string.IsNullOrEmpty(strTrimmed))
                {
                    retVal.Append(char.ToUpper(strTrimmed[0]));

                    if (strTrimmed.Length > 1)
                    {
                        for (int i = 1; i < strTrimmed.Length; i++)
                        {
                            if (char.IsUpper(strTrimmed[i])) retVal.Append(" ");

                            retVal.Append(char.ToLower(strTrimmed[i]));
                        }
                    }
                }
            }
            return retVal.ToString();
        }
        public static string ToLowerCase(this string value)
        {
            return value.ToProperCase().ToLowerInvariant();
        }
        public static string ToPlural(this string Singular)
        {
            if (MatchAndReplace(ref Singular, "people", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "craft", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "sheep", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "deer", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "series", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "species", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "tooth", "eeth", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "goose", "eese", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "trix", "ces", 1) == true) return Singular;
            if (MatchAndReplace(ref Singular, "mouse", "ice", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "louse", "ice", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "foot", "eet", 3) == true) return Singular;
            if (MatchAndReplace(ref Singular, "zoon", "a", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "info", "s", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "eau", "x", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "ieu", "x", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "man", "en", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "cis", "es", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "sis", "es", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "xis", "es", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "ies", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "ch", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "fe", "ves", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "sh", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "o", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "f", "ves", 1) == true) return Singular;
            if (MatchAndReplace(ref Singular, "s", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "x", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "y", "ies", 1) == true) return Singular;
            MatchAndReplace(ref Singular, "", "s", 0);
            return Singular;
        }
        public static string ToSingular(this string Plural)
        {
            if (MatchAndReplace(ref Plural, "people", "", 0) == true) return Plural;
            if (MatchAndReplace(ref Plural, "craft", "", 0) == true) return Plural;
            if (MatchAndReplace(ref Plural, "sheep", "", 0) == true) return Plural;
            if (MatchAndReplace(ref Plural, "deer", "", 0) == true) return Plural;
            if (MatchAndReplace(ref Plural, "series", "", 0) == true) return Plural;
            if (MatchAndReplace(ref Plural, "species", "", 0) == true) return Plural;
            if (MatchAndReplace(ref Plural, "teeth", "ooth", 4) == true) return Plural;
            if (MatchAndReplace(ref Plural, "geese", "oose", 4) == true) return Plural;
            if (MatchAndReplace(ref Plural, "trices", "x", 3) == true) return Plural;
            if (MatchAndReplace(ref Plural, "mice", "ouse", 3) == true) return Plural;
            if (MatchAndReplace(ref Plural, "lice", "ouce", 3) == true) return Plural;
            if (MatchAndReplace(ref Plural, "feet", "oot", 3) == true) return Plural;
            if (MatchAndReplace(ref Plural, "zoa", "on", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "infos", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "eaux", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "ieux", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "men", "an", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "cis", "es", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "sis", "es", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "xis", "es", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "ches", "", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "ves", "fe", 3) == true) return Plural;
            if (MatchAndReplace(ref Plural, "shes", "", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "oes", "", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "ves", "f", 3) == true) return Plural;
            if (MatchAndReplace(ref Plural, "ses", "", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "xes", "", 2) == true) return Plural;
            if (MatchAndReplace(ref Plural, "pies", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "movies", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "calories", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "freebies", "", 1) == true) return Plural;
            if (MatchAndReplace(ref Plural, "cookies", "", 1) == true) return Plural;

            if (MatchAndReplace(ref Plural, "ies", "y", 3) == true) return Plural;
            
            
            MatchAndReplace(ref Plural, "s", "", 1);
            return Plural;
        }
        private static bool MatchAndReplace(ref string Text, string Match, string Replace, int Amount)
        {
            if (Text.EndsWith(Match, System.StringComparison.CurrentCultureIgnoreCase) == true)
            {
                if (Text.Length > 0 && Text.Substring(Text.Length - 1) == Text.Substring(Text.Length - 1).ToUpper())
                    Replace = Replace.ToUpper();

                Text = Text.Substring(0, Text.Length - Amount) + Replace;
                return true;
            }
            return false;
        }
        public static decimal ParseVersion(this string version)
        {
            if (!version.StartsWith("v"))
                throw new FormatException("Version string is not in correct format.");

            return decimal.Parse(version.Substring(1), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }
        public static string ToNeo4jType(this string type)
        {
            switch (type)
            {
                case "DateTime":
                    return "long";
                case "DateTime?":
                    return "long?";
                case "decimal":
                    return "long";
                case "decimal?":
                    return "long?";
                case "Guid":
                    return "string";
                case "Guid?":
                    return "string";
                default:
                    return type;
            }
        }
        public static string ToXsdType(this string type)
        {
            switch (type.Replace("?", ""))
            {
                case "bool":
                    return "xs:boolean";
                default:
                    return string.Concat("xs:", type.Replace("?", "").ToCamelCase());
            }
        }

        public static string ToXmlEscape(this string self)
        {
            if (string.IsNullOrWhiteSpace(self))
                return self;

            return self.Replace("&", "&amp;")
                .Replace("\"", "&quot;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("'", "&apos;");
        }

        public static string ToNormalString(this string self)
        {
            if (string.IsNullOrWhiteSpace(self))
                return self;

            return self.Replace("&amp;", "&")
                .Replace("&quot;", "\"")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&apos;", "'");
        }

        public static string ToJsonNotation(this IEnumerable<Property> properties)
        {
            return string.Join(", ", properties
                                        .Where(p => p.SystemReturnType is not null && p.PropertyType == PropertyType.Attribute && (p.SystemReturnType.Namespace == "System" || p.SystemReturnType.IsEnum))
                                        .OrderBy(p => p.Name)
                                        .Select(p => $"JsNotation<{p.SystemReturnType!.ToCSharp()}{((p.SystemReturnType!.IsValueType && p.Nullable) ? "?" : "")}> {p.Name} = default")
                                        );
        }
    }
}
