using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.APIGuv.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPortfolio()
    {
        var portfolio = await _service.GetPortfolioAsync();
        return Ok(portfolio);
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction([FromBody] TransactionDto dto)
    {
        await _service.CreateTransactionAsync(dto);
        return Ok(new {message = "Transaction added succesfully!"});
    }
}