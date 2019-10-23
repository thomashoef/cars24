using System;
using cars24.Enumerations;

namespace cars24.Models
{
    public class CarAdvert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FuelType Fuel { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public int Mileage { get; set; }
        public DateTime FirstRegistration { get; set; }
    }
}
