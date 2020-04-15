using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float bulletSpeed;
    public float bulletTimeout;
    public float playerLightRadius;
    public int circleResolution;
    public Animator animator;
    public GameObject crossHair;
    public GameObject bulletPrefab;
    public Camera cam;
    public Tile normalTile;

    private GameObject darknessGameObject;
    private Tilemap darknessTilemap;
    private List<Vector3> previousPlayerLight;

    // Start is called before the first frame update
    void Start()
    {
        previousPlayerLight = new List<Vector3>();
    } 

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);     

        AimAndShoot();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        
        transform.position = transform.position + movement * Time.deltaTime * playerSpeed;

        darknessGameObject = GameObject.Find("Tilemap_Darkness");
        if(darknessGameObject != null) {
            darknessTilemap = darknessGameObject.GetComponent<Tilemap>();
        }

        if(previousPlayerLight.Count > 0)
        {
            foreach (var pos in previousPlayerLight)
            {
                darknessTilemap.SetTile(darknessTilemap.WorldToCell(pos), normalTile);
            }
            previousPlayerLight.Clear();
        }

        for (float j = 0f; j <= playerLightRadius; j = j + 0.5f) {
            for (int i = 1; i < circleResolution; i++)
            {
                float angle = i * Mathf.PI * 2f / circleResolution;
                float x = Mathf.Cos(angle) * j;
                float y = Mathf.Sin(angle) * j;
                Vector3 newPos = transform.position + new Vector3(x, y, 0);   
                previousPlayerLight.Add(newPos);       
                darknessTilemap.SetTile(darknessTilemap.WorldToCell(newPos), null);
            }
        }

    }

    private void AimAndShoot()
    {
        Vector3 objectPos = cam.WorldToScreenPoint(transform.position);
        Vector3 aim = new Vector3(Input.mousePosition.x - objectPos.x, Input.mousePosition.y - objectPos.y, 0.0f);

        
        
        if(aim.magnitude > 0) {
            aim.Normalize();
            aim *= 1.2f; //Set distance of aim cursor from player.
            crossHair.transform.localPosition = aim;

            Vector2 shootingDirection = new Vector2(aim.x, aim.y);

            
            if (Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0)) {
                GameObject bullet  = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed; //Set bullet speed.
                bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

                //Destroy bullet after 10s seconds.
                Destroy(bullet, bulletTimeout);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "enemy")
        {
            Destroy(col.gameObject, 0.2f);
            var healthBar = GameObject.FindWithTag("health").GetComponent<RectTransform>();
            Vector3 temp = healthBar.localScale;
            temp.x =  temp.x - 0.5f;
            if(temp.x >= 0) {
                healthBar.localScale = temp;
                Debug.Log("Health: " + temp.x);
            }
        }
    }
}
