using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    public class InsertRelationshipEventArgs : EventArgs
    {
        public Node SourceNode { get; private set; }
        public Node TargetNode { get; private set; }
        public PropertyType SourcePropertyType { get; private set; }
        public PropertyType TargetPropertyType { get; private set; }
        public InsertRelationshipEventArgs(Node source, Node target, PropertyType sourcePropertyType, PropertyType targetPropertyType)
        {
            SourceNode = source;
            TargetNode = target;
            SourcePropertyType = sourcePropertyType;
            TargetPropertyType = targetPropertyType;
        }
    }
}
