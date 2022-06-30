using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugHotkeys : MonoBehaviour
{
    public DancePlayer dancePlayer;

    public void EndDance(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }

        dancePlayer.End();
    }
}
