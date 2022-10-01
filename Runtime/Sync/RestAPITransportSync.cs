using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

using UnityEngine;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public class RestAPITransportSync
    {
        const string Bearer = "Authorization";
        const string CT = "Content-Type";
        const string Connection = "Connection";
        const string KeepAlive = "keep-alive";
        const string ApplicationJson = "application/json";

        public bool IsOnline { get; protected set; }

        static RestAPITransportSync()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            client = new HttpClient(handler);
        }
        public RestAPITransportSync(StrapiServerConfig config) : base()
        {
            this.config = config;
        }

        readonly JsonSerializer serializer = new JsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
        };
        readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };

        public StrapiServerConfig config;
        public static readonly HttpClient client;

        //GET   	/api/:pluralApiId	        Get a list of entries
        //POST  	/api/:pluralApiId           Create an entry
        //GET   	/api/:pluralApiId/:id       Get an entry
        //PUT   	/api/:pluralApiId/:id       Update an entry
        //DELETE	/api/:pluralApiId/:id       Delete an entry

        public REQUEST_TYPE SendAsync<REQUEST_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                     where REQUEST_TYPE : IStrapiBaseMessage
        {
            return SendAsyncWithOtherResponce<REQUEST_TYPE, REQUEST_TYPE>(data, endpoint, method);
        }

        public RESPONSE_TYPE SendAsyncWithOtherResponce<REQUEST_TYPE, RESPONSE_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                   where REQUEST_TYPE : IStrapiBaseMessage
            where RESPONSE_TYPE : IStrapiBaseMessage
        {
            try
            {
                client.Timeout = TimeSpan.FromSeconds(19);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(Bearer, config.Token);

                var url = $"{config.URL}/{endpoint}";


                HttpResponseMessage httpResponse;

                switch (method)
                {
                    case "GET":
                        httpResponse = client.GetAsync(url).Result;
                        break;
                    case "POST":
                        httpResponse = client.PostAsync(url, new StringContent(
                        Serialize(data), Encoding.UTF8, ApplicationJson)).Result;
                        break;
                    case "PUT":
                        httpResponse = client.PutAsync(url, new StringContent(
                        Serialize(data), Encoding.UTF8, ApplicationJson)).Result;
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
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IsOnline = true;
                    RESPONSE_TYPE res = serializer.Deserialize<RESPONSE_TYPE>(reader);

                    Debug.Log($"Ser: {res}");
                    return res;
                }

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
        string Serialize<REQUEST_TYPE>(REQUEST_TYPE data) where REQUEST_TYPE : IStrapiBaseMessage
        {
            return JsonConvert.SerializeObject(data, settings);
        }

    }
}
