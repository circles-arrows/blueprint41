using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Globalization;

namespace Blueprint41.DatastoreTemplates
{
    public static class Generator
    {
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
                Domain_Data_Relationship relationship_template = new()
                {
                    Settings = settings,
                    DALRelation = relation,
                    Datastore = model
                };
                string content = relationship_template.TransformText();
                generatorResult.RelationshipResult.Add(relation.Name, content);
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
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);
            foreach (KeyValuePair<string, string> item in dictionary)
            {
                string entityPath = Path.Combine(path, item.Key + ".cs");
                using FileStream fs = File.Create(entityPath);
                byte[] info = new UTF8Encoding(true).GetBytes(item.Value);
                fs.Write(info, 0, info.Length);
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
            if (MatchAndReplace(ref Singular, "people", "", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "craft", "", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "tooth", "eeth", 4)) return Singular;
            if (MatchAndReplace(ref Singular, "goose", "eese", 4)) return Singular;
            if (MatchAndReplace(ref Singular, "trix", "ces", 1)) return Singular;
            if (MatchAndReplace(ref Singular, "mouse", "ice", 4)) return Singular;
            if (MatchAndReplace(ref Singular, "louse", "ice", 4)) return Singular;
            if (MatchAndReplace(ref Singular, "foot", "eet", 3)) return Singular;
            if (MatchAndReplace(ref Singular, "zoon", "a", 2)) return Singular;
            if (MatchAndReplace(ref Singular, "info", "s", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "eau", "x", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "ieu", "x", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "man", "en", 2)) return Singular;
            if (MatchAndReplace(ref Singular, "cis", "es", 2)) return Singular;
            if (MatchAndReplace(ref Singular, "sis", "es", 2)) return Singular;
            if (MatchAndReplace(ref Singular, "xis", "es", 2)) return Singular;
            if (MatchAndReplace(ref Singular, "ies", "", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "ch", "es", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "fe", "ves", 2)) return Singular;
            if (MatchAndReplace(ref Singular, "sh", "es", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "o", "es", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "f", "ves", 1)) return Singular;
            if (MatchAndReplace(ref Singular, "s", "es", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "x", "es", 0)) return Singular;
            if (MatchAndReplace(ref Singular, "y", "ies", 1)) return Singular;
            MatchAndReplace(ref Singular, "", "s", 0);
            return Singular;
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
    }
}
