using Mitra_Journal.Data;
using Mitra_Journal.Entities;
using Mitra_Journal.Services.Interface;

namespace Mitra_Journal.Services;

public class PinService : IPinService
{
    private readonly LocalDbContext _dbContext;
    public PinService(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public bool AddPin(int pin)
    {
        var addPin = new Pin()
        {
            Id = 1,
            pinNumber = pin
        };
        _dbContext.Add(addPin);
        return _dbContext.SaveChanges() > 0;
    }

    public bool ValidatePin(int pin)
    {
       var existingPin = _dbContext.Pins.SingleOrDefault(w=>w.pinNumber == pin);
       return existingPin != null;
    }

    public bool HasPin()
    {
        return _dbContext.Pins.Any();
    }
}