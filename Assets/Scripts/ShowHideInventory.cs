using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideInventory : MonoBehaviour
{
    public GameObject theInventory;
    //private bool isActive;

    void Update()
    {
        if (Input.GetKeyDown("i") && theInventory.activeInHierarchy)
        {
            theInventory.SetActive(false);
        }
        if (Input.GetKeyDown("i") && !theInventory.activeInHierarchy)
        {
            theInventory.SetActive(true);
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        if (isActive)
    //        {
    //            isActive = false;
    //        }
    //        else
    //        {
    //            isActive = true;
    //        }

    //        theInventory.SetActive(isActive);
    //    }

    //}
}
