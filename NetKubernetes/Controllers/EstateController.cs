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
    public ActionResult<IEnumerable<EstateResponseDto>> GetEstates()
    {
        var estates = repository.GetAllEstates();
        return Ok(mapper.Map<IEnumerable<EstateResponseDto>>(estates));
    }

    [HttpGet("{id}", Name = "GetEstateById")]
    public ActionResult<EstateResponseDto> GetEstateById(int id)
    {
        var estate = repository.GetEstateById(id);
        if (estate is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { message = $"Estate with id: '{id}' not found" });
        }

        return Ok(mapper.Map<EstateResponseDto>(estate));
    }

    [HttpPost]
    public ActionResult<EstateResponseDto> CreateEstate([FromBody] EstateResponseDto estate)
    {
        var estateModel = mapper.Map<Estate>(estate);
        repository.CreateEstate(estateModel);
        repository.SaveChanges();

        var estateResponse = mapper.Map<EstateResponseDto>(estateModel);
        return CreatedAtRoute(nameof(GetEstateById), new { estateResponse.Id}, estateResponse);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteEstate(int id)
    {
        repository.DeleteEstate(id);
        repository.SaveChanges();
        return Ok();
    }
}










