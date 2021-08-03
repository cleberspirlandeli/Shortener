namespace Shortener.Domain
{
    public class UrlShortenMongoDbSettings : IUrlShortenMongoDbSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IUrlShortenMongoDbSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
