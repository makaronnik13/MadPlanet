using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;


    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {

    [SerializeField]
    private bool AxisSwaped = false;
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool JumpingAvaliable = true;
    
    public void SetJumping(bool v)
    {
        JumpingAvaliable = v;
        if (m_Character && m_Character.m_Anim)
        {
            m_Character.m_Anim.SetBool("Ground", true);
        }
    }

        private float w;
    private bool active = true;

    public void LockForSeconds(float v)
    {
        StartCoroutine(Lock(v));
    }

    private IEnumerator Lock(float v)
    {
        active = false;
        yield return new WaitForSeconds(v);
        active = true;
    }

    private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

    private void OnDisable()
    {
        m_Character.Move(0, 0, false, false);
        m_Jump = false;
    }


    private void Update()
        {
        if (!active)
        {
            return;
        }
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump") && JumpingAvaliable;
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float w = CrossPlatformInputManager.GetAxis("Horizontal");
            float h = CrossPlatformInputManager.GetAxis("Vertical");

        if (AxisSwaped)
        {
            float v = w;
            w = -h;
            h = v;
        }
        // Pass all parameters to the character control script.

            m_Character.Move(h,-w, crouch, m_Jump);
            m_Jump = false;
        }
    }
