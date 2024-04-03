using System;

namespace LegacyApp
{
    public interface IRepository
    {
        public Client GetById(int clientId);
    }
    public class UserService
    {
        
        //Wstrzykiwanie zaleznosci
        private IRepository _repository;

        //trzeba tutaj przypisac "na sztywno" ClientRepository, gdyz nie mozna zmieniac LegacyAppConsumer i zastosowac
        //wstrzykiwania poprzez konstruktor, docelowo powinnismy jako argument konstruktora podac dowolna implementacje
        //IRepository
        public UserService()
        {
            _repository = new ClientRepository();
        }
        
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

            var client = GetClientFromDb(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            CalculateClientCreditLimit(client, user);
            
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private void CalculateClientCreditLimit(Client client, User user)
        {
            switch (client.Type)
            {
                case "VeryImportantClient":
                    SetVeryImportantClientCreditLimit(user);
                    break;
                case "ImportantClient":
                    SetImportantClientCreditLimit(user);
                    break;
                default:
                    SetNotImportantClientCreditLimit(user);
                    break;
            }
        }

        private void SetVeryImportantClientCreditLimit(User user)
        {
            user.HasCreditLimit = false;
        }

        private void SetNotImportantClientCreditLimit(User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

        private void SetImportantClientCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }

        private Client GetClientFromDb(int clientId)
        {
            var client = _repository.GetById(clientId);
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
