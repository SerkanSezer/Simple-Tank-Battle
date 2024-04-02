using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : Tank
{
    private GameManager gameManager;
    private List<GameObject> friendlist;
    public event Action OnEnemyAttack;

    [SerializeField] private float moveRate;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += GameManager_OnGameFinished;
        SubscribeToList();
        friendlist = gameManager.GetFriendTankList();
        Move();
    }

    private void GameManager_OnGameFinished(bool obj)
    {
        if (!obj && gameObject)
        StopAllCoroutines();
    }

    public override void Move()
    {
        navMeshAgent.SetDestination(GetRandomPointOnNavmesh());
        StartCoroutine(CheckArrivedOnNavMesh());
    }

    private IEnumerator CheckArrivedOnNavMesh()
    {
        yield return null;
        while (Vector3.Distance(navMeshAgent.destination, transform.position) > 0.1f)
        {
            yield return null;
        }
        navMeshAgent.ResetPath();

        var targetRotation = Quaternion.LookRotation(friendlist[UnityEngine.Random.Range(0,friendlist.Count)].transform.position - transform.position);
        while (Vector3.Distance(targetRotation.eulerAngles,transform.rotation.eulerAngles) > 3)
        {
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        }

        OnEnemyAttack?.Invoke();
        yield return new WaitForSeconds(3f);
        Move();

    }

    private Vector3 GetRandomPointOnNavmesh()
    {
        Vector3 result;
        if (RandomPoint(transform.position, 5f, out result))
        {
            return result;
        }
        else
        {
            return transform.position;
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public override void SubscribeToList()
    {
        gameManager.AddEnemyList(gameObject);
    }

    public override void RemoveFromList()
    {
        gameManager.RemoveEnemyTank(gameObject);
    }
}
