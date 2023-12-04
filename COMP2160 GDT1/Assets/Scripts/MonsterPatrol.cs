using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour
{
    public float patrolSpeed = 2.0f; 
    private bool isMovingRight = true; 
    private bool canPatrol = true; 
    private float minX; 
    private float maxX; 

    private void Start()
    {
        InitializeBounds();
    }

    private void Update()
    {
       
        if (canPatrol)
        {
            
            float moveDistance = patrolSpeed * Time.deltaTime;

            
            if (isMovingRight)
            {
                transform.Translate(Vector3.right * moveDistance);
            }
            else
            {
                transform.Translate(Vector3.left * moveDistance);
            }

            
            if (transform.position.x >= maxX && isMovingRight)
            {
                isMovingRight = false;
            }
            else if (transform.position.x <= minX && !isMovingRight)
            {
                isMovingRight = true;
            }
        }
    }

    
    private void InitializeBounds()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    
    public void StopPatrolling()
    {
        canPatrol = false;
    }

    
    public void ResumePatrolling()
    {
        canPatrol = true;
    }
}

