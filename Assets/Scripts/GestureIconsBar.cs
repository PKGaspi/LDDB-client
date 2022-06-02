using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayData;

public class GestureIconsBar : MonoBehaviour
{
    public bool playing = false;
    public float speed = 100f; // Speed at which the icons move from right to left.
    public RectTransform contents;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing) {
            contents.anchoredPosition = new Vector2(
                contents.anchoredPosition.x - speed * Time.deltaTime,
                contents.anchoredPosition.y
            );
        }
    }
    
    // Receives MoveData move and loads the needed info into the game scene.
    public void LoadMove(MoveData move) {
        // TODO: look for this gesture image (somewhere?)
        // TODO: Create a UI image gameobject
        GameObject gestureIcon = new GameObject();
        Image gestureImage = gestureIcon.AddComponent<Image>();
        // Add gameobject to contents
        gestureIcon.transform.SetParent(contents);
        // TODO: Calculate image postion based on the speed and on move.startTime
        ((RectTransform) gestureIcon.transform).localPosition = new Vector2(speed * move.startTime, 0);
        // Maybe do the same for move.endTime ???
    }
}
