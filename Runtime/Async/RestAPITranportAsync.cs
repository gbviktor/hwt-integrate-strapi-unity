using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json;

using UnityEngine;

namespace com.gbviktor.hwtintegratestrapiunity.async
{
    public class RestAPITransportAsync
    {
        const string Bearer = "Authorization";
        const string CT = "Content-Type";
        const string Connection = "Connection";
        const string KeepAlive = "keep-alive";
        const string ApplicationJson = "application/json";

        public bool IsOnline { get; protected set; }

        static RestAPITransportAsync()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            client = new HttpClient(handler);
        }
        public RestAPITransportAsync(StrapiServerConfig config) : base()
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

        public StrapiServerConfig config { get; protected set; }
        public static readonly HttpClient client;

        int lastRegistredRequestNumber = 0;
        int nextRequestNumber = 1;


        private void RequestIsDone() => nextRequestNumber++;
        private int GetRequestNummber() => ++lastRegistredRequestNumber;
        private bool IsRequestAllowedWithNummber(int requestNumber) => nextRequestNumber == requestNumber;

        //
        //GET	    /api/:pluralApiId	        Get a list of entries
        //POST	    /api/:pluralApiId           Create an entry
        //GET	    /api/:pluralApiId/:id       Get an entry
        //PUT	    /api/:pluralApiId/:id       Update an entry
        //DELETE	/api/:pluralApiId/:id       Delete an entry
        //
        public UniTask<REQUEST_TYPE> SendAsync<REQUEST_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                     where REQUEST_TYPE : IStrapiBaseMessage
        {
            return SendAsyncWithOtherResponce<REQUEST_TYPE, REQUEST_TYPE>(data, endpoint, method);
        }

        string Serialize<REQUEST_TYPE>(REQUEST_TYPE data) where REQUEST_TYPE : IStrapiBaseMessage
        {
            return JsonConvert.SerializeObject(data, settings);
        }

        public async UniTask<RESPONSE_TYPE> SendAsyncWithOtherResponce<REQUEST_TYPE, RESPONSE_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                   where REQUEST_TYPE : IStrapiBaseMessage
            where RESPONSE_TYPE : IStrapiBaseMessage
        {

            var requestNumber = GetRequestNummber();

            var ct = UniTask.WaitUntil(() => { return IsRequestAllowedWithNummber(requestNumber); });
            await ct.TimeoutWithoutException(TimeSpan.FromSeconds(20));

            try
            {
                client.Timeout = TimeSpan.FromSeconds(19);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(Bearer, config.Token);

                var url = $"{config.URL}/{endpoint}";


                HttpResponseMessage httpResponse;

                switch (method)
                {
                    case "GET": //Get
                        httpResponse = await client.GetAsync(url);
                        break;
                    case "POST": //Create
                        httpResponse = await client.PostAsync(url, new StringContent(
                        Serialize(data), Encoding.UTF8, ApplicationJson));
                        break;
                    case "PUT": //Update
                        httpResponse = await client.PutAsync(url, new StringContent(
                        Serialize(data), Encoding.UTF8, ApplicationJson));
                        break;
                    case "DELETE":
                        httpResponse = await client.DeleteAsync(url);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                using Stream s = await httpResponse.Content.ReadAsStreamAsync();

                using StreamReader sr = new StreamReader(s);
                using JsonReader reader = new JsonTextReader(sr);

                var deb = await httpResponse.Content.ReadAsStringAsync();
                Debug.Log($"{url}=Response=>{deb}");
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IsOnline = true;
                    RESPONSE_TYPE res = serializer.Deserialize<RESPONSE_TYPE>(reader);

                    Debug.Log($"Ser: {res}");
                    return res;
                }

                //} finally
                //{
                //    //sr.Close();
                //    //reader.Close();
                ////    Debug.Log($"Request: {requestNumber} finally 1");
                //}

            } catch (Exception er)
            {
                client.CancelPendingRequests();
                IsOnline = false;
                Debug.LogError(er);
            } finally
            {
                //sr.Close();
                //reader.Close();
                //      Debug.Log($"Request: {requestNumber} finally 2");
                RequestIsDone();
            }

            return default;
        }

    }
}