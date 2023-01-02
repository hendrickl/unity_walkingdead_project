using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    private bool _isEquiped = false;
    [SerializeField] private Transform _gunHoldPoint;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _prefabBullet;
    [SerializeField] private int _force;
    [SerializeField] private InputActionReference _shootAction;

    private void Start()
    {
        _shootAction.action.Enable();
        _shootAction.action.performed += Shoot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isEquiped = true;

            // Locate and rotate shotgun
            transform.position = _gunHoldPoint.position;
            transform.rotation = _gunHoldPoint.rotation;

            transform.SetParent(_gunHoldPoint);
        }
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if (_isEquiped)
        {
            Debug.Log("Fired !");

            GameObject bullet = Instantiate(_prefabBullet, _spawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(_spawnPoint.forward * _force, ForceMode.Impulse);
            Destroy(bullet, 2.0f);
        }
    }
}
