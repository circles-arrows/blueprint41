using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory
{
    public class Stack
    {
        public Stack()
        {
            _stack = [];
        }
        private Stack(Stack original, string context, int depth, int index)
        {
            _stack = original._stack.Prepend(new StackItem
            {
                Context = context,
                Depth = depth,
                Index = index
            }).ToArray();
        }

        public int Length => _stack.Length;
        public StackItem? Peek() => (Length > 0) ? _stack[0] : null;
        public Stack Push(string context, int depth, int index) => new Stack(this, context, depth, index);

        private readonly StackItem[] _stack;

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _stack.Select(item => $"Context: {item.Context}, Depth: {item.Depth}, Index: {item.Index}"));
        }
        public string ToString(int skipFrames)
        {
            return string.Join(Environment.NewLine, _stack.Skip(skipFrames).Select(item => $"Context: {item.Context}, Depth: {item.Depth}, Index: {item.Index}"));
        }
    }
}
