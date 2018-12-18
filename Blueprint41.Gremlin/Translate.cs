using org.opencypher.gremlin.translation;
using org.opencypher.gremlin.translation.translator;
using System;

namespace Blueprint41.Gremlin
{
    public class Translate
    {

        /// <summary>
        /// Returns the translated cypher query to gremlin cosmos.
        /// </summary>
        /// <param name="cypher">Neo4j cypher to be translated</param>
        /// <returns>string</returns>
        public static string ToCosmos(string cypher)
        {
            try
            {
                cypher = cypher.Replace(System.Environment.NewLine, " ");
                cypher = cypher.Replace("\r\n", " ").Trim();

                CypherAst ast = CypherAst.parse(cypher);
                Translator cosmosTranslator = Translator.builder().gremlinGroovy().build(TranslatorFlavor.cosmosDb());

                return ast.buildTranslation(cosmosTranslator).ToString();
            }
            catch (Exception ex)
            {
                throw new TranslationException($"Unable to translate cypher '{cypher}'");
            }
        }
    }
}
