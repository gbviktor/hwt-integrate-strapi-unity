using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public class Repository<T> : EnitityRepositorySync<T> where T : IStrapiEntityType
    {
        public Repository(string endpoint, RestAPITransportSync transport) : base(new RestAPIClientSync(endpoint, transport))
        {
        }
    }
}