using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image crownHostImage;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
    }

    public void ActivateCrown(bool isHost)
    {
        crownHostImage.enabled = isHost;
    }
}