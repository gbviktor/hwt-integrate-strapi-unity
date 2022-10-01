using System.Collections.Generic;

using Cysharp.Threading.Tasks;

namespace com.gbviktor.hwtintegratestrapiunity.async
{

    public interface IRestClientAsync
    {
        UniTask<T> AddAsync<T, E>(E entity) where T : IStrapiEntity<E>;
        UniTask<T> Get<T, E>(int id);
        UniTask<T> Update<T, E>(T entity) where T : IStrapiEntity<E>;
        UniTask<T> Delete<T, E>(int id) where T : IStrapiEntity<E>;
        UniTask<List<T>> GetAll<T, E>();
    }
}

