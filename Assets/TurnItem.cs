public class TurnItem : IButton
{
    public string Name {get;}
    public string Desc {get;}
    public void OnClick(){}
    public TurnItem(string name, string desc)
    {
        this.Name = name;
        this.Desc = desc;
    }
}