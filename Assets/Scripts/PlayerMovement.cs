using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 2f;
    public Animator PlayerAnimator;
    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private bool _jumping = false;
    private bool _onFloor = false;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _onFloor = true;
        }
        if (collision.gameObject.tag == "Trap")
        {
            transform.position = new Vector2(-6.98f, -1.34f);
        }
        if (collision.gameObject.tag == "Wall")
        {
            PlayerAnimator.SetBool("onWall", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _onFloor = false;
        }
        if (collision.gameObject.tag == "Wall")
        {
            PlayerAnimator.SetBool("onWall", false);
        }
    }
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        if (Input.GetButton("Jump") && _onFloor == true)
        {
            _jumping = true;
        }
    }

    private void FixedUpdate()
    {
        PlayerAnimator.SetFloat("Speed", _horizontal > 0 ? _horizontal : 0);
        transform.position += new Vector3(_horizontal, 0, 0) * Time.fixedDeltaTime * Speed;
        if (_jumping)
        {
            _jumping = false;
            _rigidbody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            PlayerAnimator.SetBool("Jumping", true);
        }
        else
        {
            PlayerAnimator.SetBool("Jumping", false);
        }
    }
}
