using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShhoting : MonoBehaviour
{
    private Transform PlayerTransform;
   
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if(PlayerTransform == null)
        {
            Debug.LogError("No Player");
        }
    }

   
    void Update()
    {
        bool isShootingStatus = Animal.isShooting;
        if (isShootingStatus && PlayerTransform!=null)
        {
            float xValuve = PlayerTransform.position.x- gameObject.transform.position.x;
            float zValuve = PlayerTransform.transform.position.z - gameObject.transform.position.z;
            
            Vector3 direction = new Vector3(xValuve, 0.238f, zValuve);

            Quaternion rotation = Quaternion.LookRotation(direction);

            // Apply the rotation to object1
            gameObject.transform.rotation = rotation;
        }
    }
}
