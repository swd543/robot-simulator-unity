using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo : MonoBehaviour
{
    public Rigidbody RobotRigidBody;
    public Transform RobotRigidTransform;
    private int force = 100;
    private int torque = 10;
    public Camera chase, top;
    public Transform leftTire, rightTire;
    private uint cameras = 0x01;
    private float width;
    private float vl=0, vr = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.width = Vector3.Distance(leftTire.position, rightTire.position);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RobotRigidBody.AddRelativeForce(0, 0, force * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            RobotRigidBody.AddRelativeForce(0, 0, -force * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RobotRigidBody.AddRelativeTorque(0,-torque * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RobotRigidBody.AddRelativeTorque(0, torque * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.X))
        {
            RobotRigidBody.angularVelocity = Vector3.zero;
            RobotRigidBody.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            top.enabled = (cameras & (1 << 0 - 1)) != 0;
            chase.enabled = (cameras & (1 << 1 - 1)) != 0;
            cameras = ~cameras;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.T))
        {
            var mag=RobotRigidTransform.TransformVector(new Vector3(0, 0, 0.01f));
            RobotRigidBody.velocity = mag;
        }
        if (Input.GetKey(KeyCode.G))
        {
            var mag = RobotRigidTransform.InverseTransformVector(new Vector3(0, 0, -10));
            RobotRigidBody.velocity = mag;
        }
        if (Input.GetKey(KeyCode.L))
        {
            vr--;
        }
        if (Input.GetKey(KeyCode.C))
        {
            RobotRigidBody.AddForce(1, 0, 0);
        }
        var x = RobotRigidTransform.InverseTransformVector(RobotRigidBody.velocity);
        RobotRigidBody.velocity=RobotRigidTransform.TransformVector(new Vector3(0, x.y, x.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
