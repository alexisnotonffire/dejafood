using System;
public class HarvestItem : FieldMenuItem
{
    Crop crop;
    int turn;
    public override void OnClick() => field.HarvestCrop(turn, crop);
    public HarvestItem(Crop crop, int turn, Field field): base(crop.Name, null, field)
    {
        this.crop = crop;
        this.turn = turn;
    }
}