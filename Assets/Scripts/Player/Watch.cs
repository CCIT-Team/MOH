using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{

    /// 
    /// 일단 시계 적용된 능력 반영되야됨
    /// 추가로 카메라기능들 구현해야됨 쉐이더로 해결???
    /// 아이템아래로 내리기엔 좀그런데 따로 만들어야되나?
    ///
    public int maximum_shotting;
    public int current_shot_count;
    public int max_watch_mode = 3;
    [Range(0, 3)]
    public int currnet_watch_mode=0;
    public Transform shoting_position;
    public Transform lazer_position;

    public GameObject stun_bullet;
    public GameObject sleep_bullet;
    public GameObject[] cam_bullet;

    Material Glass;
    public GameObject watch_material;
 

    private bool cam_1, cam_2, cam_3;
    public enum Shot_mode
    {
        SLEEP,
        STUN,
        CAM
    }
    public Shot_mode watch_mode;
    // Start is called before the first frame update
    void Awake()
    {
        Glass = watch_material.GetComponent<MeshRenderer>().material;
    }
    public void Interaction_shot()
    {
        if (Glass.GetInt("_Current_View") == 0)
        {


            RaycastHit target;
            bool taregting = false;

            Physics.Raycast(lazer_position.position, lazer_position.forward, out target, 100);
            if (target.point != null)
            {
                taregting = true;
                Debug.Log("Targeting");
            }
            GameObject bullet = null;
            if (current_shot_count <= maximum_shotting)
            {


                switch (watch_mode)
                {
                    case Shot_mode.SLEEP:
                        //Shot ㅋㅋ
                        break;
                    case Shot_mode.STUN:
                        //Shot ㅋㅋ
                        break;
                    case Shot_mode.CAM:
                        bullet = cam_bullet[current_shot_count];
                        switch (current_shot_count)
                        {
                            case 0:
                                cam_1 = true;
                                Glass.SetInt("_Camera1", 1);
                                break;
                            case 1:
                                cam_2 = true;
                                Glass.SetInt("_Camera2", 1);
                                break;
                            case 2:
                                cam_3 = true;
                                Glass.SetInt("_Camera3", 1);
                                break;
                        }

                        break;
                }

                GameObject i_bullet = Instantiate(bullet, shoting_position.position, shoting_position.rotation);
                if (taregting)
                {
                    Debug.Log("Targetting");
                    i_bullet.transform.LookAt(target.point);
                }
                i_bullet.GetComponent<Bullet>().Burst();
                current_shot_count += 1;
            
            }
        }
    }
    public void Interaction_change()
    {
        if (currnet_watch_mode != max_watch_mode)
        {
            switch (currnet_watch_mode)
            {
                case 0:
                    if(!cam_1)
                    {
                        currnet_watch_mode = 0;
                    }
                    else
                    {
                        currnet_watch_mode+= 1;
                    }
                    break;
                case 1:
                    if (!cam_2)
                    {
                        currnet_watch_mode = 0;
                    }
                    else
                    {
                        currnet_watch_mode += 1;
                    }
                    break;
                case 2:
                    if (!cam_3)
                    {
                        currnet_watch_mode = 0;
                    }
                    else
                    {
                        currnet_watch_mode += 1;
                    }
                    break;
                case 3:

                    currnet_watch_mode = 0;
                    break;
            }
        }
        else
        {
            currnet_watch_mode = 0;
        }
        Glass.SetInt("_Current_View", currnet_watch_mode);
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        Debug.DrawRay(lazer_position.position, lazer_position.forward, Color.black);
        if (Input.GetKeyDown(KeyCode.Tab))
       {
            
            
            Interaction_change();
       }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {


            Interaction_shot();
        }
#endif
    }
}
