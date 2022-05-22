using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerMovement player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    public void EnablePlayer() 
    {
        player.ActivatePlayer(true);
    }
    
    public void DisablePlayer() 
    {
        player.ActivatePlayer(false);
    }
}
