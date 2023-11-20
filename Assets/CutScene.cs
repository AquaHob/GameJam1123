using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public Animation cutScene;
    public Steuerung steuerung;

    // Start is called before the first frame update
    void Awake()
    {
        cutScene.Play();
    }
}
