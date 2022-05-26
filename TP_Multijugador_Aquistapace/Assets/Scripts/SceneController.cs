using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class SceneController : MonoBehaviour
{
    #region EXPOSED_FIELD
    [Header("Scene To Go")]
    [SerializeField] private string lobbyScene = "Lobby";

    [Header("Transition Animation")]
    [SerializeField] private SceneTransition transition;
    [SerializeField] private float waitingTime = 0.5f;
    #endregion

    #region PRIVATE_FIELD

    #endregion

    #region CLICK_BUTTONS
    public void OnClickCloseGame()
    {
        Application.Quit();
    }

    public void OnClickLeaveGame()
    {
        void ChangeScene()
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(lobbyScene);
        }

        transition.ChangeAnimation(waitingTime, ChangeScene);
    }
    #endregion
}
