﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Blueprint41.Query
{
    public class Assignment
    {
        public Assignment(FieldResult field, IValue value)
        {
            Field = field;
            Value = value;
        }

        public FieldResult Field { get; private set; }
        public IValue Value { get; private set; }

        internal virtual void Compile(CompileState state)
        {
            Compile(state, false);
        }
        internal void Compile(CompileState state, bool add)
        {
            state.Translator.Compile(this, state, add);
        }
    }

    public interface IValue
    {
        object GetValue();
    }
    public struct JsNotation<T> : IValue
    {
        public Parameter Parameter;
        public T Value;
        public bool HasValue;

        object IValue.GetValue()
        {
            if (!HasValue)
                throw new NotSupportedException("No value set.");

            if (Parameter is not null)
                return Parameter;
            else
                return Parameter.Constant(Value);
        }

        public static implicit operator JsNotation<T>(T value)
        {
            return new JsNotation<T>() { Value = value, HasValue = true };
        }
        public static implicit operator JsNotation<T>(Parameter parameter)
        {
            return new JsNotation<T>() { Parameter = parameter, HasValue = true };
        }
        public static implicit operator JsNotation<T>(FieldResult field)
        {
            throw new NotImplementedException();
            return new JsNotation<T>() { Parameter = null!, HasValue = true };
        }
        public static implicit operator JsNotation<T>(FunctionalId parameter)
        {
            throw new NotImplementedException();
            return new JsNotation<T>() { Parameter = null!, HasValue = true };
        }
    }
}
