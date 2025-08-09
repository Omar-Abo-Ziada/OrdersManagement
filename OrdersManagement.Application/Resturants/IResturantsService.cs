using MyResturants.Application.Resturants.Dtos;

namespace MyResturants.Application.Resturants;

public interface IResturantsService
{
    Task<int> CreateAsync(CreateResturantDto createResturantDto);
    Task<IEnumerable<ResturantDto>> GetAllAsync();
    Task<ResturantDto?> GetByIdAsync(int id);
}