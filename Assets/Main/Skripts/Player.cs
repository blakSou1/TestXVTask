using System.Collections;
using UnityEngine;

namespace Assets.Main.Skripts
{
    public class Player : MonoBehaviour
    {
        InputSystem_Actions inputSystemActions;
        Animator animator;
        public float dis = .5f;
        new Rigidbody rigidbody;
        public float jumpForce = 10;
        bool isMove;
        float move;
        bool jump;
        public float speed;

        private void Start()
        {
            inputSystemActions = new();

            inputSystemActions.Player.Move.performed += i => Move(i.ReadValue<Vector2>().x);
            inputSystemActions.Player.Move.canceled += i => isMove = false;

            inputSystemActions.Player.Jump.performed += i => Jump();

            inputSystemActions.Enable();

            animator = GetComponentInChildren<Animator>();
            rigidbody = GetComponentInChildren<Rigidbody>();
        }
        private void OnDisable()
        {
            inputSystemActions.Disable();
        }
        private void Move(float v)
        {
            move = v;
            isMove = true;
        }
        private void FixedUpdate()
        {
            if (isMove)
            {
                rigidbody.linearVelocity = new(move * speed, rigidbody.linearVelocity.y);
                animator.SetFloat("Speed", 1);
                return;
            }
            else
                animator.SetFloat("Speed", 0);

            if (jump)
                return;

            CheckGround();
        }

        private void Jump()
        {
            if (jump)
                return; 

            if (!animator.GetBool("Grounded"))
                return;

            animator.SetBool("Grounded", false);
            jump = true;
            animator.SetBool("Jump", true);

            rigidbody.AddForce(new Vector2(rigidbody.linearVelocity.x, jumpForce), ForceMode.Impulse);

            StartCoroutine(Timers());
        }
        private IEnumerator Timers()
        {
            yield return new WaitForSeconds(.4f);

            jump = false;
            animator.SetBool("Jump", false);
        }

        public void CheckGround()
        {
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, dis))
            {
                animator.SetBool("Grounded", true);
                animator.SetBool("FreeFall", false);
            }
            else
            {
                animator.SetBool("Grounded", false);
                animator.SetBool("FreeFall", true);
            }
        }

    }
}