using System;
using System.Runtime.CompilerServices;

namespace LeeFuns.Utilities
{
    public class GridPage
    {
        public int curPage { get; set; }

        public string orderField { get; set; }

        public string orderType { get; set; }

        public int pageRows { get; set; }

        public int totalRecords { get; set; }

        public int TotaPage
        {
            get
            {
                if (this.totalRecords > 0)
                {
                    return (((this.totalRecords % this.pageRows) == 0) ? (this.totalRecords / this.pageRows) : ((this.totalRecords / this.pageRows) + 1));
                }
                return 1;
            }
        }
    }
}

