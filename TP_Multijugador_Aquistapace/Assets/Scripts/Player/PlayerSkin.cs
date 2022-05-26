using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSkin : MonoBehaviourPun
{
    [SerializeField] private List<GameObject> hatsList = new List<GameObject>();
    [SerializeField] private List<GameObject> facesList = new List<GameObject>();

    // --------------------------

    private void Awake()
    {
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(RPC_EnableHatSkin), RpcTarget.AllBuffered);
            photonView.RPC(nameof(RPC_EnableFaceSkin), RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_EnableHatSkin()
    {
        for (int i = 0; i < hatsList.Count; i++)
        {
            hatsList[i].SetActive(false);
        }

        int num = Random.Range(0, hatsList.Count);
        hatsList[num].SetActive(true);
    }

    [PunRPC]
    private void RPC_EnableFaceSkin()
    {
        for (int i = 0; i < facesList.Count; i++)
        {
            facesList[i].SetActive(false);
        }

        int num = Random.Range(0, facesList.Count);
        facesList[num].SetActive(true);
    }
}