using System;
public class HarvestItem : FieldMenuItem
{
    Crop crop;
    int turn;
    public override void OnClick() => field.HarvestCrop(turn, crop);
    string formatDesc()
    {
        string template = "Cost: {0} | Turns: {1}";
        return string.Format(template, crop.Cost, crop.HarvestAge);
    }
    public HarvestItem(Crop crop, int turn, Field field): base(crop.Name, field)
    {
        this.crop = crop;
        this.turn = turn;
        base.Desc = formatDesc();
    }
}