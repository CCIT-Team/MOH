using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public int id;
    public string item_name;
    public enum itemType
    { DOOR, EQUIPMENT, FURNITURE, TOOL, EMPTYHAND }
    public itemType itemtype;
    public enum item_size_type { SMALL, MIDIUM, DESK }
    public item_size_type item_size;
    public enum parameterType
    { SLEEP, PEE, THIRSTY, PHONE, NONE , DOOR}
    public parameterType parameter_type;
    public int price;
    public float weight;
    public bool bIsMoving, bIsQuestItem, bIsGrab;
    public Vector3 GrabRotation;
    public bool bIsLightOnPlayer;
    public Sprite item_image;

    public Room parent_room;
    //
    [HideInInspector]
    public Transform enter_spot;
    //
    void Awake()
    {
        NpcManager.instance.Sort_Out_Items(this, parameter_type);
    }

    public abstract void interaction();
}