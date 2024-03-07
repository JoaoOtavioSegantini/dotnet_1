using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class StockRepository : IStockRepository
  {

    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext dBContext)
    {
      _context = dBContext;
    }

    public Task<List<Stock>> GetAllAsync()
    {
      return _context.Stocks.ToListAsync();
    }
  }
}
