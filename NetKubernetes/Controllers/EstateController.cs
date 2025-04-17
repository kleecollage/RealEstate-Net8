using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetKubernetes.Data.Estates;
using NetKubernetes.Dto.EstateDto;
using NetKubernetes.Middleware;
using NetKubernetes.Models;

namespace NetKubernetes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstateController(IEstateRepository repository, IMapper mapper): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstateResponseDto>>> GetEstates()
    {
        var estates = await repository.GetAllEstates();
        return Ok(mapper.Map<IEnumerable<EstateResponseDto>>(estates));
    }

    [HttpGet("{id}", Name = "GetEstateById")]
    public async Task<ActionResult<EstateResponseDto>> GetEstateById(int id)
    {
        var estate = await repository.GetEstateById(id);
        if (estate is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"Estate with id: '{id}' not found" });
        }

        return Ok(mapper.Map<EstateResponseDto>(estate));
    }

    [HttpPost]
    public async Task<ActionResult<EstateResponseDto>> CreateEstate([FromBody] EstateRequestDto estate)
    {
        var estateModel = mapper.Map<Estate>(estate);
        await repository.CreateEstate(estateModel);
        await repository.SaveChanges();

        var estateResponse = mapper.Map<EstateResponseDto>(estateModel);
        return CreatedAtRoute(nameof(GetEstateById), new { estateResponse.Id}, estateResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEstate(int id)
    {
        await repository.DeleteEstate(id);
        await repository.SaveChanges();
        return Ok();
    }
}










