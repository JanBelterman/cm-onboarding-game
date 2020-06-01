using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextLVL : MonoBehaviour
{
    public GameObject nextlvl;
    public GameObject seconfloor;
    public GameObject thirdFloor;

    void Start()
    {
        seconfloor.SetActive(false);
        thirdFloor.SetActive(false);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "TestPlayer")
        {
            if(nextlvl.activeSelf)
            {
                nextlvl.SetActive(false);
            }
            else
            {
                nextlvl.SetActive(true);
            }

        }
    }
}
