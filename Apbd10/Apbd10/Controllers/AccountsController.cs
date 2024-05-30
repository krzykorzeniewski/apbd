using Apbd10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apbd10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private IAccountsService _accountsService;

    public AccountsController(IAccountsService accountsService)
    {
        _accountsService = accountsService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var res = await _accountsService.GetAccountById(id);
        if (res == null)
        {
            return NotFound();
        }

        return Ok(res);
    }
}