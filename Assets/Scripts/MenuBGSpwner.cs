using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGSpwner : MonoBehaviour
{
    private float bgX = -10.0f;
    private float bgY = 2.8f;

    public float cityScrollSpeed = 1.0f;

    public GameObject bgPrefab;
    public GameObject sun;

    private List<GameObject> BGCityTiles = new List<GameObject>();

    private SpriteRenderer ranSpriterenderer;

    public List<Sprite> BGCitySprites = new List<Sprite>();

    void Start(){
        for (int i = 0; i < 18; i++)
        {
            BGCityTiles.Add(Instantiate(bgPrefab, new Vector3(bgX, bgY, 1f), transform.rotation));
            ChangeSprite();
            bgX += 1.2f;
        }
    }

    void Update(){
        MoveCity();
    }

    private void MoveCity(){
        foreach (GameObject tile in BGCityTiles)
        {
            tile.transform.position = new Vector3(tile.transform.position.x - cityScrollSpeed * 0.1f * Time.deltaTime, bgY, 1f);

            if (tile.transform.position.x <= -10f)
            {
                tile.transform.position = new Vector3(10f, bgY, 1f);
            }
        }
    }

    private void ChangeSprite()
    {
        ranSpriterenderer = bgPrefab.GetComponent<SpriteRenderer>();
        int ranIndex = Random.Range(0, BGCitySprites.Count);
        ranSpriterenderer.sprite = BGCitySprites[ranIndex];
    }
}
