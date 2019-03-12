using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Runtime.ConstrainedExecution;

public class Main : MonoBehaviour
{
    /// <summary>
    /// CONSTANT(but not really) VARS
    /// </summary>
    /// 
    //Declaration of starting state of the provinces
    public IDictionary<string,List<string>> starting_provs = new Dictionary<string,List<string>>()
    {
        {"country1" , new List<string>() {"n","e"} },
        {"country2" , new List<string>() {"s"} },
        {"country3" , new List<string>() {"w"} }
    };
    
    public List<string> Goods = new List<string>()
    {
        "lumber",
        "metal"
    };
    
    public IDictionary<string , IDictionary<string,double> > unit_base_values = new Dictionary<string, IDictionary<string, double>>()
    {
        { "Heavy Inf", new Dictionary<string, double>()
                                    {
                                        {"shock" , 3},
                                        {"skirmish" , 0.1},
                                        {"armor" , 3},
                                        {"piercing" , 3},
                                        {"mobility" , 1}
                                    } 
        },
        {"Light Inf" , new Dictionary<string, double>() 
                                    {
                                        {"shock" , 1.5},
                                        {"skirmish" , 0.5},
                                        {"armor" , 1.5},
                                        {"piercing" , 2},
                                        {"mobility" , 1.5}
                                    } 
        },
        {"Heavy Horse" , new Dictionary<string, double>() 
                                    {
                                        {"shock" , 5},
                                        {"skirmish" , 0.5},
                                        {"armor" , 2},
                                        {"piercing" , 3},
                                        {"mobility" , 5}
                                    } 
        },
        {"Light Horse" , new Dictionary<string, double>() 
                                    {
                                        {"shock" , 3},
                                        {"skirmish" , 1},
                                        {"armor" , 0.25},
                                        {"piercing" , 2},
                                        {"mobility" , 7}
                                    } 
        },
        {"Archers" , new Dictionary<string, double>() 
                                    {
                                        {"shock" , 0.5},
                                        {"skirmish" , 3},
                                        {"armor" , 0.25},
                                        {"piercing" , 1},
                                        {"mobility" , 2}
                                    } 
        },
        {"Horse Archers" , new Dictionary<string, double>() 
                                    {
                                        {"shock" , 1},
                                        {"skirmish" , 3},
                                        {"armor" , 0.25},
                                        {"piercing" , 1},
                                        {"mobility" , 7}
                                    } 
        }
        
    };

    /// <summary>
    /// NON CONSTANT VARS
    /// </summary>
    List<Country> countries = new List<Country>();

    public IDictionary<string, double> prices = new Dictionary<string, double>();

    public IDictionary<string, int> goodsProducedLast = new Dictionary<string, int>();

    List<Province> allProvs;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        allProvs = (GameObject.Find("Provinces")).GetComponent<All_provinces>().get_provinces();

        foreach (var country in starting_provs.Keys)
        {
            initializeCountry(country);
        }
        

        foreach (var good in Goods)
        {
            goodsProducedLast[good] = 0;
            prices[good] = 5;
        }
    }

    void initializeCountry(string tag)
    {
        Country c = gameObject.AddComponent(typeof(Country)) as Country;
        c.tag = tag;
        addProvincesToCountry(starting_provs[tag] , allProvs , c);
        c.MAIN_RUNTIME = this;
        
        //Temp Stuff
        Transform copyOfButton = GameObject.Find("CreateArmy").transform;
        Transform newButtonTransform = Instantiate(copyOfButton, new Vector3(0, 0 ) , new Quaternion(0,0,0,0));
        Button newArmyButton = newButtonTransform.gameObject.GetComponent(typeof(Button)) as Button;
        newArmyButton.onClick.AddListener(c.CreateArmy);
        newButtonTransform.parent = GameObject.Find("Canvas").transform;
        //Temp Stuff
        
        countries.Add(c);
    }
    
    static void addProvincesToCountry(List<string> country1_provs , List<Province> provs , Country country)
    {
        foreach (string name_of_prov in country1_provs)
        {
            foreach (Province prov in provs)
            {
                if (prov.tag == name_of_prov)
                {
                    prov.ownedBy = country;
                    prov.controlledBy = country;
                    country.provinces.Add(prov);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Daily()
    {
        
    }

    public void Weekly()
    {
        print("--------------------------");
        foreach (var good in Goods)
        {
            goodsProducedLast[good] = 0;
        }
        
        foreach (var country in countries)
        {
            country.produceGoods();
            country.addProfits();
            country.subtractExpenses();
            country.calculateGrowth();
        }
    }

    public void Monthly()
    {
        
    }

    
}
