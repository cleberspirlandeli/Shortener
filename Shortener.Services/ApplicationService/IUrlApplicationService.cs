using Shortener.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public interface IUrlApplicationService
    {
        Task<string> GenerateShorterUrl(UrlDto dto);
    }
}
