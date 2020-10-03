using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Trader : IButtonLister
{
    List<Contract> availableContracts = new List<Contract>();
    public List<Contract> AvailableContracts { get { return availableContracts; } }
    public List<IButton> Buttons { get { return availableContracts.Cast<IButton>().ToList(); } }
    int counter = 1;
    Ledger ledger;
    int maxContracts = 3;    
    public void RefreshContracts()
    {
        availableContracts = new List<Contract>();
        for (int i = 0; i < maxContracts; i++)
        {
            int value = 5;
            Dictionary<string,int> crops = new Dictionary<string, int>(){
               {"Onion", 5}
            };
            availableContracts.Add(
                new Contract(counter.ToString(), value, crops, ledger)
            );
            counter++;
        }
    }
    public Trader(Ledger ledger)
    {
        this.ledger = ledger;
        RefreshContracts();
    }
}