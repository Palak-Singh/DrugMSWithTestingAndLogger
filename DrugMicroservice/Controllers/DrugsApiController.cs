using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugMicroservice.Models;
using DrugMicroservice.Repository;
using DrugMicroservice.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrugMicroservice.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrugsApiController : ControllerBase
    {
        private readonly IDrugService _drugService;

        public DrugsApiController(IDrugService drugService)
        {
            _drugService = drugService;
        }


        /// <summary>
        /// This method is responsible for returing the Drug Details of all drugs available
        /// </summary>
        [HttpGet]
        public IActionResult GetAllAvailableDrugs()
        {
            try
            {
                // checking if drug is available
                var drug = _drugService.GetAllAvailableDrugs();
                if (drug == null)
                {
                    return NotFound("No Drug Available");
                }
                return Ok(drug);
            }

            catch(Exception e)
            {
                return BadRequest("Error occured from " + nameof(DrugsApiController.GetAllAvailableDrugs) + " Error Message " + e.Message);
            }
        }


        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug ID
        /// </summary>
        /// <param name="drug_id"></param>
        /// <returns></returns>

        [HttpGet("{drugId}")]
        public IActionResult SearchDrugsByID(int drugId)
        {
            try
            {
                // validating drugId
                if (drugId > 0)
                {
                    // Checking if drug with specific id is present.
                    Drug drug = _drugService.SearchDrugsByID(drugId);

                    // Drug ID(id) entered For Searching.
                    if (drug == null)
                        return NotFound("Drug with specified drugId is not available");
                    return Ok(drug);
                }
                return BadRequest();
            }

            catch(Exception e)
            {
                return BadRequest("Error occured from " + nameof(DrugsApiController.SearchDrugsByID) + " Error Message " + e.Message);
            }
        }


        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug Name
        /// </summary>
        /// <param name="drug_name"></param>
        /// <returns></returns>

        [HttpGet("{drugName}")]
        public IActionResult SearchDrugsByName(string drugName)
        {
            try
            {
                // validating drugName - drugName.GetType() != typeof(string)
                if (drugName.All(Char.IsLetter))
                {

                    // Checking if drug with specific name is present.
                    var drug = _drugService.SearchDrugsByName(drugName);

                    // Drug Name(name) entered For Searching.
                    if (drug == null)
                        return NotFound("Drug with specified drugName is not available");
                    return Ok(_drugService.SearchDrugsByName(drugName));
                }
                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest("Error occured from " + nameof(DrugsApiController.SearchDrugsByName) + " Error Message " + e.Message);
            }
        }

        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug ID and Location
        /// </summary>
        /// <param name="drug_id"></param>
        /// <param name="drug_loc"></param>
        /// <returns></returns>

        [HttpPost("{drugId}/{location}")]
        public IActionResult GetDispatchableDrugStock([FromRoute] int drugId, string location)
        {
            try
            {
                //Validating drugId and location
                if (drugId > 0 && (location.All(Char.IsLetter)))
                {

                    // Checking if drug with specific id is present.
                    var drug = _drugService.GetDispatchableDrugStock(drugId, location);

                    // Drug Id(drugId) and Location(location) recieved From other Api's.
                    if (drug == null)
                        return NotFound("Drug with specified drugId and location is not available");
                    return Ok(drug);
                }
                else
                    return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest("Error occured from " + nameof(DrugsApiController.GetDispatchableDrugStock) + " Error Message " + e.Message);
            }
        }

    }
}