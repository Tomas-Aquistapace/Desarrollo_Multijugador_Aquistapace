using UnityEngine;
using Photon.Pun;

public class SpawnPlayerManager : MonoBehaviourPun
{
    [Header("Cube")]
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform[] spawnPlayers;

    [Header("UI")]
    [SerializeField] private GameObject playerUIPref;
    [SerializeField] private Transform scoresLayer;

    [Header("Skins")]
    [SerializeField] private CubeSkins cubeSkins;

    private GameObject plGO;
    private GameObject uiGO;

    private void Start() // Acá se instancian los players, sus skins y sus scores
    {
        int spawnNum = Random.Range(0, spawnPlayers.Length - 1);
        plGO = PhotonNetwork.Instantiate(playerPref.name, spawnPlayers[spawnNum].position, Quaternion.identity);

        uiGO = PhotonNetwork.Instantiate(playerUIPref.name, scoresLayer.transform.position, Quaternion.identity);
        uiGO.GetComponent<UI_PlayerScore>().SetName(PhotonNetwork.LocalPlayer.NickName);
        
        plGO.GetComponent<PlayerMovement>().playerScore = uiGO.GetComponent<UI_PlayerScore>();
    }
}