using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerM2 : MonoBehaviour
{
    private int utilityType = 1;
    private float cooldown = 2f;
    private bool canM2 = true;

    public GameObject player;
    public GameObject GroundChecker;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && canM2)
        {
            switch(utilityType)
            {
                case 1:
                    StartCoroutine(Dash());
                    break;
                case 2:
                    //StartCoroutine(Shield());
                    break;
                case 3:
                    //StartCoroutine(Grapple());
                    break;
            }
        }
    }
    IEnumerator Dash()
    {
        float dashDuration = 0.2f;
        int dashVelocity = 2;
        canM2= false;
        Vector2 dir;

        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

        GroundChecker.GetComponent<Pmovement>().canMove = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        player.GetComponent<Rigidbody2D>().AddForce(dashVelocity * dir, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity/2;
        GroundChecker.GetComponent<Pmovement>().canMove = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        yield return new WaitForSeconds(cooldown);
        canM2 = true;

        //player.GetComponent<Rigidbody2D>().velocity = new Vector2(dashVelocity*-1, 0);
    }
}
