using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Color = UnityEngine.Color;

public class TEST_ColourChange : MonoBehaviour
{
    public GameObject image;
    public void ChangeToRed() {
        image.GetComponent<Image>().color = Color.red;
    }

    public void ChangeToBlue() {
        image.GetComponent<Image>().color = Color.blue;
    }

    public void ResetColor() {
        image.GetComponent<Image>().color = Color.white;
    }
}
