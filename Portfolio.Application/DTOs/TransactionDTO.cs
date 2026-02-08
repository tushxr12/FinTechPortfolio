namespace Portfolio.Application.DTOs;

public class TransactionDto
{
    public string Symbol {get; set;} = string.Empty;

    public decimal Quantity {get; set;}

    public decimal PurchasePrice {get; set;}

    public decimal CurrentPrice {get; set;}

    public decimal ProfitLoss {get; set;}

    public decimal ProfitLossPercentage {get; set;}
}