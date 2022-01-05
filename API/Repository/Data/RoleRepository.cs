using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;

namespace MCC61_API_Project.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        private readonly MyContext context;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int SignManager(AccountRole accountRole)
        {
            var findEmployee = context.Employees.Where(e => e.NIK == accountRole.NIK).FirstOrDefault();
            if (findEmployee == null)
            {
                return 2; // Account not found
            }
            else
            {
                var findAR = context.AccountRoles.ToList().Where(ar => ar.NIK == accountRole.NIK);
                foreach (var a in findAR)
                {
                    if(a.RoleId == 2)
                    {
                        return 3; // Employee has been manager
                    }
                }
                AccountRole ar = new AccountRole()
                {
                    NIK = findEmployee.NIK,
                    RoleId = 2
                };
                context.AccountRoles.Add(ar);
                context.SaveChanges();
                return 1; // SignManager Success
            }
        }
    }
}
