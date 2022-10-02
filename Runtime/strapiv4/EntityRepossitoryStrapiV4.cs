using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;
using com.gbviktor.hwtintegratestrapiunity.strapiv4;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public class EnitityRepositoryStrapiV4<T, E> where T : Entity<E> where E : IStrapiEntityType
    {
        IRestClientSyncStrapiV4 client;

        public EnitityRepositoryStrapiV4(IRestClientSyncStrapiV4 client)
        {
            this.client = client;
        }
        public E Add(E entity)
        {
            var res = client.AddAsync<T, E>(entity);
            res.attributes.id = res.id;
            return res.attributes;
        }
        public E Get(int id)
        {
            var res = client.Get<T, E>(id);
            res.attributes.id = res.id;
            return res.attributes;
        }
        public T GetEntity(int id)
        {
            var res = client.Get<T, E>(id);
            return res;
        }
        public E Update(T entity)
        {
            var res = client.Update<T, E>(entity);
            return res.attributes;
        }
        public bool Delete(int entity)
        {
            return (client.Delete<T, E>(entity)).id == entity;
        }
        public bool Delete(T entity)
        {
            return Delete(entity.id);
        }
        public List<E> GetAll()
        {
            var res = client.GetAll<T, E>();
            return res.ConvertAll(x =>
            {
                x.attributes.id = x.id;
                return x.attributes;
            });
        }
    }
}