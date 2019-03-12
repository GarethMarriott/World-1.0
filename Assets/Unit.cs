using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Main MAIN_RUNTIME;

    public string unitType;

    public Army army;
        
    // Start is called before the first frame update
    void Start()
    {
        MAIN_RUNTIME = (GameObject.Find("Main Camera")).GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // valid options : {shock,skirmish,armor,piercing,mobility,evasion}
    public double getStat(string nameOfStat)
    {
        double baseValue = (MAIN_RUNTIME.unit_base_values[unitType])[nameOfStat];
        return baseValue + baseValue * (army.ownedBy.MODIFIERS[unitType])[nameOfStat];
    }
}
