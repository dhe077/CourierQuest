using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPackages : MonoBehaviour
{
    // List of objects to spawn
    public List<GameObject> firstPackages;
    public List<GameObject> secondPackages;
    public List<GameObject> thirdPackages;

    public Queue<GameObject> allPackages;

    private void Start()
    {
        allPackages = new Queue<GameObject>();
    }

    private int index = 0;

    public Vector3 position1;
    public Vector3 position2;
    public Vector3 position3;
    public Vector3 position4;

    private Quaternion noRotation = Quaternion.identity;


    // The story commands need to be able to access the following functions through the environment manager
    public void SpawnPackageObjects()
    {
        if (index == 0)
        {
            GameObject smallPackage = Instantiate(firstPackages[0], position2, noRotation);
            GameObject largePackage = Instantiate(firstPackages[1], position3, noRotation);

            allPackages.Enqueue(smallPackage);
            allPackages.Enqueue(largePackage);
            index += 1;
        }
        else if (index == 1)
        {
            DestroyPackage();
            DestroyPackage();
            GameObject roundPackage = Instantiate(secondPackages[0], position1, noRotation);
            GameObject squarePackage = Instantiate(secondPackages[1], position2, noRotation);
            GameObject trianglePackage = Instantiate(secondPackages[2], position3, noRotation);
            allPackages.Enqueue(roundPackage);
            allPackages.Enqueue(squarePackage);
            allPackages.Enqueue(trianglePackage);
            index += 1;
        }
        else if (index == 2)
        {
            DestroyPackage();
            DestroyPackage();
            DestroyPackage();
            GameObject redPackage = Instantiate(thirdPackages[0], position1, noRotation);
            GameObject greenPackage = Instantiate(thirdPackages[1], position2, noRotation);
            GameObject bluePackage = Instantiate(thirdPackages[2], position3, noRotation);
            GameObject yellowPackage = Instantiate(thirdPackages[3], position4, noRotation);

            allPackages.Enqueue(redPackage);
            allPackages.Enqueue(greenPackage);
            allPackages.Enqueue(bluePackage);
            allPackages.Enqueue(yellowPackage);
            index += 1;
        }
    }

    private void DestroyPackage()
    {
        GameObject package = allPackages.Dequeue();
        Destroy(package);
        
    }

}
