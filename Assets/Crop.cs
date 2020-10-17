using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class Crop
{
    public string Name {get;}
    int Age {get;set;}
    public int HarvestAge {get;}
    int Value {get;}
    public int Cost {get;}
    List<Sprite> sprites = new List<Sprite>();
    public Sprite HarvestSprite;
    List<string> stages = new List<string>(){
        "seed",
        "age",
        "ready"
    };
    public Sprite NextTurn()
    {
        Age++;
        return GetSprite();
    }
    public Sprite GetSprite()
    {
        if (Age >= HarvestAge) { return sprites[sprites.Count - 1]; }
        if (Age == 0) { return sprites[0]; }
        return sprites[1];
    }
    public Crop(string n, int a, int ha, int v, int c)
    {
        Name = n;
        Age = a;
        HarvestAge = ha;
        Value = v;
        Cost = c;
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("game-sprites");
        foreach (string stage in stages)
        {
            sprites.Add(atlas.GetSprite(string.Format("{0}_{1}", n.ToLower(), stage)));
        }
        HarvestSprite = atlas.GetSprite(string.Format("{0}_{1}", n.ToLower(), "ready"));
    }
}