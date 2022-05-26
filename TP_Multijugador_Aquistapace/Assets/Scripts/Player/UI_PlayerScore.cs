using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UI_PlayerScore : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private TextMeshProUGUI namePl;
    [SerializeField] private TextMeshProUGUI scorePl;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsReading)
        {
            namePl.text = (string)stream.ReceiveNext();
            scorePl.text = (string)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(namePl.text);
            stream.SendNext(scorePl.text);
        }
    }

    private void Start()
    {
        this.transform.parent = FindObjectOfType<UI_ScoreLayer>().transform;
        this.transform.localScale = Vector3.one;
    }

    public void SetName(string _name)
    {
        namePl.text = _name;
    }

    public void SetScore(string _score)
    {
        scorePl.text = _score;
    }
}
