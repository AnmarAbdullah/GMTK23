using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleWander : State
{
    private float _timer;
    private float dist;
    private bool _wandering;
    [HideInInspector] public bool _isAlerted;
    private Vector2 _randomPosition;
    private NavMeshAgent navmesh;
    
    [Header("Wander Settings")]
    public float _wanderCoolDown;
    public float _wanderDistance;
    public float _walkSpeed;

    public State _alert;

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        navmesh.updateRotation = false;
        navmesh.updateUpAxis = false;
    }

    public override State RunCurrentState()
    {
        dist = Vector2.Distance(transform.position, _randomPosition);

        if (!_wandering) _timer += Time.deltaTime;
        
        if (_timer > _wanderCoolDown)
        {
            _randomPosition = SetWanderPosition();
            _timer= 0;
        }

        if (_wandering)
        {
            //transform.position = Vector2.MoveTowards(transform.position, _randomPosition, _walkSpeed * Time.deltaTime);
            navmesh.SetDestination(_randomPosition);
            if (dist < 0.01f) { _wandering = false; _randomPosition = Vector2.zero; }
        }
        Debug.Log(_isAlerted);
        if (_isAlerted)
        {
            _isAlerted = false;
            return _alert;
        }

        return this;
    }

    public Vector2 SetWanderPosition()
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

        _randomPosition = Random.insideUnitCircle * _wanderDistance;

        _wandering = true;

        return playerPos += _randomPosition;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(_randomPosition, 0.5f);
    }
}
