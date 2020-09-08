using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Range(0.01f, 50f)] public float soarForce = 2f;
    [Range(0.0f, 4f)] public float fallSpeedOffset = 0.5f;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Boost();
        LimitFall();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Boost()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb2d.velocity = Vector2.up * soarForce;
        }
    }

    private void LimitFall()
    {
        if (rb2d.velocity.y < -(soarForce + fallSpeedOffset))
        {
            rb2d.velocity = Vector2.down * soarForce;
        }
    }
}
