using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * implementar o dash com um swipe de tela
 */

public class character : MonoBehaviour
{

    private bool grounded = true;
    private bool wallClingRight = false;
    private bool wallClingLeft = false;
    private bool hasDoubleJump = false;
    private bool collisionTop = false;
    private float lateralAirAcc = 0.6f;
    private float maxAirLateralVelocity = 8;
    private float groundLateralVelocity = 7;
    private float jumpVelocity = 20;
    private float wallJumpX = 8;
    private float wallJumpY = 13;
    private float rayLength = 0.5f;
    private float sideRay = 0.4f;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 6;
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameControl.instance.gameOver && !GameControl.instance.gameStart)
        {

            RaycastHit2D groundLeft = Physics2D.Raycast(new Vector2(rb2d.position.x - sideRay, rb2d.position.y), -Vector2.up, rayLength);
            RaycastHit2D groundMid = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y), -Vector2.up, rayLength);
            RaycastHit2D groundRight = Physics2D.Raycast(new Vector2(rb2d.position.x + sideRay, rb2d.position.y), -Vector2.up, rayLength);
            if (groundLeft.collider != null || groundMid.collider != null || groundRight.collider != null)
            {
                grounded = true;
                hasDoubleJump = true;
            }
            else grounded = false;

            RaycastHit2D rightUp = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y + sideRay), Vector2.right, rayLength);
            RaycastHit2D rightMid = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y), Vector2.right, rayLength);
            RaycastHit2D rightDown = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y - sideRay), Vector2.right, rayLength);
            if ((rightUp.collider != null || rightDown.collider != null || rightMid.collider != null) && !grounded) wallClingRight = true;
            else wallClingRight = false;

            RaycastHit2D leftUp = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y + sideRay), Vector2.left, rayLength);
            RaycastHit2D leftMid = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y), Vector2.left, rayLength);
            RaycastHit2D leftDown = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y - sideRay), Vector2.left, rayLength);
            if ((leftDown.collider != null || leftMid.collider != null || leftUp.collider != null) && !grounded) wallClingLeft = true;
            else wallClingLeft = false;

            RaycastHit2D topLeft = Physics2D.Raycast(new Vector2(rb2d.position.x - sideRay, rb2d.position.y), Vector2.up, rayLength);
            RaycastHit2D topMid = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y), Vector2.up, rayLength);
            RaycastHit2D topRight = Physics2D.Raycast(new Vector2(rb2d.position.x + sideRay, rb2d.position.y), Vector2.up, rayLength);
            if (topLeft.collider != null || topMid.collider != null || topRight.collider != null) collisionTop = true;
            else collisionTop = false;

            bool up = false, dir = false, esq = false;

            Touch myTouch = Input.GetTouch(0);

            Touch[] myTouches = Input.touches;
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (myTouches[i].position.x < Screen.width / 3) esq = true;
                else if (myTouches[i].position.x < Screen.width * 2 / 3) up = true;
                else dir = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || up)
            {
                if (grounded)
                {
                    rb2d.velocity += new Vector2(0, jumpVelocity);
                }
                else
                {
                    if (wallClingRight) rb2d.velocity = new Vector2(-wallJumpX, wallJumpY);
                    else if (wallClingLeft) rb2d.velocity = new Vector2(wallJumpX, wallJumpY);
                    else if (hasDoubleJump)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
                        hasDoubleJump = false;
                    }
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || esq)
            {
                if (wallClingRight) wallClingRight = false;
                if (grounded) rb2d.velocity = new Vector2(-groundLateralVelocity, rb2d.velocity.y);
                else rb2d.velocity += new Vector2(-lateralAirAcc, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow) || dir)
            {
                if (wallClingLeft) wallClingLeft = false;
                if (grounded) rb2d.velocity = new Vector2(groundLateralVelocity, rb2d.velocity.y);
                else rb2d.velocity += new Vector2(lateralAirAcc, 0);
            }

            if (wallClingLeft || wallClingRight)
            {
                if (rb2d.velocity.y < -2) rb2d.velocity = new Vector2(0, -2);
            }

            if (!grounded && rb2d.velocity.x > maxAirLateralVelocity) rb2d.velocity = new Vector2(maxAirLateralVelocity, rb2d.velocity.y);
            if (!grounded && rb2d.velocity.x < -maxAirLateralVelocity) rb2d.velocity = new Vector2(-maxAirLateralVelocity, rb2d.velocity.y);
            
            if (grounded && collisionTop || rb2d.position.y < sobe_mapa.instance.posicao().y - 11.5f) //Inserir condições para morte
            {
                GameControl.instance.characterDied();
            }
        }
    }
}
