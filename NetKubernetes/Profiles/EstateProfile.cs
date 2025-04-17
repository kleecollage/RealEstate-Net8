using AutoMapper;
using NetKubernetes.Dto.EstateDto;
using NetKubernetes.Models;

namespace NetKubernetes.Profiles;

public class EstateProfile: Profile
{
    public EstateProfile()
    {
        CreateMap<Estate, EstateResponseDto>();
        CreateMap<EstateRequestDto, Estate>();
    }
}