using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulatePool : MonoBehaviour
{
    private List<GameObject> _pools = new List<GameObject>();
    [SerializeField] private GameObject Bulate;
   [SerializeField] private int poolSize = 5;
    private int ActiveSize = 0;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulateClone = Instantiate(Bulate,transform.position,Quaternion.identity);
            bulateClone.SetActive(false); // Ensure the object starts as inactive
            _pools.Add(bulateClone); // Add the clone to the pool
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBulate = bulateReturn();
            if (newBulate != null)
            {
                newBulate.SetActive(true);
                ActiveSize++; // Increment activeSize when a new object is activated
            }
        }
    }

    private GameObject bulateReturn()
    {
        if (ActiveSize < poolSize)
        {
            GameObject activeBulate = _pools[ActiveSize];
            Debug.Log("Return");
            return activeBulate;
        }
        else
        {
            Debug.LogWarning("Bulate pool exceeded. Consider increasing pool size.");
            return null;
        }
    }
}
