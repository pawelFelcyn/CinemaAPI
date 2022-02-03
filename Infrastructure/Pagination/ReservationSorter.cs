using Domain.Entities;
using Domain.Models;
using Domain.Static;

namespace Infrastructure.Pagination;

internal class ReservationSorter : ISorter<Reservation>
{
    public IQueryable<Reservation> Sort(IQueryable<Reservation> collection, SortDirection sortDirection, string sortBy)
    {
        return sortDirection == SortDirection.Asc ? collection.OrderBy(SortByColumnsSelectors.ForReservations[sortBy])
            : collection.OrderByDescending(SortByColumnsSelectors.ForReservations[sortBy]);
    }
}
