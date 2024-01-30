using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using t = Blueprint41.Neo4j.Refactoring.Templates;

namespace Blueprint41.Core
{
    public class RefactorTemplates_v4 : RefactorTemplates
    {
        public override t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.v4.SetDefaultConstantValue();
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
    }
}
