using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBrete.Shared.Service.Interfaces.Auth
{
    public interface ITokenContextHolder
    {
        string? Token { get; set; }
    }
}
