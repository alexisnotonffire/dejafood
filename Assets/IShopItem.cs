public interface IShopItem
{
    string Name {get;}
    int Cost {get;}
    T OnPurchase<T>();
}