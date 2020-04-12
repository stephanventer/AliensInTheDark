using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public GameObject crossHair;
    public GameObject bulletPrefab;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    } 

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        

        AimAndShoot();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        
        
        transform.position = transform.position + movement * Time.deltaTime * speed;
    }

    private void AimAndShoot()
    {
        Vector3 objectPos = cam.WorldToScreenPoint(transform.position);
        Vector3 aim = new Vector3(Input.mousePosition.x - objectPos.x, Input.mousePosition.y - objectPos.y, 0.0f);

        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        
        if(aim.magnitude > 0) {
            aim.Normalize();
            aim *= 1.2f; //Set distance of aim cursor from player.
            crossHair.transform.localPosition = aim;

            if (Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0)) {
                GameObject bullet  = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * 0.1f; //Set bullet speed.
                bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

                //Destroy bullet after 5 seconds.
                Destroy(bullet, 5.0f);
            }
        }

    }
}
