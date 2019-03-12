using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Worker : MonoBehaviour
{
    public double wealth = 0;
    public string occupation;
    public double goods_produced_last = 0;
    private List<string> goods; 
    public IDictionary<string, int> goods_bought;
    public Province province_belongs_to;


    public Worker(int wealth , string occupation)
    {
        this.occupation = occupation;
        this.wealth = wealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        goods = new List<string>() { "grain", "fish", "lumber", "metal" };

        goods_bought = new Dictionary<string, int>();
        foreach (string good in goods)
        {
            goods_bought[good] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
