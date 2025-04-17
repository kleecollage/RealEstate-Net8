using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Estates;

public class EstateRepository(AppDbContext context, IUserSession session, UserManager<User> userManager): IEstateRepository
{
    public async Task<bool> SaveChanges()
    {
        return await context.SaveChangesAsync() >= 0;
    }

    public async Task<IEnumerable<Estate>> GetAllEstates()
    {
        return await context.Estates!.ToListAsync();
    }

    public async Task<Estate> GetEstateById(int id)
    {
        return await context.Estates!.FirstOrDefaultAsync(e => e.Id == id);
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
        await context.Estates!.AddAsync(estate);
    }

    public void UpdateEstate(Estate estate)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEstate(int id)
    {
        var estate = await context.Estates!.FirstOrDefaultAsync(e => e.Id == id);
        context.Estates!.Remove(estate!);
    }
}