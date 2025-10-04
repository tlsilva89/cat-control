using CatControl.API.DTOs.Cat;

namespace CatControl.API.Services;

public interface ICatService
{
    Task<IEnumerable<CatDto>> GetUserCats(int userId);
    Task<CatDto?> GetCatById(int catId, int userId);
    Task<CatDto?> CreateCat(CreateCatDto createCatDto, int userId);
    Task<CatDto?> UpdateCat(int catId, UpdateCatDto updateCatDto, int userId);
    Task<bool> DeleteCat(int catId, int userId);
}
