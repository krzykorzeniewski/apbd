﻿namespace Apbd9.ResponseModels;

public class GetTripsModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public ICollection<CountryDetails> Countries { get; set; }
    public ICollection<ClientDetails> Clients { get; set; }
}

public class CountryDetails
{
    public string Name { get; set; }
}

public class ClientDetails
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}