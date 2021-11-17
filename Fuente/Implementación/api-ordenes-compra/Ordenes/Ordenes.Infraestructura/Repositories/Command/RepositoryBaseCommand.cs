using Microsoft.EntityFrameworkCore;
using Ordenes.Infraestructura.IRepositories.Command;
using Ordenes.Infraestructura.ISpecification;
using Ordenes.Infraestructura.SettingsDB;
using Ordenes.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ordenes.Infraestructura.Repositories.Command
{
    public class RepositoryBaseCommand<T> : IRepositoryBaseCommand<T> where T : class
    {
        protected readonly ContextoDB _context;

        public RepositoryBaseCommand(ContextoDB context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
