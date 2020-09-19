using System.Collections.Generic;
public class Shop : IUIButtonLister
{
    Field field;
    public List<IButton> Buttons {get;} = new List<IButton>(){
        new Crop("Onion", 0, 5, 15, 5),
        new Crop("Potato", 0, 10, 45, 15),
    };
    public Shop(Field field)
    {
        this.field = field;
    }
}