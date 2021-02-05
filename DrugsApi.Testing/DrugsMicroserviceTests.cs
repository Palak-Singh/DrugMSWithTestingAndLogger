using NUnit.Framework;
using Moq;
using DrugMicroservice.Models;
using System.Linq;
using System;
using DrugMicroservice.Controllers;
using DrugMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using DrugMicroservice.Service;
using System.Collections.Generic;

namespace DrugMicroservice.Testing
{
    [TestFixture]
    public class DrugMicroserviceTests
    {
        private DrugsApiController _drugsController;
        private Mock<IDrugService> _drugServiceMock = new Mock<IDrugService>();
        List<Drug> drugList;

        public DrugMicroserviceTests()
        {
            _drugsController = new DrugsApiController(_drugServiceMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            drugList = DrugHelper.drugList;
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void SearchDrugsById_WithValidInput_ReturnsOK(int drugId)
        {
            // Arrange
            Drug drug = drugList.FirstOrDefault(d => d.Id == drugId);
            _drugServiceMock.Setup(p => p.SearchDrugsByID(drugId)).Returns(drug);

            // Act
            OkObjectResult data = _drugsController.SearchDrugsByID(drugId) as OkObjectResult;

            // Assert
            Assert.AreEqual(200, data.StatusCode);
        }

        [Test]
        public void SearchDrugsById_WithInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            Drug drug = drugList.FirstOrDefault(d => d.Id == 1);
            _drugServiceMock.Setup(p => p.SearchDrugsByID(1)).Returns(drug);

            // Act
            BadRequestResult data = _drugsController.SearchDrugsByID(-2) as BadRequestResult;

            // Assert
            Assert.AreEqual(400,data.StatusCode);
        }

        [Test]
        public void SearchDrugsById_WithInputNotPresentInList_ReturnsNotFound()
        {
            // Arrange
            Drug drug = drugList.FirstOrDefault(d => d.Id == 1);
            _drugServiceMock.Setup(p => p.SearchDrugsByID(1)).Returns(drug);

            // Act
            NotFoundObjectResult data = _drugsController.SearchDrugsByID(10) as NotFoundObjectResult;

            // Assert
            Assert.AreEqual(404, data.StatusCode);
        }

        [Test]
        [TestCase("Paracetamol")]
        [TestCase("Saridon")]
        public void SearchDrugsByName_WithValidInput_ReturnsOK(string drugName)
        {
            // Arrange
            Drug drug = drugList.FirstOrDefault(d => d.Name == drugName);
            _drugServiceMock.Setup(p => p.SearchDrugsByName(drugName)).Returns(drug);

            // Act
            OkObjectResult data = _drugsController.SearchDrugsByName(drugName) as OkObjectResult;

            // Assert
            Assert.AreEqual(200, data.StatusCode);
        }

        [Test]
        public void SearchDrugsByName_WithInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            Drug drug = drugList.FirstOrDefault(d => d.Name == "Saridon");
            _drugServiceMock.Setup(p => p.SearchDrugsByName("Saridon")).Returns(drug);

            // Act
            BadRequestResult data = _drugsController.SearchDrugsByName("552") as BadRequestResult;

            // Assert
            Assert.AreEqual(400, data.StatusCode);
        }

        [Test]
        public void SearchDrugsByName_WithInputNotPresentInList_ReturnsNotFound()
        {
            // Arrange
            Drug drug = drugList.FirstOrDefault(d => d.Name == "Disprin");
            _drugServiceMock.Setup(p => p.SearchDrugsByName("Disprin")).Returns(drug);

            // Act
            NotFoundObjectResult data = _drugsController.SearchDrugsByName("Dolo") as NotFoundObjectResult;

            // Assert
            Assert.AreEqual(404, data.StatusCode);
        }

        [Test]
        [TestCase(1, "Delhi")]
        [TestCase(2, "Bangalore")]
        public void GetDispatchableDrugStock_WithValidInput_ReturnsOK(int drugId, string location)
        {
            // Arrange
            DrugLocation drugLocation = (drugList.FirstOrDefault(d => d.Id == drugId && d.DrugLocation.Location == location)).DrugLocation;
            _drugServiceMock.Setup(p => p.GetDispatchableDrugStock(drugId, location)).Returns(drugLocation);

            // Act
            OkObjectResult data = _drugsController.GetDispatchableDrugStock(drugId, location) as OkObjectResult;

            // Assert
            Assert.AreEqual(200, data.StatusCode);
        }

        [Test]
        public void GetDispatchableDrugStock_WithInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            DrugLocation drugLocation = (drugList.FirstOrDefault(d => d.Id == 1 && d.DrugLocation.Location == "Delhi")).DrugLocation;
            _drugServiceMock.Setup(p => p.GetDispatchableDrugStock(1, "Delhi")).Returns(drugLocation);

            // Act
            BadRequestResult data = _drugsController.GetDispatchableDrugStock(0, "55") as BadRequestResult;

            // Assert
            Assert.AreEqual(400, data.StatusCode);
        }

        [Test]
        public void GetDispatchableDrugStock_WithInputNotPresentInList_ReturnsNotFound()
        {
            // Arrange
            DrugLocation drugLocation = (drugList.FirstOrDefault(d => d.Id == 1 && d.DrugLocation.Location == "Delhi")).DrugLocation;
            _drugServiceMock.Setup(p => p.GetDispatchableDrugStock(1, "Delhi")).Returns(drugLocation);

            // Act
            NotFoundObjectResult data = _drugsController.GetDispatchableDrugStock(10, "Agra") as NotFoundObjectResult;

            // Assert
            Assert.AreEqual(404, data.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            drugList = null;
        }
    }
}