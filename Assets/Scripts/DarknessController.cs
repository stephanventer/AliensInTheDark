using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DarknessController : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var tilePos = map.WorldToCell(bulletPrefab.transform.position);
        //map.SetTile(tilePos, null);
    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
	void LateUpdate ()
	{
		// Set the position of the Darkness (the game object this script is attached to)
		// to the player's position, plus the offset amount
		// transform.position = player.transform.position;// + offset;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Darkness Collision Triggered");
    }
}
