using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    // Use this for initialization
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private Rigidbody2D rgb2d;
    public float FallingRatio;
    public float gravityScaleLimit=50f;
    private Player_Controller Player_Controller;
    void Awake () {
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
        Player_Controller = gameObject.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update () {

        if (Player_Controller.onLadder == false)
        {
            if (rgb2d.velocity.y >= 0 || Player_Controller.wallSliding)
            {

                rgb2d.gravityScale = lowJumpMultiplier;

            }

            if (rgb2d.velocity.y < 0)
            {
                rgb2d.gravityScale = fallMultiplier + rgb2d.velocity.y * -FallingRatio;
            }
            if (rgb2d.gravityScale >= gravityScaleLimit)
            {
                rgb2d.gravityScale = gravityScaleLimit;
            }
        }
    }
}
