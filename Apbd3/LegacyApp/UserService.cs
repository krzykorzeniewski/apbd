using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (CheckFirstName(firstName) || CheckLastName(lastName))
            {
                return false;
            }

            if (ValidateEmail(email))
            {
                return false;
            }

            int age = CalculateAge(dateOfBirth);
            if (age < 21)
            {
                return false;
            }

            var client = GetClientFromRepo(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static Client GetClientFromRepo(int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            return client;
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int res = now.Year - dateOfBirth.Year;
            
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                res--;
            
            return res;
        }

        private static bool ValidateEmail(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private static bool CheckLastName(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        private static bool CheckFirstName(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}
