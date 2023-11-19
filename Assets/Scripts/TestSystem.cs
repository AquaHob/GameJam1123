using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestSystem : MonoBehaviour
{

    public TextMeshProUGUI TestText;

    // Start is called before the first frame update
    void Start()
    {
        TestText.text = "Wir sind gleich wieder da!";
    }
}
