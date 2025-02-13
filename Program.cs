namespace TrackMyMoney;

class Program
{
    private static async Task Main(string[] args)
    {
        Menu menu = new Menu();
        InvestmentFileManager fileManager = new InvestmentFileManager
        {
            Path = "./investments.txt"
        };
        InvestmentManager manager = new InvestmentManager(fileManager);
        StockApi api = new StockApi();

        menu.PrintWelcome();

        while (true)
        {
            menu.PrintMenu();
            Action action = menu.GetUserInput();

            Console.Clear();

            switch (action.ActionType)
            {
                case ActionType.Add:
                {
                    Console.Write("Enter the ticker code you'd like to add (example AAPL, GOOG, INTC) ==> ");
                    string? code = Console.ReadLine()?.ToUpper();
                    Console.Write("Enter the number of shares you bought (at least 1) ==> ");
                    int numShares = int.Abs(int.Parse(Console.ReadLine() ?? "0"));
                    if (code != null && numShares != 0)
                    {
                        Investment? newInvestment = await api.AddInvestment(code, numShares);
                        if (newInvestment != null)
                        {
                            manager.AddInvestment(newInvestment);
                            manager.Save();
                            Console.WriteLine("\n--------- NEW INVESTMENT ADDED ---------");
                            menu.PrintSingleInvestment(newInvestment);
                        }
                        else
                        {
                            Console.WriteLine("\n--------- FAILED TO ADD INVESTMENT ---------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("=!=!=!=! INVALID INPUT !=!=!=!=");
                    }

                    break;
                }
                case ActionType.Delete:
                {
                    Console.WriteLine("************************** DELETE **************************\n");
                    menu.PrintInvestments(manager.Investments, true);
                    Console.WriteLine("************************************************************\n");

                    Console.Write("Choose the number (IDX) of the investment to delete ==> ");
                    int deleteIdx = int.Abs(int.Parse(Console.ReadLine() ?? "0"));

                    if (deleteIdx > 0 && deleteIdx <= manager.Investments.Count)
                    {
                        manager.DeleteInvestment(manager.Investments[deleteIdx - 1]);
                        Console.WriteLine("\n--------- INVESTMENT DELETED ---------\n");
                    }

                    break;
                }
                case ActionType.ShowAll:
                {
                    Console.WriteLine(
                        "*************************************** INVESTMENTS ***************************************\n");
                    menu.PrintInvestments(manager.Investments, false);
                    Console.WriteLine(
                        "*******************************************************************************************\n");
                    break;
                }
                case ActionType.ShowByTicker:
                {
                    Console.Write("Enter the ticker (symbol) you want to find (example AAPL, GOOG, INTC) ==> ");
                    string? code = Console.ReadLine()?.ToUpper();

                    if (code != null)
                    {
                        List<Investment> investments = manager.Investments.FindAll(i => i.Code.Equals(code));
                        if (investments.Count > 0)
                        {
                            Console.WriteLine(
                                "*************************************** INVESTMENTS ***************************************\n");
                            menu.PrintInvestments(investments, false);
                            Console.WriteLine(
                                "*******************************************************************************************\n");
                        }
                        else
                        {
                            Console.WriteLine($"\n--------- NO INVESTMENTS FOUND FOR '{code}' ---------\n");
                        }
                    }

                    break;
                }
                case ActionType.Update:
                {
                    foreach (Investment investment in manager.Investments)
                    {
                        if (investment.UpdatedSince >= 10)
                        {
                            await api.UpdateSingleInvestment(investment);
                            Console.WriteLine($"{investment.Code} UPDATED!");
                        }
                    }
                    
                    manager.Save();
                    Console.WriteLine($"\n--------- INVESTMENTS UPDATED ---------\n");
                    break;
                }
                case ActionType.Explain:
                {
                    Console.WriteLine("\n----------------------------------- ? -----------------------------------\n" +
                                      "TrackMyMoney is a console application that lets you record your investments.\n" +
                                      "When you add a new investment, the current price of that stock is stored, together with the number of shares.\n\n" +
                                      "You can show all your investments, filter them by ticker (symbol) or delete them.\n\n" +
                                      "Think of this program as a simulation of investing, but without the risks (or limited budget)!\n" +
                                      "----------------------------------- ? -----------------------------------\n");
                    break;
                }
                case ActionType.Exit:
                {
                    Environment.Exit(0);
                    break;
                }
            }
        }
    }
}