using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    public override void interaction()
    {
        Debug.Log("테스트 아이템 사용");
    }

    public bool interact_obj = false;
    private void Awake()
    {
        if(interact_obj == true && parent_room != null)
        {
            parent_room.GetComponent<Room>().target_item = this.gameObject;
        }
    }
}
