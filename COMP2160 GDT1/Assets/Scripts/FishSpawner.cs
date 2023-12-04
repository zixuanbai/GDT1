using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour{
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private float spawnRandomness = 1.0f;
    [SerializeField] private float timer;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1.5f;
    [SerializeField] private float minMoveSpeed = 2.0f;
    [SerializeField] private float maxMoveSpeed = 6.0f;
    [SerializeField] private Fish fish;
    [SerializeField] private BoxCollider2D spawner;
    [SerializeField] private float minY = 0.0f; 
    [SerializeField] private float maxY = 10.0f; 


    // Start is called before the first frame update
    private void Start()
    {
        timer = Random.Range(spawnInterval - spawnRandomness, spawnInterval + spawnRandomness);
    }

    // Update is called once per frame
   private void Update()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Fish newFish = Instantiate(fish, spawnPosition, Quaternion.identity, transform);
            newFish.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), 1);
            newFish.MoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed); 
            timer = Random.Range(spawnInterval - spawnRandomness, spawnInterval + spawnRandomness);
        }
    }
    
    private Vector2 GetRandomSpawnPosition()
    {
        if (spawner == null)
        {
            Debug.LogError("Spawn area not assigned in FishSpawner!");
            return transform.position; 
        }

        float minX = spawner.bounds.min.x;
        float maxX = spawner.bounds.max.x;
        float randomX = Random.Range(minX, maxX); 
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

}
