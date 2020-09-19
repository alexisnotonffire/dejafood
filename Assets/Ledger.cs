using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ledger : IUIButtonLister
{
    List<IButton> acceptedContracts = new List<IButton>();
    
    public void AcceptContract(Contract contract)
    {
        acceptedContracts.Add(contract);
    }
    public List<IButton> Buttons
    {
        get {return acceptedContracts;}
    }
}