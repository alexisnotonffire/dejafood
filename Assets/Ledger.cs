using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Ledger : IButtonLister
{
    public List<Contract> AcceptedContracts = new List<Contract>();
    public List<IButton> Buttons
    {
        get { return AcceptedContracts.Cast<IButton>().ToList(); }
    }
    public void AcceptContract(Contract contract)
    {
        AcceptedContracts.Add(contract);
        Debug.Log("accepted contract: " + contract.Name);
    }
    public void CancelContract(Contract contract)
    {
        AcceptedContracts.RemoveAll(c => c.Name == contract.Name);
        Debug.Log("cancelled contract: " + contract.Name);
    }
    public List<Contract> NextTurn(int turn)
    {
        return AcceptedContracts.Where<Contract>(c => c.IsDue(turn)).ToList();
    }
}