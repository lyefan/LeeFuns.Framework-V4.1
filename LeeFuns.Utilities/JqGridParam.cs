using System;
using System.Runtime.CompilerServices;

namespace LeeFuns.Utilities
{
    public class JqGridParam
    {
        public int Page { get; set; }

        public int Records { get; set; }

        public int Rows { get; set; }

        public string Sidx { get; set; }

        public string Sord { get; set; }

        public int Total
        {
            get
            {
                if (this.Records > 0)
                {
                    return (((this.Records % this.Rows) == 0) ? (this.Records / this.Rows) : ((this.Records / this.Rows) + 1));
                }
                return 1;
            }
        }
    }
}

