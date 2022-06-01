using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuEntry : MonoBehaviour
{
    public bool selected = false;
    public float alphaDiff = .2f;
    public Text mainText;
    public UnityEvent activationEvent;
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

    public void Activate() {
        Debug.Log("Activated entry " + mainText.text);
        activationEvent.Invoke();
    }

    
    private void ModifyAlpha(Image img, float ammount) {
        var color = img.color;
        color.a += ammount;
        img.color = color;
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
