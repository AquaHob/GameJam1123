using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCarSpawner : MonoBehaviour
{
    public GameObject bgCarPrefab;
    public float moveSpeed = 5f;
    private float bgCarY = 2.65f;
    private float bgCarXLeft = -0.2f;
    private float bgCarXRight = 0.2f;
    private List<GameObject> BgCarLeft = new List<GameObject>();
    private List<GameObject> BgCarRight = new List<GameObject>();
    private SpriteRenderer ranSpriteRenderer;
    public List<Sprite> BGCarSprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        CarSpawner();
    }

    private void CarSpawner()
    {
        for (int i = 0; i < 7; i++)
        {
            //Debug.Log("Du auch hier?");
            bgCarXLeft -= 1.4f;
            BgCarLeft.Add(Instantiate(bgCarPrefab, new Vector3(bgCarXLeft, bgCarY, 0f), transform.rotation));
            ChangeSprite();
        }
        for (int i = 0; i < 7; i++)
        {
            bgCarXRight += 1.4f;
            BgCarRight.Add(Instantiate(bgCarPrefab, new Vector3(bgCarXRight, bgCarY, 0f), transform.rotation));
            ChangeSprite();
        }
            
    }
    private void ChangeSprite()
    {
        ranSpriteRenderer = bgCarPrefab.GetComponent<SpriteRenderer>();
        int ranIndex = Random.Range(0, BGCarSprites.Count);
        ranSpriteRenderer.sprite = BGCarSprites[ranIndex];
        if (bgCarPrefab.transform.position.x > 5)
        {
            //Debug.Log("Grüß deine Mama von mir");
            ranSpriteRenderer.flipX = true;
        } else
        {
            ranSpriteRenderer.flipX = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        CarMover();        
    } 

    private void CarMover()
    {
        //Debug.Log("Ich bin es, mein Herr");
        foreach (GameObject bgCarL in BgCarLeft)
        {
            bgCarL.transform.position = new Vector3(bgCarL.transform.position.x - moveSpeed * Time.deltaTime, bgCarY, 0f);
            
            if ( bgCarL.transform.position.x <= -10)
            {
                bgCarL.transform.position = new Vector3(bgCarL.transform.position.x + 10, bgCarY, 0f);
            }
        }
        foreach (GameObject bgCarR in BgCarRight)
        {
            bgCarR.transform.position = new Vector3(bgCarR.transform.position.x + moveSpeed * Time.deltaTime, bgCarY, 0f);
            
            if ( bgCarR.transform.position.x >= 10)
            {
                bgCarR.transform.position = new Vector3(bgCarR.transform.position.x - 10, bgCarY, 0f);
            }
        }
    }
    private void CarRespawner()
    {

    }
}
