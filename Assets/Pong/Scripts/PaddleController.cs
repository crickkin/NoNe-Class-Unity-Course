using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    [Range(0f, -10f)] public float min = 0f;
    [Range(0f, 10f)] public float max = 1f;
}

public class PaddleController : MonoBehaviour
{
    [Range(1, 2)] public int player = 1;
    public bool auto = false;
    [Range(.0001f, 1f)] public float botSmoothness = .3f;
    public float yStartingPosition = 0f;
    [Range(1f, 50f)] public float speed = 1f;
    [Range(.01f, 1f)] public float movementSmoothness = .8f;

    public Boundary clampY;

    private Vector2 startingPosition;
    private string inputName;

    void Start()
    {
        startingPosition = transform.position;
        transform.position = new Vector2(startingPosition.x, yStartingPosition);

        inputName = (player == 1) ? "Vertical" : "Vertical2";
    }
    
    void Update()
    {
        if (!auto)
        {
            PlayerMovement();
        }
        else
        {
            if (Input.GetAxis(inputName) != 0)
            {
                auto = false;
            }

            if (GameManager.script.ball)
            {
                MoveToPosition(GameManager.script.ball.transform.position.y, botSmoothness);
            }
        }
    }

    private void PlayerMovement()
    {
        float vertical = Input.GetAxis(inputName);

        MoveToPosition(new Vector2(0.0f, vertical * speed * Time.deltaTime) + (Vector2)transform.position, movementSmoothness);
    }

    private void MoveToPosition(float yMovement, float smoothness)
    {
        MoveToPosition(new Vector2(transform.position.x, yMovement), smoothness);
    }

    private void MoveToPosition(Vector2 movement, float smoothness)
    {
        movement.y = Mathf.Clamp(movement.y, clampY.min, clampY.max);
        transform.position = Vector2.Lerp(transform.position, movement, smoothness);
    }
}
