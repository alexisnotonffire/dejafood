using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Contract : IButton
{
    Ledger ledger;
    public string Name {get;}
    public int Value {get;}
    int Due {get;}
    public Dictionary<string,int> Crops {get;}
    public void OnClick()
    {
        ledger.AcceptContract(this);
    }
    public bool IsDue(int turn)
    {
        return true;
    }
    public Contract(string n, int v, Dictionary<string,int> c, Ledger l)
    {
        Name = n;
        Value = v;
        Crops = c;
        ledger = l;
    }
}