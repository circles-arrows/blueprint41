﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class AliasListResult : ListResult<AliasListResult, AliasResult>
    {
        public AliasListResult(AliasResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type)
        {
        }
    }
    public class StringListResult : ListResult<StringListResult, StringResult, string>
    {
        public StringListResult(FieldResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        public StringListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
    }

    public class BooleanListResult : ListResult<BooleanListResult, BooleanResult, bool>
    {
        public BooleanListResult(FieldResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        public BooleanListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
    }

    public class DateTimeListResult : ListResult<DateTimeListResult, DateTimeResult, DateTime>
    {
        public DateTimeListResult(FieldResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        public DateTimeListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
    }

    public class FloatListResult : ListResult<FloatListResult, FloatResult, double>
    {
        public FloatListResult(FieldResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        public FloatListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
    }

    public class NumericListResult : ListResult<NumericListResult, NumericResult, long>
    {
        public NumericListResult(FieldResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        public NumericListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
    }
}