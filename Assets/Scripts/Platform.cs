using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private static int platformsAmount;
    private static Platform currentPlatform;

    private int maxPlatformsAmount = 40;
    private bool isApplicationClosed;

    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private GameObject leftPlatform;
    [SerializeField] private GameObject rightPlatform;

    private float fallingSpeed;
    private void OnApplicationQuit()
    {
        isApplicationClosed = true;
        platformsAmount = 0;
    }
    private void OnDestroy()
    {
        if (!isApplicationClosed)
        {
            platformsAmount--;
            currentPlatform.SpawnRandomPlatform();
        }
    }
    void Start()
    {
        fallingSpeed = 0f;
        platformsAmount++;
        currentPlatform = this;
        if (currentPlatform == this && platformsAmount <= maxPlatformsAmount)
        {
            SpawnRandomPlatform();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Player>())
        {
            DestroyPlatform();
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
    }

    private void SpawnRandomPlatform()
    {
        if(Random.Range(0, 2) == 0)
        {
            GameObject newPlatform = Instantiate(leftPlatform, leftSpawnPoint.position,leftSpawnPoint.rotation);
            newPlatform.transform.SetParent(null);
        }
        else
        {
            GameObject newPlatform = Instantiate(rightPlatform, rightSpawnPoint.position, rightSpawnPoint.rotation);
            newPlatform.transform.SetParent(null);
        }
    }
    private void DestroyPlatform()
    {
        fallingSpeed = 2f;
    }
}
