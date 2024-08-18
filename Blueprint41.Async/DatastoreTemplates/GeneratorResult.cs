using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Async.DatastoreTemplates
{
    public class GeneratorResult
    {
        public Dictionary<string, string> EntityResult { get; set; }
        public Dictionary<string, string> RelationshipResult { get; set; }
        public Dictionary<string, string> NodeResult { get; set; }

        public GeneratorResult()
        {
            EntityResult = new Dictionary<string, string>();
            RelationshipResult = new Dictionary<string, string>();
            NodeResult = new Dictionary<string, string>();
        }
    }
}
