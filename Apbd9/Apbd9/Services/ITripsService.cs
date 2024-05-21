using Apbd9.Models;
using Apbd9.ResponseModels;

namespace Apbd9.Services;

public interface ITripsService
{
    public Task<ICollection<GetTripsModel>> GetAllTrips();
}