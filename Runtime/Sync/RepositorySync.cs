namespace com.gbviktor.hwtintegratestrapiunity
{
    public class RepositorySync<T> : EnitityRepositorySync<Entity<T>, T> where T : IStrapiEntityType
    {
        public RepositorySync(string endpoint, RestAPITransportSync transport) : base(new RestAPIClientSync(endpoint, transport))
        {
        }
    }
}
