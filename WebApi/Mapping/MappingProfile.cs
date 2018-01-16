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
            // EntreeDetail
            DomainToApiEntreeDetail();
            // StapleFood
            DomainToApiStapleFood();
            #endregion

            #region API Resource/View Model to Domain
            // Vegetable
            this.CreateMap<VegetableQueryResource, VegetableQuery>();
            // EntreeDetail
            ApiToDomainEntreeDetail();
            // StapleFood
            ApiToDomainStapleFood();
            #endregion
        }

        private void DomainToApiEntreeDetail()
        {
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
        }

        private void DomainToApiStapleFood()
        {
            this.CreateMap<StapleFood, SaveEntreeResource>()
                .ForMember
                (svr => svr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
            this.CreateMap<StapleFood, GridEntreeResource>()
                .ForMember(gvr => gvr.NumberOfEntreeIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.EntreesIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.AddedOn, opt => opt.MapFrom(v => v.AddedOn.ToString()))
                .ForMember(gvr => gvr.LastUpdatedByOn, opt => opt.MapFrom(v => v.LastUpdatedByOn.HasValue ? v.LastUpdatedByOn.Value.ToString() : string.Empty))
                .ForMember
                (gvr => gvr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
        }

        private void ApiToDomainEntreeDetail()
        {
            this.CreateMap<SaveEntreeResource, EntreeDetail>()
                                .ForMember(v => v.Id, opt => opt.Ignore())
                                .ForMember(v => v.Name, opt => opt.MapFrom(svr => svr.keyValuePairInfo.Name));
        }

        private void ApiToDomainStapleFood()
        {
            this.CreateMap<SaveEntreeResource, StapleFood>()
                                .ForMember(v => v.Id, opt => opt.Ignore())
                                .ForMember(v => v.Name, opt => opt.MapFrom(svr => svr.keyValuePairInfo.Name));
        }
    }
}
