using System.Collections.Generic;
using System;
using UnityEngine;
public class Shop : IUIButtonLister
{
    Field field;
    public List<IButton> Buttons {get;} = new List<IButton>();
    public Shop(Field field)
    {
        this.field = field;
        Debug.Log("shop for field: " + field.ToString());
        List<Crop> crops = new List<Crop>(){
            new Crop("Onion", 0, 5, 15, 5),
            new Crop("Potato", 0, 10, 45, 15),
        };
        foreach (Crop crop in crops)
        {
            Buttons.Add(
                new ShopItem(
                    crop.Name,
                    crop.Cost,
                    () => {
                        field.AddCrop(crop);
                    }
                )
            );
        }
    }
}