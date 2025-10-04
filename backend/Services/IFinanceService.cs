using CatControl.API.DTOs.Finance;

namespace CatControl.API.Services;

public interface IFinanceService
{
    Task<IEnumerable<FinanceDto>> GetUserFinances(int userId);
    Task<IEnumerable<FinanceDto>> GetFinancesByPeriod(int userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<FinanceDto>> GetFinancesByCategory(int userId, string categoria);
    Task<FinanceDto?> GetFinanceById(int financeId, int userId);
    Task<FinanceDto?> CreateFinance(CreateFinanceDto createFinanceDto, int userId);
    Task<FinanceDto?> UpdateFinance(int financeId, UpdateFinanceDto updateFinanceDto, int userId);
    Task<bool> DeleteFinance(int financeId, int userId);
    Task<FinanceSummaryDto> GetFinanceSummary(int userId, int? year = null, int? month = null);
}
