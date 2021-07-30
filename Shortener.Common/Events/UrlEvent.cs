using Shortener.Common.DTO;
using Shortener.Common.Events.IEvents;
using Shortener.Domain.Modules;
using System;

namespace Shortener.Common.Events
{
    public class UrlEvent : IUrlEvent
    {
        public Guid IdMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public Url Url { get; set; }
    }
}
