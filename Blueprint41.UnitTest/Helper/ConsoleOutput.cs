using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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

        //TODO: Implement to support 'lastTransactionOnly' argument
        public string GetOuput(bool lastTransactionOnly = false)
        {
            string output = stringWriter.ToString();
            if (!lastTransactionOnly)
                return output;

            int index = output.LastIndexOf("BeginTransaction");
            if (index == -1)
                return output;

            return output.Substring(index);
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
                if (output.Contains(line))
                    one = true;
            }

            Assert.IsTrue(one ^ not);
        }
        private void AllLines(bool not, params string[] lines)
        {
            if (lines is null || lines.Length == 0)
                throw new ArgumentNullException(nameof(lines));

            string[] output = Regex.Split(GetOuput(true), "\r\n|\r|\n");

            bool found = false;
            int lastSearchLine = (output.Length - lines.Length);
            if (lastSearchLine >= 0)
            {
                for (int searchLine = 0; searchLine <= lastSearchLine; searchLine++)
                {
                    bool all = true;
                    
                    for (int line = 0; line < lines.Length; line++)
                    {
                        if (!output[searchLine + line].Contains(lines[line]))
                        {
                            all = false;
                            break;
                        }
                    }

                    found = all || found;
                }
            }

            Assert.True(found ^ not);
        }
    }
}
