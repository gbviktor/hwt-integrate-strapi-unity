using System;
using System.IO;
using System.Net.Http;

using com.gbviktor.hwtintegratestrapiunity.core;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json;

using UnityEngine;

namespace com.gbviktor.hwtintegratestrapiunity.async
{
    public class RestAPITransportAsync : BaseRestAPITransport
    {
        public RestAPITransportAsync(StrapiServerConfig config) : base(config) { }

        int lastRegistredRequestNumber = 0;
        int nextRequestNumber = 1;


        private void RequestIsDone() => nextRequestNumber++;
        private int GetRequestNummber() => ++lastRegistredRequestNumber;
        private bool IsRequestAllowedWithNummber(int requestNumber) => nextRequestNumber == requestNumber;

        //GET	    /api/:pluralApiId	        Get a list of entries
        //POST	    /api/:pluralApiId           Create an entry
        //GET	    /api/:pluralApiId/:id       Get an entry
        //PUT	    /api/:pluralApiId/:id       Update an entry
        //DELETE	/api/:pluralApiId/:id       Delete an entry

        public UniTask<REQUEST_TYPE> SendAsync<REQUEST_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                     where REQUEST_TYPE : IStrapiBaseMessage
        {
            return SendAsyncWithOtherResponse<REQUEST_TYPE, REQUEST_TYPE>(data, endpoint, method);
        }

        public async UniTask<RESPONSE_TYPE> SendAsyncWithOtherResponse<REQUEST_TYPE, RESPONSE_TYPE>(REQUEST_TYPE data, string endpoint, string method = default)
                   where REQUEST_TYPE : IStrapiBaseMessage
            where RESPONSE_TYPE : IStrapiBaseMessage
        {

            var requestNumber = GetRequestNummber();

            var ct = UniTask.WaitUntil(() => { return IsRequestAllowedWithNummber(requestNumber); });
            await ct.TimeoutWithoutException(TimeSpan.FromSeconds(20));

            try
            {
                SetupClientHeaders();

                var url = $"{config.URL}/{endpoint}";

                HttpResponseMessage httpResponse;

                switch (method)
                {
                    case "GET":
                        httpResponse = await client.GetAsync(url);
                        break;
                    case "POST":
                        httpResponse = await client.PostAsync(url, ToHttpContent(data));
                        break;
                    case "PUT":
                        httpResponse = await client.PutAsync(url, ToHttpContent(data));
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

                //if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                IsOnline = true;
                RESPONSE_TYPE res = serializer.Deserialize<RESPONSE_TYPE>(reader);

                Debug.Log($"Ser: {res}");
                return res;
                //}

            } catch (Exception er)
            {
                client.CancelPendingRequests();
                IsOnline = false;
                Debug.LogError(er);
            } finally
            {
                RequestIsDone();
            }
            return default;
        }

    }
}