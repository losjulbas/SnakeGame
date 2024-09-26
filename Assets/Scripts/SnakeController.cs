using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private bool isMoving = false; // Flag to prevent simultaneous moves
    [SerializeField] private float gridSize = 1.0f; // Size of each grid tile
    [SerializeField] private float moveDuration = 0.3f; // Time to move between tiles
    [SerializeField] private float timeBetweenMoves = 0.5f; // Delay between each automatic move
    [SerializeField] private float turnAmount = 90.0f; // Turn speed
    public float rotationSpeed = 1.0f;
    private Vector3 direction = Vector3.forward; // Default movement direction


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the automatic movement
        StartCoroutine(AutomaticMovement());
    }

    void Update()
    {   

        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
        {
            StartCoroutine(Move(Vector3.right));
            transform.Rotate(0, 90, 0);
            //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnAmount);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
        {
            StartCoroutine(Move(Vector3.left));
        }
    }

    private IEnumerator AutomaticMovement()
    {
        while (true) // Infinite loop for continuous movement
        {
            // Move one tile at a time automatically
            if (!isMoving)
            {
                StartCoroutine(Move(direction)); // Move in the current direction
            }

            // Wait for the set time before the next move
            yield return new WaitForSeconds(timeBetweenMoves);
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

        Vector3 startPos = transform.position; // Store the start position
        Vector3 endPos = startPos + (direction * gridSize); // Calculate the next tile position

        float elapsedTime = 0f;

        // Smooth movement from startPos to endPos
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly on the grid
        transform.position = endPos;

        // Mark the movement as complete
        isMoving = false;
    }

}
