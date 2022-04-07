using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCard : MonoBehaviour
{
    public Mission mission;
    public InfoCard ic;
    public WhiteBoard mi;
    public InformaionPopup ip;

    void OnMouseEnter()
    {
        mission.InfoCard_Update(ic);
        ic.gameObject.SetActive(true);
    }

    void OnMouseDown()
    {
        mission.WhiteBoard_Update(mi);
        mission.InfoPopup_Update(ip);
    }

    void OnMouseExit()
    {
        ic.gameObject.SetActive(false);
    }

    //void 
}
