using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ListMenu : MonoBehaviour
{
    public Transform entriesContainer;
    public int selected = 0;
    public float alphaDiff = .2f;

    // Start is called before the first frame update
    void Start()
    {
        ModifyAlpha(entriesContainer.GetChild(selected).GetComponent<Image>(), alphaDiff);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSelected(int value) {
        // deselect previous entry
        ModifyAlpha(entriesContainer.GetChild(selected).GetComponent<Image>(), -alphaDiff);
        selected = Math.Clamp(value, 0, entriesContainer.childCount);
        ModifyAlpha(entriesContainer.GetChild(selected).GetComponent<Image>(), alphaDiff);
    }

    private void ModifyAlpha(Image img, float ammount) {
        var color = img.color;
        color.a += ammount;
        img.color = color;
    }
}
