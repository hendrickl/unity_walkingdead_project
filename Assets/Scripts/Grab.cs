using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private float _rayLength;
    [SerializeField] private float _grabForce;
    [SerializeField] private float _throwForce;
    [SerializeField] private Transform _holdPosition;
    private Rigidbody _grabbedObject;
    private bool _isGrabbed = false;

    void FixedUpdate()
    {
        if (_grabbedObject)
        {
            // _grabbedObject.AddForce((transform.GetChild(0).position - _grabbedObject.transform.position) * _grabForce, ForceMode.Impulse);
            _grabbedObject.velocity = (_holdPosition.position - _grabbedObject.transform.position) * _grabForce;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.GetChild(0).position, transform.GetChild(0).forward * _rayLength);
    }

    private void OnGrab()
    {
        if (!_isGrabbed)
        {
            ShootRayCast();
            _isGrabbed = true;

            if (_grabbedObject)
            {
                _grabbedObject.transform.parent = transform;
            }
        }
        else
        {
            if (_grabbedObject)
            {
                _grabbedObject.transform.parent = null;
            }
            _grabbedObject = null;
            _isGrabbed = false;
        }
    }

    private void OnThrowGrabbed()
    {
        Debug.Log("Object thrown");

        if (_grabbedObject)
        {
            _grabbedObject.AddForce(_holdPosition.forward * _throwForce);
            DropGrabbedObject();
        }
    }

    private void DropGrabbedObject()
    {
        _grabbedObject.transform.parent = null;
        _grabbedObject = null;
    }


    private void ShootRayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.GetChild(0).position, transform.GetChild(0).forward, out hit, _rayLength))
        {
            Debug.Log("Le rayon a croisé un objt " + hit.transform.name);
            if (hit.transform.CompareTag("Grabbable"))
            {
                _grabbedObject = hit.rigidbody;
            }
        }
    }
}
