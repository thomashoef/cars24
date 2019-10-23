using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cars24.Controllers;
using cars24.Enumerations;
using cars24.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace cars24tests.Controllers
{
    public class CarAdvertsControllerFixture
    {
        private CarAdvertContext GetUnitTestsDbContext(string dbname)
        {
            var options = new DbContextOptionsBuilder<CarAdvertContext>().UseInMemoryDatabase(dbname).Options;
            var context = new CarAdvertContext(options);
            context.CarAdverts.Add(new CarAdvert() { Id = 1, Fuel = FuelType.Diesel, IsNew = false, Mileage = 10000, FirstRegistration = DateTime.Now.Date, Price = 10000, Title = "First test car" });
            context.CarAdverts.Add(new CarAdvert() { Id = 2, Fuel = FuelType.Gasoline, IsNew = false, Mileage = 20000, FirstRegistration = DateTime.Now.Date, Price = 8000, Title = "Second test car" });
            context.CarAdverts.Add(new CarAdvert() { Id = 3, Fuel = FuelType.Gasoline, IsNew = true, Mileage = 0, Price = 6000, Title = "Third test car" });
            context.CarAdverts.Add(new CarAdvert() { Id = 4, Fuel = FuelType.Diesel, IsNew = true, Mileage = 0, Price = 36000, Title = "Fourth test car" });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Get_Returns_All_CardAdverts()
        {
            // Arrange
            var controller = new CarAdvertsController(GetUnitTestsDbContext("1"));

            // Act
            var result = await controller.GetCarAdverts();

            // Assert
            var adverts = Assert.IsType<List<CarAdvert>>(result.Value);
            Assert.Equal(4, adverts.Count);
        }

        [Fact]
        public async Task Get_With_Sorting_By_Price_Returns_All_CardAdverts_In_Correct_Order()
        {
            // Arrange
            var controller = new CarAdvertsController(GetUnitTestsDbContext("2"));

            // Act
            var result = await controller.GetCarAdverts("price");

            // Assert
            var adverts = Assert.IsType<List<CarAdvert>>(result.Value);
            Assert.Equal(adverts[0].Price, 6000);
            Assert.Equal(adverts[1].Price, 8000);
            Assert.Equal(adverts[2].Price, 10000);
            Assert.Equal(adverts[3].Price, 36000);
        }

        [Fact]
        public async Task Get_With_Sorting_By_Mileage_Returns_All_CardAdverts_In_Correct_Order()
        {
            // Arrange
            var controller = new CarAdvertsController(GetUnitTestsDbContext("3"));

            // Act
            var result = await controller.GetCarAdverts("mileage");

            // Assert
            var adverts = Assert.IsType<List<CarAdvert>>(result.Value);
            Assert.Equal(adverts[2].Mileage, 10000);
            Assert.Equal(adverts[3].Mileage, 20000);
        }

        [Fact]
        public async Task Delete_CardAdvert_Reduces_Record_Size()
        {
            // Arrange
            var controller = new CarAdvertsController(GetUnitTestsDbContext("4"));

            // Act
            var deleteResult = await controller.DeleteCarAdvert(1);

            var result = await controller.GetCarAdverts();

            // Assert
            var adverts = Assert.IsType<List<CarAdvert>>(result.Value);
            Assert.Equal(3, adverts.Count);
        }

        [Fact]
        public async Task Update_CardAdvert_Changes_Entity()
        {
            // Arrange
            var controller = new CarAdvertsController(GetUnitTestsDbContext("5"));

            // Act
            var getResult = await controller.GetCarAdvert(1);

            // Assert
            var advert = Assert.IsType<CarAdvert>(getResult.Value);
            Assert.Equal("First test car", advert.Title);

            // Act and update
            advert.Title = "Updated test car";
            await controller.PutCarAdvert(1, advert);

            // Act
            getResult = await controller.GetCarAdvert(1);

            // Assert
            advert = Assert.IsType<CarAdvert>(getResult.Value);
            Assert.Equal("Updated test car", advert.Title);
        }
    }
}
