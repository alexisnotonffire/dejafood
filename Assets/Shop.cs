using System.Collections.Generic;
using System;
using UnityEngine;
public class Shop : FieldMenu
{
    public Shop(Field field): base(field)
    {
        Name = "Shop";
        List<Crop> crops = CropFactory.cropList;
        foreach (Crop crop in crops)
        {
            Buttons.Add(
                new ShopItem(
                    crop,
                    field
                )
            );
        }
    }
}