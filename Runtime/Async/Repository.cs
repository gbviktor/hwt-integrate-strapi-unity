using com.gbviktor.hwtintegratestrapiunity.core;
using com.gbviktor.hwtintegratestrapiunity.strapiv4;

namespace com.gbviktor.hwtintegratestrapiunity.async
{
    public class Repository<T> : EnitityRepositoryAsync<Entity<T>, T> where T : IStrapiEntityType
    {
        public Repository(string endpoint) : base(new RestAPIClientAsync(endpoint))
        {

        }
    }
}
