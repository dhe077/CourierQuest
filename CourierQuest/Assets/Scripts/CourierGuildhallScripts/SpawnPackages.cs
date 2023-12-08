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

    private Vector3 position1 = new Vector3(147, 5, 70);
    private Vector3 position2 = new Vector3(152, 5, 62);
    private Vector3 position3 = new Vector3(160, 5, 36);
    private Vector3 position4 = new Vector3(162, 5, 26);

    private Quaternion noRotation = Quaternion.identity;


    // The story commands need to be able to access the following functions through the environment manager
    public void SpawnPackageObjects()
    {
        if (index == 0)
        {
            GameObject smallPackage = Instantiate(firstPackages[0], position2, RandomRotation());
            GameObject largePackage = Instantiate(firstPackages[1], position3, RandomRotation());

            allPackages.Enqueue(smallPackage);
            allPackages.Enqueue(largePackage);
            index += 1;
        }
        else if (index == 1)
        {
            DestroyPackage();
            DestroyPackage();
            GameObject roundPackage = Instantiate(secondPackages[0], position1, RandomRotation());
            GameObject squarePackage = Instantiate(secondPackages[1], position2, RandomRotation());
            GameObject trianglePackage = Instantiate(secondPackages[2], position3, RandomRotation());
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
            GameObject redPackage = Instantiate(thirdPackages[0], position1, RandomRotation());
            GameObject greenPackage = Instantiate(thirdPackages[1], position2, RandomRotation());
            GameObject bluePackage = Instantiate(thirdPackages[2], position3, RandomRotation());
            GameObject yellowPackage = Instantiate(thirdPackages[3], position4, RandomRotation());

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

    private Quaternion RandomRotation()
    {
        System.Random rnd = new System.Random();
        int yRotation = rnd.Next(0, 180);
        // int xRotation = rnd.Next(0, 180);
        Quaternion newRotation = Quaternion.Euler(0, yRotation, 0);
        return newRotation;
    }
}
