using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SplinePositionResetter : MonoBehaviour
{

    
    public void ResetSplinePercent(string objectName)
    {
        EnvironmentManager environmentManager = GetComponent<EnvironmentManager>();
        GameObject resetObject = environmentManager.FindObjectInList(objectName);

        resetObject.GetComponent<SplineFollower>().SetPercent(0);
    }
}
