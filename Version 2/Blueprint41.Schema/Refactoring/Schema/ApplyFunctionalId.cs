using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blueprint41.Refactoring.Schema
{
    public class ApplyFunctionalId
    {
        internal ApplyFunctionalId(SchemaInfo parent, string label, string prefix, long startFrom, ApplyFunctionalIdAction action)
        {
            Parent = parent;
            Label = label;
            Prefix = prefix;
            StartFrom = startFrom < 0 ? 0 : startFrom;
            Action = action;
        }

        private SchemaInfo Parent { get; set; }
        public string Label { get; protected set; }
        public string Prefix { get; protected set; }
        public long StartFrom { get; protected set; }
        public ApplyFunctionalIdAction Action { get; protected set; }


        internal virtual List<string> ToCypher()
        {
            List<string> queries = new List<string>();
            switch (Action)
            {
                case ApplyFunctionalIdAction.CreateFunctionalId:
                    queries.Add($"CALL blueprint41.functionalid.create('{Label}', '{Prefix}', {StartFrom});");
                    break;
                case ApplyFunctionalIdAction.UpdateFunctionalId:
                    queries.Add($"CALL blueprint41.functionalid.dropdefinition('{Label}');");
                    queries.Add($"CALL blueprint41.functionalid.create('{Label}', '{Prefix}', {StartFrom});");
                    break;
                case ApplyFunctionalIdAction.DeleteFunctionalId:
                    queries.Add($"CALL blueprint41.functionalid.dropdefinition('{Label}');");
                    break;
                default:
                    throw new NotImplementedException($"The FunctionalIdAction {Action.ToString()} is not implemented yet.");
            }
            return queries;
        }
        public override string ToString()
        {
            return $"Differences for {Label} (\"{Prefix}\" : {StartFrom}) -> {Action.ToString()}";
        }
    }
}
