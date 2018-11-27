using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Newtonsoft.Json;
using org.opencypher.gremlin.translation;
using org.opencypher.gremlin.translation.translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CypherAst ast = CypherAst.parse(cypher);
                Translator cosmosTranslator = Translator.builder().gremlinGroovy().build(TranslatorFlavor.cosmosDb());

                return ast.buildTranslation(cosmosTranslator).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
