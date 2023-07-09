using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Alert : State
{
    [SerializeField] private float _timer;
    [SerializeField] private float _alertLimit;

    [SerializeField] private State _Idle;

    private StateManager _stateManager;

    private PlayerMovement pl;
    private NavMeshAgent navMesh;
    private bool setPos;

    [SerializeField] private State _idleWander;

    void Start()
    {
        _stateManager = GetComponent<StateManager>(); 
        pl = FindObjectOfType<PlayerMovement>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override State RunCurrentState()
    {
        if (!setPos)
        {
            Vector3 playerPos = pl.transform.position;
            navMesh.SetDestination(playerPos);
            setPos = true;
        }
        else
        {
            _timer += Time.deltaTime;
            if(_timer >= _alertLimit)
            {
                _timer = 0;
                
                return _idleWander;
            }    
        }
        return this;
    }
}
