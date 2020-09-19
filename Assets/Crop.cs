using System;
public class Crop : IShopItem, IButton
{
    public string Name {get;}
    int Age {get;}
    int HarvestAge {get;}
    int Value {get;}
    public int Cost {get;}

    public void OnClick(){}

    public T OnPurchase<T>()
    {
        Type pType = typeof(T);
        if (pType.Equals(typeof(Crop)))
        {
            return (T)(object)this;
        }
        return default(T);
    }

    public Crop(string n, int a, int ha, int v, int c)
    {
        Name = n;
        Age = a;
        HarvestAge = ha;
        Value = v;
        Cost = c;
    }
}