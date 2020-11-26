using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models.DTO
{
    public class CityAPICreate
    {
        [Required]
        public string Region
        {
            get; set;
        }

        [Required]
        public string Name
        {
            get; set;
        }

        public string Climate
        {
            get; set;
        }

        [Required]
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
