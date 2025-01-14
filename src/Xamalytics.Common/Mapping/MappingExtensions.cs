using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Xamalytics.Common.Mapping
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, MapperConfiguration configuration, CancellationToken cancellationToken)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync(cancellationToken);
    }
}
