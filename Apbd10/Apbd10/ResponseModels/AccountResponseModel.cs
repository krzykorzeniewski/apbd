﻿namespace Apbd10.ResponseModels;

public class AccountResponseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public ICollection<ProductResponseModel> Cart { get; set; }
}
