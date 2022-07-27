using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    [SerializeField] private Transform EndPoint;

    private NavMeshAgent _navMeshAgent;
    private HealthOfNPC _healthOfNPC;
    private SeachEnemy _seachEnemy;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthOfNPC = GetComponent<HealthOfNPC>();
        _seachEnemy = GetComponent<SeachEnemy>();
    }

    void Update()
    {
        if(_seachEnemy.Target.IsDead || !_seachEnemy.Target || _healthOfNPC.IsDead)
        {
            return;
        }

        GetComponent<Animator>().SetBool("OnOffMeshLink", _navMeshAgent.isOnOffMeshLink);

        _navMeshAgent.SetDestination(_seachEnemy.Target.transform.position);
    }
}
