using System;
using System.Runtime.CompilerServices;

namespace LeeFuns.Utilities
{
    public class Condition
    {
        public string LeftBrace { get; set; }

        public string Logic { get; set; }

        public ConditionOperate Operation { get; set; }

        public string ParamName { get; set; }

        public object ParamValue { get; set; }

        public string RightBrace { get; set; }
    }
}

