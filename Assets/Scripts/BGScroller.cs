using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private float stripeSpeed;
    [SerializeField] private float buildingSpeed;
    [SerializeField] private float lampSpeed;
    [SerializeField] private float bgCarSpeed;
    [SerializeField] private bool reverseDirection;
    //[SerializeField] private float skyBoxLength = 10;
    
    public List<GameObject> StripeList = new List<GameObject>();
    public List<GameObject> BuildingList = new List<GameObject>();
    public List<GameObject> LampList = new List<GameObject>();
    public List<GameObject> BGCarList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
