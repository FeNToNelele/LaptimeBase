using Refit;
using Shared.Car;

namespace WebUI.Services;

public interface ICarService
{
    [Get("/api/cars")]
    Task<List<CarDto>> GetCarsAsync();
    
    [Post("/api/cars")]
    Task<CarDto> PostCarAsync([Body] NewCarRequest request);
}