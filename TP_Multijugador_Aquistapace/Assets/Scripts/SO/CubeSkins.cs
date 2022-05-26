using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeSkins", menuName = "ScriptableObjects/CubeSkins", order = 1)]
public class CubeSkins : ScriptableObject
{
    public List<GameObject> hatsList = new List<GameObject>();
    public List<GameObject> faceList = new List<GameObject>();

    // ---------------------

    public GameObject ReturnRandomHat()
    {
        int num = Random.Range(0, hatsList.Count-1);
        return hatsList[num];
    }
    
    public GameObject ReturnRandomFace()
    {
        int num = Random.Range(0, faceList.Count-1);
        return faceList[num];
    }
}