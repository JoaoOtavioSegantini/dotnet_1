using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;

namespace api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
      return Ok(stocks);

    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
      var stock = _context.Stocks.Find(id);

      if (stock == null)
      {
        return NotFound();

      }

      return Ok(stock.ToStockDto());

    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
      if (stockDto is null)
      {
        throw new ArgumentNullException(nameof(stockDto));
      }

      var stockModel = stockDto.ToStockFromCreateDTO();
      _context.Stocks.Add(stockModel);
      _context.SaveChanges();

      return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {

      var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

      if (stockModel == null)
      {
        return NotFound();

      }

      stockModel.Symbol = stockDto.Symbol;
      stockModel.LastDiv = stockDto.LastDiv;
      stockModel.Industry = stockDto.Industry;
      stockModel.CompanyName = stockDto.CompanyName;
      stockModel.Purchase = stockDto.Purchase;
      stockModel.MarketCap = stockDto.MarketCap;

      _context.SaveChanges();

      return Ok(stockModel.ToStockDto());

    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
      var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

      if (stockModel is null)
      {
        return NotFound();
      }

      _context.Stocks.Remove(stockModel);
      _context.SaveChanges();

      return NoContent();
    }
  }
}
