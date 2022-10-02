using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;
using com.gbviktor.hwtintegratestrapiunity.strapiv4;

using Cysharp.Threading.Tasks;

namespace com.gbviktor.hwtintegratestrapiunity.async
{
    public class EnitityRepositoryAsyncStrapiV4<T, E> where T : Entity<E> where E : IStrapiEntityType
    {
        IRestClientAsync client;

        public EnitityRepositoryAsyncStrapiV4(IRestClientAsync client)
        {
            this.client = client;
        }
        public async UniTask<E> Add(E entity)
        {
            var res = await client.AddAsync<T, E>(entity);
            res.attributes.id = res.id;
            return res.attributes;
        }
        public async UniTask<E> Get(int id)
        {
            var res = await client.Get<T, E>(id);
            res.attributes.id = res.id;
            return res.attributes;
        }
        public async UniTask<T> GetEntity(int id)
        {
            var res = await client.Get<T, E>(id);
            return res;
        }
        public async UniTask<E> Update(T entity)
        {
            var res = await client.Update<T, E>(entity);
            return res.attributes;
        }
        public async UniTask<bool> Delete(int entityId)
        {
            return (await client.Delete<T, E>(entityId)).id == entityId;
        }
        public UniTask<bool> Delete(T entity)
        {
            return Delete(entity.id);
        }
        public async UniTask<List<E>> GetAll()
        {
            var res = await client.GetAll<T, E>();
            return res.ConvertAll(x =>
            {
                x.attributes.id = x.id;
                return x.attributes;
            });
        }
        public async UniTask<List<T>> GetAllEntities()
        {
            return await client.GetAll<T, E>();
        }
    }
    public class EnitityRepositoryAsync<T, E> where T : Entity<E> where E : IStrapiEntityType
    {
        IRestClientAsync client;

        public EnitityRepositoryAsync(IRestClientAsync client)
        {
            this.client = client;
        }
        public async UniTask<E> Add(E entity)
        {
            var res = await client.AddAsync<T, E>(entity);
            res.attributes.id = res.id;
            return res.attributes;
        }
        public async UniTask<E> Get(int id)
        {
            var res = await client.Get<T, E>(id);
            res.attributes.id = res.id;
            return res.attributes;
        }
        public async UniTask<T> GetEntity(int id)
        {
            var res = await client.Get<T, E>(id);
            return res;
        }
        public async UniTask<E> Update(T entity)
        {
            var res = await client.Update<T, E>(entity);
            return res.attributes;
        }
        public async UniTask<bool> Delete(int entityId)
        {
            return (await client.Delete<T, E>(entityId)).id == entityId;
        }
        public UniTask<bool> Delete(T entity)
        {
            return Delete(entity.id);
        }
        public async UniTask<List<E>> GetAll()
        {
            var res = await client.GetAll<T, E>();
            return res.ConvertAll(x =>
            {
                x.attributes.id = x.id;
                return x.attributes;
            });
        }
        public async UniTask<List<T>> GetAllEntities()
        {
            return await client.GetAll<T, E>();
        }
    }
}