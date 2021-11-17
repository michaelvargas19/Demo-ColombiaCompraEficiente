using Catalogos.Infraestructura.ISpecification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Catalogos.Infraestructura.IRepositories.Query
{
    public interface IRepositoryBaseQuery<T> where T : class
    {
        T FindById(int id);

        IEnumerable<T> Find(ISpecification<T> specification = null);

        bool Contains(ISpecification<T> specification = null);
        bool Contains(Expression<Func<T, bool>> predicate);

        int Count(ISpecification<T> specification = null);
        int Count(Expression<Func<T, bool>> predicate);

    }
}
