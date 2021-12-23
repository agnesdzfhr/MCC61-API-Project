using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;

namespace MCC61_API_Project.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {
           
        }
    }
}
