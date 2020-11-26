using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models
{
    [DynamoDBTable("City")]
    public class City
    {
        [DynamoDBIgnore]
        private string id = string.Empty;

        [DynamoDBHashKey]
        [Required]
        public string ID
        {
            get {
                if (id == string.Empty)
                {
                    id = Guid.NewGuid().ToString();
                }
                return id;
            }
            set { id = value; }
        }

        [DynamoDBProperty]
        [Required]
        public string Name
        {
            get; set;
        }

        [DynamoDBProperty]
        public string Climate
        {
            get; set;
        }

        [DynamoDBProperty]
        [Required]
        public string Region
        {
            get; set;
        }

        [DynamoDBProperty]
        [Required]
        public string Country
        {
            get; set;
        }

        [DynamoDBProperty]
        public string Currency
        {
            get; set;
        }

        [DynamoDBProperty]
        public string GuideURI
        {
            get; set;
        }

        [DynamoDBProperty]
        public string ImageURI
        {
            get; set;
        }
    }
}
