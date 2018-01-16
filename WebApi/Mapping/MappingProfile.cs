using AutoMapper;
using DomainLibrary.Meal;
using WebApi.Persistent.Query;
using WebApi.Resource.Meal.EntreeResource;
using WebApi.Resource.QueryResource;
using WebApi.Resource.Shared;

namespace WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Domain to API Resource/View Model
            // QueryResult
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            // Vegetable
            this.CreateMap<EntreeDetail, SaveEntreeResource>()
                .ForMember
                (svr => svr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
            this.CreateMap<EntreeDetail, GridEntreeResource>()
                .ForMember(gvr => gvr.NumberOfEntreeIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.EntreesIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.AddedOn, opt => opt.MapFrom(v => v.AddedOn.ToString()))
                .ForMember(gvr => gvr.LastUpdatedByOn, opt => opt.MapFrom(v => v.LastUpdatedByOn.HasValue ? v.LastUpdatedByOn.Value.ToString() : string.Empty))
                .ForMember
                (gvr => gvr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
            // Meat
            this.CreateMap<EntreeDetail, SaveEntreeResource>()
                .ForMember
                (svr => svr.keyValuePairInfo, opt => opt.MapFrom(m => new KeyValuePairResource { Id = m.Id, Name = m.Name }));
            this.CreateMap<EntreeDetail, GridEntreeResource>()
                .ForMember(gvr => gvr.NumberOfEntreeIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.EntreesIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.AddedOn, opt => opt.MapFrom(m => m.AddedOn.ToString()))
                .ForMember(gvr => gvr.LastUpdatedByOn, opt => opt.MapFrom(m => m.LastUpdatedByOn.HasValue ? m.LastUpdatedByOn.Value.ToString() : string.Empty))
                .ForMember
                (gvr => gvr.keyValuePairInfo, opt => opt.MapFrom(m => new KeyValuePairResource { Id = m.Id, Name = m.Name }));
            #endregion

            #region API Resource/View Model to Domain
            // Vegetable
            this.CreateMap<VegetableQueryResource, VegetableQuery>();
            this.CreateMap<SaveEntreeResource, EntreeDetail>()
                    .ForMember(v => v.Id, opt => opt.Ignore())
                    .ForMember(v => v.Name, opt => opt.MapFrom(svr => svr.keyValuePairInfo.Name));
            // Meat
            this.CreateMap<SaveEntreeResource, EntreeDetail>()
                    .ForMember(m => m.Id, opt => opt.Ignore())
                    .ForMember(m => m.Name, opt => opt.MapFrom(svr => svr.keyValuePairInfo.Name));
            #endregion
        }
    }
}
