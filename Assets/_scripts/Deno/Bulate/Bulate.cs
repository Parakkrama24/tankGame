using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Bulate : MonoBehaviour
{
    private Transform bulateShootPos;
    [SerializeField] private float shootPowerMagnitude = 100f; // Magnitude of the shoot power
    [SerializeField] private Vector3 shootDirection; // Direction of the shoot power

    void Start()
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        bulateShootPos = GameObject.FindGameObjectWithTag("BulateShhotPos").transform;
        transform.position = bulateShootPos.position;
        shootDirection = new Vector3(0, 0, transform.localPosition.z);
        // Calculate the force vector using the shootDirection and shootPowerMagnitude
        Vector3 force = shootDirection.normalized * shootPowerMagnitude;

        // Apply the force to the rigidbody
        rb.AddForce(force, ForceMode.Impulse);
    }

    void Update()
    {
        // You can update shootDirection or shootPowerMagnitude here if needed
    }
}
