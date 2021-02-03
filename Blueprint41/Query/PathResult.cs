using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class PathResult : Result, IPathResult
    {
        private object[] emptyArguments = new object[0];

        [Obsolete("Not supported", true)]
        protected internal PathResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : this((AliasResult)null!, function, arguments, type) => throw new NotSupportedException();
        [Obsolete("Not supported", true)]
        protected internal PathResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : this(parent?.Alias!, function, arguments, type) => throw new NotSupportedException();
        protected internal PathResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null)
        {
            Alias = alias ?? throw new ArgumentNullException(nameof(alias));
            Node = alias.Node ?? throw new ArgumentNullException($"{nameof(alias)}.{nameof(alias.Node)}"); ;
            OverridenReturnType = type;
        }

        public AliasResult Alias { get; private set; }
        private Type? OverridenReturnType { get; set; }
        public Node Node { get; protected internal set; }

        public NumericResult Lenght()
        {
            return new NumericResult(Alias, t => t.FnSize, null, typeof(long));
        }

        public AsResult As(string aliasName)
        {
            return new AsResult(this, aliasName);
        }
        public AsResult As(string aliasName, out AliasResult alias)
        {
            alias = new AliasResult()
            {
                AliasName = aliasName,
            };
            return new AsResult(this, aliasName);
        }
        AsResult IResult.As<T>(string aliasName, out T alias)
        {
            AsResult retval = As(aliasName, out AliasResult genericAlias);
            alias = (T)(object)genericAlias;
            return retval;
        }
        public override string? GetFieldName() => Alias?.AliasName;
        public override Type? GetResultType() => OverridenReturnType;

        protected internal override void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }
    }
    public class PathNode : Node
    {
        internal PathNode(Node node, AliasResult alias)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            NodeAlias = alias ?? throw new ArgumentNullException(nameof(alias));
            IsReference = true;
        }
        public Node Node { get; protected internal set; }

        protected override Entity GetEntity() => null!;
        protected override string GetNeo4jLabel() => null!;

        internal override void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }
    }
}
