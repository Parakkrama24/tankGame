using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Transform Guntransform;
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(0f, 0.1f)] private float speed;
    [SerializeField] private Transform initialPoint;
    [SerializeField] private GameObject shootingPoint;
   
    void Start()
    {
        mainCamera=Camera.main;
        
    }

  
    void Update()
    {
       // initialPoint = mainCamera.transform.position;
        if (Input.GetMouseButton(1))
        {
            Vector3 startPotition=mainCamera.transform.position;
            Vector3 endPotition= Guntransform.position - offset;
            mainCamera.transform.position=Vector3.Lerp(startPotition, endPotition, speed);
            mainCamera.transform.rotation=Guntransform.rotation;
            shootingPoint.SetActive(true);


        }
        else
        {
            Vector3 endPotition = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.Lerp(endPotition, initialPoint.position, speed);
            mainCamera.transform.rotation= initialPoint.rotation;
            shootingPoint.SetActive(false);

        }
    }
}
