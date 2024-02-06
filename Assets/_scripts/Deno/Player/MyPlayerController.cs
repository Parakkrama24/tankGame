using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MyPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate move direction based on player's local forward direction
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection = moveDirection.normalized;

        Vector3 movement = moveDirection * speed * Time.deltaTime;

        MovePlayer(movement);
        RotatePlayer(moveDirection);
    }

    private void MovePlayer(Vector3 movement)
    {
        playerRb.MovePosition(playerRb.position + movement);
    }

    private void RotatePlayer(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            playerRb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
}
