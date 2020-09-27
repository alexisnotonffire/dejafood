using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Trader : IButtonLister
{
    List<Contract> availableContracts;
    public List<Contract> AvailableContracts { get { return availableContracts; } }
    int maxContracts = 3;
    Ledger ledger;
    int counter = 1;
    public List<IButton> Buttons { get { return availableContracts.Cast<IButton>().ToList(); } }
    public void RefreshContracts()
    {
        availableContracts = new List<Contract>();
        for (int i = 0; i < maxContracts; i++)
        {
            int value = 5;
            Dictionary<string,int> crops = new Dictionary<string, int>(){
               {"onion", 5}
            };
            availableContracts.Add(
                new Contract(counter.ToString(), value, crops, ledger)
            );
            counter++;
        }
    }

    public Trader(Ledger l)
    {
        this.ledger = l;
        RefreshContracts();
    }
}