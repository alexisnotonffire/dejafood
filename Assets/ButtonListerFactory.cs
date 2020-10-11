using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class ButtonListerFactory
{
    class Button : IButton
    {
        public string Name {get;}
        public string Desc {get;}
        public void OnClick() { return; }
        public Button(string name, string desc)
        {
            Name = name;
            Desc = desc;
        }
    }
    class ButtonLister : IButtonLister
    {
        IEnumerable enumerable;
        public List<IButton> Buttons {  
            get {
                List<IButton> buttons = new List<IButton>();
                if (typeof(List<string>) == enumerable.GetType())
                {
                    foreach (string item in enumerable)
                    {
                        buttons.Add(new Button(item, null));
                    }  
                } else if (typeof(Dictionary<string, int>) == enumerable.GetType())
                {
                    foreach (KeyValuePair<string, int> item in enumerable)
                    {
                        buttons.Add(new Button(string.Format("{0} {1}", item.Value, item.Key), null));
                    }
                }
                return buttons;
            } 
        }
        public ButtonLister(IEnumerable list)
        {
            enumerable = list;            
        }
    }
    public static IButtonLister ToButtonLister(IEnumerable list)
    {
        return new ButtonLister(list);
    }
}