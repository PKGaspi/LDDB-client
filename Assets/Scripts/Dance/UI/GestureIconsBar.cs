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
    public Color gestureTintColor;

    private const string SPRITE_FOLDER = "Sprites/DanceMoves";
    
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
        // Look for this gesture image.
        Sprite gestureSprite = Resources.Load<Sprite>($"{SPRITE_FOLDER}/{move.gestureName}");
        // Create a UI image gameobject.
        GameObject gestureIcon = new GameObject();
        Image gestureImage = gestureIcon.AddComponent<Image>();
        gestureImage.sprite = gestureSprite;
        gestureImage.color = gestureTintColor;
        // Add gameobject to contents.
        gestureIcon.transform.SetParent(contents);
        // Calculate image postion based on the speed and on move.startTime.
        ((RectTransform) gestureIcon.transform).localPosition = new Vector2(speed * move.startTime, 0);
        gestureIcon.transform.localScale = Vector3.one;
        // Maybe do the same for move.endTime ???
    }
}
