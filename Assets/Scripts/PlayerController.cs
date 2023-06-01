using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool canMove;
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator playerAnimation;
    public string areaTransitionName;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity =
                new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
                * moveSpeed;

            playerAnimation.SetFloat("moveX", rb.velocity.x);
            playerAnimation.SetFloat("moveY", rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (
            (
                Input.GetAxisRaw("Horizontal") == 1
                || Input.GetAxisRaw("Horizontal") == -1
                || Input.GetAxisRaw("Vertical") == 1
                || Input.GetAxisRaw("Vertical") == -1
            ) && canMove
        )
        {
            playerAnimation.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnimation.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
        );
    }

    public void SetBounds(Vector3 botLef, Vector3 topRight)
    {
        bottomLeftLimit = botLef + new Vector3(1f, 1f, 1f);
        topRightLimit = topRight + new Vector3(-1f, -1f, 0f);
    }
}
