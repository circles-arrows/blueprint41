using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class CompileState
    {
        internal CompileState(IEnumerable<TypeMapping> typeMappings, QueryTranslator translator)
        {
            TypeMappings = typeMappings.ToDictionary(item => item.ReturnType, item => item);
            Translator = translator;
        }
        internal CompileState(IReadOnlyDictionary<Type, TypeMapping> typeMappings, QueryTranslator translator)
        {
            TypeMappings = typeMappings;
            Translator = translator;
        }

        public StringBuilder Text = new StringBuilder();
        public List<Parameter> Parameters = new List<Parameter>();
        public List<Parameter> Values = new List<Parameter>();
        public List<string> Errors = new List<string>();
        public int patternSeq = 0;
        public int paramSeq = 0;

        public IReadOnlyDictionary<Type, TypeMapping> TypeMappings;
        public QueryTranslator Translator { get; private set; }
        internal string Preview(Action<CompileState> compile, CompileState? state = null)
        {
            string compiled;

            if (state == null)
            {
                CompileState tempState = new CompileState(TypeMappings, Translator);
                compile.Invoke(tempState);
                compiled = tempState.Text.ToString();
            }
            else
            {
                StringBuilder old = state.Text;
                state.Text = new StringBuilder();
                compile.Invoke(state);
                compiled = state.Text.ToString();
                state.Text = old;
            }
         
            return compiled;
        }
    }
}
