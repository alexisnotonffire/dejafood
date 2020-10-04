using System.Collections.Generic;
using System;
using UnityEngine;
public class FieldMenu : IButtonLister
{
    public string Name { get; protected set;}
    Field field;
    public List<IButton> Buttons {get; protected set;} = new List<IButton>();
    public FieldMenu(Field field)
    {
        this.field = field;
    }
}