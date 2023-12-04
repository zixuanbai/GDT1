using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetectPlayer : MonoBehaviour
{
    public float sightDistance = 50f; 
    private GameObject Boat; 
    private bool hasDetectedBoat = false;
    private MonsterPatrol patrolScript; 
    private Quaternion initialRotation; 
    private bool canAttack = true; 
    public float attackDuration = 2f; 
    public float attackSpeed = 5f; 

    private Vector3 originalPosition; 
    private Vector3 attackStartPosition; 
    private PlayerController playerController; 

    private void Start()
    {
        Boat = GameObject.FindWithTag("Boat"); 
        patrolScript = GetComponent<MonsterPatrol>(); 
        initialRotation = transform.rotation; 
        originalPosition = transform.position; 
        attackStartPosition = transform.position; 
        playerController = Boat.GetComponent<PlayerController>(); 
    }

    private void Update()
    {
        if (!hasDetectedBoat)
        {
            
            if (IsBoatInSight() && playerController.isAlive)
            {
                
                hasDetectedBoat = true;
                patrolScript.StopPatrolling(); 
                ChangeColor(Color.red);

                
                if (canAttack)
                {
                    
                    StartCoroutine(AttackBoat());
                }
            }
        }
        else if (hasDetectedBoat)
        {
           
            if (IsBoatInSight() && playerController.isAlive)
            {
                
                LookAtBoat();

            }
            else
            {
                
                hasDetectedBoat = false;
                patrolScript.ResumePatrolling(); 
                transform.rotation = initialRotation; 
                ChangeColor(Color.white); 
            }
        }
    }

    
    private bool IsBoatInSight()
    {
        if (Boat != null)
        {
            Vector2 direction = Boat.transform.position - transform.position;
            float distanceToBoat = direction.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distanceToBoat, LayerMask.GetMask("Rocks"));

            if (hit.collider != null && hit.collider.CompareTag("Rocks"))
            {
                
                return false;
            }
            
            if (distanceToBoat <= sightDistance)
            {
                
                return true;
            }
        }
        return false;
    }

    
    private void LookAtBoat()
    {
        if (Boat != null)
        {
            Vector3 direction = Boat.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    
    private void ChangeColor(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    
    private IEnumerator AttackBoat()
    {
        canAttack = false; 

        attackStartPosition = transform.position; 

        
        yield return new WaitForSeconds(3f);

       
        while (Vector3.Distance(transform.position, Boat.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Boat.transform.position, attackSpeed * Time.deltaTime);
            yield return null;
        }

        if (playerController != null && playerController.isAlive)
        {
            playerController.TakeDamage(1); 
        }

        
        yield return new WaitForSeconds(attackDuration);

        
        while (Vector3.Distance(transform.position, attackStartPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, attackSpeed * Time.deltaTime);
            yield return null;
        }

        
        patrolScript.ResumePatrolling();

        canAttack = true; 
    }
}






















