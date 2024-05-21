using Apbd9.Context;
using Apbd9.Exceptions;
using Apbd9.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd9.Services;

public class ClientsServiceImpl : IClientsService
{
    private MasterContext _dbContext;

    public ClientsServiceImpl(MasterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> DeleteById(int id) 
    {
        if (await ClientHasTripsAttached(id))
        {
            throw new BadRequestException("Unable to delete client!");
        }
        var affectedRows = await _dbContext.Clients.Where(c => c.IdClient == id).ExecuteDeleteAsync();
        if (affectedRows == 0)
        {
            return false;
        }
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ClientHasTripsAttached(int id)
    {
        var trips = await _dbContext.ClientTrips.Where(ct => ct.IdClient == id).ToListAsync();
        return trips.Count != 0;
    }

}