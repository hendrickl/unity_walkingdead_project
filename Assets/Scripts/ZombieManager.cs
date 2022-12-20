using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{

    private NavMeshAgent _agent;

    [SerializeField] private Transform _destination;

    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _destination = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


        var dist = Vector3.Distance(transform.position, _destination.position);

        if (dist > 3.0f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_destination.position);
            _animator.SetBool("isAttacking",false);
        }
        else
        {
// attaque
            _agent.isStopped = true;
            _animator.SetBool("isAttacking",true);
        }
    }
}
