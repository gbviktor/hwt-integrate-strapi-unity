using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;
using com.gbviktor.hwtintegratestrapiunity.strapiv4;

using Cysharp.Threading.Tasks;

namespace com.gbviktor.hwtintegratestrapiunity.async
{

    public class RestAPIClientAsync : BaseRestClient<RestAPITransportAsync>, IRestClientAsync
    {
        public RestAPIClientAsync(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public async UniTask<T> AddAsync<T, E>(E entity) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<E>(entity);
            var res = await transport.SendAsyncWithOtherResponse<StrapiBaseMessage<E>, StrapiBaseMessage<T>>(req, endpoint, POST);
            return res.data;
        }

        public async UniTask<T> Delete<T, E>(int id) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<T>();
            var res = await transport.SendAsync(req, $"{endpoint}/{id}", DELETE);
            return res.data;
        }

        public async UniTask<T> Get<T, E>(int id)
        {
            var req = new StrapiBaseMessage<T>();
            var res = await transport.SendAsync(req, $"{endpoint}/{id}", GET);
            return res.data;
        }

        public async UniTask<List<T>> GetAll<T, E>()
        {
            var req = new StrapiBaseMessage<List<T>>();
            var res = await transport.SendAsync(req, endpoint, GET);
            return res.data;
        }

        public async UniTask<T> Update<T, E>(T entity) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<E>(entity.attributes);
            var res = await transport.SendAsyncWithOtherResponse<StrapiBaseMessage<E>, StrapiBaseMessage<T>>(req, $"{endpoint}/{entity.id}", PUT);
            return res.data;
        }
    }
}