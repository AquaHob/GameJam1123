using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeinScript : MonoBehaviour
{
    public int zahl;

    public bool turn = false;
    public int counter = 0;
    public int counter1 = 1;
    public GameObject kasten;

    // Start is called before the first frame update
    void Start()
    {
     	Debug.Log(zahl);  
    }

    // Update is called once per frame
    void Update()
    {
	if (!turn && kasten.transform.position.x <= 1000) {
    kasten.transform.position = new Vector3(kasten.transform.position.x +1, kasten.transform.position.y, kasten.transform.position.z);
    
    }else {
        turn = true;
    }

    
    // if (kasten.transform.position.x == 1000) {
    //     counter++;
    //     counter1 = counter; 
    //     counter = 0;
    //     Debug.Log("Oskar ist cool");
        
    // }
    if (turn && kasten.transform.position.x >= 0) {
        kasten.transform.position = new Vector3(kasten.transform.position.x -1, kasten.transform.position.y, kasten.transform.position.z);
    }else{
        turn = false;
    }

    
	//TestSystem TS = kasten.GetComponent<TestSystem>();
	//TS.TestText.text = "Was Anderes";
    }
}
