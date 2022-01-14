using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            var findEmail = context.Employees.FirstOrDefault(e => e.Email == loginVM.Email);

            if (findEmail != null)
            {
                var findNIK = context.Accounts.FirstOrDefault(a => a.NIK == findEmail.NIK);
                bool verifiedPass = Hashing.ValidatePassword(loginVM.Password, findNIK.Password);
                if(verifiedPass == true)
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

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var checkEmail = context.Employees.AsNoTracking().FirstOrDefault(e => e.Email == forgotPasswordVM.Email);
            if (checkEmail == null)
            {
                return 2; //Email Not Found
            }
            else
            {
                var findAccount = context.Accounts.Find(checkEmail.NIK);
                context.Entry(findAccount).State = EntityState.Detached;
                string name = $"{checkEmail.FirstName} {checkEmail.LastName}";
                int otp = GenerateOTP();
                DateTime expiredTime = DateTime.Now.AddMinutes(5);
                try
                {
                    SendEmail(forgotPasswordVM, name, otp, expiredTime);
                    Account account = new Account()
                    {
                        NIK = findAccount.NIK,
                        Password = findAccount.Password,
                        OTP = otp,
                        ExpiredDate = expiredTime,
                        isUsed = false
                    };
                    context.Entry(account).State = EntityState.Modified;
                    context.SaveChanges();
                    return 1;//email sent
                }catch(SmtpException ex)
                {
                    throw new SmtpException(ex.Message);
                }

            }
        }



        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var checkEmail = context.Employees.AsNoTracking().FirstOrDefault(e => e.Email == changePasswordVM.Email);
            if (checkEmail == null)
            {
                return 2; //Email Not Found
            }
            else
            {
                var findAccount = context.Accounts.FirstOrDefault(a => a.NIK == checkEmail.NIK);
                context.Entry(findAccount).State = EntityState.Detached;
                DateTime nowTime = DateTime.Now;
                if (findAccount.OTP != changePasswordVM.OTP)
                {
                    return 4; //Wrong OTP
                }
                else
                { 
                    if(findAccount.isUsed == true)
                    {
                        return 3; //OTP Has Used
                    }
                    else
                    {
                        try
                        {
                            if (findAccount.ExpiredDate <= nowTime)
                            {
                                return 0; //OTP Expired
                            }
                            else
                            {
                                Account account = new Account()
                                {
                                    NIK = findAccount.NIK,
                                    Password = Hashing.HashPassword(changePasswordVM.NewPassword),
                                    OTP = changePasswordVM.OTP,
                                    ExpiredDate = null,
                                    isUsed = true
                                };
                                context.Entry(account).State = EntityState.Modified;
                                context.SaveChanges();
                                return 1; //Password Changed
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                    }

                }

            }
        }

        public static void SendEmail(ForgotPasswordVM forgotPasswordVM,string name, int otp, DateTime expiredTime)
        {
            string to = forgotPasswordVM.Email;
            string from = "mccreg61net@gmail.com";
            MailMessage message = new MailMessage(from, to);

            message.Subject = $"Hi, {name}! Here's Your OTP Number";
            message.Body = $"<h2>You have requested password change. If it's not you, ignore this email!</h2>" +
                $"<p>Use this OTP for change your password: {otp}</p> \n" +
                $"<p>Expired: {expiredTime} \n</p>";
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, "61mccregnet"),
                EnableSsl = true
            };
            try
            {
                client.Send(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int GenerateOTP()
        {
            try
            {
                int min = 10000;
                int max = 99999;
                int otp = 0;

                Random random = new Random();
                otp = random.Next(min, max);
                return otp;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<object> GetRoles(string email)
        {
            var findEmail = context.Employees.Where(e => e.Email == email).FirstOrDefault();
            var AccountRole = context.AccountRoles.Where(ar => ar.NIK == findEmail.NIK).Select(ar => ar.Role.RoleName).ToList();
            return AccountRole;

        }

        public Array ViewRole(string email)
        {
            var findEmail = context.Employees.Where(e => e.Email == email).FirstOrDefault();
            var AccountRole = context.AccountRoles.Where(ar => ar.NIK == findEmail.NIK).Select(ar => ar.Role.RoleName).ToArray();
            return AccountRole;
        }

        //public string ViewRoles(string email)
        //{
        //    var findRole = ViewRole(email);
        //    var r = "";
        //    for (int i = 0; i < findRole.Count; i++)
        //    {
        //        r += findRole[i];
        //    }
        //    Console.WriteLine(r);

        //    return r;
        //}

    }
}
