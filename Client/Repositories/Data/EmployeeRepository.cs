using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Base;
using MCC61_API_Project.Models;
using MCC61_API_Project.ViewModels;
using Newtonsoft.Json;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        //public async Task<List<Object>> GetRegisterData()
        //{
        //    List<Object> entities = new List<Object>();

        //    using (var response = await httpClient.GetAsync(request + "GetRegisterData/"))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<List<Object>>(apiResponse);
        //    }
        //    return entities;
        //}

        public async Task<Object> GetRegisterData()
        {
            Object entities = new Object();

            using (var response = await httpClient.GetAsync(request + "GetRegisterData/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entities;
        }

        //public async Task<RegisterVM> GetRegisterByNIK(string NIK)
        //{
        //    RegisterVM entities = new RegisterVM();

        //    using (var response = await httpClient.GetAsync(request + "GetRegisterData/" + NIK))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
        //    }
        //    return entities;
        //}

        public async Task<RegisterVM> GetRegisterByNIK(string NIK)
        {
            RegisterVM entity = null;

            using (var response = await httpClient.GetAsync(request + $"GetRegisterData/{NIK}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
            }
            return entity;
        }

        //public HttpStatusCode Register(RegisterVM registerVM)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
        //    var result = httpClient.PostAsync(address.link + request + "Register/", content).Result;
        //    return result.StatusCode;
        //}

        public Object Register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            Object entities = new Object();

            using (var response = httpClient.PostAsync(request + "Register/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        //public RegisterVM Register(RegisterVM registerVM)
        //{
        //    RegisterVM entity = null;
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
        //    using (var result = httpClient.PostAsync(address.link + request + "Register/", content).Result)
        //    {
        //        //string apiResponse = await response.Content.ReadAsStringAsync();
        //        string apiResponse = result.Content.ReadAsStringAsync().ToString();
        //        entity = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
        //    }
        //    return entity;
        //}

        public HttpStatusCode UpdateRegister(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "UpdateRegister/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode DeleteRegisterData(string NIK)
        {
            var result = httpClient.DeleteAsync(request + "DeleteRegisterData/" + NIK).Result;
            return result.StatusCode;
        }

    }
}
