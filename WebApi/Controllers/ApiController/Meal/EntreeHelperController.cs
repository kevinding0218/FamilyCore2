﻿using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Persistent.Meal.EntreeHelperRepo;
using WebApi.Resource.Meal.EntreeResource;
using WebApi.Resource.Shared;

namespace WebApi.Controllers.ApiController.Meal
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/entreeHelper")]
    public class EntreeHelperController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEntreeHelperRepository _entreeHelperRepository;

        public EntreeHelperController(
            IMapper mapper,
            IEntreeHelperRepository entreeHelperRepository
        )
        {
            this._entreeHelperRepository = entreeHelperRepository;
            this._mapper = mapper;
        }

        [HttpGet("getEntreeHelperDropdownItems")]  //api/entreeHelper/attribute?attribute=a&currentEntreeId=b
        public async Task<IEnumerable<KeyValuePairResource>> GetEntreeHelperDropdownItems(string attribute, int currentEntreeId)
        {
            if (attribute.ToLower().Equals("style"))
            {
                var styles = await this._entreeHelperRepository.GetEntreeStyles();
                var result = _mapper.Map<List<EntreeStyle>, List<KeyValuePairResource>>(styles);
                return result;
            }
            else if (attribute.ToLower().Equals("catagory"))
            {
                var catagories = await this._entreeHelperRepository.GetEntreeCatagorys();
                return _mapper.Map<List<EntreeCatagory>, List<KeyValuePairResource>>(catagories);
            }
            else if (attribute.ToLower().Equals("staplefood"))
            {
                var staplefoods = await this._entreeHelperRepository.GetStapleFoods();
                return _mapper.Map<List<StapleFood>, List<KeyValuePairResource>>(staplefoods);
            }
            else if (attribute.ToLower().Equals("entreedetailtype"))
            {
                var entreeDetails = await this._entreeHelperRepository.GetEntreeDetailTypes();
                return _mapper.Map<List<EntreeDetailType>, List<KeyValuePairResource>>(entreeDetails);
            }
            else
            {
                return await this._entreeHelperRepository.GetAvailableEntreeDetailByType(attribute, currentEntreeId);
            }
        }

        [HttpGet("getEntreeStyleOrCatagory")]  //api/entreeHelper/getEntreeStyleOrCatagory?splitBy=a&splitId=b
        public async Task<string> GetEntreeStyleOrCatagoryById(string splitBy, int splitId)
        {
            return await this._entreeHelperRepository.GetEntreeStyleOrCatagoryById(splitBy, splitId);
        }

        [HttpGet("getEntreeCountBySplit")]  //api/entreeHelper/getEntreeCountBySplit?splitBy=a
        public async Task<List<EntreeCountBySplit>> GetEntreeCountBySplit(string splitBy)
        {
            return await this._entreeHelperRepository.GetEntreeCountBySplit(splitBy);
        }

        [HttpPost("getSimilarEntreeList")]  //api/entreeHelper/similar
        public async Task<IEnumerable<KeyValuePairResource>> GetSimilarEntreeList([FromBody] SimilarEntreeInputObj entreeObj)
        {
            return await this._entreeHelperRepository.GetSimilarEntreeList(entreeObj.entreeName, entreeObj.stapleFoodId, entreeObj.entreeDetailIdList);
        }
    }
}