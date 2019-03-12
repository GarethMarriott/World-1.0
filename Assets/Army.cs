using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    public Main MAIN_RUNTIME;
    
    public Country ownedBy;

    public List<Unit> units;
    // Start is called before the first frame update
    void Start()
    {
        MAIN_RUNTIME = (GameObject.Find("Main Camera")).GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
