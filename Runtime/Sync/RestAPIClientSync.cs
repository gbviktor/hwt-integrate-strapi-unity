using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity
{
    internal class RestAPIClientSync : BaseRestClient<RestAPITransportSync>, IRestClientSync
    {
        internal RestAPIClientSync(string endpoint, RestAPITransportSync transport)
        {
            this.endpoint = endpoint;
            this.transport = transport;
        }

        public E AddAsync<E>(E entity) where E : IStrapiEntityType => Request(entity, endpoint, POST);

        public E Delete<E>(int id) where E : IStrapiEntityType => transport.Send<StrapiBaseMessage<E>>(null, $"{endpoint}/{id}", DELETE).data;

        public E Get<E>(int id) => transport.Send<StrapiBaseMessage<E>>(null, $"{endpoint}/{id}", GET).data;

        public List<E> GetAll<E>() => transport.Send<StrapiBaseMessage<List<E>>>(null, endpoint, GET).data;

        public E Update<E>(E entity) where E : IStrapiEntityType => Request(entity, $"{endpoint}/{entity.id}", PUT);

        protected E Request<E>(E entity, string endpoint, string requestType) where E : IStrapiEntityType
        {
            var req = new StrapiBaseMessage<E>(entity);
            return transport.Send(req, endpoint, requestType).data;
        }
    }
}
