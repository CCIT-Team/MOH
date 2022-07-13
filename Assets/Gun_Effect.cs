using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Effect : MonoBehaviour
{
    public Animator ani;
    public GameObject gun_effect;
    public Transform effect_trans;

    /// <summary>
    /// 
    /// </summary>
    readonly int Shot = Animator.StringToHash("Shot");


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject gun_light;
    public void Gun_Light_On()
    {
        if(gun_light == null)
        gun_light = Instantiate(gun_effect, effect_trans.position , Quaternion.Euler(0, 90, 0));
        gun_light.transform.parent = effect_trans;
        //Debug.Log(23);
        ani.SetTrigger(Shot);
        Invoke("Destroy_Light",0.1f);
    }

    void Destroy_Light()
    {
        Destroy(gun_light);
    }
}
