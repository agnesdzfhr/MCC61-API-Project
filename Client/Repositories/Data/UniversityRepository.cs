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
    public class UniversityRepository : GeneralRepository<University, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public UniversityRepository(Address address, string request = "Universities/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

    }
}
