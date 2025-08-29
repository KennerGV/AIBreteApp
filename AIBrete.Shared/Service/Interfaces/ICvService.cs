using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBrete.Shared.Service.Interfaces
{
    public interface ICvService
    {
        Task<string> UploadCvAsync(IBrowserFile file);
    }
}
