using System;
public class Crop
{
    public string Name {get;}
    int Age {get;}
    int HarvestAge {get;}
    int Value {get;}
    public int Cost {get;}
    public Crop(string n, int a, int ha, int v, int c)
    {
        Name = n;
        Age = a;
        HarvestAge = ha;
        Value = v;
        Cost = c;
    }
}