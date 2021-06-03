using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;


public enum PlayerState { freeRoam, dialogue, riverMiniGame }

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerClass : MonoBehaviour
{

    public static PlayerClass playerClass;

    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object

    // private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

    public PlayerState currentState;
    InteractableObject currentInteractiveObj;

    [SerializeField] int score;

    Rigidbody rb;
    private void Awake()
    {
        playerClass = this;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // get the transform of the main camera
        // if (Camera.main != null)
        // {
        //     m_Cam = Camera.main.transform;
        // }
        // else
        // {
        //     Debug.LogWarning(
        //         "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
        //     // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        // }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractiveObj != null)
            {
                currentInteractiveObj.Interact();
            }
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        switch (currentState)
        {
            case PlayerState.riverMiniGame:

                m_Move = h * Vector3.left * 5.0f;

                rb.velocity = m_Move;

                break;

            case PlayerState.freeRoam:
                // calculate move direction to pass to character
                // if (m_Cam != null)
                // {
                //     // calculate camera relative direction to move:
                //     m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                //     m_Move = v * m_CamForward + h * m_Cam.right;
                // }
                // else
                // {
                // we use world-relative directions in the case of no main camera
                m_Move = v * transform.forward + h * transform.right;
                // }
                // pass all parameters to the character control script
                m_Character.Move(m_Move, crouch, m_Jump);
                m_Jump = false;
                break;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("dialogue"))
        {
            var obj = other.GetComponent<DialogueObject>();
            UIScript.Instance.AssignDialogue(obj.dialogue);
            currentInteractiveObj = obj;
        }

        if (currentState == PlayerState.riverMiniGame)
        {
            if (other.gameObject.CompareTag("garbage"))
            {
                score += 5;
                other.transform.parent.gameObject.SetActive(false);
                UIScript.Instance.UpdateScore(score);
            }
            if (other.gameObject.CompareTag("star"))
            {
                score += 50;
                other.transform.parent.gameObject.SetActive(false);
                UIScript.Instance.UpdateScore(score);
            }
            if (other.gameObject.CompareTag("fish"))
            {
                score -= 10;
                other.transform.parent.gameObject.SetActive(false);
                UIScript.Instance.UpdateScore(score);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("dialogue"))
        {
            currentInteractiveObj = null;
            UIScript.Instance.AssignDialogue(null);
        }
    }
}

