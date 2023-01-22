using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int Hp = 100;
    private int damage = 10;
    private float knockbackStrength = 3.0f;
    bool facingRight = true;

    public GameObject enemyObject;
    private Rigidbody2D rb;
    private GameObject enemyGfx;


    // AI stuff
    [HideInInspector] public bool pathIsEnded = false;
    public float updateRate = 2.0f; // # of path updates each second
    public float speed = 10.0f;
    public float jumpPower = 40.0f;
    public float jumpAngle = 0.7f;
    public bool onGround = true;
    public float nextWaypointDist = 3; // waypoint proximity detection range
    public ForceMode2D fMode;
    public Transform target;
    public Path path;
    private int currentWaypoint = 0; // waypoint we are moving towards
    private Seeker seeker;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        enemyGfx = transform.Find("Enemy_Gfx").gameObject;
        if(!enemyGfx)
        {
            Debug.LogError("Enemy_Gfx not found!");
        }
        if (!target)
        {
            target = GameObject.Find("player").transform;
            if(target)
            {
                Debug.Log("Enemy target automatically assigned!");
            }
            else
            {
                Debug.LogError("Enemy target not assigned!");
            }
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p)
    {
        //Debug.Log("Path completed! Error message: " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    IEnumerator UpdatePath()
    {
        if(!target)
        {
            Debug.LogError("Enemy target not assigned!");
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1.0f / updateRate);
        StartCoroutine(UpdatePath());
    }
    void FixedUpdate()
    {
        if (!target)
        {
            Debug.LogError("Enemy target not assigned!");
        }
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count) // if(end of path)
        {
            if (pathIsEnded)
                return;
            Debug.Log("End of path reached!");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        // move towards next waypoint
        Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // left or right movement
        bool rightMovement = Vector2.Dot(dir, Vector2.right) > 0.0f;
        Vector2 horMovement = (rightMovement) ? Vector2.right * speed * Time.fixedDeltaTime : Vector2.left * speed * Time.fixedDeltaTime;
        // TODO: limb flipping
        if(facingRight != rightMovement)
        {
            facingRight = rightMovement;
            enemyGfx.GetComponent<SpriteRenderer>().flipX = !facingRight;
        }
        if (onGround && Mathf.Acos( Vector2.Dot(dir, Vector2.up))*Mathf.Rad2Deg <= jumpAngle) // jump condition
            rb.velocity = Vector2.up * jumpPower;
        rb.AddForce(horMovement, fMode);
        if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDist)
        {
            currentWaypoint++;
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
            onGround = true;   
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
            onGround = false;
    }
    public void getDamage(int dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
            Destroy(enemyObject);
    }
    public void applyKnockback(in Quaternion projectileRotation)
    {
        Vector3 eulerAngles = projectileRotation.eulerAngles;
        Vector2 direction = new Vector2(Mathf.Cos(eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(eulerAngles.z * Mathf.Deg2Rad));
        applyKnockback(direction);
    }
    public void applyKnockback(in Vector2 direction)
    {
        direction.Normalize();
        rb.AddForce(direction * knockbackStrength + Vector2.up * 1.0f, ForceMode2D.Impulse);
    }
}
