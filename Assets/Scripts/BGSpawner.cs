using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    [Header("Prefab Referenzen")]
    public GameObject BuildingPrefab;
    public GameObject StripePrefab;
    public GameObject LampPrefab;
    public GameObject BGCarPrefab;

    [Header("GO Referenzen")]
    public GameObject sun;
    public GameObject SkyBox;

    [Header("Klassen Referenzen")]
    public BgCarSpawner BgCarSpawner;
    public Steuerung Steuerung;

    private float skyBoxY;
    
    [SerializeField] private float skyBoxWidth = 10f;
    [SerializeField] private float buildingWidth = 1.2f;
    [SerializeField] private float stripeWidth = 1f;
    [SerializeField] private float lampWidth = 1.1f;
    [SerializeField] private float bgCarWidth = 1.4f;
    [SerializeField] private float stripeYOffset = -1.39f;
    [SerializeField] private float lampYOffset = 0.115f;
    [SerializeField] private float bgCarYOffset = 0;

    public List<Sprite> BuildingSprites = new List<Sprite>();
    public List<Sprite> BGCarSprites = new List<Sprite>();

    private List<GameObject> BuildingsLeft = new List<GameObject>();
    private List<GameObject> BuildingsRight = new List<GameObject>();
    private List<GameObject> StripesLeft = new List<GameObject>();
    private List<GameObject> StripesRight = new List<GameObject>();
    private List<GameObject> LampsLeft = new List<GameObject>();
    private List<GameObject> LampsRight = new List<GameObject>();
    private List<GameObject> BGCarsLeft = new List<GameObject>();
    private List<GameObject> BGCarsRight = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        skyBoxY = SkyBox.transform.position.y;
        SpawnBackground();
    }
    // Update is called once per frame
    void Update()
    {
        //BGMover();
    }

    #region Spawn Functions
    private void SpawnBackground()
    {
        SpawnBuildings();
        SpawnStripes();
        SpawnLamps();
        SpawnBGCars();
    }
    private void SpawnBuildings()
    {
        float buildingX = 0f;
        while (buildingX <= skyBoxWidth)
        {
            BuildingsLeft.Add(Instantiate(BuildingPrefab, new Vector3(buildingX * (-1), skyBoxY, 1f), transform.rotation));
            ChangeBuildingSprite();
            BuildingsRight.Add(Instantiate(BuildingPrefab, new Vector3(buildingX, skyBoxY, 1f), transform.rotation));
            ChangeBuildingSprite();
            buildingX += buildingWidth;
        }
    }
    private void SpawnStripes()
    {
        float stripeX = 1f;
        while (stripeX <= skyBoxWidth)
        {
            StripesLeft.Add(Instantiate(StripePrefab, new Vector3(stripeX * (-1), skyBoxY + stripeYOffset, -1f), transform.rotation));
            StripesRight.Add(Instantiate(StripePrefab, new Vector3(stripeX, skyBoxY + stripeYOffset, -1f), transform.rotation));
            stripeX += stripeWidth;
        }
    }
    private void SpawnLamps()
    {
        float lampX = 1.1f;
        while (lampX <= skyBoxWidth)
        {
            LampsLeft.Add(Instantiate(LampPrefab, new Vector3(lampX * (-1), skyBoxY + lampYOffset, 0.5f), transform.rotation));
            LampsRight.Add(Instantiate(LampPrefab, new Vector3(lampX, skyBoxY + lampYOffset, 0.5f), transform.rotation));
            lampX += lampWidth;
        }
    }
    private void SpawnBGCars()
    {
        float bgCarX = 1.4f;
        while (bgCarX <= skyBoxWidth)
        {
            BGCarsLeft.Add(Instantiate(BGCarPrefab, new Vector3(bgCarX * (-1), skyBoxY + bgCarYOffset, 0f), transform.rotation));
            ChangeBGCarSprite();
            BGCarsRight.Add(Instantiate(BGCarPrefab, new Vector3(bgCarX, skyBoxY + bgCarYOffset, 0f), transform.rotation));
            ChangeBGCarSprite();
            bgCarX += bgCarWidth;
        }
    }
    private void ChangeBuildingSprite()
    {
        SpriteRenderer ranSpriteRenderer = BuildingPrefab.GetComponent<SpriteRenderer>();
        int ranIndex = Random.Range(0, BuildingSprites.Count);
        ranSpriteRenderer.sprite = BuildingSprites[ranIndex];
    }
    private void ChangeBGCarSprite()
    {
        SpriteRenderer ranSpriteRenderer = BGCarPrefab.GetComponent<SpriteRenderer>();
        int ranIndex = Random.Range(0, BGCarSprites.Count);
        ranSpriteRenderer.sprite = BGCarSprites[ranIndex];
    }

    #endregion

    private void BGMover()
    {
        StageSelect();
        foreach (GameObject bgL in BuildingsLeft)
        {
            bgL.transform.position = new Vector3(bgL.transform.position.x - BgCarSpawner.moveSpeedL * 0.1f * Time.deltaTime, skyBoxY, 1f);

            if (bgL.transform.position.x <= -10)
            {
                bgL.transform.position = new Vector3(bgL.transform.position.x + 10, skyBoxY, 1f);
            }
        }
        foreach (GameObject bgR in BuildingsRight)
        {
            bgR.transform.position = new Vector3(bgR.transform.position.x + BgCarSpawner.moveSpeedR * 0.1f * Time.deltaTime, skyBoxY, 1f);

            if (bgR.transform.position.x >= 10)
            {
                bgR.transform.position = new Vector3(bgR.transform.position.x - 10, skyBoxY, 1f);
            }
        }

        foreach (GameObject StripeGOLeft in StripesLeft)
        {
            StripeGOLeft.transform.position = new Vector3(StripeGOLeft.transform.position.x - BgCarSpawner.moveSpeedL * Time.deltaTime, 1.412f, 0f);
            if (StripeGOLeft.transform.position.x <= -10)
            {
                StripeGOLeft.transform.position = new Vector3(StripeGOLeft.transform.position.x + 10, 1.412f, 0f);
            }
        }
        foreach (GameObject StripeGORight in StripesRight)
        {
            StripeGORight.transform.position = new Vector3(StripeGORight.transform.position.x + BgCarSpawner.moveSpeedR * Time.deltaTime, 1.412f, 0f);
            if (StripeGORight.transform.position.x >= 10)
            {
                StripeGORight.transform.position = new Vector3(StripeGORight.transform.position.x - 10, 1.412f, 0f);
            }
        }
        foreach (GameObject LanternGOLeft in LampsLeft)
        {
            LanternGOLeft.transform.position = new Vector3(LanternGOLeft.transform.position.x - BgCarSpawner.moveSpeedL * 0.8f * Time.deltaTime, 2.91f, 0.5f);
            if (LanternGOLeft.transform.position.x <= -10)
            {
                LanternGOLeft.transform.position = new Vector3(LanternGOLeft.transform.position.x + 10, 2.91f, 0.5f);
            }
        }
        foreach (GameObject LanternGORight in LampsRight)
        {
            LanternGORight.transform.position = new Vector3(LanternGORight.transform.position.x + BgCarSpawner.moveSpeedR * 0.8f * Time.deltaTime, 2.91f, 0.5f);
            if (LanternGORight.transform.position.x >= 10)
            {
                LanternGORight.transform.position = new Vector3(LanternGORight.transform.position.x - 10, 2.91f, 0.5f);
            }
        }
        sun.transform.position = new Vector3(sun.transform.position.x - 5f * 0.0005f * Time.deltaTime, sun.transform.position.y - 5f * 0.00005f * Time.deltaTime, 2f);
    }
    private void StageSelect()
    {
        if (Steuerung.Player1.gameStage == 1)
        {
            BgCarSpawner.moveSpeedL = 5f;
        }
        else if (Steuerung.Player1.gameStage == 2)
        {
            BgCarSpawner.moveSpeedL = 5f * 1.5f;
        }
        else if (Steuerung.Player1.gameStage == 3)
        {
            BgCarSpawner.moveSpeedL = 5f * 2.2f;
        }
        else
        {
            BgCarSpawner.moveSpeedL = 5f * 3f;
        }

        if (Steuerung.Player2.gameStage == 1)
        {
            BgCarSpawner.moveSpeedR = 5f;
        }
        else if (Steuerung.Player2.gameStage == 2)
        {
            BgCarSpawner.moveSpeedR = 5f * 1.5f;
        }
        else if (Steuerung.Player2.gameStage == 3)
        {
            BgCarSpawner.moveSpeedR = 5f * 2.2f;
        }
        else
        {
            BgCarSpawner.moveSpeedR = 5f * 3f;
        }
        return;
    }
}