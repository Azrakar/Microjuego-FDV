using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        // Only called once when instantiated
        Invoke("DespawnBullet", maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    public void Initialize(Vector3 newTargetVector)
    {
        // Called each time the bullet is spawned
        targetVector = newTargetVector;
        Invoke("DespawnBullet", maxLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            DespawnBullet();
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }

    private void DespawnBullet()
    {
        Pooler.Despawn(gameObject);
    }
}
