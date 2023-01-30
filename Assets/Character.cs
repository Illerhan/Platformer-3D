using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;

    // Update is called once per frame
    void Update()
    {

        // gestion des inputs pour le mouvement du personnage
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

        // Espace pour le saut
        if (Input.GetButtonDown("Jump"))
        {
            moveDir.y =  jumpForce;
        }
        // Mouvement de personnage selon la direction
        
        moveDir.y -= gravity * Time.deltaTime;

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.5f);
        }

        // Application du deplacement
        cc.Move(moveDir *Time.deltaTime);
    }
}
