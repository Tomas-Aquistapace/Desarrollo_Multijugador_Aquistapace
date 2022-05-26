using System.Collections;
using UnityEngine;

public class PlayerHeadCollider : MonoBehaviour
{
    [SerializeField] private float secondsToActivate = 1f;

    private PlayerMovement player;
    private Collider coll;

    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement plMov = other.GetComponent<PlayerMovement>();

        if(plMov != null)// && other.transform.tag == "Player")
        {
            player.SmashPlayer();
            plMov.EarnPoint();
            plMov.PushUp();

            IEnumerator EnableCollider()
            {
                coll.enabled = false;

                yield return new WaitForSeconds(secondsToActivate);

                coll.enabled = true;
            }

            StartCoroutine(EnableCollider());
        }
    }
}
