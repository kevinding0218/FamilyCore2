using AutoMapper;
using DomainLibrary.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // Entree
            DomainToApiEntree();
            // Entree Helper
            DomainToApiEntreeHelper();
            #endregion

            #region API Resource/View Model to Domain
            // Vegetable
            this.CreateMap<VegetableQueryResource, VegetableQuery>();
            // EntreeDetail
            ApiToDomainEntreeDetail();
            // StapleFood
            ApiToDomainStapleFood();
            // Entree
            ApiToDomainEntree();
            #endregion
        }

        #region Domain to API Resource/View Model
        private void DomainToApiEntreeDetail()
        {
            this.CreateMap<EntreeDetail, SaveEntreeDetailResource>()
                .ForMember
                (svr => svr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }))
                .ForMember
                (svr => svr.DetailType, opt => opt.MapFrom(ed => ed.EntreeDetailType.DetailName.ToLower()));

            this.CreateMap<EntreeDetail, GridEntreeDetailResource>()
                .ForMember(gvr => gvr.NumberOfEntreeIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.EntreesIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.AddedOn, opt => opt.MapFrom(v => v.AddedOn.ToString()))
                .ForMember(gvr => gvr.LastUpdatedByOn, opt => opt.MapFrom(v => v.LastUpdatedByOn.HasValue ? v.LastUpdatedByOn.Value.ToString() : string.Empty))
                .ForMember
                (gvr => gvr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
        }

        private void DomainToApiStapleFood()
        {
            this.CreateMap<StapleFood, SaveEntreeDetailResource>()
                .ForMember
                (svr => svr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
            this.CreateMap<StapleFood, GridEntreeDetailResource>()
                .ForMember(gvr => gvr.NumberOfEntreeIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.EntreesIncluded, opt => opt.Ignore())
                .ForMember(gvr => gvr.AddedOn, opt => opt.MapFrom(v => v.AddedOn.ToString()))
                .ForMember(gvr => gvr.LastUpdatedByOn, opt => opt.MapFrom(v => v.LastUpdatedByOn.HasValue ? v.LastUpdatedByOn.Value.ToString() : string.Empty))
                .ForMember
                (gvr => gvr.keyValuePairInfo, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Id, Name = v.Name }));
            this.CreateMap<StapleFood, KeyValuePairResource>();
        }

        private void DomainToApiEntree()
        {
            this.CreateMap<Entree, EntreeInfoResource>()
                .ForMember(eir => eir.EntreeId, opt => opt.MapFrom(e => e.Id))
                .ForMember(eir => eir.EntreeName, opt => opt.MapFrom(e => e.Name))
                .ForMember(eir => eir.VegetableCount, opt => opt.Ignore())
                .ForMember(eir => eir.MeatCount, opt => opt.Ignore())
                .ForMember(eir => eir.StapleFood, opt => opt.MapFrom(e => e.StapleFood.Name))
                .ForMember(eir => eir.Style, opt => opt.MapFrom(e => e.EntreeStyle.Style))
                .ForMember(eir => eir.Catagory, opt => opt.MapFrom(e => e.EntreeCatagory.Catagory))
                .ForMember(eir => eir.AddedOn, opt => opt.MapFrom(e => e.AddedOn.ToString()))
                .ForMember(eir => eir.EntreeDetailList, opt => opt.Ignore());
            this.CreateMap<Entree, SaveEntreeResource>()
                .ForMember(svr => svr.EntreeDetails, opt => opt.Ignore());
        }

        private void DomainToApiEntreeHelper()
        {
            this.CreateMap<EntreeStyle, KeyValuePairResource>()
                .ForMember(kpr => kpr.Name, opt => opt.MapFrom(es => es.Style));
            this.CreateMap<EntreeCatagory, KeyValuePairResource>()
                .ForMember(kpr => kpr.Name, opt => opt.MapFrom(ec => ec.Catagory));
            this.CreateMap<EntreeDetailType, KeyValuePairResource>()
                .ForMember(kpr => kpr.Name, opt => opt.MapFrom(edt => edt.DetailName));
        }
        #endregion

        #region API Resource/View Model to Domain
        private void ApiToDomainEntreeDetail()
        {
            this.CreateMap<SaveEntreeDetailResource, EntreeDetail>()
                                .ForMember(ed => ed.Id, opt => opt.Ignore())
                                .ForMember(ed => ed.Name, opt => opt.MapFrom(svr => svr.keyValuePairInfo.Name))
                                .ForMember(ed => ed.EntreeDetailTypeId, opt => opt.Ignore())
                                .ForMember(ed => ed.EntreeDetailType, opt => opt.Ignore());
        }

        private void ApiToDomainStapleFood()
        {
            this.CreateMap<SaveEntreeDetailResource, StapleFood>()
                                .ForMember(v => v.Id, opt => opt.Ignore())
                                .ForMember(v => v.Name, opt => opt.MapFrom(svr => svr.keyValuePairInfo.Name));
        }
        private void ApiToDomainEntree()
        {
            this.CreateMap<SaveEntreeResource, Entree>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.StapleFood, opt => opt.Ignore())
                .ForMember(e => e.EntreeCatagory, opt => opt.Ignore())
                .ForMember(e => e.EntreeStyle, opt => opt.Ignore())
                .ForMember(e => e.MappingDetailsWithCurrentEntree, opt => opt.Ignore())
                .AfterMap((se, e) =>
                {
                    // Remove unselected entree details
                    var removedEntreeDetails = new List<Entrees_Details>();
                    foreach (var ed in e.MappingDetailsWithCurrentEntree)
                    {
                        if (!se.EntreeDetails.Any(edm => edm.EntreeDetailId == ed.EntreeDetailId))
                            removedEntreeDetails.Add(ed);
                    }

                    foreach (var esds in removedEntreeDetails)
                        e.MappingDetailsWithCurrentEntree.Remove(esds);

                    // Add new entree details
                    //var addedEntreeDetails = new List<Entrees_Details>();
                    //foreach (var edm in se.EntreeDetails)
                    //{
                    //    if (!e.MappingDetailsWithCurrentEntree.Any(esds => esds.EntreeDetailId == edm.EntreeDetailId))
                    //        addedEntreeDetails.Add(new Entrees_Details { EntreeId = e.Id, EntreeDetailId = edm.EntreeDetailId });
                    //} 
                    var addedEntreeDetails = se.EntreeDetails
                                                    .Where(edm => !e.MappingDetailsWithCurrentEntree.Any(esds => esds.EntreeDetailId == edm.EntreeDetailId))
                                                    .Select(edm => new Entrees_Details { EntreeId = e.Id, EntreeDetailId = edm.EntreeDetailId, Quantity = edm.Quantity, AddedById = se.AddedById, AddedOn = DateTime.Now }).ToList();

                    foreach (var esds in addedEntreeDetails)
                        e.MappingDetailsWithCurrentEntree.Add(esds);



                    #region Old Linq
                    /*
                    foreach (var ed in e.MappingDetailsWithCurrentEntree)
                        if (!se.EntreeDetailIds.Contains(ed.EntreeDetailId))
                            removedEntreeDetails.Add(ed);
                    var removedEntreeDetails = e.MappingDetailsWithCurrentEntree.Where(esds => !se.EntreeDetailIds.Contains(esds.EntreeDetailId));
                    */
                    #endregion
                });
        }
        #endregion
    }
}
