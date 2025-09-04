using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBrete.Shared.Configuration
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public string[] KeyLines { get; set; } = Array.Empty<string>();
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; } = 30;
    }
}
