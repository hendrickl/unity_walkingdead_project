using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private GameObject _ragdollPrefab;
    [SerializeField] private List<AudioClip> _sounds;

    private AudioSource _audioSource;
    private NavMeshAgent _agent;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _destination = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sounds[Random.Range(0, _sounds.Count - 1)];
        _animator.SetFloat("offset", Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(transform.position, _destination.position);

        if (dist > 3.0f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_destination.position);
            _animator.SetBool("isAttacking", false);
        }
        else
        {
            // attaque
            _agent.isStopped = true;
            _animator.SetBool("isAttacking", true);
        }

        // audiosource
        if (!_audioSource.isPlaying)
        {
            int randomNumber;
            randomNumber = Random.Range(1, 1000);
            if (randomNumber > 990)
            {
                _audioSource.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision avec : " + collision.gameObject.name);

        if (collision.collider.CompareTag("Grabbable"))
        {
            Debug.Log("Collision avec : " + collision.gameObject.name);
            Instantiate(_ragdollPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
