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
            var findEmail = context.Employees.AsNoTracking().FirstOrDefault(e => e.Email == forgotPasswordVM.Email);
            if (findEmail == null)
            {
                return 2; //Email Not Found
            }
            else
            {
                var findAccount = context.Accounts.Find(findEmail.NIK);
                context.Entry(findAccount).State = EntityState.Detached;
                int otp = GenerateOTP();
                string to = forgotPasswordVM.Email;
                string from = "mccreg61net@gmail.com";
                MailMessage message = new MailMessage(from, to);

                message.Subject = "Your OTP to Change Password";
                message.Body = $"!Use this otp to change password! \n \n Your OTP: {otp} \n \n Valid for 5 Minutes since you received this email";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(from, "61mccregnet"),
                    EnableSsl = true
                };

                try
                {
                    client.Send(message);
                    Account account = new Account()
                    {
                        NIK = findAccount.NIK,
                        Password = findAccount.Password,
                        OTP = otp,
                        ExpiredDate = DateTime.Now.AddMinutes(5),
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
    }
}
