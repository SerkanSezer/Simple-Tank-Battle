using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyMove : Tank
{
    private GameManager gameManager;
    private List<GameObject> enemylist;
    public event Action OnFriendlyAttack;

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
        enemylist = gameManager.GetEnemyTankList();
        Move();
    }

    private void GameManager_OnGameFinished(bool obj)
    {
        if (obj && gameObject)
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

        var targetRotation = Quaternion.LookRotation(enemylist[UnityEngine.Random.Range(0,enemylist.Count)].transform.position - transform.position);
        while (Vector3.Distance(targetRotation.eulerAngles, transform.rotation.eulerAngles) > 3)
        {
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        }

        OnFriendlyAttack?.Invoke();
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

        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, range * 2, NavMesh.AllAreas))
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
        gameManager.AddFriendlyList(gameObject);
    }

    public override void RemoveFromList()
    {
        gameManager.RemoveFriendTank(gameObject);
    }
}
