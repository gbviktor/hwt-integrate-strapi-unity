using System.Collections.Generic;

using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public interface IRestClientSync
    {
        T AddAsync<T>(T entity) where T : IStrapiEntityType;
        T Get<T>(int id);
        T Update<T>(T entity) where T : IStrapiEntityType;
        T Delete<T>(int id) where T : IStrapiEntityType;
        List<T> GetAll<T>();
    }
}