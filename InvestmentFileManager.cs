namespace TrackMyMoney;

public class InvestmentFileManager
{
    public required string Path { get; init; }

    public List<Investment> LoadInvestmentsFromFile()
    {
        try
        {
            StreamReader sr = new StreamReader(Path);
            List<Investment> investments = new List<Investment>();
            string? line = sr.ReadLine();

            while (line != null)
            {
                Investment? investment = StringToInvestment(line);
                if (investment != null)
                {
                    investments.Add(investment);
                }

                line = sr.ReadLine();
            }

            sr.Close();

            return investments;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("Investment file not found. Creating...");
            File.Create(Path);
            return [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void WriteInvestmentsToFile(List<Investment> investments)
    {
        try
        {
            StreamWriter writer = new StreamWriter(Path);
            foreach (Investment investment in investments)
            {
                writer.WriteLine(InvestmentToString(investment));
            }

            writer.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Investment? StringToInvestment(string line)
    {
        string[] parts = line.Split(' ');

        return new Investment
        {
            Code = parts[0],
            PurchasePricePerShare = Decimal.Parse(parts[1]),
            Shares = Int32.Parse(parts[2]),
            Date = DateOnly.Parse(parts[3]),
            LastPricePerShare = Decimal.Parse(parts[4]),
            
            LastUpdate = DateTime.FromBinary(long.Parse(parts[5])),
        };
    }

    private string? InvestmentToString(Investment investment)
    {
        return investment.Code + " " + investment.PurchasePricePerShare + " " + investment.Shares + " " +
               investment.Date + " " + investment.LastPricePerShare + " " + investment.LastUpdate.ToBinary();
    }
}