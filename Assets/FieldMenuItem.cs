using System;
public abstract class FieldMenuItem : IButton
{
    protected Field field;
    public string Name {get;}
    public abstract void OnClick();
    public FieldMenuItem(string name, Field field)
    {
        Name = name;
        this.field = field;
    }
}