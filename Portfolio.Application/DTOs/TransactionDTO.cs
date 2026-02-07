namespace Portfolio.Application.DTOs;

public class TransactionDto
{
    public string Symbol {get; set;} = string.Empty;

    public decimal Quantity {get; set;}

    public decimal PurchasePrice {get; set;}
}