using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 moveDirection;
    float rotatingAngle;
    public Transform[] wheels;
    public float Speed = 0.5f;
    public GameObject maxLocation;
    public GameObject minLocation;



    private float leftMax = 23;
    private float rightMin = -10;
    void Start()
    {
        leftMax = maxLocation.transform.position.z;
        rightMin = minLocation.transform.position.z;
    }

    private void Update()
    {
        if (SimpleInput.GetButton("Left"))
        {
            if (this.transform.position.z < leftMax)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.left* Speed, Time.deltaTime * 5);
                rotatingAngle = Mathf.Lerp(rotatingAngle, 90* Speed, Time.deltaTime * 5);
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
                rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
            }



        }
        else if (SimpleInput.GetButton("Right"))
        {
            if(this.transform.position.z > rightMin)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.right* Speed, Time.deltaTime * 5);
                rotatingAngle = Mathf.Lerp(rotatingAngle, -90 * Speed, Time.deltaTime * 5);
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
                rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
            }

        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
            rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
        }

        this.transform.Translate(moveDirection);

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.forward, rotatingAngle);
        }
    }

}
