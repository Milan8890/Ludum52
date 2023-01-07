using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedhelper : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
           player.GetComponent<Pmovement>().IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
            player.GetComponent<Pmovement>().IsGrounded = false;
    }


}
