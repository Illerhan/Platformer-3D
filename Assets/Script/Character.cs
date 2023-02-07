using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController cc;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;
    public Animator anim;
    bool isWalking = false;
    public Camera curentCam;

    public static Character Instance;

    private void Awake()
    {
        Instance= this;
    }
    private void Start()
    {
        cc  = GetComponent<CharacterController>();
        anim= GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float previousY = moveDir.y;
        // gestion des inputs pour le mouvement du personnage
        Vector3 forward= curentCam.transform.forward;
        Vector3 right= curentCam.transform.right;
        forward.y = 0;
        right.y = 0;
        Vector3 relativeForward = Input.GetAxis("Vertical")*forward;
        Vector3 relativeRight = Input.GetAxis("Horizontal")*right;
        // Application du deplacement
        moveDir = relativeForward+ relativeRight;
        moveDir *= moveSpeed;
        moveDir.y = previousY;

        // Espace pour le saut si on est au sol
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            moveDir.y =  jumpForce;
        }
        // Mouvement de personnage selon la direction
        
        moveDir.y -= gravity * Time.deltaTime;

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            isWalking= true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.5f);
        }
        else
        {
            isWalking= false; // On arrrete de marcher
        }

        anim.SetBool("IsWalking", isWalking);
        
        cc.Move(moveDir *Time.deltaTime);
    }
}
