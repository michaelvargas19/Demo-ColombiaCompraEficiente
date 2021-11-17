using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Infraestructura.IRepositories.Command
{
    public interface IRepositoryBaseCommand<T> where T : class
    {

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

    }
}
