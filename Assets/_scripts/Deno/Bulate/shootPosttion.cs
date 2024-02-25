using UnityEngine;

public class shootPosttion : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private void Update()
    {
        // Get the world rotation of the parent object
        Quaternion parentRotation = parentObject.transform.rotation;

        // Rotate this object using the parent's rotation
        transform.rotation = parentRotation;
    }
}
