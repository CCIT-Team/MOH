using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Npc
{
    RaycastHit hit;//레이
    public Camera cam;//Npc 눈

    
    public GameObject ghost;
    GameObject npc_ghost;

    [Space]
    public GameObject npc_room;//Npc 자신의 방 
    public GameObject kitchen_room;//Npc가 이동할 주방
    public GameObject toilet_room;//Npc가 이동할 화장실
    public GameObject water_fountain_room;//Npc가 이동할 정수기

    List<float> second_state = new List<float>(1);


    public override void Move()
    {
        this.state = State.Move;
    }

    public override void Select_Personality()
    {
        int a = Random.Range(0, 2);
        if(a == 0)
        {
            this.personality = Npc_Personality.AGGESSIVE;
        }
        else if(a == 1)
        {
            this.personality = Npc_Personality.Defensive;
        }
        //생성하고 Npc가 공격적인지 방어적인지 정해줄거임
        //Start에서 한번만 돌려주자고~
    }

    //스크린 스케일이 변하는 걸 Update로 계속해서 받아와줄지 한번 생각해봐야함;;
    

    
    float sleepy_percent_check
    {
        get
        {
            return sleepy_percent;
        }
        set
        {
            if(value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    //npc_ghost = Instantiate(ghost,new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z),Quaternion.identity); 
                    //npc_ghost.GetComponent<Ghost>().parent_npc = this;
                    //npc_ghost.transform.position = this.transform.position;
                    //npc_ghost.GetComponent<Ghost>().Move_Point(npc_room);
                    //agent.SetDestination(player.transform.position);




                    
                    this.state = State.SLEEP;
                }
                else
                {
                    if (second_state.Count == 0)
                        second_state.Add(sleepy_percent);
                }
            }
            else
            {
                sleepy_percent = value;
            }
        }
    }
    float hungry_percent_check
    {
        get
        {
            return hungry_percent;
        }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                    this.state = State.HUNGRY;
                else
                {
                    if (second_state.Count == 0)
                        second_state.Add(sleepy_percent);
                }
            }
            else
            {
                hungry_percent = value;
            }
        }
    }
    float pee_percent_check
    {
        get
        {
            return pee_percent;
        }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                    this.state = State.PEE;
                else
                {
                    if (second_state.Count == 0)
                        second_state.Add(pee_percent);
                }
            }
            else
            {
                pee_percent = value;
            }
        }
    }
    float thirst_percent_check
    {
        get
        {
            return thirst_percent;
        }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                    this.state = State.THIRST;
                else
                {
                    if (second_state.Count == 0)
                        second_state.Add(thirst_percent);
                }
            }
            else
            {
                thirst_percent = value;
            }
        }
    }

    State state_check
    {
        get
        {
            return this.state;
        }
        set
        {
            switch(value)
            {
                case State.SLEEP:
                    Sleep();
                    break;
                case State.HUNGRY:
                    Hungry();
                    break;
                case State.PEE:
                    Pee();
                    break;
                case State.THIRST:
                    Thirst();
                    break;
            }
        }
    }



    


    private void Sleep()
    {
        //자기 방 침대로 이동
        
        //이동


        //숙면




        //숙면이 끝난 것을 체크


        //퍼센트 게이지 초기화
        if (this.state != State.SLEEP)
        {
            sleepy_percent = 0;
            sleepy_percent_check = sleepy_percent;
            hungry_percent = 0f;
            pee_percent = 0f;
            thirst_percent = 0f;
            this.state = State.Move;
        }


        //나음 게이지중 가장 높은 게이지로 상태 변화
        




        //없으면 Move로 변화 예정

    }
    private void Hungry()
    {
        hungry_percent = 0;
        hungry_percent_check = hungry_percent;

        sleepy_percent = 0f;
        pee_percent = 0f;
        thirst_percent = 0f;
        this.state = State.Move;
    }
    private void Pee()
    {
        pee_percent = 0;
        pee_percent_check = pee_percent;

        sleepy_percent = 0f;
        hungry_percent = 0f;
        thirst_percent = 0f;
        this.state = State.Move;
    }
    private void Thirst()
    {
        thirst_percent = 0;
        thirst_percent_check = thirst_percent;

        sleepy_percent = 0f;
        hungry_percent = 0f;
        pee_percent = 0f;
        this.state = State.Move;
    }

    







    

    void Start()
    {
        this.state = State.IDLE;
        Select_Personality();
        StartCoroutine(State_Gaze_Change());
        player_texture = (Texture2D)player.GetComponent<MeshRenderer>().material.mainTexture;
        //player_texture = player.GetComponent<MeshRenderer>().material.mainTexture;
    }
    Color player_texture_Color;
    Color screen_uv_color;


    public bool Check_Unit()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(player.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }

    void Update()
    {

        state_check = this.state;

        if (Check_Unit())
        {
            if(Physics.Raycast(cam.transform.position,(player.transform.position - cam.transform.position),out hit, Mathf.Infinity))
            {
                Debug.DrawRay(cam.transform.position, (player.transform.position - cam.transform.position) * hit.distance, Color.yellow);
                
                if(hit.transform.gameObject.layer == 6)//player
                {
                    Vector2 player_uv = hit.textureCoord;
                    Vector2 screen_pos = cam.WorldToViewportPoint(player.transform.position);
                    
                    player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
                    screen_uv_color = uv_texture(tex).GetPixel((int)(Screen.width * screen_pos.x),(int)(Screen.height * screen_pos.y));



                    //Debug.Log("Pixel position   :   "+screen_pos.x +"   "+screen_pos.y);

                    //Debug.Log("Color R : " + player_texture_Color.r + " , " + "Color G : " + +player_texture_Color.g + " , " + "Color B : " + +player_texture_Color.b + " , " + "Texture uv Pixel Color");


                    //Debug.Log("Color R : " + screen_uv_color.r + " , " + "Color G : " + + screen_uv_color.g + " , "+"Color B : " + + screen_uv_color.b + " , "+ "Scene uv Pixel Color");
                    ////화면에서 보는 플레이어 컬러

                }
            }
        }
    }
 
    public void Fear_Check()//경계도 100 이상 되엇을 때 
    {
        if (fear_percent == 100)
        {
            this.state = State.REPORT;
            Report_Pattern();
        }
    }

    public void Report_Pattern()//신고 했을 떄 성격 확인
    {
        if (this.personality == Npc_Personality.AGGESSIVE && Check_Unit())
        {
            //오래동안 도둑을 놓치면 다시 신고하러 가도록
        }
        else if(this.personality == Npc_Personality.AGGESSIVE && Check_Unit() == false)
        {
            this.state = State.TRACE;
        }
        ////
        if(this.personality == Npc_Personality.Defensive && Check_Unit())
        {

        }
        else if(this.personality == Npc_Personality.Defensive && Check_Unit() == false)
        {

        }
    }

    public void Go_Report_Zone()
    {

    }


    IEnumerator State_Gaze_Change()
    {
        yield return new WaitForSeconds(2f);
        
        StateGazeUp(this.state);
        ///
        sleepy_percent_check = sleepy_percent;
        hungry_percent_check = hungry_percent;
        pee_percent_check = pee_percent;
        thirst_percent_check = thirst_percent;
        ///

        StartCoroutine(State_Gaze_Change());
        //npc 기절,신고 등의 상태에서 코루틴 꺼주기 생각해보니까 꺼줄필요가 없을 것도 같음
    }

    
 
}
