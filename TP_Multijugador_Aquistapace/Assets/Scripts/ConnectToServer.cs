using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private string sceneLobby = "Lobby";
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Transition Animation")]
    [SerializeField] private SceneTransition transition;
    [SerializeField] private float waitingTime;


    // ----------------------------

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            OnClickConnect();
        }
    }

    // ----------------------------

    public void OnClickConnect()
    {
        if(usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        void ChangeScene()
        {
            SceneManager.LoadScene(sceneLobby);
        }

        transition.ChangeAnimation(waitingTime, ChangeScene);
    }
}
