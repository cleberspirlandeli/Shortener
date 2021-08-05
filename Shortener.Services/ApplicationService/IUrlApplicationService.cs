using Shortener.Common.DTO;
using Shortener.Domain.Modules;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public interface IUrlApplicationService
    {
        Task<string> GenerateShorterUrl(UrlDto dto, CancellationToken cancellationToken = default);
        void RegisterUrl(Url url, CancellationToken cancellationToken = default);
        Task<List<Url>> GetUrl();
        Task<string> GetUrlByKey(string keyUrl);
        Task UrlUpdateInfo(UrlUpdateInfoDto dto);
    }
}
