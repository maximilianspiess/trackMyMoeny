namespace TrackMyMoney;

public enum ActionType
{
    Add,
    Delete,
    ShowAll,
    ShowByTicker,
    Explain,
    Update,
    Exit
}

public class Action
{
    public ActionType ActionType { get; }
    public string Value { get; }
    public string? Icon { get; }

    private Action(ActionType actionType, string value, string? icon = null)
    {
        ActionType = actionType;
        Value = value;
        Icon = icon;
    }

    public static readonly Action Add = new(ActionType.Add, "Add investment");
    public static readonly Action Delete = new(ActionType.Delete, "Delete investment");
    public static readonly Action ShowAll = new(ActionType.ShowAll, "Show all investments");
    public static readonly Action ShowByTicker = new(ActionType.ShowByTicker, "Show investment by ticker");
    public static readonly Action Explain = new(ActionType.Explain, "What can I do?", "?");
    public static readonly Action Update = new(ActionType.Update, "Update investment values");
    public static readonly Action Exit = new(ActionType.Exit, "Exit", "X");
    
}