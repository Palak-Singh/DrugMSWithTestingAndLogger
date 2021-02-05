using DrugMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugMicroservice.Repository
{
    public class DrugHelper
    {
        public static List<Drug> drugList = new List<Drug>()
        {
          new Drug
          {
              Id = 1, Name = "Paracetamol", Manufacturer = "Bristol", ManufacturedDate = new DateTime(2021,05,05), ExpiryDate = new DateTime(2023,02,02),
              DrugLocation = new DrugLocation
              {
                  DrugId = 1, DrugName = "Paracetamol", Location = "Delhi", ExpiryDate = new DateTime(2023,02,02), Quantity = 50
              }
          },

          new Drug
          {
              Id = 2, Name = "Saridon", Manufacturer = "Piramal", ManufacturedDate = new DateTime(2021,02,20), ExpiryDate = new DateTime(2022,02,20),
              DrugLocation = new DrugLocation
              {
                  DrugId = 2, DrugName = "Saridon", Location = "Bangalore", ExpiryDate = new DateTime(2022,02,20), Quantity = 80
              }
          },

          new Drug
          {
              Id = 3, Name = "Disprin", Manufacturer = "abbott", ManufacturedDate = new DateTime(2021,06,15), ExpiryDate = new DateTime(2023,06,15),
              DrugLocation = new DrugLocation
              {
                  DrugId = 3, DrugName = "Disprin", Location = "Pune", ExpiryDate = new DateTime(2023,06,15), Quantity = 60
              }
          }
        };


        public static List<DrugLocation> drugLocationList = new List<DrugLocation>()
        {
          new DrugLocation { DrugId = 1, DrugName = "Paracetamol", Location = "Delhi", ExpiryDate = new DateTime(2023,02,02), Quantity = 50 },
          new DrugLocation { DrugId = 2, DrugName = "Saridon", Location = "Bangalore", ExpiryDate = new DateTime(2022,02,20), Quantity = 80 },
          new DrugLocation { DrugId = 3, DrugName = "Disprin", Location = "Pune", ExpiryDate = new DateTime(2023,06,15), Quantity = 60 }
        };
    }
}
