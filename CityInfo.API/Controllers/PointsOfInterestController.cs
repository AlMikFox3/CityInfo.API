using CityInfo.API.Models;
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
        [HttpGet("{cid}/pointsofinterest/{pid}", Name = "GetPointOfInterest")]
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

        [HttpPost("{cityid}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityid, [FromBody] PointOfInterestCreationDto pointofInterest)
        {
            if (pointofInterest == null)
                return BadRequest();
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityid);
            if (city == null)
                return NotFound();
            var maxPOI = CityDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
            var finalPOI = new PointOfInterestDto()
            {
                Id = ++maxPOI,
                Name = pointofInterest.Name,
                Description = pointofInterest.Description,
            };
            city.PointsOfInterest.Add(finalPOI);
            return CreatedAtRoute("GetPointofInterest", new { cid = cityid, pid = finalPOI.Id }, finalPOI);
        }
    }
}
