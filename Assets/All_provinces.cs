using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class All_provinces : MonoBehaviour
{

    
    private List<Province> provs = new List<Province>();
    // Start is called before the first frame update
    void Start()
    {

        
        

        /*foreach (Transform child in transform)
            print("Foreach loop: " + child.gameObject);
            */
    }
        // Update is called once per frame
    void Update()
    {
        
    }

    public List<Province> get_provinces()
    {
        provs = new List<Province>();
        foreach (Transform child in transform)
        {
            //print(child.GetComponent<Province>());
            provs.Add(child.GetComponent<Province>());
        }
        return provs;
    }


}
