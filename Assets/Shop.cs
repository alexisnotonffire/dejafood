using System.Collections.Generic;
using System;
using UnityEngine;
public class Shop : FieldMenu
{
    public Shop(Field field): base(field)
    {
        Name = "Shop";
        List<Crop> crops = new List<Crop>(){
            new Crop("Onion", 0, 5, 15, 5),
            new Crop("Potato", 0, 10, 45, 15),
        };
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