using Catalogos.Infraestructura.IRepositories.Query;
using Catalogos.Infraestructura.ISpecification;
using Catalogos.Infraestructura.SettingsDB;
using Catalogos.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Catalogos.Infraestructura.Repositories
{
    public class RepositoryBaseQuery<T> : IRepositoryBaseQuery<T> where T : class
    {
        protected readonly ContextoDB _context;

        public RepositoryBaseQuery(ContextoDB context)
        {
            _context = context;
        }

        public bool Contains(ISpecification<T> specification = null)
        {
            return Count(specification) > 0 ? true : false;
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate) > 0 ? true : false;
        }

        public int Count(ISpecification<T> specification = null)
        {
            return ApplySpecification(specification).Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public IEnumerable<T> Find(ISpecification<T> specification = null)
        {
            return ApplySpecification(specification);
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
