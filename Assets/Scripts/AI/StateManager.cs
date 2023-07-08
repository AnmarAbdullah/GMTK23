using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State _currentState;

    [Header("Field Of View Settings")]
    [SerializeField] private float DetectionRadius;
    [SerializeField] private float DetectionAngle;
    private PlayerAlert _player;

    enum NPCType
    {
        Pacifist,
        Aggressive
    }

    void Start()
    {
        _player = FindObjectOfType<PlayerAlert>();
    }

    void Update()
    {
        State _nextState = _currentState.RunCurrentState();

        if (_nextState != null) SwitchToNextState(_nextState);

        FieldOfView();
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
                    Debug.Log("Gay Detected");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {

    }
}
