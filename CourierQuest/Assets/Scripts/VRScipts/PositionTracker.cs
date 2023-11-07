using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PositionTracker : MonoBehaviour
{
    public InputDevice head;

    void Update()
    {
        if (!head.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref head);
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputChar, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(inputChar, devices);

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }
}