using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    public GameObject bgPrefab;
    public GameObject sun;
    public GameObject stripePrefabL;
    public GameObject stripePrefabR;
    public GameObject lanternPrefabL;
    public GameObject lanternPrefabR;
    public BgCarSpawner BgCarSpawner;
    private float bgXLeft;
    private float bgXRight;
    private float bgY = 2.8f;
    private float stripeLeft;
    private float stripeRight;
    private float lanternLeft;
    private float lanternRight;

    private List<GameObject> BGLeft = new List<GameObject>();
    private List<GameObject> BGRight = new List<GameObject>();
    private List<GameObject> StripesLeft = new List<GameObject>();
    private List<GameObject> StripesRight = new List<GameObject>();
    private List<GameObject> LanternsLeft = new List<GameObject>();
    private List<GameObject> LanternsRight = new List<GameObject>();

    public List<Sprite> BGSprites = new List<Sprite>();

    private SpriteRenderer ranSpriterenderer;



    // Start is called before the first frame update
    void Start()
    {
        BackgroundSpawner();
    }
    private void BackgroundSpawner()
    {
        for (int i = 0; i < 9; i++)
        {
            bgXLeft -= 1.2f;
            BGLeft.Add(Instantiate(bgPrefab, new Vector3(bgXLeft, bgY, 1F), transform.rotation));
            bgXRight += 1.2f;
            BGRight.Add(Instantiate(bgPrefab, new Vector3(bgXRight, bgY, 1F), transform.rotation));
            ChangeSprite();
        }
        for (int i = 0; i < 10; i++)
        {
            stripeLeft -= 1f;
            StripesLeft.Add(Instantiate(stripePrefabL, new Vector3(stripeLeft, 1.412f, -1f), transform.rotation));
            stripeRight += 1f;
            StripesRight.Add(Instantiate(stripePrefabR, new Vector3(stripeRight, 1.412f, -1f), transform.rotation));
        }
        for (int i = 0; i < 9; i++)
        {
            lanternLeft -= 1.1f;
            LanternsLeft.Add(Instantiate(lanternPrefabL, new Vector3(lanternLeft, 2.91f, 0.5f), transform.rotation));
            lanternRight += 1.1f;
            LanternsRight.Add(Instantiate(lanternPrefabR, new Vector3(lanternRight, 2.91f, 0.5f), transform.rotation));
        }
    }
    private void ChangeSprite()
    {
        ranSpriterenderer = bgPrefab.GetComponent<SpriteRenderer>();
        int ranIndex = Random.Range(0, BGSprites.Count);
        ranSpriterenderer.sprite = BGSprites[ranIndex];
    }


    // Update is called once per frame
    void Update()
    {
        BGMover();
    }
    private void BGMover()
    {
        foreach (GameObject bgL in BGLeft)
        {
            bgL.transform.position = new Vector3(bgL.transform.position.x - BgCarSpawner.moveSpeed * 0.1f * Time.deltaTime, bgY, 1f);

            if (bgL.transform.position.x <= -10)
            {
                bgL.transform.position = new Vector3(bgL.transform.position.x + 10, bgY, 1f);
            }
        }
        foreach (GameObject bgR in BGRight)
        {
            bgR.transform.position = new Vector3(bgR.transform.position.x + BgCarSpawner.moveSpeed * 0.1f * Time.deltaTime, bgY, 1f);

            if (bgR.transform.position.x >= 10)
            {
                bgR.transform.position = new Vector3(bgR.transform.position.x - 10, bgY, 1f);
            }
        }
        sun.transform.position = new Vector3(sun.transform.position.x - BgCarSpawner.moveSpeed * 0.0005f * Time.deltaTime, sun.transform.position.y - BgCarSpawner.moveSpeed * 0.00005f * Time.deltaTime, 2f);

        foreach (GameObject StripeGOLeft in StripesLeft)
        {
            StripeGOLeft.transform.position = new Vector3(StripeGOLeft.transform.position.x - BgCarSpawner.moveSpeed * Time.deltaTime, 1.412f, 0f);
            if (StripeGOLeft.transform.position.x <= -10)
            {
                StripeGOLeft.transform.position = new Vector3(StripeGOLeft.transform.position.x + 10, 1.412f, 0f);
            }
        }
        foreach (GameObject StripeGORight in StripesRight)
        {
            StripeGORight.transform.position = new Vector3(StripeGORight.transform.position.x + BgCarSpawner.moveSpeed * Time.deltaTime, 1.412f, 0f);
            if (StripeGORight.transform.position.x >= 10)
            {
                StripeGORight.transform.position = new Vector3(StripeGORight.transform.position.x - 10, 1.412f, 0f);
            }
        }
        foreach (GameObject LanternGOLeft in LanternsLeft)
        {
            LanternGOLeft.transform.position = new Vector3(LanternGOLeft.transform.position.x - BgCarSpawner.moveSpeed * 0.8f * Time.deltaTime, 2.91f, 0.5f);
            if (LanternGOLeft.transform.position.x <= -10)
            {
                LanternGOLeft.transform.position = new Vector3(LanternGOLeft.transform.position.x + 10, 2.91f, 0.5f);
            }
        }
        foreach (GameObject LanternGORight in LanternsRight)
        {
            LanternGORight.transform.position = new Vector3(LanternGORight.transform.position.x + BgCarSpawner.moveSpeed * 0.8f * Time.deltaTime, 2.91f, 0.5f);
            if (LanternGORight.transform.position.x >= 10)
            {
                LanternGORight.transform.position = new Vector3(LanternGORight.transform.position.x - 10, 2.91f, 0.5f);
            }
        }
    }
}