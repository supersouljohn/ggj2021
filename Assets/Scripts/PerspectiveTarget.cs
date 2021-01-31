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

    private void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + (this.transform.forward * 5), Color.white);
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
            Debug.DrawLine(other.transform.position, other.transform.position + (other.transform.forward * 5), Color.blue);

            float distance = (this.transform.position - other.transform.position).magnitude;
            //Debug.Log("Distance: " + distance);
            if (distance > 0.9f)
            {
                return;
            }
            float dot = Vector3.Dot(this.transform.forward, other.transform.Find("Main Camera").forward);
            //Debug.Log("Dot: " + dot);

            //if (other.transform.rotation == TargetAngle)
            if (dot >= 0.995)
            {
                //Debug.Log("Hit");
                manager.TargetHit(other.gameObject);
            }
        }
    }
}
