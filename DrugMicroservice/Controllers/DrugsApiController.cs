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
        readonly log4net.ILog _log4net;

        public DrugsApiController(IDrugService drugService)
        {
            _drugService = drugService;
            _log4net = log4net.LogManager.GetLogger(typeof(DrugsApiController));
        }


        /// <summary>
        /// This method is responsible for returing the Drug Details of all drugs available
        /// </summary>
        [HttpGet]
        public IActionResult GetAllAvailableDrugs()
        {
            try
            {
                _log4net.Info("Displaying All Drugs Available ");

                // checking if drug is available
                var drug = _drugService.GetAllAvailableDrugs();
                if (drug == null)
                {
                    _log4net.Info("No Drug Available");
                    return NotFound("No Drug Available");
                }
                return Ok(drug);
            }

            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugsApiController.GetAllAvailableDrugs) + " Error Message " + e.Message);
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
                _log4net.Info("Searched drug with DrugId "+ drugId);

                // validating drugId
                if (drugId > 0)
                {
                    // Checking if drug with specific id is present.
                    Drug drug = _drugService.SearchDrugsByID(drugId);

                    // Drug ID(id) entered For Searching.
                    if (drug == null)
                    {

                        _log4net.Info("Drug with drugId -> " + drugId + " not available.");
                        return NotFound("Drug with specified drugId is not available");
                    }
                    return Ok(drug);
                }
                _log4net.Info("Invalid DrugId " + drugId);
                return BadRequest();
            }

            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugsApiController.SearchDrugsByID) + " Error Message " + e.Message);
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
                _log4net.Info("Searched drug with DrugName " + drugName);

                // validating drugName - drugName.GetType() != typeof(string)
                if (drugName.All(Char.IsLetter))
                {

                    // Checking if drug with specific name is present.
                    var drug = _drugService.SearchDrugsByName(drugName);

                    // Drug Name(name) entered For Searching.
                    if (drug == null)
                    {

                        _log4net.Info("Drug with drugName -> " + drugName + " not available.");
                        return NotFound("Drug with specified drugName is not available");
                    }
                    return Ok(_drugService.SearchDrugsByName(drugName));
                }
                _log4net.Info("Invalid DrugName " + drugName);
                return BadRequest();
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugsApiController.SearchDrugsByName) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(DrugsApiController.SearchDrugsByName) + " Error Message " + e.Message);
            }
        }

        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug ID and Location
        /// </summary>
        /// <param name="drug_id"></param>
        /// <param name="drug_loc"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult GetDispatchableDrugStock(int drugId, string location)
        {
            try
            {
                _log4net.Info("Searched drug with DrugId " + drugId + " and Location " + location);

                //Validating drugId and location
                if (drugId > 0 && (location.All(Char.IsLetter)))
                {

                    // Checking if drug with specific id is present.
                    var drug = _drugService.GetDispatchableDrugStock(drugId, location);

                    // Drug Id(drugId) and Location(location) recieved From other Api's.
                    if (drug == null)
                    {
                        _log4net.Info("Drug with drugId -> " + drugId + " and Location -> " + location +" not available.");
                        return NotFound("Drug with specified drugId and location is not available");
                    }
                    return Ok(drug);
                }
                else
                {
                    _log4net.Info("Invalid DrugId or location");
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugsApiController.GetDispatchableDrugStock) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(DrugsApiController.GetDispatchableDrugStock) + " Error Message " + e.Message);
            }
        }

    }
}