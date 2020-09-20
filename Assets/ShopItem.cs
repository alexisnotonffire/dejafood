using System;
public class ShopItem : IButton
{
    public string Name {get;}
    int Cost {get;}
    public void OnClick(){
        OnPurchase();
    }
    Action OnPurchase;
    public ShopItem(string name, int cost, Action purchaseFunc)
    {
        Name = name;
        Cost = cost;
        OnPurchase = purchaseFunc;
    }
}