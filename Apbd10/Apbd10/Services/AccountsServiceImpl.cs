using Apbd10.Contexts;
using Apbd10.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Apbd10.Services;

public class AccountsServiceImpl(MyDatabaseContext context) : IAccountsService
{
    
    public async Task<AccountResponseModel?> GetAccountById(int id)
    {
        var res = from a in context.Accounts
            join r in context.Roles on a.IdRole equals r.IdRole
            join c in context.ProductAccounts on a.IdAccount equals c.IdAccount
            join p in context.Products on c.IdProduct equals p.IdProduct
            where a.IdAccount.Equals(id)
            select new AccountResponseModel()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Phone = a.Phone,
                Role = r.Name,
                Cart = new List<ProductResponseModel>
                {
                    new ProductResponseModel
                    {
                        ProductId = p.IdProduct,
                        Amount = c.Amount,
                        ProductName = p.Name
                    }
                }
            };
        return await res.SingleOrDefaultAsync();
    }
}
