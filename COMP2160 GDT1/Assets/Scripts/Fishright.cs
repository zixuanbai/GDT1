using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Fishright : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float floatAmplitude = 0.5f;
    [SerializeField] private float floatSpeed = 1.0f;
    private bool isHooked = false;
    private Transform hookedHook;
    private Collider2D fishCollider;
    private Vector3 startPosition;
    public int score = 10;
    public float MoveSpeed
     
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isHooked && hookedHook != null)
        {
            
            transform.position = hookedHook.position;
        }
        else
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook")) 
        {
            isHooked = true; 
            hookedHook = other.transform; 
            
            moveSpeed = 0f;
           
        }
        if (other.CompareTag("Boat")) 
        {
            
            DestroyFish();
            FindObjectOfType<PlayerController>().score += score;

            
            FindObjectOfType<PlayerController>().UpdateScoreText();
        }
    }
    private void DestroyFish()
    {
        
        Destroy(gameObject);
    }
}



