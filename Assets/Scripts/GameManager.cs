using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    
    
    [SerializeField] private GameObject _prefab;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private int _nombreDeZombies;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
        
        InstantiateXobjectsRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InstantiateObject()
    {
    // Instantiate zombie dans la scene
    GameObject.Instantiate(_prefab);
    }
    
    public void InstantiateObjectAtSpawnPoint()
    {
        // Instantiate zombie dans la scene
        GameObject.Instantiate(_prefab,_spawnPoint.position,_spawnPoint.rotation);
    }
    
    public void InstantiateObjectAtPosition(int x, int z)
    {
        // Instantiate zombie dans la scene
        Debug.Log("Instantiate Object At Position");
        GameObject.Instantiate(_prefab,new Vector3(x,0,z),Quaternion.identity);
    }

    public void InstantiateXobjectsRandomly()
    {

        for (int i=0; i < _nombreDeZombies; i++)
        {
            int a;
            int b;
            a = Random.Range(-50, 50);
            b= Random.Range(-50, 50);
            InstantiateObjectAtPosition(a,b);
        }

    }


    private void OnSpawnZombie()
    {
        InstantiateObjectAtSpawnPoint();
    }
}
