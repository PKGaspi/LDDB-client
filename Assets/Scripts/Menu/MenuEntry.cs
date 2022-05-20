using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntry : MonoBehaviour
{
    public bool selected = false;
    public float alphaDiff = .2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select() {
        if (selected)
            return;
        selected = true;
        ModifyAlpha(GetComponent<Image>(), alphaDiff);
    }
    
    public void Deselect() {
        if (!selected)
            return;
        selected = false;
        ModifyAlpha(GetComponent<Image>(), -alphaDiff);
    }

    
    private void ModifyAlpha(Image img, float ammount) {
        var color = img.color;
        color.a += ammount;
        img.color = color;
    }
}
