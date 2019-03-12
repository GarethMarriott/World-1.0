//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using System;
//
//public class Province : MonoBehaviour
//{
//    public string good_produced;
//
//    public double starting_grain_demand_rate;
//    public double starting_fish_demand_rate;
//    public double starting_lumber_demand_rate;
//    public double starting_metal_demand_rate;
//
//    public int starting_number_of_workers;
//    
//    public double production_rate;
//
//    public IDictionary<string, double> demand_rates;
//
//    public int supplied_last;
//
//    private List<string> goods = new List<string>() { "grain", "fish", "lumber", "metal" }; // ORDER MATTERS
//
//    public List<Worker> workers = new List<Worker>();
//
//    System.Random rnd = new System.Random();
//
//    public double growth_rate;
//
//    public double percent_until_next_pop;
//
//    // Start is called before the first frame update
//    void Start()
//    {
//        demand_rates = new Dictionary<string, double>()
//                            {
//                                {"grain",starting_grain_demand_rate},
//                                {"fish",starting_fish_demand_rate},
//                                {"lumber",starting_lumber_demand_rate},
//                                {"metal",starting_metal_demand_rate}
//                            };
//        
//        for (int i = 0; i < starting_number_of_workers; i++)
//        {
//            Worker worker = gameObject.AddComponent(typeof(Worker)) as Worker;
//            worker.occupation = good_produced;
//            worker.wealth = UnityEngine.Random.Range(50,100);
//            worker.province_belongs_to = this;
//            workers.Add(worker);
//        }
//        
//    }
//
//
//    // Update is called once per frame
//    void Update()
//    {
//
//    }
//    
//    public int get_supply()
//    {
//        int supply = 0;
//        foreach(Worker w in workers)
//        {
//            supply += (int)Math.Round(production_rate);
//        }
//        supplied_last = supply;
//        return supply;
//    }
//    
//    public IDictionary<string,int> get_demand(IDictionary<string,double> price)
//    {
//        IDictionary<string, int> demand = new Dictionary<string, int>();
//        foreach (string good in goods)
//        {
//            demand[good] = 0;
//        }
//
//
//
//        foreach(Worker w in workers)
//        {
//            
//            foreach(string good in goods)
//            {
//                int i = 0;
//                double wealth_of_worker = w.wealth;
//                
//                while (( (wealth_of_worker - price[good]) > 0) && i < demand_rates[good])
//                {
//                    demand[good] += 1;
//                    wealth_of_worker = wealth_of_worker - price[good];
//                    i++;
//                }
//            }
//            
//        }
//        return demand;
//    }
//
//    public IDictionary<string,int> buy(IDictionary<string,int> supply , IDictionary<string, double> price)
//    {
//        
//
//        IDictionary<string, int> bought = new Dictionary<string, int>();
//        foreach (string good in goods)
//        {
//            bought[good] = 0;
//        }
//
//        foreach (string good in goods)
//        {
//
//            foreach (Worker w in workers)
//            {
//                w.goods_bought[good] = 0;
//            }
//
//
//            int i = 0;
//            int supply_of_good = supply[good]/4;
//            while (i < demand_rates[good])
//            {
//                foreach (Worker w in workers)
//                {
//                    if(w.wealth - price[good] > 0 && supply_of_good > 0){
//                        w.wealth = w.wealth - price[good];
//                        w.goods_bought[good] = w.goods_bought[good] + 1;
//                        bought[good]++;
//                        supply_of_good--;
//                    }
//                }
//                i++;
//            }
//        }
//
//        return bought;
//    }
//
//    public void pay_workers(IDictionary<string,double> percent_bought , IDictionary<string,double> prices)
//    {
//        foreach (Worker w in workers)
//        {
//            w.wealth += production_rate * percent_bought[good_produced] * prices[good_produced];
//        }
//    }
//
//    public void srink_pop()
//    {
//        List<Worker> to_be_removed = new List<Worker>();
//
//        foreach (Worker w in workers)
//        {
//            
//        }
//
//        for (int i = workers.Count - 1; i >= 0; i--)
//        {
//            //print("w.goods_bought:" + workers[i].goods_bought["grain"] + "demand rates: " + demand_rates["grain"]);
//            if (workers[i].goods_bought["grain"] < demand_rates["grain"])
//            {
//                double random_num = Math.Round(((double)rnd.Next(50)) / 100.0, 2);
//                double percent_of_grain = Math.Round(((double)workers[i].goods_bought["grain"]) / demand_rates["grain"], 2);
//                //print("*** percent of grain:" + percent_of_grain);
//                //print("random num" + random_num);
//                if (random_num > percent_of_grain)
//                {
//                    workers.RemoveAt(i);
//                    print("pop died!");
//                }
//            }
//        }
//
//        foreach (Worker w in to_be_removed)
//        {
//            workers.Remove(w);
//        }
//    }
//
//    public void grow_pop()
//    {
//        percent_until_next_pop += growth_rate;
//        foreach (Worker w in workers)
//        {
//            if (w.goods_bought["metal"] == demand_rates["metal"])
//            {
//                percent_until_next_pop += 0.001;
//            }
//        }
//
//        if (percent_until_next_pop > 1)
//        {
//            Worker worker = gameObject.AddComponent(typeof(Worker)) as Worker;
//            worker.occupation = good_produced;
//            worker.wealth = UnityEngine.Random.Range(50, 100);
//            worker.province_belongs_to = this;
//            workers.Add(worker);
//            percent_until_next_pop = 0;
//        }
//    }
//  
//}
