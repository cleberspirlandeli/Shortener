﻿using Shortener.Common.DTO;
using Shortener.Domain.Modules;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public interface IUrlApplicationService
    {
        Task<string> GenerateShorterUrl(UrlDto dto, CancellationToken cancellationToken = default);
        Task<string> RegisterUrl(Url url, CancellationToken cancellationToken = default);
    }
}
