using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Country : MonoBehaviour
{
    public Main MAIN_RUNTIME;
    
    public string tag;
    
    public IDictionary<string,IDictionary<string,double>> MODIFIERS = new Dictionary<string, IDictionary<string, double>>();

    public int reserves;

    public List<Province> provinces = new List<Province>();

    public int foodProduced;

    public IDictionary<string, int> goodsProduced = new Dictionary<string, int>();
    
    public List<Army> armies = new List<Army>();
    
    // Start is called before the first frame update
    void Start()
    {
        MAIN_RUNTIME = (GameObject.Find("Main Camera")).GetComponent<Main>();
        
        foreach (var good in MAIN_RUNTIME.Goods)
        {
            goodsProduced[good] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void produceGoods()
    {
        foreach (var prov in provinces)
        {
            int provGoodsProduced = prov.produceGoods();
            goodsProduced[prov.good] += provGoodsProduced;
            MAIN_RUNTIME.goodsProducedLast[prov.good] += provGoodsProduced;
        }
    }

    public void addProfits()
    {
        foreach (var good in goodsProduced.Keys)
        {
            reserves += (int)Math.Round(goodsProduced[good] * MAIN_RUNTIME.prices[good]);
        }
    }
    
    public void calculateGrowth()
    {
        foreach (var prov in provinces)
        {
            int foodProduced = prov.produceFood();
            double foodRate = foodProduced / (double) (prov.slaves + prov.farmers + prov.nobility + prov.laborers);
            prov.foodRateLast = foodRate;
            prov.grow(foodRate);
        }
    }

    public void subtractExpenses()
    {
        reserves -= 5;
    }

    public void CreateArmy()
    {
        Army a = gameObject.AddComponent(typeof(Army)) as Army;
        print("hello pal");
    }
}
