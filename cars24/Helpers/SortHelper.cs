using System;
using System.Linq;
using System.Linq.Expressions;
using cars24.Models;

namespace cars24.Helpers
{
    public static class SortHelper
    {
        public static Expression<Func<CarAdvert, object>> CarAdvert(string orderByProperty)
        {
            switch (orderByProperty)
            {
                case "id":
                    return x => x.Id;
                case "title":
                    return x => x.Title;
                case "fuel":
                    return x => x.Fuel;
                case "price":
                    return x => x.Price;
                case "isNew":
                    return x => x.IsNew;
                case "mileage":
                    return x => x.Mileage;
                case "firstRegistration":
                    return x => x.FirstRegistration;
                default:
                    return x => x.Id;
            }
        }
    }
}
