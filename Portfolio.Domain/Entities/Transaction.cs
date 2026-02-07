namespace Portfolio.Domain.Entities;

public class Transaction
{
    public int Id {get; set;}

    public string Symbol {get; set;} = string.Empty;

    public decimal Quantity {get; set;}

    public decimal PurchasePrice {get; set;}

    public DateTime TransactionDate {get; set;}

    public decimal TotalCose => Quantity * PurchasePrice;
}