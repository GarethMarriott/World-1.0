using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Province : MonoBehaviour
{
    public string tag;

    public Country ownedBy;

    public Country controlledBy;

    public string good;
    
    public int farmers;
    public int slaves;
    public int laborers;
    public int nobility;

    public double farmerProductionRate;
    public double laborerProductionRate;

    public int baseFoodCapacity;
    public int baseLaborerCapacity;

    public double farmerGrowthRate;
    public double slaveGrowthRate;
    public double laborerGrowthRate;
    public double nobilityGrowthRate;

    public double percentFarmerGrowth;
    public double percentSlaveGrowth;
    public double percentLaborerGrowth;
    public double percentNobilityGrowth;

    public double foodRateLast = 0;
    public int foodProducedLast = 0;
    
    System.Random rnd = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    public int produceFood()
    {
        int workers = farmers + slaves;
        foodProducedLast = (int) Math.Round(farmerProductionRate * (workers + (1 - workers / baseFoodCapacity)));
        return foodProducedLast;
    }

    public int produceGoods()
    {
        int workers = laborers + slaves;
        return (int)Math.Round(laborerProductionRate * (workers + (1 - workers / baseLaborerCapacity)));
    }
    
    public void grow(double foodRate)
    {
        if (foodRate < 1)
        {
            if (farmers != 0)
            {
                if (percentFarmerGrowth - (1 - foodRate)/2 < 0)
                {
                    farmers -= 1;
                    percentFarmerGrowth = 0.25;
                    print("farmer died in "+tag);
                }
                else
                {
                    percentFarmerGrowth -= (1 - foodRate);
                }
            }

            if (slaves != 0)
            {
                if (percentSlaveGrowth - (1 - foodRate)*2 < 0)
                {
                    slaves -= 1;
                    percentSlaveGrowth = 0.25;
                    print("slave died in "+tag);
                }
                else
                {
                    percentSlaveGrowth -= (1 - foodRate);
                }
            }

            if (laborers != 0)
            {
                if (percentLaborerGrowth - (1 - foodRate) < 0)
                {
                    laborers -= 1;
                    percentLaborerGrowth = 0.25;
                    print("Laborer died in "+tag);
                    if (rnd.Next(100) > 50)
                    {
                        farmers += 1;
                        print("but converted to farmer in "+tag);
                    }
                    
                }
                else
                {
                    percentLaborerGrowth -= (1 - foodRate);
                    
                }
            }

            if (nobility != 0)
            {
                if (percentNobilityGrowth - (1 - foodRate) < 0 )
                {
                    nobility -= 1;
                    percentNobilityGrowth = 0.25;
                    print("Nobility died in "+tag);
                    if (rnd.Next(100) > 50)
                    {
                        farmers += 1;
                        print("but converted to farmer in "+tag);
                    }
                }
                else
                {
                    percentNobilityGrowth -= (1 - foodRate);
                }
            }
        }
        else
        {
            bool farmersGrew = growFarmers(foodRate-1);
            bool slavesGrew = growSlaves(foodRate-1);
            bool laborersGrew = growLaborers(foodRate-1);
            bool nobilityGrew = growNobility(foodRate-1);  
        }
    }

    private bool growFarmers(double foodMultiplier)
    {
        if (percentFarmerGrowth + farmerGrowthRate*foodMultiplier > 1)
        {
            percentFarmerGrowth = 0.25;
            farmers += 1;
            print("farmers grew in "+tag);
            return true;
        }
        else
        {
            percentFarmerGrowth = percentFarmerGrowth + farmerGrowthRate;
            return false;
        }
    }
    
    private bool growSlaves(double foodMultiplier)
    {
        if (percentSlaveGrowth + (slaveGrowthRate*foodMultiplier/2) > 1)
        {
            percentSlaveGrowth = 0.25;
            slaves += 1;
            print("slaves grew in "+tag);
            return true;
        }
        else
        {
            percentSlaveGrowth = percentSlaveGrowth + slaveGrowthRate;
            return false;
        }
    }
    
    private bool growLaborers(double foodMultiplier)
    {
        if (percentLaborerGrowth + laborerGrowthRate*foodMultiplier > 1)
        {
            percentLaborerGrowth = 0.25;
            laborers += 1;
            print("laborers grew in "+tag);

            return true;
        }
        else
        {
            percentLaborerGrowth = percentLaborerGrowth + laborerGrowthRate;
            return false;
        }
    }
    
    private bool growNobility(double foodMultiplier)
    {
        if (percentNobilityGrowth + nobilityGrowthRate*foodMultiplier > 1)
        {
            percentNobilityGrowth = 0.25;
            nobility += 1;
            print("nobility grew in "+tag);
            return true;
        }
        else
        {
            percentNobilityGrowth = percentNobilityGrowth + nobilityGrowthRate;
            return false;
        }
    }

    
  
}
