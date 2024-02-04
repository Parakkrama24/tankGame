using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum AnimalState
{
    Idel,
    Moving,
    Shooting
}

[RequireComponent(typeof(NavMeshAgent))]
public class Animal : MonoBehaviour
{
    [Header("Wonder")]
    [SerializeField] private float wanderDistance = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float maxWalkTime = 6f;

    [Header("Walk")]
    [SerializeField] private float idleTime = 1f;

    protected NavMeshAgent navAgent;
    protected AnimalState currentState = AnimalState.Idel;

    [Header("Shooting")]
    [SerializeField] private GameObject player;
    [SerializeField] private float minDistanceForShoot = 5f;
    public static bool isShooting = false;

    private void Start()
    {
        InitializeAnimal();
        
    }

    private void Update()
    {
        Vector3 distance = transform.position - player.transform.position;

        if (distance.x < minDistanceForShoot || distance.z < minDistanceForShoot)
        {
            isShooting = true;
        }

        if (isShooting)
        {
            SetState(AnimalState.Shooting);
        }

       // Debug.Log(currentState);
    }

    protected virtual void InitializeAnimal()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = walkSpeed;

        currentState = AnimalState.Idel;
        UpdateState();
    }

    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case AnimalState.Idel:
                HandleIdleState();
                break;
            case AnimalState.Moving:
                HandleMovingState();
                break;
            case AnimalState.Shooting:
                HandleShootingState();
                break;
        }
    }

    protected Vector3 GetRandomHandlePosition(Vector3 origin, float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        else
        {
            return GetRandomHandlePosition(origin, distance);
        }
    }

    protected virtual void HandleMovingState()
    {
        StartCoroutine(WaitToDestination());
    }

    private IEnumerator WaitToMove()
    {
        float waitTime = UnityEngine.Random.Range(idleTime / 2, idleTime * 2);
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = GetRandomHandlePosition(transform.position, wanderDistance);
        navAgent.SetDestination(randomDestination);
        SetState(AnimalState.Moving);
    }

    protected virtual void HandleIdleState()
    {
        StartCoroutine(WaitToMove());
    }

    private IEnumerator WaitToDestination()
    {
        float startTime = Time.time;
        while (navAgent.remainingDistance > navAgent.stoppingDistance)
        {
            if (Time.time - startTime >= maxWalkTime)
            {
                navAgent.ResetPath();
                SetState(AnimalState.Idel);
                yield break;
            }
            yield return null;
        }

        // Destination has been reached
        SetState(AnimalState.Idel);
    }

    private void HandleShootingState()
    {
        Vector3 updatePos = player.transform.position;
        updatePos  = new Vector3(transform.position.x-3, transform.position.y, transform.position.z-3);
        navAgent.SetDestination(player.transform.position);
    }

    protected void SetState(AnimalState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        OnStateChange(newState);
    }

    protected virtual void OnStateChange(AnimalState newState)
    {
        UpdateState();
    }
}
