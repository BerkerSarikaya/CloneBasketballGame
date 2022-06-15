using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Power : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float addPower;


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(angle, 90, 0) * addPower, ForceMode.Force); 
    }
}
