﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Query
{
    public class PropertiesAliasResult : AliasResult
    {
        public MiscResult Get(FieldResult result, bool withCoalesce = false, Type? type = null)
        {
            if (withCoalesce)
                return new MiscResult("{0}[COALESCE({1}, '')]", new object[] { this, result }, type ?? typeof(object));
            else
                return new MiscResult("{0}[{1}]", new object[] { this, result }, type ?? typeof(object));
        }
    }
}
