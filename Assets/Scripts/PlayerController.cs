using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool InvertX;
    [SerializeField] private bool InvertY;
    [SerializeField] private float maxAngle;
    [SerializeField] private float speed;
    [SerializeField] private float recoverSpeed;
    private float xSpeed;
    private float ySpeed;
    private Vector3 rotationAngle;
    private Rigidbody terrainRigidBody;

    private void Start()
    {
        terrainRigidBody = GetComponent<Rigidbody>();
        rotationAngle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform);
    }

    private void Update()
    {
        xSpeed = Input.GetAxis("Horizontal");
        ySpeed = Input.GetAxis("Vertical");

        rotationAngle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform);
    }

    private void FixedUpdate()
    {
        if (xSpeed < 0 && rotationAngle.x > -maxAngle)
        {
            // Quaternion deltaRotation = Quaternion.Euler(new Vector3(xSpeed * speed, 0, 0) * Time.deltaTime);
            // terrainRigidBody.MoveRotation(terrainRigidBody.rotation * deltaRotation);
            RotateRigidBodyAroundPoint(Vector3.left, new Vector3(xSpeed, 0, ySpeed));
        }

        if (xSpeed > 0 && rotationAngle.x < maxAngle)
        {
            // Quaternion deltaRotation = Quaternion.Euler(new Vector3(xSpeed * speed, 0, 0) * Time.deltaTime);
            // terrainRigidBody.MoveRotation(terrainRigidBody.rotation * deltaRotation);
            RotateRigidBodyAroundPoint(Vector3.right, new Vector3(xSpeed, 0, ySpeed));
        }

        if (ySpeed < 0 && rotationAngle.z > -maxAngle)
        {
            // Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, ySpeed * speed) * Time.deltaTime);
            // terrainRigidBody.MoveRotation(terrainRigidBody.rotation * deltaRotation);
            RotateRigidBodyAroundPoint(Vector3.back, new Vector3(xSpeed, 0, ySpeed));
        }

        if (ySpeed > 0 && rotationAngle.z < maxAngle)
        {
            // Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, ySpeed * speed) * Time.deltaTime);
            // terrainRigidBody.MoveRotation(terrainRigidBody.rotation * deltaRotation);
            RotateRigidBodyAroundPoint(Vector3.forward, new Vector3(xSpeed, 0, ySpeed));
        }

        if (xSpeed == 0 && ySpeed == 0)
        {
            terrainRigidBody.MoveRotation(
                Quaternion.Lerp(
                    terrainRigidBody.rotation, Quaternion.identity, recoverSpeed * Time.deltaTime
                )
            );
        }
    }

    public void RotateRigidBodyAroundPoint(Vector3 direction, Vector3 point)
    {
        Quaternion q = Quaternion.AngleAxis(speed * Time.deltaTime, direction);
        terrainRigidBody.MovePosition(q * (terrainRigidBody.transform.position - point) + point);
        terrainRigidBody.MoveRotation(terrainRigidBody.transform.rotation * q);
    }
}