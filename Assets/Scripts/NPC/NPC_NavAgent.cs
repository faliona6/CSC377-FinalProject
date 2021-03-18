using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_NavAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool paused = false;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) { 
            agent.SetDestination(transform.position);
            return;
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Choosing new path");
            //GoToRandomPoint();
            StartCoroutine(PauseForSeconds(2));
        }
        UpdateAnimation();

        /*
        ExecuteAction(
            new Action(
                () => StartCoroutine(PauseForSeconds(3))),
            0.001f);*/
    }

    private void OnDrawGizmos()
    {
        if (paused)
        {
            Gizmos.DrawSphere(transform.position, 1);
        }
    }

    private void UpdateAnimation()
    {
        float magnitude = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("InputMagnitude", magnitude);
    }

    /// <summary>
    /// Executes a function with a certain percentage of execution.
    /// </summary>
    /// <param name="func">Function to execute</param>
    /// <param name="percentage">Float from 0-1 percentage will execute.</param>
    private void ExecuteAction(Action func, float percentage)
    {
        float r = UnityEngine.Random.Range(0.0f, 100.0f);
        if (r < percentage * 100.0f) {
            Debug.Log("hello?");
            func?.Invoke();
        }
    }

    private void GoToRandomPoint()
    {
        Debug.Log("going to random point");
        Vector3 target;
        if (RandomPoint(transform.position, 30f, out target))
        {
            agent.SetDestination(target);
        }
    }

    private IEnumerator PauseForSeconds(int seconds)
    {
        Debug.Log("pausing");
        paused = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(seconds);
        Debug.Log("unpausing");
        paused = false;
        agent.isStopped = false;
        GoToRandomPoint();

    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
