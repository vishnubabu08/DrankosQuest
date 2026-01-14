using UnityEngine;

public class MobileControl : MonoBehaviour
{
    public Joystick joystick;
    public playermovement playermovement;
    void Update()
    {
        playermovement.horizontalInput = joystick.Horizontal;
    }
}
