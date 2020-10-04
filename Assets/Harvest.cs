using System.Collections.Generic;
using System;
using UnityEngine;
public class Harvest : FieldMenu
{
    public Harvest(Field field, int turn): base(field)
    {
        Name = "Harvest";
        List<Crop> crops = new List<Crop>(){
            new Crop("Onion", 0, 5, 15, 5),
            new Crop("Potato", 0, 10, 45, 15),
        };
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