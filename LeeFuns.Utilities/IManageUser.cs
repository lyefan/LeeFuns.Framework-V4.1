using System;
using System.Runtime.CompilerServices;

namespace LeeFuns.Utilities
{
    public class IManageUser
    {
        public string Account { get; set; }

        public string Code { get; set; }

        public string CompanyId { get; set; }

        public string DepartmentId { get; set; }

        public string Gender { get; set; }

        public string IPAddress { get; set; }

        public string IPAddressName { get; set; }

        public bool IsSystem { get; set; }

        public DateTime LogTime { get; set; }

        public string ObjectId { get; set; }

        public string Password { get; set; }

        public string Secretkey { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}

