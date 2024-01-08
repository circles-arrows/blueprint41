using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Blueprint41.UnitTest.Helper
{
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput(bool lastTransactionOnly = false)
        {
            string output = stringWriter.ToString();
            if (!lastTransactionOnly)
                return output;

            int index = output.LastIndexOf("BeginTransaction");
            if (index == -1)
                return output;

            return lineEndings.Replace(output.Substring(index), "\n");
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }

        public void AssertNodeCreated(string node, bool not = false)
        {
            AnyLine(not, @$"MERGE (inserted:{node} {{ Uid: key}}) ON CREATE SET inserted += $node ON MATCH SET inserted = $node, inserted.Uid = key RETURN inserted");
        }
        public void AssertRelationshipCreated(string inNode, string relationship, string outNode, bool not = false)
        {
            AllLines(not,@$"MATCH (in:{inNode}) WHERE in.Uid = $inKey",
                         @$"MATCH (out:{outNode}) WHERE out.Uid = $outKey",
                         @$"MERGE (in)-[outr:{relationship}]->(out) ON CREATE SET outr.CreationDate = $CreationDate SET outr += $node");
        }
        public void AssertTimeDependentRelationshipCreated(string inNode, string relationship, string outNode, bool not = false)
        {
            AllLines(not,   $$"""
                            MATCH (in:{{inNode}} { Uid: $inKey })-[rel:{{relationship}}]->(out:{{outNode}} { Uid: $outKey })
                            WHERE COALESCE(rel.StartDate, $min) >= $moment
                            DELETE rel
                            """,
                            $$"""
                            MATCH (in:{{inNode}} { Uid: $inKey })-[rel:{{relationship}}]->(out:{{outNode}} { Uid: $outKey })
                            WHERE COALESCE(rel.StartDate, $min) <= $moment AND COALESCE(rel.EndDate, $max) >= $moment
                            SET rel.EndDate = $max
                            """,
                            $$"""
                            MATCH (in:{{inNode}} { Uid: $inKey }), (out:{{outNode}} { Uid: $outKey })
                            OPTIONAL MATCH (in)-[rel:{{relationship}}]->(out)
                            WHERE COALESCE(rel.StartDate, $min) <= $moment AND COALESCE(rel.EndDate, $max) >= $moment
                            WITH in, out, rel
                            WHERE rel is null
                            CREATE (in)-[outr:{{relationship}}]->(out) SET outr.CreationDate = $create SET outr += $node
                            """);
        }
        public void AssertNodeUpdated(string node, bool not = false)
        {
            AnyLine(not, @$"MATCH (node:{node}) WHERE node.Uid = $key AND node.LastModifiedOn = $lockId SET node = $newValues");
        }
        public void AssertNodeDeleted(string node, bool not = false)
        {
            AnyLine(not, @$"MATCH (node:{node}) WHERE node.Uid = $key AND node.LastModifiedOn = $lockId DELETE node");
        }
        public void AssertRelationshipDeleted(string inNode, string relationship, string outNode, bool not = false)
        {
            AnyLine(not, @$"MATCH (in:{inNode} {{ Uid: $inKey }})-[r:{relationship}]->(out:{outNode} {{ Uid: $outKey }}) DELETE r",
                         @$"MATCH (in:{inNode} {{ Uid: $inKey }})<-[r:{relationship}]-(out:{outNode} {{ Uid: $outKey }}) DELETE r",
                         @$"MATCH (in:{inNode} {{ Uid: $inKey }})-[r:{relationship}]->(out:{outNode}) DELETE r",
                         @$"MATCH (in:{inNode} {{ Uid: $inKey }})<-[r:{relationship}]-(out:{outNode}) DELETE r",
                         @$"MATCH (in:{inNode})-[r:{relationship}]->(out:{outNode} {{ Uid: $outKey }}) DELETE r",
                         @$"MATCH (in:{inNode})<-[r:{relationship}]-(out:{outNode} {{ Uid: $outKey }}) DELETE r");
        }
        public void AssertTimeDependentRelationshipDeleted(string inNode, string relationship, string outNode, bool not = false)
        {
            AnyLine(not,    $$"""
                            MATCH (in:{{inNode}} { Uid: $inKey })-[rel:{{relationship}}]->(out:{{outNode}} { Uid: $outKey })
                            WHERE COALESCE(rel.StartDate, $min) >= $moment
                            DELETE rel

                            MATCH (in:{{inNode}} { Uid: $inKey })-[rel:{{relationship}}]->(out:{{outNode}} { Uid: $outKey })
                            WHERE COALESCE(rel.StartDate, $min) <= $moment AND COALESCE(rel.EndDate, $max) >= $moment
                            SET rel.EndDate = $moment
                            """,
                            @$"MATCH (in:{inNode} {{ Uid: $inKey }})-[r:{relationship}]->(out:{outNode} {{ Uid: $outKey }}) DELETE r",
                            @$"MATCH (in:{inNode} {{ Uid: $inKey }})<-[r:{relationship}]-(out:{outNode} {{ Uid: $outKey }}) DELETE r",
                            @$"MATCH (in:{inNode} {{ Uid: $inKey }})-[r:{relationship}]->(out:{outNode}) DELETE r",
                            @$"MATCH (in:{inNode} {{ Uid: $inKey }})<-[r:{relationship}]-(out:{outNode}) DELETE r",
                            @$"MATCH (in:{inNode})-[r:{relationship}]->(out:{outNode} {{ Uid: $outKey }}) DELETE r",
                            @$"MATCH (in:{inNode})<-[r:{relationship}]-(out:{outNode} {{ Uid: $outKey }}) DELETE r"
                         );
        }

        public void AssertNodeLoaded(string node, bool not = false)
        {
            AnyLine(not, @$"MATCH (node:{node}) WHERE node.Uid = $key RETURN node");
        }
        public void AssertRelationshipLoaded(string fromNode, string relationship, string loadedNode, bool not = false)
        {
            AnyLine(not, @$"MATCH (node:{fromNode})-[rel:{relationship}]->(out:{loadedNode}) WHERE node.Uid = $key RETURN out, rel",
                         @$"MATCH (node:{fromNode})<-[rel:{relationship}]-(out:{loadedNode}) WHERE node.Uid = $key RETURN out, rel");
        }

        public void AssertQuery(string cypher, bool not = false)
        {
            AnyLine(not, cypher);
        }

        private void AnyLine(bool not, params string[] lines)
        {
            if (lines is null || lines.Length == 0)
                throw new ArgumentNullException(nameof(lines));

            string output = GetOuput(true);

            bool one = false;
            foreach (string line in lines)
            {
                if (output.Contains(lineEndings.Replace(line, "\n")))
                    one = true;
            }

            Assert.IsTrue(one ^ not);
        }
        private void AllLines(bool not, params string[] lines)
        {
            if (lines is null || lines.Length == 0)
                throw new ArgumentNullException(nameof(lines));

            string[] output = Regex.Split(GetOuput(true), "\r\n|\r|\n").Select(item => item.Trim()).Where(item => !string.IsNullOrEmpty(item)).ToArray();
            string[][] searches = lines.Select(line => Regex.Split(line, "\r\n|\r|\n").Select(item => item.Trim()).Where(item => !string.IsNullOrEmpty(item)).ToArray()).ToArray();

            bool found = true;
            foreach (string[] search in searches)
            {
                bool all = false;

                int lastSearchLine = (output.Length - search.Length);
                if (lastSearchLine >= 0)
                {
                    for (int searchLine = 0; searchLine <= lastSearchLine; searchLine++)
                    {
                        all = true;

                        for (int line = 0; line < search.Length; line++)
                        {
                            if (!output[searchLine + line].Contains(search[line]))
                            {
                                all = false;
                                break;
                            }
                        }

                        if (all)
                            break;
                    }
                }

                if (!all)
                    found = false;
            }

            Assert.True(found ^ not);
        }

        private static Regex lineEndings = new Regex(@"\r\n?|\n", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
    }
}
