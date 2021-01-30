using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveTarget : MonoBehaviour
{
    Quaternion TargetAngle;
    TargetManager manager;

    private void Awake()
    {
        TargetAngle = this.transform.root.transform.rotation;
        manager = this.transform.root.GetComponent<TargetManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        CheckAngle(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckAngle(other);
    }

    private void CheckAngle(Collider other)
    {
        //Debug.Log(other.transform.rotation);
        if (other.name.Equals("Player"))
        {
            if (other.transform.rotation == TargetAngle)
            {
                //Debug.Log("Hit");
                manager.TargetHit(other.gameObject);
            }
        }
    }
}
