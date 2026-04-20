using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;
using AutoMapper;

namespace Arzly.Api.Mappings
{
    public class SearchQueryProfile : Profile
    {
        public SearchQueryProfile()
        {
            CreateMap<SearchQuery, SearchQueryResponse>();

            CreateMap<SearchQueryAddRequest, SearchQuery>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SearchedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
