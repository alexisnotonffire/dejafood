using System;
public class ShopItem : FieldMenuItem
{
    Crop crop;

    private int GetCost()
    { return crop.Cost; }
    override public void OnClick() => base.field.AddCrop(crop);
    public ShopItem(Crop crop, Field field): base(crop.Name, field)
    {
        this.crop = crop;
    }
}