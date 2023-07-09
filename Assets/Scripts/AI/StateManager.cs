using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public State _currentState;

    [Header("Field Of View Settings")]
    [SerializeField] private float DetectionRadius;
    [SerializeField] private float DetectionAngle;
    private PlayerMovement _player;

    [SerializeField] private State _alertState;
    [SerializeField] private State _panicState;

    public enum NPCType
    {
        Pacifist,
        Aggressive
    }

    public NPCType _Type;

    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        State _nextState = _currentState.RunCurrentState();

        if (_nextState != null) SwitchToNextState(_nextState);

        FieldOfView();

        Debug.Log(_currentState);
    }

    void SwitchToNextState(State nextState)
    {
        _currentState = nextState;
    }

    void FieldOfView()
    {
        Vector2 dir = _player.transform.position - transform.position;
        float angle = Vector3.Angle(dir, transform.up);
        RaycastHit2D r = Physics2D.Raycast(transform.position, dir, DetectionRadius);

        if(angle < DetectionAngle / 2)
        {
            if (r.collider != null)
            {
                if (r.collider.CompareTag("Player"))
                {
                    if(_Type == NPCType.Pacifist)
                    {
                        _currentState = _panicState;
                    }
                    if (_Type == NPCType.Aggressive)
                    {
                        _currentState = _alertState;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {

    }
}
