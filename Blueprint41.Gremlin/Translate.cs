﻿using org.opencypher.gremlin.translation;
using org.opencypher.gremlin.translation.translator;
using System;

namespace Blueprint41.Gremlin
{
    public enum GremlinFlavor
    {
        Default,
        Cosmos,
        Neptune
    }


    public class Translate
    {

        /// <summary>
        /// Returns the translated cypher query to gremlin cosmos.
        /// </summary>
        /// <param name="cypher">Neo4j cypher to be translated</param>
        /// <returns>string</returns>
        public static string ToGremlin(string cypher, GremlinFlavor flavor = GremlinFlavor.Default)
        {
            try
            {
                cypher = cypher.Replace(Environment.NewLine, " ");
                cypher = cypher.Replace("\r\n", " ").Trim();

                CypherAst ast = CypherAst.parse(cypher);

                TranslatorFlavor tflavor = TranslatorFlavor.gremlinServer();
                Translator.FlavorBuilder builder = Translator.builder().gremlinGroovy();

                switch (flavor)
                {
                    case GremlinFlavor.Cosmos:
                        tflavor = TranslatorFlavor.cosmosDb();
                        break;
                    case GremlinFlavor.Neptune:
                        tflavor = TranslatorFlavor.neptune();
                        builder = (builder as Translator.ParametrizedFlavorBuilder)
                            .inlineParameters()
                            .enableCypherExtensions()
                            .enableMultipleLabels();
                        break;
                    default:
                        builder = (builder as Translator.ParametrizedFlavorBuilder)
                            .inlineParameters()
                            .enableCypherExtensions()
                            .enableMultipleLabels();
                        break;
                }

                Translator cosmosTranslator = builder.build(TranslatorFlavor.cosmosDb());
                return ast.buildTranslation(cosmosTranslator).ToString();
            }
            catch (Exception ex)
            {
                throw new TranslationException($"Unable to translate cypher '{cypher}'", ex);
            }
        }
    }
}
