using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRController : MonoBehaviour
{

    public float m_Gravity = 30.0f;
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;

    public float m_RotateIncrement = 90;

    public SteamVR_Action_Boolean m_RotatePress = null;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    public float m_Speed = 0.0f;

    private CharacterController m_CharacterController = null;

    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();

        
    }


    // Start is called before the first frame update
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;

    }

    // Update is called once per frame
    private void Update()
    {

    //  HandleHead();
        HandleHeight();
        CalculateMovement();
        SnapRotation();
     



    }


    /*private void HandleHead()
    {
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f);

        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;

    }
    */
    private void HandleHeight()
    {

        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);


        m_CharacterController.height = headHeight;



        Vector3 newCenter = Vector3.zero;

        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;


        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;




        m_CharacterController.center = newCenter;




    }



    private void CalculateMovement()
    {

        Quaternion orientation = CalaculateOrientation();
        Vector3 movement = Vector3.zero;

        //if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
            if (m_MoveValue.axis.magnitude == 0)
        {
            m_Speed = 0;
        }

       /* if(m_MovePress.state) */
       // {

            m_Speed += m_MoveValue.axis.magnitude * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);


            movement += orientation * (m_Speed * Vector3.forward);


       // }

        //Gravity
        movement.y -= m_Gravity * Time.deltaTime;


        m_CharacterController.Move(movement * Time.deltaTime);



    }

    public Quaternion CalaculateOrientation()
    {
        float rotation = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;


        Vector3 oritentationEuler = new Vector3(0, m_Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(oritentationEuler);




    }


    private void SnapRotation()
    {
        float snapValue = 0.0f;
       if(m_RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            snapValue = -Mathf.Abs(m_RotateIncrement);

        }
        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            snapValue = Mathf.Abs(m_RotateIncrement);

        }


        transform.RotateAround(m_Head.position, Vector3.up, snapValue);


    }

  




}
