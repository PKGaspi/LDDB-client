using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KinectController : MonoBehaviour
{
    public KinectManager manager;
    public int ammount = 1;
    public void AngleAdd(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }
        Vector2 value = (Vector2) context.ReadValueAsObject();
        manager.SensorAngle += (int) (ammount * value.y);
        KinectWrapper.NuiCameraElevationSetAngle(manager.SensorAngle);
        Debug.Log($"Set Sensor Angle to {manager.SensorAngle}");
    }
}
