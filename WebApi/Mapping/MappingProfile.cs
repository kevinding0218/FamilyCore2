using AutoMapper;
using DomainLibrary.Meal;
using DomainLibrary.Member;
using DomainLibrary.Menu;
using DomainLibrary.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Persistent.Query;
using WebApi.Resource.Meal.EntreeResource;
using WebApi.Resource.Meal.PhotoResource;
using WebApi.Resource.Member;
using WebApi.Resource.Menu;
using WebApi.Resource.Order;
using WebApi.Resource.QueryResource;
using WebApi.Resource.Shared;
using WebApi.Resource.User;

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
            // Menu
            DomainToApiApplicationMenu();
            // User
            DomainToApiUser();
            #endregion

            #region API Resource/View Model to Domain
            // Vegetable
            CreateMap<VegetableQueryResource, VegetableQuery>();
            // EntreeDetail
            ApiToDomainEntreeDetail();
            // StapleFood
            ApiToDomainStapleFood();
            // Entree
            ApiToDomainEntree();
            // Order
            ApiToDomainInitialOrder();
            // User
            ApiToDomainUser();
            // Member 
            ApiToDomainMember();
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
            this.CreateMap<EntreePhoto, EntreePhotoResource>();
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

            this.CreateMap<Order, OrderProcessInfo>()
                .ForMember(opi => opi.EntreeInfoList, opt => opt.Ignore());
        }

        private void DomainToApiApplicationMenu()
        {
            this.CreateMap<ApplicationMenu, Navigation>()
                .ForMember(nav => nav.children, opt => opt.MapFrom(am => am.Children.Select(nav => new Navigation
                {
                    Icon = nav.Icon,
                    Url = nav.Url,
                    Name = nav.Name,
                    ShowBadge = nav.ShowBadge
                })))
                .ForMember(nav => nav.Badge, opt => opt.Ignore());
        }

        private void DomainToApiUser()
        {
            this.CreateMap<DomainLibrary.Member.UserPassword, UserPasswordResource>();
            this.CreateMap<DomainLibrary.Member.User, ViewUserResource>();
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
                .ForMember(o => o.StartDate, opt => opt.MapFrom(so => so.StartDate.Date.AddHours(0).AddMinutes(0).AddSeconds(0)))
                .ForMember(o => o.EndDate, opt => opt.MapFrom(so => so.EndDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)))
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
                            if (esos.EntreeId == mapping.EntreeId && esos.Note != mapping.Note && esos.Note.Equals(string.Empty))
                                esos.Note = mapping.Note;
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
                                            .Select(eom => new Entrees_Orders { EntreeId = eom.EntreeId, OrderId = so.Id, Count = eom.Count, Note = eom.Note }).ToList();

                    foreach (var e in addedEntrees)
                        o.MappingEntreesWithCurrentOrder.Add(e);
                });

            this.CreateMap<OrderProcessInfo, Order>()
                    .ForMember(o => o.MappingEntreesWithCurrentOrder, opt => opt.Ignore())
                    .AfterMap((opi, o) =>
                    {
                        //// Remove unselected entree order mapping
                        var removedEntrees = new List<Entrees_Orders>();
                        foreach (var mapping in o.MappingEntreesWithCurrentOrder)
                        {
                            if (!opi.EntreeInfoList.Any(opse => opse.EntreeId == mapping.EntreeId))
                                removedEntrees.Add(mapping);
                        }
                        foreach (var e in removedEntrees)
                            o.MappingEntreesWithCurrentOrder.Remove(e);

                        //// Update existed entree if count changes
                        var updateExistedEntreeCountList = new List<Entrees_Orders>();
                        foreach (var mapping in opi.EntreeInfoList)
                        {
                            foreach (var esos in o.MappingEntreesWithCurrentOrder)
                            {
                                if (esos.EntreeId == mapping.EntreeId && esos.Count != mapping.EntreeCount)
                                    esos.Count = mapping.EntreeCount;
                                if (esos.EntreeId == mapping.EntreeId && esos.Note != mapping.Note)
                                    esos.Note = mapping.Note;
                            }
                        }
                    });
        }

        private void ApiToDomainUser()
        {
            this.CreateMap<UserPasswordResource, DomainLibrary.Member.UserPassword>();

            this.CreateMap<RegisterNewUserResource, DomainLibrary.Member.User>()
                                .ForMember(u => u.UserID, opt => opt.Ignore())
                                .ForMember(u => u.FirstName, opt => opt.Ignore())
                                .ForMember(u => u.LastName, opt => opt.Ignore())
                                .ForMember(u => u.IsFCUser, opt => opt.Ignore())
                                .ForMember(u => u.LastLogIn, opt => opt.Ignore())
                                .ForMember(u => u.Note, opt => opt.Ignore())
                                .ForMember(u => u.Active, opt => opt.Ignore())
                                .ForMember(u => u.FullName, opt => opt.Ignore())
                                .ForMember(u => u.PasswordExpired, opt => opt.Ignore())
                                .ForMember(u => u.AddedOn, opt => opt.Ignore())
                                .ForMember(u => u.LastUpdatedById, opt => opt.Ignore())
                                .ForMember(u => u.LastUpdatedByOn, opt => opt.Ignore())
                                .ForMember(u => u.LatestUserPassword, opt => opt.Ignore())
                                .ForMember(u => u.UserPasswords, opt => opt.Ignore())
                                .AfterMap((rnur, u) =>
                                {
                                    u.UserPasswords.Add(new DomainLibrary.Member.UserPassword
                                    {
                                        Password = rnur.Password,
                                        Active = true,
                                        PasswordCreated = DateTime.Now
                                    });
                                });
        }

        private void ApiToDomainMember()
        {
            this.CreateMap<RegistrationViewModel, AppUser>()
                .ForMember(app => app.UserName, map => map.MapFrom(vm => vm.Email)); ;
        }
        #endregion
    }
}
