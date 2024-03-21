using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
  public interface IStockRepository
  {
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetBySymbolAsync(string symbol);   
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StocksExists(int id);

  }

}
