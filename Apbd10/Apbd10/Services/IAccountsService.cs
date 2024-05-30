using Apbd10.ResponseModels;

namespace Apbd10.Services;

public interface IAccountsService
{
    public Task<AccountResponseModel?> GetAccountById(int id);
}