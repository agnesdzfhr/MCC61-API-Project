using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.ViewModels;
using static MCC61_API_Project.Repository.Data.EmployeeRepository;

namespace MCC61_API_Project.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            Employee findEmail = NewMethod(loginVM);

            if (findEmail != null)
            {
                var findNIK = context.Accounts.FirstOrDefault(a => a.NIK == findEmail.NIK);
                bool verifiedPass = Hashing.ValidatePassword(loginVM.Password, findNIK.Password);
                if (verifiedPass == true)
                {
                    return 1; //Login Success
                }
                else
                {
                    return 2; //Wrong Password
                }
            }
            else
            {
                return 3; //Email Not Found
            }
        }

        private Employee NewMethod(LoginVM loginVM)
        {
            return context.Employees.FirstOrDefault(e => e.Email == loginVM.Email);
        }

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var findEmail = context.Employees
        }
    }
}
