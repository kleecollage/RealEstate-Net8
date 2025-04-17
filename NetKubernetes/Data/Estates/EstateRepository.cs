using System.Net;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Estates;

public class EstateRepository(AppDbContext context, IUserSession session, UserManager<User> userManager): IEstateRepository
{
    public bool SaveChanges()
    {
        return (context.SaveChanges() >= 0);
    }

    public IEnumerable<Estate> GetAllEstates()
    {
        return context.Estates!.ToList();
    }

    public Estate GetEstateById(int id)
    {
        return context.Estates!.FirstOrDefault(e => e.Id == id)!;
    }

    public async Task CreateEstate(Estate estate)
    {
        var user = await userManager.FindByNameAsync(session.GetUserSession());
        if (user is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new {message = "User is not authorized to perform this operation"});
        }

        if (estate is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new {message = "Bad state data"});
        }
        estate.CreatedAt = DateTime.Now;
        estate.UserId = Guid.Parse(user.Id);
        context.Estates!.Add(estate);
    }

    public void UpdateEstate(Estate estate)
    {
        throw new NotImplementedException();
    }

    public void DeleteEstate(int id)
    {
        var estate = context.Estates!.FirstOrDefault(e => e.Id == id);
        context.Estates!.Remove(estate!);
    }
}