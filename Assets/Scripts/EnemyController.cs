using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private bool isHit;

    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per framedw
    void Update()
    {
        if((Vector2.Distance(transform.position, target.position) > 0.1f) && !isHit) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag == "bullet")
        {
            isHit = true;
            this.GetComponent<Renderer>().material.color = Color.cyan;
        }

        if(col.gameObject.tag == "player")
        {
            isHit = true;
            this.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
