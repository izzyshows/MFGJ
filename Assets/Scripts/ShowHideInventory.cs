using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideInventory : MonoBehaviour
{
    public GameObject theInventory;

    void Update()
    {
        if (Input.GetKeyDown("i") && theInventory.activeInHierarchy)
        {
            gameObject.GetComponent<CanvasRenderer>().cull = false;
        }
        if (Input.GetKeyDown("i") && !theInventory.activeInHierarchy)
        {
            theInventory.SetActive(true);
        }
    }
}
