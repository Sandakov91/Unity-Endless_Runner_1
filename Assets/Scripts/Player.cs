using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int multiplyScore;

    public int score { get; private set; }
    private float playerSpeed;
    private Vector3 playerDirection;
    private Rigidbody playerRigidbody;

    private float startY;
    private float gameEndY = 0.05f;
    void Start()
    {
        startY = transform.position.y;
        playerSpeed = startSpeed;
        playerDirection = Vector3.zero;
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangePlayerDirection();
        }
        if(score == multiplyScore)
        {
            multiplyScore += multiplyScore;
            AddSpeed();
        }
        if(transform.position.y < startY - gameEndY)
        {
            Time.timeScale = 0f;
        }
    }
    private void FixedUpdate()
    {
        playerRigidbody.velocity = playerDirection * playerSpeed;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Platform>())
        {
            score++;
        }
    }

    private void ChangePlayerDirection()
    {
        if (playerDirection == Vector3.zero || playerDirection == Vector3.forward)
        {
            playerDirection = Vector3.right;
        }
        else
        {
            playerDirection = Vector3.forward;
        }
    }
    private void AddSpeed()
    {
        if (playerSpeed != maxSpeed)
        {
            playerSpeed *= speedMultiplier;
        }
        if (playerSpeed > maxSpeed)
        {
            playerSpeed = maxSpeed;
        }
    }
}
