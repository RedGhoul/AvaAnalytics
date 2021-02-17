using Application.DTO;
using Application.Queries;
using AutoMapper;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DateRangeDTO, GetBrowserStatsQuery>().ReverseMap();
            CreateMap<DateRangeDTO, GetInteractionByPathCountsStatsQuery>().ReverseMap();
            CreateMap<DateRangeDTO, GetLocationStatsQuery>().ReverseMap();
            CreateMap<DateRangeDTO, GetPageViewStatsQuery>().ReverseMap();
            CreateMap<DateRangeDTO, GetScreenSizeStatsQuery>().ReverseMap();
            CreateMap<DateRangeDTO, GetSystemStatsQuery>().ReverseMap();

            AllowNullCollections = true;
        }
    }
}
