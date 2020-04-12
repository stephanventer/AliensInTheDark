using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb2D.transform.position += Vector3.up * speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb2D.transform.position += Vector3.down * speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb2D.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb2D.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
