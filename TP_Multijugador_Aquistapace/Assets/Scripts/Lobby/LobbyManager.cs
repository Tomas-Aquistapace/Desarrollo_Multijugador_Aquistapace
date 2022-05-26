using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Space(10)]
    [Header("-- Lobby --")]
    [SerializeField] private TMP_InputField roomInputField;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private TextMeshProUGUI username;

    [SerializeField] private RoomItem roomItemPref;
    private List<RoomItem> roomItemsList = new List<RoomItem>();
    [SerializeField] private Transform contentObject;

    [Space(10)]
    [Header("-- Room --")]
    [Space(20)]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TextMeshProUGUI roomName;
    [Space(10)]
    [SerializeField] private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    [SerializeField] private PlayerItem playerItemPref;
    [SerializeField] private Transform playerItemParent;
    [Space(10)]
    [SerializeField] private GameObject playButton;

    [Space(10)]
    [Header("-- Transition --")]
    [Space(20)]
    [SerializeField] private SceneTransition transitionPref;
    [SerializeField] private float waitingTime = 1f;

    private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime = 0f;

    private const int MAX_PLAYERS = 5;

    // -------------------------

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        username.text = PhotonNetwork.NickName;
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    // -----------------

    #region OVERRIDE_FUNCTIONS
    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);

        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    public override void OnLeftRoom()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
    #endregion

    #region COMMON_FUNCTIONS
    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomItem room in roomItemsList)
        {
            Destroy(room.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in roomList)
        {
            RoomItem newRoom = Instantiate(roomItemPref, contentObject);
            newRoom.SetRoomInfo(room.Name, username.text);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    private void UpdatePlayerList()
    {
        foreach(PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPref, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            newPlayerItem.ActivateCrown(player.Value.IsMasterClient);
            playerItemsList.Add(newPlayerItem);
        }
    }
    #endregion

    #region BUTTON_FUNCTIONS
    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = MAX_PLAYERS });
        }
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClickPlayButton()
    {
        void ChangeScene()
        {
            PhotonNetwork.LoadLevel("Gameplay");
        }

        transitionPref.ChangeAnimation(waitingTime, ChangeScene);
    }

    public void OnClickCloseGame()
    {
        Application.Quit();
    }
    #endregion
}