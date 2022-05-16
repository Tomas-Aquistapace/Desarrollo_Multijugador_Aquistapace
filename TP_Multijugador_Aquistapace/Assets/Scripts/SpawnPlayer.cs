using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform[] spawnPlayers;


    private void Start()
    {
        int spawnNum = Random.Range(0, spawnPlayers.Length-1);
        PhotonNetwork.Instantiate(playerPref.name, spawnPlayers[spawnNum].position, Quaternion.identity);
    }
    
}
