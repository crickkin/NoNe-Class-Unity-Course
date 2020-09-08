using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Range(0.01f, 50f)] public float speed = 2f;
    
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
