using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    private GameObject darknessGameObject;
    private Tilemap darknessTilemap;
    
    public Tile darknessTile;
    public Tile blueLightTile;
    public int bulletTail;
    public float bulletRadius;
    public int circleResolution;

    
    List<Vector3> bulletTailPos = new List<Vector3>();
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        darknessGameObject = GameObject.Find("Tilemap_Darkness");
        if(darknessGameObject != null) {
            darknessTilemap = darknessGameObject.GetComponent<Tilemap>();
        }
        bulletTailPos.Add(transform.position);    
        darknessTilemap.SetTile(darknessTilemap.WorldToCell(transform.position), blueLightTile);

        for (int i = 1; i < circleResolution; i++)
        {
            float angle = i * Mathf.PI * 2 / circleResolution;
            float x = Mathf.Cos(angle) * bulletRadius;
            float y = Mathf.Sin(angle) * bulletRadius;
            Vector3 newPos = transform.position + new Vector3(x, y, 0);   
            bulletTailPos.Add(newPos);    
            darknessTilemap.SetTile(darknessTilemap.WorldToCell(newPos), blueLightTile);
        }
             
        count++;
        if(count > bulletTail)
        {
            for (int i = 0; i < circleResolution-1; i++)
            {
                GameObject playerGameObject = GameObject.Find("Player");
                if(playerGameObject != null) {
                    Vector3 playerPos = playerGameObject.transform.position;
                    float rad = playerGameObject.GetComponent<PlayerController>().playerLightRadius - 0.2f;
                    if(bulletTailPos[i].x > playerPos.x + rad||
                    bulletTailPos[i].x < playerPos.x - rad ||
                    bulletTailPos[i].y > playerPos.y + rad ||
                    bulletTailPos[i].y < playerPos.y - rad )
                        darknessTilemap.SetTile(darknessTilemap.WorldToCell(bulletTailPos[i]), darknessTile);
                }
            }
            bulletTailPos.RemoveRange(0, circleResolution-1);
        }
    }

    public void OnDestroy()
    {
        foreach(var pos in bulletTailPos)
        {
            darknessTilemap.SetTile(darknessTilemap.WorldToCell(pos), darknessTile);
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet Collision!!");
    }
}
