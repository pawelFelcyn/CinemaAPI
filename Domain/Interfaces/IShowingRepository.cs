using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface IShowingRepository
{
    Page<Showing> GetAll(int cinemaId, ResourceQuery query);
    Showing GetById(int cinemaId, int id);
    Showing Add(Showing showing);
    void Remove(Showing showing);
}
