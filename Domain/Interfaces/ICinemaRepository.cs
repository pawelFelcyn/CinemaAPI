using Domain.Entities;
using Domain.Models;
using Domain.Modification;

namespace Domain.Interfaces;

public interface ICinemaRepository
{
    Page<Cinema> GetAll(ResourceQuery query);
    Cinema GetById(int id);
    Cinema Add(Cinema cinema);
    Cinema Update(Cinema cinema, CinemaModificationParams updated);
    void Remove(Cinema cinema);
}
