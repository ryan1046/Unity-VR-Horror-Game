using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovePlayer : MonoBehaviour
{

    public SteamVR_Action_Vector2 moveValue;
    public float maxSpeed;
    public float sensititvity;
    public Rigidbody head;

    public float distanceFromObjects;


    private float speed = 0.0f;





    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (head.SweepTest(Player.instance.hmdTransform.TransformDirection(Vector3.forward), out hit, distanceFromObjects))
        {

        }
        else
        {
            if (moveValue.axis.y > 0)
            {
                Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, moveValue.axis.y));
                speed = moveValue.axis.y * sensititvity;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);

            }
            if (moveValue.axis.y < 0)
            {
                Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, moveValue.axis.y));
                speed = moveValue.axis.y * sensititvity;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.down);

            }
        }

      

    }
}
