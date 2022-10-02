using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public class EnitityRepositorySync<T> where T : IStrapiEntityType
    {
        IRestClientSync client;

        public EnitityRepositorySync(IRestClientSync client)
        {
            this.client = client;
        }
        public T Add(T entity)
        {
            return client.AddAsync<T>(entity);
        }
        public T Get(int id)
        {
            return client.Get<T>(id);
        }
        public T GetEntity(int id)
        {
            var res = client.Get<T>(id);
            return res;
        }
        public T Update(T entity)
        {
            return client.Update<T>(entity);
        }
        public bool Delete(int entity)
        {
            return (client.Delete<T>(entity)).id == entity;
        }
        public bool Delete(T entity)
        {
            return Delete(entity.id);
        }
        public List<T> GetAll()
        {
            return client.GetAll<T>();
        }
    }
}