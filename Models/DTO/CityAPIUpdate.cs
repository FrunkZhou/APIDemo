using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models.DTO
{
    public class CityAPIUpdate
    {
        public string Region
        {
            get; set;
        }

        public string Climate
        {
            get; set;
        }

        public string Country
        {
            get; set;
        }

        public string Currency
        {
            get; set;
        }

        public string GuideURI
        {
            get; set;
        }

        public string ImageURI
        {
            get; set;
        }
    }
}
