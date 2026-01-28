using Mitra_Journal.Entities;

namespace Mitra_Journal.Services.Interface;

public interface IPinService
{
    bool AddPin(int pin);
    bool ValidatePin(int pin);
    bool HasPin();
}