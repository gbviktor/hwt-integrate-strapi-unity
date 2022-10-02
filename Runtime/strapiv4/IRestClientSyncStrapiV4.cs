﻿using System.Collections.Generic;

namespace com.gbviktor.hwtintegratestrapiunity.strapiv4
{
    public interface IRestClientSyncStrapiV4
    {
        T AddAsync<T, E>(E entity) where T : IStrapiEntity<E>;
        T Get<T, E>(int id);
        T Update<T, E>(T entity) where T : IStrapiEntity<E>;
        T Delete<T, E>(int id) where T : IStrapiEntity<E>;
        List<T> GetAll<T, E>();
    }
}