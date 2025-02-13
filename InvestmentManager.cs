namespace TrackMyMoney;

using System;

public class InvestmentManager
{
    public List<Investment> Investments { get; }
    private InvestmentFileManager FileManager { get; }

    public InvestmentManager(InvestmentFileManager manager)
    {
        FileManager = manager;
        Investments = FileManager.LoadInvestmentsFromFile();
    }

    public void AddInvestment(Investment investment)
    {
        Investments.Add(investment);
    }

    public void DeleteInvestment(Investment investment)
    {
        if (Investments.Remove(investment))
        {
            Save();
        }
    }

    public void UpdateInvestmentValues()
    {
        
    }

    public void Save()
    {
        FileManager.WriteInvestmentsToFile(Investments);
    }
}