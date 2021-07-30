using Shortener.Common.DTO;
using Shortener.Domain.Modules;
using System;

namespace Shortener.Common.Events.IEvents
{
    public interface IUrlEvent
    {
        Guid IdMessage { get; set; }
        DateTime CreatedAt { get; set; }
        Url Url { get; set; }
    }
}
