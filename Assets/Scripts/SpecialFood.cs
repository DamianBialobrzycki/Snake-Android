using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFood : MonoBehaviour
{
    // Food Prefab
    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    // Random time to instantiate
    System.Random randomTime = new System.Random();

    // Public values to define the interval time
    public int minTimeToSpawn = 5;
    public int maxTimeToSpawn = 20;

    // Time when food will be spawn
    private float timeToSpawn;

    // Counter of spawn time
    private float spawnTimer = 0.0f;

    // Life time of Special Food
    private const float lifeTime = 10.0f;

    // Counter of life time
    private float counterOfLifeTime = 0.0f;

    // Time when food will start blinking
    private float startBlinkingAfterTime = 6.5f;

    // Can spawn the food?
    private bool canSpawnNextFood;

    // Food on the scene
    private GameObject specialFood;

    // Use this for initialization
    void Start ()
    {
        timeToSpawn = CalculateRandomTime();
	}
	
	// Once per frame check if exist some special food
	void Update ()
    {
        // If Special Food don't exist on the scene
        if (GameObject.Find("Special Food(Clone)") == null)
        {
            counterOfLifeTime = 0.0f;
            // Set flag value
            canSpawnNextFood = true;
            // Start counting time
            spawnTimer += Time.deltaTime;

            // If counter >= time when food should be spawn
            if(spawnTimer >= timeToSpawn)
            {
                Spawn();
                spawnTimer = 0.0f;
                canSpawnNextFood = false;
                timeToSpawn = CalculateRandomTime();
            }
        }
        // If Special food exist on the scene
        else
        {
            specialFood = GameObject.Find("Special Food(Clone)");
            counterOfLifeTime += Time.deltaTime;

            // If counter >= the time when food should blinking
            if(counterOfLifeTime >= startBlinkingAfterTime)
            {
                specialFood.GetComponent<Animation>().Play();

                // If food should end his existence
                if(counterOfLifeTime >= lifeTime)
                {
                    counterOfLifeTime = 0.0f;
                    Destroy(specialFood);    
                }
            }
        }
    }

    // Spawn one piece of food
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }

    // Calculate random time when food should be spawn on the scene
    float CalculateRandomTime()
    {
        return randomTime.Next(minTimeToSpawn, maxTimeToSpawn);
    }
}
