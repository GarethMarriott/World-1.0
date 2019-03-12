//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;
//using System;
//
//public class Economy : MonoBehaviour
//{
//
//    
//    public GameObject provinces;
//    
//    public double starting_grain_price;
//    public double starting_fish_price;
//    public double starting_lumber_price;
//    public double starting_metal_price;
//
//    public IDictionary<string, int> supply;
//
//    public IDictionary<string, int> demand;
//
//    public IDictionary<string, double> prices;
//
//    public IDictionary<string, int> actual_bought;
//
//    public IDictionary<string, int> shortage;
//
//    public IDictionary<string, double> percent_supply_bought;
//
//    public IDictionary<string, double> percent_demand_bought;
//
//    private List<string> goods = new List<string>() { "grain", "fish", "lumber", "metal" };
//
//    private List<Worker> all_workers;
//
//    // Start is called before the first frame update
//    void Start()
//    {
//        provinces = GameObject.Find("Provinces");
//        
//        prices = new Dictionary<string, double>()
//                {
//                    {"grain" , starting_grain_price },
//                    {"fish" , starting_fish_price },
//                    {"lumber" , starting_lumber_price },
//                    {"metal" , starting_metal_price }
//                };
//
//        supply = new Dictionary<string, int>();
//                
//        demand = new Dictionary<string, int>();
//                
//        actual_bought = new Dictionary<string, int>();
//
//        shortage = new Dictionary<string, int>();
//
//        percent_supply_bought = new Dictionary<string, double>();
//
//        percent_demand_bought = new Dictionary<string, double>();
//
//        all_workers = new List<Worker>();
//
//        foreach (string good in goods)
//        {
//            supply[good] = 0;
//            demand[good] = 0;
//            actual_bought[good] = 0;
//            shortage[good] = 0;
//            percent_supply_bought[good] = 0;
//            percent_demand_bought[good] = 0;
//        }
//    }
//
//
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
//
//    public void Next_Turn()
//    {
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//
//            foreach (string good in goods)
//            {
//                foreach (Worker w in prov.workers)
//                {
//                    w.goods_bought[good] = 0;
//                }
//
//            }
//
//        }
//
//        foreach (string good in goods)
//        {
//            supply[good] = 0;
//            demand[good] = 0;
//            actual_bought[good] = 0;
//            shortage[good] = 0;
//            percent_supply_bought[good] = 0;
//            percent_demand_bought[good] = 0;
//        }
//
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//            supply[prov.good_produced] += prov.get_supply();
//        }
//
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//            IDictionary<string, int> prov_demand = prov.get_demand(prices);
//            foreach (string good in goods)
//            {
//                demand[good] += prov_demand[good];
//            }
//            
//        }
//
//        
//        foreach (string good in goods)
//        {
//            actual_bought[good] = 0;
//        }
//
//        IDictionary<string, int> supply_of_goods = new Dictionary<string, int>()
//                {
//                    {"grain" , supply["grain"] },
//                    {"fish" , supply["fish"] },
//                    {"lumber" , supply["lumber"] },
//                    {"metal" , supply["metal"] }
//                };
//        
//
//        all_workers.Clear();
//
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//            foreach (Worker w in prov.workers)
//            {
//                all_workers.Add(w);
//            }
//        }
//
//        print("World Population: " + all_workers.Count);
//        foreach (string good in goods)
//        {
//            bool good_bought_this_cycle = true;
//            while (good_bought_this_cycle && supply_of_goods[good] > 0)
//            {
//                //print("in");
//                good_bought_this_cycle = false;
//
//                foreach (Worker w in all_workers)
//                {
//                    //print("w.wealth - prices[good] > 0" + (bool)(w.wealth - prices[good] > 0));
//                    //print("supply_of_goods[good] > 0" + (bool)(supply_of_goods[good] > 0));
//                    //print("w.province_belongs_to.demand_rates[good] > w.goods_bought[good]" + (bool)(w.province_belongs_to.demand_rates[good] > w.goods_bought[good]));
//                    if (w.wealth - prices[good] > 0 && supply_of_goods[good] > 0 && w.province_belongs_to.demand_rates[good] > w.goods_bought[good])
//                    {
//                        //print("good bought");
//                        good_bought_this_cycle = true;
//
//                        w.wealth = w.wealth - prices[good];
//                        w.goods_bought[good] = w.goods_bought[good] + 1;
//                        actual_bought[good]++;
//                        supply_of_goods[good]--;
//                    }
//                }
//            }
//            
//        }
//
//
//        foreach (string good in goods)
//        {
//            shortage[good] = supply[good] - actual_bought[good];
//            percent_supply_bought[good] = Math.Round(1 - ((double)shortage[good] / (double)supply[good]) , 2);
//            percent_demand_bought[good] = Math.Round((double)actual_bought[good] / (double)demand[good] , 2);
//
//        }
//
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//            prov.pay_workers(percent_supply_bought, prices);
//        }
//
//        foreach (string good in goods)
//        {
//            update_price(good);
//        }
//
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//            prov.srink_pop();
//        }
//        
//        foreach (Province prov in provinces.GetComponent<All_provinces>().get_provinces())
//        {
//            prov.grow_pop();
//        }
//
//        foreach (string good in goods)
//        {
//            print(good);
//            print("price: " + prices[good]);
//            print("supply: " + supply[good] + " demand: " + demand[good] + " actual bought: " + actual_bought[good] + " percent supply bought: " + percent_supply_bought[good] + " percent demand bought: " + percent_demand_bought[good]);
//        }
//
//        
//
//        print("----\n\n");
//        
//    }
//
//    void update_price(string price_name)
//    {
//        if (demand[price_name] > supply[price_name])
//        {
//            //print(price_name + " price increased");
//            prices[price_name] = Math.Round(prices[price_name] + 0.1, 1);
//        }
//        else if (demand[price_name] < supply[price_name])
//        {
//            if (prices[price_name] - 0.1 <= 0)
//            {
//                //print(price_name + "price already minimum");
//                return;
//            }
//            //print(price_name + " price decreased");
//            prices[price_name] = Math.Round(prices[price_name] - 0.1, 1);
//        }
//        else
//        {
//            //print(price_name + " price equalized");
//        }
//    }
//    
//}
