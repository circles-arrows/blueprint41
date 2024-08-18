using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Globalization;

namespace Blueprint41.Async.DatastoreTemplates
{
    public static class Generator
    {
        private const string FileExtension = ".cs";
        public static GeneratorResult Execute<T>(GeneratorSettings settings)
            where T : DatastoreModel<T>, new()
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));

            DatastoreModel model = DatastoreModel<T>.Model;

            GeneratorResult generatorResult = new();

            #region Entities and Nodes

            foreach (var entity in model.Entities.Where(item => item.IsAbstract))
            {
                Domain_Data_Entity_Abstract t4 = new()
                {
                    Settings = settings,
                    DALModel = entity,
                    Datastore = model
                };
                string content = t4.TransformText();
                generatorResult.EntityResult.Add(entity.Name, content);

                Domain_Data_Node node = new()
                {
                    Settings = settings,
                    DALModel = entity,
                    Datastore = model
                };
                string nodeContent = node.TransformText();
                generatorResult.NodeResult.Add($"{entity.Name}Node", nodeContent);
            }
            foreach (var entity in model.Entities.Where(item => !item.IsAbstract))
            {
                Domain_Data_Entity t4 = new()
                {
                    Settings = settings,
                    DALModel = entity,
                    Datastore = model
                };
                string content = t4.TransformText();
                generatorResult.EntityResult.Add(entity.Name, content);

                Domain_Data_Node node = new()
                {
                    Settings = settings,
                    DALModel = entity,
                    Datastore = model
                };
                string nodeContent = node.TransformText();
                generatorResult.NodeResult.Add($"{entity.Name}Node", nodeContent);
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

            Domain_Data_Register register = new()
            {
                Settings = settings,
                DALModel = null,
                Datastore = model
            };
            string registerContent = register.TransformText();
            generatorResult.EntityResult.Add("_Register", registerContent);

            #endregion

            #region GraphEvents

            Domain_Data_GraphEvents ge = new()
            {
                Settings = settings,
                DALModel = null,
                Datastore = model
            };
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
            EnsureDirectoryExists(path);
            DeleteUnmatchedFiles(path, dictionary.Keys);
            CreateOrUpdateFiles(path, dictionary);
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void DeleteUnmatchedFiles(string path, ICollection<string> validFileNames)
        {
            var existingFiles = Directory.GetFiles(path, "*" + FileExtension)
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();

            var filesToDelete = existingFiles.Except(validFileNames);
            foreach (var fileToDelete in filesToDelete)
            {
                File.Delete(Path.Combine(path, fileToDelete + FileExtension));
            }
        }

        private static void CreateOrUpdateFiles(string path, Dictionary<string, string> filesContent)
        {
            foreach (var fileEntry in filesContent)
            {
                string filePath = Path.Combine(path, fileEntry.Key + FileExtension);
                File.WriteAllText(filePath, fileEntry.Value, new UTF8Encoding(false));
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
            StringBuilder retVal = new(32);

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
            if (Text.EndsWith(Match, System.StringComparison.CurrentCultureIgnoreCase))
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
            return type switch
            {
                "DateTime" => "long",
                "DateTime?" => "long?",
                "decimal" => "long",
                "decimal?" => "long?",
                "Guid" => "string",
                "Guid?" => "string",
                _ => type,
            };
        }
        public static string ToXsdType(this string type)
        {
            return type.Replace("?", "") switch
            {
                "bool" => "xs:boolean",
                _ => string.Concat("xs:", type.Replace("?", "").ToCamelCase()),
            };
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

        public static string ToJsonNotation(this IEnumerable<Property> properties, Relationship? relation = null, bool prepend = false, bool creationDate = false)
        {
            //if (relation is not null)
            //    prepend = true;

            IEnumerable<Property> items = properties
                .Where(p => p.SystemReturnType is not null && p.PropertyType == PropertyType.Attribute && (p.SystemReturnType.Namespace == "System" || p.SystemReturnType.IsEnum))
                .OrderBy(p => p.Name);

            if (relation is not null)
                items = items.Where(p => p.Name != relation.StartDate && p.Name != relation.EndDate && (p.Name != relation.CreationDate || creationDate));

            string[] content = items.Select(p => $"JsNotation<{p.SystemReturnType!.ToCSharp()}{((p.SystemReturnType!.IsValueType && p.Nullable) ? "?" : "")}> {p.Name} = default").ToArray();
            if (content.Length == 0)
                return string.Empty;
            else if (prepend)
                return string.Concat(", ", string.Join(", ", content));
            else
                return string.Join(", ", content);
        }
    }
}
