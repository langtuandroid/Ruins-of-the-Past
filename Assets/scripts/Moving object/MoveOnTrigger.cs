using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Quaternion moveRotation;
    [SerializeField] private float speed;
    [SerializeField] private List<Collider> triggeredBy;
    [SerializeField] private float WPradius = 1;
    private bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(moveDirection, transform.position) < WPradius && Quaternion.Angle(moveRotation, transform.rotation) < WPradius) moving = false;
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, moveRotation, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered movePlatform");
        foreach (Collider c in triggeredBy)
        {
            if (other.name == c.name)
            {
                TriggerMove();
            }
        }
    }

    private void TriggerMove()
    {
        moving = true;
    }
}
