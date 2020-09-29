using System.Collections.Generic;
using UnityEngine;
public class Turn : IButtonLister
{
    public List<IButton> Buttons {get;} = new List<IButton>();
    public Turn(int cash, int causalityPoints)
    {
        this.Buttons.Add(new TurnItem("Current Cash", cash.ToString()));
        this.Buttons.Add(new TurnItem("Causality Points", causalityPoints.ToString()));
    }
}