using System.ComponentModel.DataAnnotations;

namespace Shortener.Common.DTO
{
    public class UrlDto
    {
        [Required]
        public string MainDestinationUrl { get; set; }
    }
}
