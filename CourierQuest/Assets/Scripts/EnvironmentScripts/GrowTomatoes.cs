using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTomatoes : MonoBehaviour
{
    public List<GameObject> tomatoes;

    public float maxScale = 8;
    public float scaleSpeed = 1f;
    public float maxTime = 8;
    [SerializeField] private float timer = 0;

    public void Update()
    {
        timer += Time.deltaTime;
        
        if (timer <= maxTime)
        {
            ScaleUp();
        }
    }

    private void ScaleUp()
    {
        for (int i = 0; i < tomatoes.Count; i++)
        {
            float newScale = Mathf.Lerp(tomatoes[i].transform.localScale.x, maxScale, Time.deltaTime * scaleSpeed);
            tomatoes[i].transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
