using System;

using Newtonsoft.Json;

using UnityEngine;

namespace com.gbviktor.hwtintegratestrapiunity
{
    [Serializable]
    public class Entity<T> : IStrapiEntity<T> where T : IStrapiEntityType
    {
        [JsonProperty("id")]
        [SerializeField] int _id;

        [JsonProperty("attributes")]
        [SerializeField] T attribute;

        [JsonIgnore]
        public int id { get => _id; set { _id = value; } }
        [JsonIgnore]
        public T attributes { get => attribute; set { attribute = value; } }

        public Entity(T obj)
        {
            attributes = obj;
        }
    }

    public interface IStrapiEntityType
    {
        int id { get; set; }
    }
    public interface IStrapiEntity<T>
    {
        int id { get; set; }
        T attributes { get; set; }
    }
}