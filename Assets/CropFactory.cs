using System.Collections.Generic;
public class CropFactory
{
    public static List<Crop> cropList {get;}
    static CropFactory()
    {
        cropList = new List<Crop>();

        cropList.Add(new Crop("Onion", 0, 5, 10, 5));
        cropList.Add(new Crop("Potato", 0, 7, 15, 5));
        cropList.Add(new Crop("Grape", 0, 2, 5, 2));
        cropList.Add(new Crop("Carrot", 0, 4, 8, 2));
        cropList.Add(new Crop("Tomato", 0, 6, 12, 5));
    }
}