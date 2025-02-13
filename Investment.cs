namespace TrackMyMoney;

using System;

public class Investment
{
    public required string Code { get; init; }
    public decimal TotalAmount => PurchasePricePerShare * Shares;
    public required DateOnly Date { get; init; }
    public required int Shares { get; init; }
    public required decimal PurchasePricePerShare { get; init; }
    
    public required decimal LastPricePerShare { get; set; }
    public required DateTime LastUpdate { get; set; }
    public int UpdatedSince => (DateTime.Now - LastUpdate).Minutes;

    public void UpdateTime()
    {
        LastUpdate = DateTime.Now;
    }
    
    
}