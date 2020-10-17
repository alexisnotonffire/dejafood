using System.Collections.Generic;
using System;
using UnityEngine;
public class Harvest : FieldMenu
{
    public Harvest(Field field, int turn): base(field)
    {
        Name = "Harvest";
        List<Crop> crops = CropFactory.cropList;
        foreach (Crop crop in crops)
        {
            Buttons.Add(
                new HarvestItem(
                    crop,
                    turn,
                    field
                )
            );
        }
    }
}