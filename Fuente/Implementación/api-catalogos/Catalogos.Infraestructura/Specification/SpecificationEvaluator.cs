using Catalogos.Infraestructura.ISpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class SpecificationEvaluator<TDocument>
    {
        public static IQueryable<TDocument> GetQuery(IQueryable<TDocument> inputQuery, ISpecification<TDocument> specification)
        {
            var query = inputQuery;

            // modificar el IQueryable utilizando la expresión de criterios de la especificación
            if (specification.Criterio != null)
            {
                query = query.Where(specification.Criterio);
            }

            // Incluye todas las inclusiones basadas en expresiones
            //query = specification.Includes.Aggregate(query,
            //                        (current, include) => current.Include(include));

            // Incluye cualquier declaración de inclusión basada en cadenas
            //query = specification.IncludeStrings.Aggregate(query,
            //                        (current, include) => current.Include(include));

            // Aplicar el orden si se establecen expresiones
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }
            return query;
        }
    }
}
