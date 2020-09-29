using System;
public abstract class FieldMenuItem : IButton
{
    protected Field field;
    public string Name {get;}
    public string Desc {get;}
    public abstract void OnClick();
    public FieldMenuItem(string name, string desc, Field field)
    {
        Name = name;
        this.field = field;
    }
}