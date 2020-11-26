using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models.Database
{
    public class CityRepository : ICityRepository
    {
        private IDynamoDBContext dynamoContext;

        public CityRepository(IDynamoDBContext dynamoContext)
        {
            this.dynamoContext = dynamoContext;

        }
        public async Task<bool> CreateAsync(City city)
        {
            try
            {
                await dynamoContext.SaveAsync(city);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(City city)
        {
            try
            {
                await dynamoContext.DeleteAsync(city);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<City>> GetAllAsync()
        {
            try
            {
                var scanConditions = new List<ScanCondition>()
                {
                    new ScanCondition("ID", ScanOperator.IsNotNull, "")
                };
                var scanSearch = dynamoContext.ScanAsync<City>(scanConditions);
                var result = await scanSearch.GetRemainingAsync();
                return result.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<City> GetByIdAsync(string id)
        {
            try
            {
                var scanConditions = new List<ScanCondition>()
                {
                    new ScanCondition("ID", ScanOperator.Equal, id)
                };
                var scanSearch = dynamoContext.ScanAsync<City>(scanConditions);
                var result = await scanSearch.GetRemainingAsync();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(City city)
        {
            try
            {
                var scanConditions = new List<ScanCondition>()
                {
                    new ScanCondition("ID", ScanOperator.Equal, city.ID)
                };
                var scanSearch = dynamoContext.ScanAsync<City>(scanConditions);
                var result = (await scanSearch.GetRemainingAsync()).FirstOrDefault();
                if (result != null)
                {
                    await dynamoContext.SaveAsync(city);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
