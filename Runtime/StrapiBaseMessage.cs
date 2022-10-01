using Newtonsoft.Json;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public interface IStrapiBaseMessage { }

    [System.Serializable]
    public class StrapiBaseMessage<T> : IStrapiBaseMessage
    {
        [JsonProperty("data")] public T data;
        [JsonProperty("error")] public string error;

        public StrapiBaseMessage()
        {

        }
        public StrapiBaseMessage(T entity)
        {
            data = entity;
        }
    }
}
