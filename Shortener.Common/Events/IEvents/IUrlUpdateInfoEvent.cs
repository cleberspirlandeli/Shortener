using Shortener.Common.DTO;
using System;

namespace Shortener.Common.Events.IEvents
{
    public interface IUrlUpdateInfoEvent
    {
        Guid IdMessage { get; set; }
        DateTime CreatedAt { get; set; }
        UrlUpdateInfoDto Data { get; set; }
    }
}
