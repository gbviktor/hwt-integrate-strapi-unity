using System.Collections.Generic;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public class RestAPIClientSync : IRestClientSync
    {
        RestAPITransportSync transport;
        string endpoint;

        const string GET = "GET";
        const string POST = "POST";
        const string PUT = "PUT";
        const string DELETE = "DELETE";

        public RestAPIClientSync(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public RestAPIClientSync(string endpoint, RestAPITransportSync transport) : this(endpoint)
        {
            this.transport = transport;
        }

        public T AddAsync<T, E>(E entity) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<E>(entity);
            var res = transport.SendAsyncWithOtherResponce<StrapiBaseMessage<E>, StrapiBaseMessage<T>>(req, endpoint, POST);
            return res.data;
        }

        public T Delete<T, E>(int id) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<T>();
            var res = transport.SendAsync(req, $"{endpoint}/{id}", DELETE);
            return res.data;
        }

        public T Get<T, E>(int id)
        {
            var req = new StrapiBaseMessage<T>();
            var res = transport.SendAsync(req, $"{endpoint}/{id}", GET);
            return res.data;
        }

        public List<T> GetAll<T, E>()
        {
            var req = new StrapiBaseMessage<List<T>>();
            var res = transport.SendAsync(req, endpoint, GET);
            return res.data;
        }

        public T Update<T, E>(T entity) where T : IStrapiEntity<E>
        {
            var req = new StrapiBaseMessage<E>(entity.attributes);
            var res = transport.SendAsyncWithOtherResponce<StrapiBaseMessage<E>, StrapiBaseMessage<T>>(req, $"{endpoint}/{entity.id}", PUT);
            return res.data;
        }
    }

}
