using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Blend");

    private NavMeshAgent _agent;
    [SerializeField] Transform target;

    [SerializeField] LayerMask mask;
    
    [SerializeField] private Animator _animator;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        
        float normalizedSpeed = _agent.velocity.magnitude / _agent.speed; //0 / 3.5 = 0 - 1.75 / 3.5 --- 3.5/ 3.5 = 1
        
        if (_agent.hasPath)
        {
            _animator.SetFloat(Speed, normalizedSpeed); // 0...1
        }
        else
        {
            _animator.SetFloat(Speed, 0);
        }
        
    }

    public void setTarget()
    {
        _agent.SetDestination(target.position);
    }
}
