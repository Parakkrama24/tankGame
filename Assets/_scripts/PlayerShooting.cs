using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
   // [SerializeField] private GameObject gun;
    [SerializeField] private float rotateSpeed;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q)) { 
            rotateSpeed = -100;

            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.E)) { rotateSpeed = 100;

            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }
}
