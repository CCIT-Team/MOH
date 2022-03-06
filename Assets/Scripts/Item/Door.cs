using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public bool isLock;
    Rigidbody rg;
    float value;
    public float sensitivity = 10f;
    public enum DoorDir { FRONT, BACK}
    public DoorDir doorDir;
    public GameObject door;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    public override void interaction()
    {
        if(isLock)
        {
            Debug.Log("잠겨있어요");
        }
        else if(!isLock)
        {
            switch(doorDir)
            {
                case DoorDir.FRONT:
                    value += Input.GetAxis("Mouse Y") * sensitivity;
                    break;
                case DoorDir.BACK:
                    value -= Input.GetAxis("Mouse Y") * sensitivity;
                    break;
            }
            door.transform.rotation = Quaternion.Euler(0, transform.rotation.y - value, 0);
        }
    }

    //test code 안드로이드 빌드 제한은 추후에 (VR외 로 사용금지)
    public  void interaction(Vector2 vrinput)
    {
        if (isLock)
        {
            Debug.Log("잠겨있어요");
        }
        else if (!isLock)
        {
            switch (doorDir)
            {
                case DoorDir.FRONT:
                    value += vrinput.y * sensitivity;
                    break;
                case DoorDir.BACK:
                    value -= vrinput.y * sensitivity;
                    break;
            }
            door.transform.rotation = Quaternion.Euler(0, transform.rotation.y - value, 0);
        }
    }
}
