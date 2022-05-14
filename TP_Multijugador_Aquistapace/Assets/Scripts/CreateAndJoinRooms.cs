using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    
    [SerializeField] private string gameSceneName = "Gameplay";

    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;

    public void CreateRoom()
    {
        Debug.Log("Create");
        PhotonNetwork.CreateRoom(createInput.text);
    }
    
    public void JoinRoom()
    {
        Debug.Log("Join");
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        PhotonNetwork.LoadLevel(gameSceneName);
    }
}
