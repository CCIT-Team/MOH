using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_Player : Singleton<p_Player>
{
    [Header("플레이어 프리즈")]
    public bool freeze;
    public bool move;
    [Header("플레이어 스테이터스")]
    public int health;
    public int money { get { return PlayerPrefs.GetInt("pMoney", 0); } }
    public int myung_seong { get { return PlayerPrefs.GetInt("pMyung_Seong"); } }
    public float defaultSpped;
    public float walkingSpeed;
    public float runningSpeed;
    protected float sensitivity = 0.5f;
    public bool lighted;
    public bool isJumping;
    public bool isMoving;

    [Header("Player로 옮겨야할수도있음")]
    public float unlock_default;
    public float unlock_tool;
    public float unlock_skill;
    public float unlock_speed { get { return unlock_default + unlock_tool + unlock_skill; } }
    public float noiseValue;
    public List<Item> itemBag = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Freezing()
    {
        freeze = true;
        move = true;
    }
    public void Melting()
    {
        freeze = false;
        move = false;
    }

    #region Light
    List<Lighting> Lighting = new List<Lighting>();

    public List<Lighting> p_lighting
    {
        get { return Lighting; }
        set { Lighting = value;  }

    }
    public void Lighting_Update()
    {
        if(p_lighting.Count==0)
        {
            lighted = false;
        }
        else
        {
            lighted = true;
        }
    }
    public bool LightCheck(Lighting light)
    {
        foreach (Lighting l in p_lighting)
        {
            if (l == light)
            {
                return true;
            }

        }
        return false;
    }
    public void Enter_Light(Lighting light)
    {
        p_lighting.Add(light);
        Lighting_Update();
    }
    public void Exit_Light(Lighting light)
    {
        foreach (Lighting l in p_lighting)
        {
            if (l == light)
            {
                p_lighting.Remove(l);
                Lighting_Update();
                break;
            }

        }
    }
    #endregion

}
