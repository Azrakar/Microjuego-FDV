using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 2000f;
    public float rotationSpeed = 120f;
    public GameObject gun, bulletPrefab;

    private Rigidbody _rigid;
    private int hp = 3;
    public static int SCORE = 0;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;
        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Use the Pooler to spawn bullets
            GameObject bullet = Pooler.Spawn(bulletPrefab, gun.transform.position, Quaternion.identity);
            
            // Initialize the bullet's movement direction
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(transform.right);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp--;
            switch (hp)
            {
                case 2:
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    gameObject.transform.position = originalPos;
                    Destroy(collision.gameObject);
                    Destroy(GameObject.FindGameObjectWithTag("HP2"));
                    break;
                case 1:
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    gameObject.transform.position = originalPos;
                    Destroy(GameObject.FindGameObjectWithTag("HP2"));
                    Destroy(GameObject.FindGameObjectWithTag("HP1"));
                    Destroy(collision.gameObject);
                    break;
                case 0:
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Destroy(collision.gameObject);
                    SCORE = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
            }
        }
        else
        {
            Debug.Log("He colisionado con otro objeto");
        }
    }
}
