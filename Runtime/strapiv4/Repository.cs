using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity.strapiv4
{
    public class Repository<T> : EnitityRepositoryStrapiV4<Entity<T>, T> where T : IStrapiEntityType
    {
        internal Repository(string endpoint, RestAPITransportSync transport) : base(new RestAPIClientSync(endpoint, transport))
        {
        }
    }
}