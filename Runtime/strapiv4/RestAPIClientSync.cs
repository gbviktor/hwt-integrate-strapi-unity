using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity.strapiv4
{
    internal class RestAPIClientSync : BaseRestClient<RestAPITransportSync>, IRestClientSyncStrapiV4
    {
        internal RestAPIClientSync(string endpoint, RestAPITransportSync transport)
        {
            this.endpoint = endpoint;
            this.transport = transport;
        }

        public T AddAsync<T, E>(E entity) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<E>(entity);
            var res = transport.SendWithDifferentResponseType<StrapiBaseMessage<E>, StrapiBaseMessage<T>>(req, endpoint, POST);
            return res.data;
        }

        public T Delete<T, E>(int id) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<T>();
            var res = transport.Send(req, $"{endpoint}/{id}", DELETE);
            return res.data;
        }

        public T Get<T, E>(int id)
        {
            var req = new StrapiBaseMessage<T>();
            var res = transport.Send(req, $"{endpoint}/{id}", GET);
            return res.data;
        }

        public List<T> GetAll<T, E>()
        {
            var req = new StrapiBaseMessage<List<T>>();
            var res = transport.Send(req, endpoint, GET);
            return res.data;
        }

        public T Update<T, E>(T entity) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<E>(entity.attributes);
            var res = transport.SendWithDifferentResponseType<StrapiBaseMessage<E>, StrapiBaseMessage<T>>(req, $"{endpoint}/{entity.id}", PUT);
            return res.data;
        }
    }
}
