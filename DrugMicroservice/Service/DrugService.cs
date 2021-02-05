using DrugMicroservice.Models;
using DrugMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugMicroservice.Service
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;

        public DrugService(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }

        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug ID
        /// </summary>
        public Drug SearchDrugsByID(int drugId)
        {
            return _drugRepository.SearchDrugsByID(drugId);

        }

        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug Name
        /// </summary>
        public Drug SearchDrugsByName(string drugName)
        {
            return _drugRepository.SearchDrugsByName(drugName);
        }

        /// <summary>
        /// This method is responsible for returing the Drug Details searched by Drug ID and Location
        /// </summary>
        public DrugLocation GetDispatchableDrugStock(int drugId, string location)
        {
            return _drugRepository.GetDispatchableDrugStock(drugId, location);
        }

        /// <summary>
        /// This method is responsible for returing the Drug Details of all drugs available
        /// </summary>
        public List<Drug> GetAllAvailableDrugs()
        {    
            return _drugRepository.GetAllAvailableDrugs();
        }
    }
}
