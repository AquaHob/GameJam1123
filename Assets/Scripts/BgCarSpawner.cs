using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCarSpawner : MonoBehaviour
{
    public GameObject bgCarPrefab;
    public GameObject carL;
    public GameObject carR;
    public float moveSpeedL = 5f;
    public float moveSpeedR = 5f;
    private float bgCarY = 2.65f;
    private List<GameObject> BgCarLeft = new List<GameObject>();
    private List<GameObject> BgCarRight = new List<GameObject>();
    public List<Sprite> BGCarSprites = new List<Sprite>();
    public Steuerung Steuerung;

    // Start is called before the first frame update
    void Start()
    {
        //CarSpawner();
    }
    // Update is called once per frame
    void Update()
    {
        //CarMover();
    }

    private void CarMover()
    {
        StageSelect();
        //Debug.Log("Ich bin es, mein Herr");
        foreach (GameObject bgCarL in BgCarLeft)
        {
            bgCarL.transform.position = new Vector3(bgCarL.transform.position.x - moveSpeedL * Time.deltaTime, bgCarY, 0f);

            if (bgCarL.transform.position.x <= -10)
            {
                bgCarL.transform.position = new Vector3(bgCarL.transform.position.x + 10, bgCarY, 0f);
            }
        }
        foreach (GameObject bgCarR in BgCarRight)
        {
            bgCarR.transform.position = new Vector3(bgCarR.transform.position.x + moveSpeedR * Time.deltaTime, bgCarY, 0f);

            if (bgCarR.transform.position.x >= 10)
            {
                bgCarR.transform.position = new Vector3(bgCarR.transform.position.x - 10, bgCarY, 0f);
            }
        }
    }
    private void StageSelect()
    {
        if (Steuerung.Player1.gameStage == 1)
        {
            moveSpeedL = 5f;
            carL.transform.position = new Vector3(carL.transform.position.x + moveSpeedL * Time.deltaTime, 2.5f, -5f);
            if (carL.transform.position.x >= -7.5f)
            {
                carL.transform.position = new Vector3(-7.5f, 2.5f, -5f);
            }
        }
        else if (Steuerung.Player1.gameStage == 2)
        {
            moveSpeedL = 5f * 1.5f;
            carL.transform.position = new Vector3(carL.transform.position.x + moveSpeedL * Time.deltaTime, 2.5f, -5f);
            if (carL.transform.position.x >= -6.5f)
            {
                carL.transform.position = new Vector3(-6.5f, 2.5f, -5f);
            }
        }
        else if (Steuerung.Player1.gameStage == 3)
        {
            moveSpeedL = 5f * 2.2f;
            carL.transform.position = new Vector3(carL.transform.position.x + moveSpeedL * Time.deltaTime, 2.5f, -5f);
            if (carL.transform.position.x >= -4.5f)
            {
                carL.transform.position = new Vector3(-4.5f, 2.5f, -5f);
            }
        }
        else
        {
            moveSpeedL = 5f * 3f;
            carL.transform.position = new Vector3(carL.transform.position.x + moveSpeedL * Time.deltaTime, 2.5f, -5f);
            if (carL.transform.position.x >= -2.5f)
            {
                carL.transform.position = new Vector3(-2.5f, 2.5f, -5f);
            }
        }

        if (Steuerung.Player2.gameStage == 1)
        {
            moveSpeedR = 5f;
            carR.transform.position = new Vector3(carR.transform.position.x - moveSpeedR * Time.deltaTime, 2.5f, -5f);
            if (carR.transform.position.x <= 7.5f)
            {
                carR.transform.position = new Vector3(7.5f, 2.5f, -5f);
            }
        }
        else if (Steuerung.Player2.gameStage == 2)
        {
            moveSpeedR = 5f * 1.5f;
            carR.transform.position = new Vector3(carR.transform.position.x - moveSpeedR * Time.deltaTime, 2.5f, -5f);
            if (carR.transform.position.x <= 6.5f)
            {
                carR.transform.position = new Vector3(6.5f, 2.5f, -5f);
            }
        }
        else if (Steuerung.Player2.gameStage == 3)
        {
            moveSpeedR = 5f * 2.2f;
            carR.transform.position = new Vector3(carR.transform.position.x - moveSpeedR * Time.deltaTime, 2.5f, -5f);
            if (carR.transform.position.x <= 4.5f)
            {
                carR.transform.position = new Vector3(4.5f, 2.5f, -5f);
            }
        }
        else
        {
            moveSpeedR = 5f * 3f;
            carR.transform.position = new Vector3(carR.transform.position.x - moveSpeedR * Time.deltaTime, 2.5f, -5f);
            if (carR.transform.position.x <= 2.5f)
            {
                carR.transform.position = new Vector3(2.5f, 2.5f, -5f);
            }
        }
        return;
    }

}
