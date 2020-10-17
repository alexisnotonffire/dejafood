using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
public class Trader : IButtonLister
{
    static Random rnd = new Random();
    List<Contract> availableContracts = new List<Contract>();
    public List<Contract> AvailableContracts { get { return availableContracts; } }
    public List<IButton> Buttons { get { return availableContracts.Cast<IButton>().ToList(); } }
    int counter = 1;
    int turn = 1;
    Ledger ledger;
    int maxContracts = 3;  
    int maxCrops = 5; 
    public void NextTurn()
    {
        turn++;
        RefreshContracts();
    } 
    public void RefreshContracts()
    {
        availableContracts = new List<Contract>();
        for (int i = 0; i < maxContracts; i++)
        {
            int contractValue = rnd.Next(1, 11);
            List<Crop> cropList = new List<Crop>();
            for (int j = 0; j < maxCrops; j++)
            {
                int availableCropCount = CropFactory.cropList.Count;
                if (rnd.NextDouble() < .7f || j == 1)
                {
                    cropList.Add(CropFactory.cropList[rnd.Next(availableCropCount)]);
                }
            }

            Dictionary<string,int> crops = new Dictionary<string, int>();
            foreach (Crop crop in cropList)
            {
                if (!crops.TryGetValue(crop.Name, out int k))
                {
                    crops.Add(crop.Name, 0);
                }
                crops[crop.Name]++;
                contractValue += (int)(crop.Cost * (1 + (rnd.NextDouble() / 10)));

            }
            
            int due = turn + 1;
            availableContracts.Add(
                new Contract(counter.ToString(), contractValue, crops, ledger, due)
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