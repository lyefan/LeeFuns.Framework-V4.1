using System;

namespace LeeFuns.Utilities
{
    public class ToState
    {
        public static string IsEnabled(int? Enabled)
        {
            if (Enabled == 1)
            {
                return "<img src='/Content/Images/checkokmark.gif'/>";
            }
            return "<img src='/Content/Images/checknomark.gif'/>";
        }
    }
}

