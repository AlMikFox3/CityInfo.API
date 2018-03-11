using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{id}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int id)
        {
            var city_ret = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (city_ret == null)
            {
                return NotFound();
            }
            return Ok(city_ret.PointsOfInterest);
   
        }
        [HttpGet("{cid}/pointsofinterest/{pid}")]
        public IActionResult GetPointOfInterest(int cid, int pid)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cid);
            if (city == null)
                return NotFound();
            var PointsOfInt = city.PointsOfInterest.FirstOrDefault(p=> p.Id == pid);
            if (PointsOfInt == null)
                return NotFound();
            return Ok(PointsOfInt);
        }
    }
}
