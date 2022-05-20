using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MenuList : MonoBehaviour
{
    public Transform entriesContainer;
    public int selected = 0;
    public float alphaDiff = .2f;

    // Start is called before the first frame update
    void Start()
    {
        entriesContainer.GetChild(selected).GetComponent<MenuEntry>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSelected(int value) {
        // deselect previous entry
        entriesContainer.GetChild(selected).GetComponent<MenuEntry>().Deselect();
        selected = Math.Clamp(value, 0, entriesContainer.childCount);
        entriesContainer.GetChild(selected).GetComponent<MenuEntry>().Select();
    }

}
