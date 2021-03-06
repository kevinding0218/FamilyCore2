﻿using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Meal.EntreeResource;
using WebApi.Resource.Shared;

namespace WebApi.Persistent.Meal.EntreeHelperRepo
{
    public interface IEntreeHelperRepository
    {
        Task<List<EntreeStyle>> GetEntreeStyles();
        Task<List<EntreeCatagory>> GetEntreeCatagorys();
        Task<List<StapleFood>> GetStapleFoods();
        Task<List<KeyValuePairResource>> GetAvailableEntreeDetailByType(string entreeDetailType, int currentEntreeId);
        Task<List<EntreeDetailType>> GetEntreeDetailTypes();
        Task<List<KeyValuePairResource>> GetSimilarEntreeList(string entreeName, int? stapleFoodId, string EntreeDetailIdList);
        Task<string> GetEntreeStyleOrCatagoryById(string splitBy, int splitId);
        Task<List<EntreeCountBySplit>> GetEntreeCountBySplit(string splitBy);
    }
}
