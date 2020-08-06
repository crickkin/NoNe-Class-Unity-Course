using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Range(1f, 10f)] public float speed = 5f;
    private Rigidbody2D rb2d;
    
    [HideInInspector] public bool startMoving = false;
    [HideInInspector] public float increaseOfSpeed = 1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (startMoving)
        {
            StartMovement();
        }
    }

    public bool isMoving()
    {
        return rb2d.velocity.magnitude == 0;
    }

    public void StartMovement()
    {
        int xDirection = (Random.Range(0f, 1f) > .5f) ? -1 : 1;
        int yDirection = (Random.Range(0f, 1f) > .5f) ? -1 : 1;
        
        rb2d.velocity = new Vector2(increaseOfSpeed * speed * xDirection, increaseOfSpeed * speed * yDirection);
        //Debug.Log($"Velocity = ({rb2d.velocity.x},{rb2d.velocity.y})");
    }
}
