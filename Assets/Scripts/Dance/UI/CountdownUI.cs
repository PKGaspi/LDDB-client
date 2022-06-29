using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountdownUI : MonoBehaviour
{
    public int countFrom = 3;
    public string ceroText = "Dance!";
    public Text countText;
    public UnityEvent countFinishEvent;
    private float timer;
    private int counter;
    private bool counting = false;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (counting) {
            timer -= Time.deltaTime;
            if (timer <= counter) {
                counter--;
                if (counter == 0) {
                    countText.text = ceroText;
                }
                else if (counter < 0) {
                    countText.text = "";
                    counting = false;
                    countFinishEvent.Invoke();
                }
                else {
                    countText.text = counter.ToString();
                }
            }
        }
    }

    public void StartCounter() {
        counter = countFrom;
        timer = countFrom + 1;
        countText.text = counter.ToString();
        counting = true;
    }
}
