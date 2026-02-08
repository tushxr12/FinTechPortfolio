using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;

namespace Portfolio.APIGuv.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataService _marketDataService;

    public MarketDataController(IMarketDataService marketDataService)
    {
        _marketDataService = marketDataService;
    }

    [HttpGet("price/{symbol}")]
    public async Task<IActionResult> GetPrice(string symbol)
    {
        var price = await _marketDataService.GetLivePriceAsync(symbol);

        if(price == 0)
            return NotFound(new {message = "Symbol not found or API limit reached"});
        
        return Ok(new {Symbol = symbol.ToUpper(), CurrentPrice = price});
    }
}