using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {

        public SpriteRenderer View;
        [SerializeField] public bool SideView = false;
        [SerializeField] public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private bool canJumpBack = true;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private float drawnRate = 0;
        [SerializeField] private float dieRate = 0;
        [SerializeField] public bool Drawn = false;
        [SerializeField] public bool Diyng = false;
        [SerializeField] private float DrawnTime = 3;
        [SerializeField] private float DieTime = 3;

        [SerializeField] public bool Grabed = false;

        private bool platformed = false;
        public bool Platformed
        {
            get
            {
                return platformed;
            }
            set
            {
                platformed = false;
            }
        }

        public Action<float> DrawnRateChanged = (v) => { };
        public Action<float> DieRateChanged = (v) => { };

        public Transform Visual;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

        [SerializeField]
        private Animator m_Anim;            // Reference to the player's animator component.


        private Rigidbody m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Rigidbody2D = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            float lastDieRate = dieRate;
            float lastDrawnRate = drawnRate;

            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.

            m_Grounded = IsGrounded();

            m_Anim.SetBool("Ground", m_Grounded);
            // Set the vertical animation
           // m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if (Drawn)
            {
                drawnRate += Time.deltaTime / DrawnTime;
            }
            else
            {
                drawnRate -= Time.deltaTime;
            }

            if (Diyng)
            {
                dieRate += Time.deltaTime / DieTime;
            }

            drawnRate = Mathf.Clamp(drawnRate, 0,1f);
            dieRate = Mathf.Clamp(dieRate, 0, 1f);

            if (lastDieRate!=dieRate)
            {
                DieRateChanged(dieRate);
            }

            if (lastDrawnRate!=drawnRate)
            {
                DrawnRateChanged(drawnRate);
            }


            Visual.transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.down*2, drawnRate);



            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                m_FacingRight = false;
                View.flipX = m_FacingRight;
            }

            if (Mathf.Abs(m_Anim.GetFloat("Horizontal")) < 0.75f)
            {
                m_FacingRight = false;
                View.flipX = m_FacingRight;
            }
        }

        public void SetSideView(bool v)
        {
            SideView = v;
        }

        public void SetDrawn(bool v, float drawnTime = 3)
        {
            DrawnTime = drawnTime;
            Drawn = v;
        }

        public void SetJumpForce(float f)
        {
            m_JumpForce = f;
        }

        public void Grab(bool v)
        {
            m_Anim.SetBool("Grabbed", v);
            GetComponent<Rigidbody>().useGravity = !v;
            GetComponent<Rigidbody>().isKinematic = v;
            Grabed = v;
            GetComponent<Platformer2DUserControl>().enabled = !v;
        }

        public void Die(bool v, float dieTime = 1f)
        {
            GetComponent<Rigidbody>().useGravity = !v;
            Diyng = v;
            DieTime = dieTime;
            GetComponent<Platformer2DUserControl>().enabled = !v;
        }

        private bool IsGrounded()
    {
        return Physics.Raycast(m_GroundCheck.position, -Vector3.up, k_GroundedRadius);
    }  

    public void Move(float moveH, float moveV, bool crouch, bool jump)
        {

            if (Drawn)
            {
                moveH *= 1-drawnRate;
                moveV *= 1 - drawnRate;
                jump = false;
            }

            m_Anim.SetFloat("Horizontal", -moveV);
           

            m_Anim.SetFloat("Vertical", moveH);

            if (!m_Grounded && m_AirControl && !canJumpBack)
            {
                moveH = Mathf.Clamp(moveH, 0, 1f);
            }

            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);


            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                moveH = (crouch ? moveH*m_CrouchSpeed : moveH);
                moveV = (crouch ? moveV * m_CrouchSpeed : moveV);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(moveH)+ Mathf.Abs(moveV));

                // Move the character
                m_Rigidbody2D.velocity = new Vector3(moveH * m_MaxSpeed, m_Rigidbody2D.velocity.y, moveV * m_MaxSpeed);

                // If the input is moving the player right and the player is facing left...
                if (moveV > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (moveV < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode.Acceleration);
            }

            /*
            if (m_Anim.GetBool("Ground"))
            {
                m_Anim.SetLayerWeight(1, Mathf.Abs(moveH) + Mathf.Abs(moveV));
            }
            else
            {
                m_Anim.SetLayerWeight(1, 0);
            }*/

            if (Mathf.Abs(moveH)+ Mathf.Abs(moveV)>0)
            {
                m_Anim.SetFloat("Dir", moveH);
                m_Anim.SetFloat("Dir2", moveV);
            }
        }


        private void Flip()
        {
            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                m_FacingRight = false;
                View.flipX = m_FacingRight;
                return;
            }

            if (Mathf.Abs(m_Anim.GetFloat("Horizontal"))<0.5f)
            {
                m_FacingRight = false;
                View.flipX = m_FacingRight;
                return;
            }

            if (!View || !m_Grounded)
            {
                return;
            }


            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            if (View)
            {
                View.flipX = m_FacingRight;
            }
        }
    }
}
