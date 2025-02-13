namespace TrackMyMoney;

public class Menu
{
    private readonly List<Action> _actions =
    [
        Action.Add,
        Action.ShowAll,
        Action.ShowByTicker,
        Action.Update, 
        Action.Delete,
        Action.Explain,
        Action.Exit
    ];

    public void PrintWelcome()
    {
        Console.WriteLine(
            "====================\nTRACK MY MONEY - Your personal investment tracker\n====================");
    }

    public void PrintMenu()
    {
        Console.WriteLine("Choose what to do:");
        for (int i = 0; i < _actions.Count; i++)
        {
            if (_actions[i].Icon != null)
            {
                Console.WriteLine(_actions[i].Icon + ". " + _actions[i].Value);
            }
            else
            {
                Console.WriteLine(i + ". " + _actions[i].Value);
            }
        }

        Console.Write("==> ");
    }

    public Action GetUserInput()
    {
        string? rawInput = Console.ReadLine();

        if (rawInput != null)
        {
            if (rawInput == "X")
            {
                return Action.Exit;
            }

            if (rawInput == "?")
            {
                return Action.Explain;
            }

            try
            {
                int input = Int32.Abs(Int32.Parse(rawInput));
                if (input < _actions.Count - 1)
                {
                    return _actions[input];
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        Console.Write("Enter a valid option ==> ");
        return GetUserInput();
    }

    public void PrintInvestments(List<Investment> investments, bool withNumbers)
    {
        if (withNumbers)
        {
            Console.Write("{0, -4}", "IDX");
        }

        Console.WriteLine("{0, -7} {1, 15} {2, 8} {3, 15} {4, 10} {5, 15} {6, 5} {7, 20}", "TICKER", "PRICE PER SHARE", "SHARES", "TOTAL",
            "DATE", "LAST PRICE PER SHARE", "CHANGE", "LAST UPDATE");
        for (int i = 0; i < investments.Count; i++)
        {
            if (withNumbers)
            {
                Console.Write("{0, -4} ", i + 1);
            }

            Console.WriteLine("{0, -7} {1, 15} {2, 8} {3, 15} {4, 10} {5, 15} {6, 5:#.##} {7, 20}", investments[i].Code,
                investments[i].PurchasePricePerShare,
                investments[i].Shares, investments[i].TotalAmount, investments[i].Date,
                investments[i].LastPricePerShare, (((investments[i].LastPricePerShare/investments[i].PurchasePricePerShare)-1)*100),
                $"{investments[i].UpdatedSince} minutes ago");
        }
    }

    public void PrintSingleInvestment(Investment investment)
    {
        Console.WriteLine("{0, -7} {1, 15} {2, 8} {3, 15} {4, 10} {5, 15} {6, 5} {7, 20}", "TICKER", "PRICE PER SHARE", "SHARES", "TOTAL",
            "DATE", "LAST PRICE PER SHARE", "CHANGE", "LAST UPDATE");
        Console.WriteLine("{0, -10} {1, 15} {2, 8} {3, 15} {4, 10} {5, 15} {6, 5:#.##} {7, 20}\n", investment.Code, investment.PurchasePricePerShare,
            investment.Shares, investment.TotalAmount, investment.Date, investment.LastPricePerShare, ((investment.LastPricePerShare/investment.PurchasePricePerShare)-1)*100,
            $"{investment.UpdatedSince} minutes ago");
    }
}