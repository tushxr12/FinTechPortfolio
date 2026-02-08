namespace Portfolio.Application.DTOs;

public class TransactionCreateDTO
{
    public string Symbol {get; set;} = string.Empty;

    public int Quantity {get; set;}

    public decimal PurchasePrice {get; set;}
}