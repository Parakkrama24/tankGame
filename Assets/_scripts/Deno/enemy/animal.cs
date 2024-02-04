using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum AnimalState
{
    Idel,
    Moving,
}

[RequireComponent(typeof(NavMeshAgent))]
public class animal : MonoBehaviour
{
    [Header("Wonder")]
   [SerializeField] private float wanderDistance = 5f;
   [SerializeField ]private float walkSpeed = 5f;
    [SerializeField] private float MaxwalkTime = 6f;

    [Header("Walk")]
    [SerializeField]
    private float idleTime = 1f;

    protected NavMeshAgent navaget;
    protected AnimalState Current_state= AnimalState.Idel;

    private void Start()
    {
        initializeAnimal();
    }

    protected virtual void initializeAnimal()
    {
       
        navaget = GetComponent<NavMeshAgent>();
        navaget.speed = walkSpeed;

        Current_state = AnimalState.Idel;
        updateState();
    }

    protected virtual void updateState()
    {
        switch (Current_state)
        {
            case AnimalState.Idel:
                handleIdleState();
                break;
            case AnimalState.Moving:
                handleMovingState();
                break;
        }
    }

    protected Vector3  GetRandomhandlepostition(Vector3 origine,float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origine;
        NavMeshHit _navemeshHit;
        
        if (NavMesh.SamplePosition(randomDirection,out _navemeshHit,distance,NavMesh.AllAreas)){
             return _navemeshHit.position;
        }
        else
        {
         return GetRandomhandlepostition(origine,distance);
        }
        
    }

    protected virtual void handleMovingState()
    {
        StartCoroutine(waitToDistaination());
    }

    private IEnumerator waitToMove()
    {
       
        float waitTime =UnityEngine.Random.Range(idleTime/2, idleTime*2);
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = GetRandomhandlepostition(transform.position, wanderDistance);
        navaget.SetDestination(randomDestination);
        setState(AnimalState.Moving);
      
    }

    protected virtual void handleIdleState()
    {
        StartCoroutine(waitToMove());
    }

    private IEnumerator waitToDistaination()
    {
        float startTime = Time.time;
        while(navaget.remainingDistance>navaget.stoppingDistance)
        {
            if(Time.time-startTime>= MaxwalkTime)
            {
                navaget.ResetPath();
                setState(AnimalState.Idel);
                yield break;
            }
            yield return null;
        }
        
        //destination has been reached
        setState(AnimalState.Idel);
    }

    protected void setState(AnimalState newState)
    {
        if(Current_state==newState) return;

        Current_state = newState;
        onstateChange(newState);
    }

    protected virtual void onstateChange(AnimalState newState)
    {
        updateState();
    }
}

