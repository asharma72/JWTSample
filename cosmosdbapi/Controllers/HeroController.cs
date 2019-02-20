using cosmosdbapi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace cosmosdbapi.Controllers
{
    [RoutePrefix("api/Hero")]
    public class HeroController : ApiController
    {

        [HttpGet]
        public async Task<IEnumerable<Hero>> GetAsync()
        {

            IEnumerable<Hero> value = await DocumentDBRepository<Hero>.GetItemsAsync();
            return value;
        }

        [HttpPost]
        public async Task<Hero> CreateAsync([FromBody] Hero hero)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Hero>.CreateItemAsync(hero);
                return hero;
            }
            return null;
        }
        public async Task<string> Delete(string uid)
        {
            try
            {
                Hero item = await DocumentDBRepository<Hero>.GetSingleItemAsync(d => d.UId == uid);
                if (item == null)
                {
                    return "Failed";
                }
                await DocumentDBRepository<Hero>.DeleteItemAsync(item.Id);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public async Task<Hero> Put(string uid, [FromBody] Hero hero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Hero item = await DocumentDBRepository<Hero>.GetSingleItemAsync(d => d.UId == uid);
                    if (item == null)
                    {
                        return null;
                    }
                    hero.Id = item.Id;
                    await DocumentDBRepository<Hero>.UpdateItemAsync(item.Id, hero);
                    return hero;
                }
                return null; ;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}