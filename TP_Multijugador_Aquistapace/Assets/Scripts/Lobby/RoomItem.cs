using UnityEngine;
using TMPro;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI hostName;

    private LobbyManager lobbyManager;

    private void Awake()
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomInfo(string _roomName, string _hostName)
    {
        roomName.text = _roomName;
        hostName.text = _hostName;
    }

    public void OnClickItem()
    {
        lobbyManager.JoinRoom(roomName.text);
    }
}