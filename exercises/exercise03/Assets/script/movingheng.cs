using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving1 : MonoBehaviour
{
    float moveRate = 1;
    float moveTimer;
    float moveSpeed = 0.15f;

    void Start()
    {
        moveTimer = moveRate / 2;
    }
    void Update()
    {
        moveTimer = moveTimer - Time.deltaTime;
        if (moveTimer < 0)
        {
            moveSpeed = moveSpeed * -1;
            moveTimer = moveRate;
        }
        transform.Translate(0, 0, moveSpeed);
    }
}
