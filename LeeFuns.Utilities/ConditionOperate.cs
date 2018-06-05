using System;

namespace LeeFuns.Utilities
{
    public enum ConditionOperate : byte
    {
        AfterDay = 0x16,
        BeforeDay = 0x15,
        Equal = 0,
        Greater = 2,
        GreaterThan = 3,
        LastMonth = 0x12,
        LastWeek = 15,
        LeftLike = 10,
        Less = 4,
        LessThan = 5,
        Like = 8,
        NextMonth = 20,
        NextWeek = 0x11,
        NotEqual = 1,
        NotLike = 9,
        NotNull = 7,
        Null = 6,
        RightLike = 11,
        ThisMonth = 0x13,
        ThisWeek = 0x10,
        Today = 13,
        Tomorrow = 14,
        Yesterday = 12
    }
}

