using Apbd9.Context;
using Apbd9.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Apbd9.Services;

public class TripsServiceImpl : ITripsService
{
    private MasterContext _dbContext;

    public TripsServiceImpl(MasterContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ICollection<GetTripsModel>> GetAllTrips()
    {
        return await _dbContext.Trips.Select(t => new GetTripsModel
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.IdCountries.Select(c => new CountryDetails
            {
                Name = c.Name
            }).ToList(),
            Clients = t.ClientTrips.Select(c => new ClientDetails
            {
                FirstName = c.IdClientNavigation.FirstName,
                LastName = c.IdClientNavigation.LastName
            }).ToList()
        }).ToListAsync();
    }
}