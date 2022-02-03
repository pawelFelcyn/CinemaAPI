using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Pagination;

public interface IPageCreator<T> 
{
    Page<T> CreatePage(IQueryable<T> collection, ResourceQuery query);
}
