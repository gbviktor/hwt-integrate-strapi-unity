using System;
using System.Net;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace com.gbviktor.hwtintegratestrapiunity.core
{
    public class BaseRestAPITransport
    {
        protected const string Bearer = "Authorization";
        protected const string ApplicationJson = "application/json";

        public bool IsOnline { get; protected set; }

        protected StrapiServerConfig config;

        static readonly HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        };

        protected readonly HttpClient client;
        protected readonly JsonSerializer serializer;
        protected readonly JsonSerializerSettings settings;

        public BaseRestAPITransport(StrapiServerConfig config, JsonSerializer serializer = default, JsonSerializerSettings settings = default)
        {
            this.config = config;
            this.serializer = serializer ?? new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };
            this.settings = settings ?? new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };

            client = new HttpClient(handler);
        }

        protected StringContent ToHttpContent<REQUEST_TYPE>(REQUEST_TYPE data) where REQUEST_TYPE : IStrapiBaseMessage
        {
            return new StringContent(JsonConvert.SerializeObject(data, settings), Encoding.UTF8, ApplicationJson);
        }

        protected void SetupClientHeaders()
        {
            client.Timeout = TimeSpan.FromSeconds(19);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(Bearer, config.Token);
        }
    }
}
