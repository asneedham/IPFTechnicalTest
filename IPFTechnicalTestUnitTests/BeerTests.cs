using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

using NSubstitute;

using IPFTechnicalTest.Controllers;
using IPFTechnicalTest.DataAccess;
using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;
using IPFTechnicalTest.ViewModels;

namespace IPFTechnicalTestUnitTests
{
    [TestClass]
    public class BeerTests
    {
        [TestMethod]
        public void Repository_GetBarWithNoParameters_AllBarsReturned()
        {
            // Arrange
            var repositoryMock = Substitute.For<IBeerRepository>();
            repositoryMock.GetAllBars().Returns(new List<Bar>() { new Bar() });

            var barsController = new BarsController(repositoryMock);

            // Act
            var result = barsController.GetBar().Result;

            // Assert
            repositoryMock.Received().GetAllBars();
        }

        [TestMethod]
        public void Repository_GetBarWithWithIdParameter_SpecificBarReturned()
        {
            // Arrange
            var repositoryMock = Substitute.For<IBeerRepository>();
            repositoryMock.GetBar(1).Returns(new Bar { BarId = 1 });

            var barsController = new BarsController(repositoryMock);

            // Act
            var result = barsController.GetBar(1).Result;

            // Assert
            repositoryMock.Received().GetBar(1);
        }

        [TestMethod]
        public void Repository_PostBar_NewBarAdded()
        {
            // Arrange
            var barViewModel = new BarViewModel
            {
                BarId = 1,
                Name = "Test Bar",
                Address = "Test Address for Test Bar"
            };

            var bar = new Bar
            {
                BarId = 1,
                Name = barViewModel.Name,
                Address = barViewModel.Address
            };

            var repositoryMock = Substitute.For<IBeerRepository>();
            repositoryMock.AddBar(bar).ReturnsForAnyArgs(1);

            var barsController = new BarsController(repositoryMock);

            // Act
            var result = barsController.PostBar(barViewModel).Result;

            // Assert
            repositoryMock.Received().AddBar(bar);
        }

        //[TestMethod]
        //public void Repository_DeleteExistingBar_ReturnNoContentStatusCode()
        //{
        //    // Arrange
        //    var repositoryMock = Substitute.For<IBeerRepository>();
        //    repositoryMock.DeleteBar(1).ReturnsForAnyArgs(true);

        //    var barsController = new BarsController(repositoryMock);

        //    // Act
        //    var result = barsController.DeleteBar(1).Result;

        //    // Assert
        //    Assert.AreEqual("NoContentResult", result.GetType().Name); // status 204
        //}

        //[TestMethod]
        //public void Repository_DeleteNonExistantBar_ReturnNotFoundStatusCode()
        //{
        //    // Arrange
        //    var repositoryMock = Substitute.For<IBeerRepository>();
        //    repositoryMock.DeleteBar(1).ReturnsForAnyArgs(false);

        //    var barsController = new BarsController(repositoryMock);

        //    // Act
        //    var result = barsController.DeleteBar(1).Result;

        //    // Assert
        //    Assert.AreEqual("NotFoundResult", result.GetType().Name);  // status 404
        //}

        [TestMethod]
        public void Repository_UpdateBar_ReturnNoContentStatusCode()
        {
            // Arrange

            var barViewModel = new BarViewModel
            {
                BarId = 1,
                Name = "Test Bar",
                Address = "Test Address for Test Bar"
            };

            var bar = new Bar
            {
                BarId = 1,
                Name = barViewModel.Name,
                Address = barViewModel.Address
            };

            var repositoryMock = Substitute.For<IBeerRepository>();
            repositoryMock.UpdateBar(bar).ReturnsForAnyArgs(bar.BarId);

            var barsController = new BarsController(repositoryMock);

            // Act
            var result = barsController.PutBar(bar.BarId, barViewModel).Result;

            // Assert
            Assert.AreEqual("NoContentResult", result.GetType().Name); // status 204
        }

        [TestMethod]
        public void Repository_UpdateBarReceiveException_ReturnNoContentStatusCode()
        {
            // Arrange
            var barViewModel = new BarViewModel
            {
                BarId = 1,
                Name = "Test Bar",
                Address = "Test Address for Test Bar"
            };

            var bar = new Bar
            {
                BarId = 1,
                Name = barViewModel.Name,
                Address = barViewModel.Address
            };

            var repositoryMock = Substitute.For<IBeerRepository>();
            repositoryMock.UpdateBar(bar).ReturnsForAnyArgs(0);

            var barsController = new BarsController(repositoryMock);

            // Act
            var result = barsController.PutBar(bar.BarId, barViewModel).Result;

            // Assert
            Assert.AreEqual("NotFoundResult", result.GetType().Name); // status 204
        }

        [TestMethod]
        public void Repository_UpdateBarWithInvalidBarId_ReturnBadRequestStatusCode()
        {
            // Arrange
            var barViewModel = new BarViewModel
            {
                BarId = 1,
                Name = "Test Bar",
                Address = "Test Address for Test Bar"
            };

            var bar = new Bar
            {
                BarId = 1,
                Name = barViewModel.Name,
                Address = barViewModel.Address
            };

            var repositoryMock = Substitute.For<IBeerRepository>();

            var barsController = new BarsController(repositoryMock);

            // Act
            var result = barsController.PutBar(999, barViewModel).Result;

            // Assert
            Assert.AreEqual("BadRequestResult", result.GetType().Name); // status 400
        }

       

        [TestMethod]
        public void Repository_GetAllBarsAndAssociatedBeers()
        {
            // Arrange
            var context = PrepareTestContext();
            var repo = new BeerRepository(context);

            // Act
            repo.GetAllBarsAndAssociatedBeers();

            // Assert
        }

        [TestMethod]
        public void Repository_GetBeersByPercentageAlcohol_GreaterThan()
        {
            // Arrange

            var context = PrepareTestContext();
            var repository = new BeerRepository(context);
         
            var beersController = new BeersController(repository);


            // Act
            var result = beersController.GetBeerByAlcoholVolumeRange(4.8m, null).Result;

            // Assert
            Assert.AreEqual(result.Value.FirstOrDefault().Name, "High Percent", "The Beer with high alcohol volume should have been returned.");
        }


        [TestMethod]
        public void Repository_GetBeersByPercentageAlcohol_LessThan()
        {
            // Arrange

            var context = PrepareTestContext();
            var repository = new BeerRepository(context);

            var beersController = new BeersController(repository);


            // Act
            var result = beersController.GetBeerByAlcoholVolumeRange(null, 3.0m).Result;

            // Assert
            Assert.IsTrue(result.Value.Count() == 1);
            Assert.AreEqual(result.Value.FirstOrDefault().Name, "Low Percent", "The Beer with low alcohol volume should have been returned.");
        }

        [TestMethod]
        public void Repository_GetBeersByPercentageAlcohol_WithinRange()
        {
            // Arrange

            var context = PrepareTestContext();
            var repository = new BeerRepository(context);

            var beersController = new BeersController(repository);


            // Act
            var result = beersController.GetBeerByAlcoholVolumeRange(2, 7).Result;

            // Assert
            Assert.IsTrue(result.Value.Count() == 2);
            Assert.AreEqual(result.Value.ToList()[0].Name, "Medium Percent", "The Beer with medium alcohol volume should have been returned.");
            Assert.AreEqual(result.Value.ToList()[1].Name, "High Percent", "The Beer with high alcohol volume should have been returned.");
        }

        private IBeerDbContext PrepareTestContext()
        {
            var options = new DbContextOptionsBuilder<BeerDbContext>().UseInMemoryDatabase(databaseName: "Beer").Options;
            var context = new BeerDbContext(options);

            // clear database content left from previous test(s)
            context.Database.EnsureDeleted();

            var beers = new[]
            {
                new Beer
                {
                    BeerId = 1,
                    Name = "Low Percent",
                    PercentageAlcoholByVolume = 1.5m
                },

                new Beer
                {
                    BeerId = 2,
                    Name = "Medium Percent",
                    PercentageAlcoholByVolume = 3.5m
                },

                new Beer
                {
                    BeerId = 3,
                    Name = "High Percent",
                    PercentageAlcoholByVolume = 6.5m
                }
            };

            context.Beer.AddRange(beers);
            context.SaveChanges();

            return context;
        }
    }
}