using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaeraMove : MonoBehaviour
{
     float cameraMove = 2f;
     // float cameraRotate = 100f;
    Rigidbody cameraRb;

    // Start is called before the first frame update
    void Start()
    {
        cameraRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        // �������� ���� ���� ���� ����
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
            cameraRb.AddForce(moveDirection * cameraMove);
        }
        else
        {
            // �������� ���� �� ����
            cameraRb.velocity = Vector3.zero;
        }

        /*** ī�޶� ȸ�� - Ű�� ���� �� ȸ���� ���ߵ��� ����
        if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            cameraRb.angularVelocity = Vector3.zero;
        }
        else
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up, -cameraRotate * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up, cameraRotate * Time.deltaTime);
            }
        } ***/
    }
}