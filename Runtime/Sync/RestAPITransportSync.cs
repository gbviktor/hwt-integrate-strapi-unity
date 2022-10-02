using System;
using System.IO;
using System.Net.Http;

using com.gbviktor.hwtintegratestrapiunity.core;

using Newtonsoft.Json;

using UnityEngine;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public sealed class RestAPITransportSync : BaseRestAPITransport
    {
        public RestAPITransportSync(StrapiServerConfig config) : base(config) { }

        //GET   	/api/:pluralApiId	        Get a list of entries
        //POST  	/api/:pluralApiId           Create an entry
        //GET   	/api/:pluralApiId/:id       Get an entry
        //PUT   	/api/:pluralApiId/:id       Update an entry
        //DELETE	/api/:pluralApiId/:id       Delete an entry

        public REQUEST_TYPE Send<REQUEST_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                     where REQUEST_TYPE : IStrapiBaseMessage
        {
            return SendWithDifferentResponseType<REQUEST_TYPE, REQUEST_TYPE>(data, endpoint, method);
        }

        public RESPONSE_TYPE SendWithDifferentResponseType<REQUEST_TYPE, RESPONSE_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                   where REQUEST_TYPE : IStrapiBaseMessage
            where RESPONSE_TYPE : IStrapiBaseMessage
        {
            try
            {
                SetupClientHeaders();

                var url = $"{config.URL}/{endpoint}";

                HttpResponseMessage httpResponse;

                switch (method)
                {
                    case "GET":
                        httpResponse = client.GetAsync(url).Result;
                        break;
                    case "POST":
                        httpResponse = client.PostAsync(url, ToHttpContent(data)).Result;
                        break;
                    case "PUT":
                        httpResponse = client.PutAsync(url, ToHttpContent(data)).Result;
                        break;
                    case "DELETE":
                        httpResponse = client.DeleteAsync(url).Result;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                using Stream s = httpResponse.Content.ReadAsStreamAsync().Result;

                using StreamReader sr = new StreamReader(s);
                using JsonReader reader = new JsonTextReader(sr);

                var deb = httpResponse.Content.ReadAsStringAsync().Result;
                Debug.Log($"{url}=Response=>{deb}");

                IsOnline = true;

                return serializer.Deserialize<RESPONSE_TYPE>(reader);

            } catch (Exception er)
            {
                client.CancelPendingRequests();
                IsOnline = false;
                Debug.LogError(er);
            } finally
            {

            }

            return default;
        }

    }
}
