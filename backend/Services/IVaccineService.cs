using CatControl.API.DTOs.Vaccine;

namespace CatControl.API.Services;

public interface IVaccineService
{
    Task<IEnumerable<VaccineDto>> GetUserVaccines(int userId);
    Task<IEnumerable<VaccineDto>> GetCatVaccines(int catId, int userId);
    Task<VaccineDto?> GetVaccineById(int vaccineId, int userId);
    Task<VaccineDto?> CreateVaccine(CreateVaccineDto createVaccineDto, int userId);
    Task<VaccineDto?> UpdateVaccine(int vaccineId, UpdateVaccineDto updateVaccineDto, int userId);
    Task<bool> DeleteVaccine(int vaccineId, int userId);
    Task<IEnumerable<VaccineDto>> GetUpcomingVaccines(int userId, int days = 30);
}
