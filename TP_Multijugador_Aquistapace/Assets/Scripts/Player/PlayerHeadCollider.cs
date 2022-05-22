using UnityEngine;

public class PlayerHeadCollider : MonoBehaviour
{
    private PlayerMovement player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            player.SmashPlayer();
        }
    }
}
