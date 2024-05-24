using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public const string SectionName = "JwtSettings";
    }
}
