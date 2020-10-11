using System;
public class ShopItem : FieldMenuItem
{
    Crop crop;

    private int GetCost()
    { return crop.Cost; }
    override public void OnClick() => base.field.AddCrop(crop);
    string formatDesc()
    {
        string template = "Cost: {0} | Turns: {1}";
        return string.Format(template, crop.Cost, crop.HarvestAge);
    }
    public ShopItem(Crop crop, Field field): base(crop.Name, field)
    {
        this.crop = crop;
        base.Desc = formatDesc();
    }
}