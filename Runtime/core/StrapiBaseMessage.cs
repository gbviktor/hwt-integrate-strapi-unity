using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace com.gbviktor.hwtintegratestrapiunity.core
{
    public interface IStrapiBaseMessage { }

    [System.Serializable]
    public class StrapiBaseMessage<T> : IStrapiBaseMessage
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)] public T data;
        [JsonProperty("error")] public JRaw error;

        public StrapiBaseMessage()
        {

        }
        public StrapiBaseMessage(T entity)
        {
            data = entity;
        }
    }
}
