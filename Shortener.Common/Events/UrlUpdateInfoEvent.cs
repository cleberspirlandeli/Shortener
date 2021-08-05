using Shortener.Common.DTO;
using Shortener.Common.Events.IEvents;
using System;

namespace Shortener.Common.Events
{
    public class UrlUpdateInfoEvent : IUrlUpdateInfoEvent
    {
        public Guid IdMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public UrlUpdateInfoDto Data { get; set; }
    }
}
