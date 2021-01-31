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

    bool hit = false;

    private void CheckAngle(Collider other)
    {
        //Debug.Log(other.transform.rotation);
        if (other.name.Equals("Player"))
        {
            Debug.DrawLine(other.transform.position, other.transform.position + (other.transform.forward * 5), Color.blue);
            Debug.DrawLine(this.transform.position, other.transform.position, Color.green);

            //Vector3 t = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            //Vector3 o = new Vector3(other.transform.position.x, 0, other.transform.position.z);
            float distance = (this.transform.position - other.transform.position).magnitude;

            float dot = Vector3.Dot(this.transform.forward, other.transform.Find("Main Camera").forward);
            if (!hit)
            {
                Debug.Log("Distance: " + distance);
                Debug.Log("Dot: " + dot);
            }
            if (distance > 0.12f)
            {
                return;
            }

            //if (other.transform.rotation == TargetAngle)
            if (dot >= 0.90)
            {
                hit = true;
                //Debug.Log("Hit");
                manager.TargetHit(other.gameObject);
            }
        }
    }
}
