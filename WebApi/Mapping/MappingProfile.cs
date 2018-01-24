﻿using AutoMapper;
using DomainLibrary.Meal;
using DomainLibrary.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Persistent.Query;
using WebApi.Resource.Meal.EntreeResource;
using WebApi.Resource.Order;
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
            // Order
            DomainToApiInitialOrder();
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
            // Order
            ApiToDomainInitialOrder();
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

        private void DomainToApiInitialOrder()
        {
            this.CreateMap<Order, SaveInitialOrder>()
                .ForMember(sc => sc.EntreeOrderMappingsWithCurrentOrder,
                opt => opt.MapFrom(o => o.MappingEntreesWithCurrentOrder.Select(esos => new EntreeOrderMapping { EntreeId = esos.EntreeId, Count = esos.Count }).ToArray()));
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
                    // e.g: save contains 1,2 origianl contains 1,2,3 need to remove 3
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
                    // e.g: save contains 1,2, 3 origianl contains 1,2 need to add 3
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

        private void ApiToDomainInitialOrder()
        {
            this.CreateMap<SaveInitialOrder, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.MappingEntreesWithCurrentOrder, opt => opt.Ignore())
                .AfterMap((so, o) =>
                {
                    //// Remove unselected entree order mapping
                    //// e.g: save contains 1,2 origianl contains 1,2,3 need to add count of 1,2 from original
                    var updateExistedEntreeCountList = new List<Entrees_Orders>();
                    foreach (var mapping in so.EntreeOrderMappingsWithCurrentOrder)
                    {
                        foreach (var esos in o.MappingEntreesWithCurrentOrder)
                        {
                            if (esos.EntreeId == mapping.EntreeId && esos.Count != mapping.Count)
                                esos.Count = mapping.Count;
                        }
                    }

                    //var removedEntrees = new List<Entrees_Orders>();
                    //foreach (var mapping in o.MappingEntreesWithCurrentOrder)
                    //{
                    //    if (!so.EntreeOrderMappingsWithCurrentOrder.Any(eom => eom.EntreeId == mapping.EntreeId))
                    //        removedEntrees.Add(mapping);
                    //}
                    //foreach (var e in removedEntrees)
                    //    o.MappingEntreesWithCurrentOrder.Remove(e);


                    //// Add new selected entree order mapping
                    //// e.g: save contains 1,2, 3 origianl contains 1,2 need to add 3
                    var addedEntrees = so.EntreeOrderMappingsWithCurrentOrder
                                            .Where(eom => !o.MappingEntreesWithCurrentOrder.Any(esos => esos.EntreeId == eom.EntreeId))
                                            .Select(eom => new Entrees_Orders { EntreeId = eom.EntreeId, OrderId = so.Id, Count = eom.Count }).ToList();

                    foreach (var e in addedEntrees)
                        o.MappingEntreesWithCurrentOrder.Add(e);
                });
        }
        #endregion
    }
}
