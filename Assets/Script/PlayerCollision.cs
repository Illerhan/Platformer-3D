using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool canSwitchCamera = true;
    private bool isTriggering = false;
    private enum cameraAngle
    {
        main,
        tower,
    }
    private cameraAngle currentCameraAngle = cameraAngle.main;
    public Camera Cam1;
    public Camera CamDef;
    public int nbCoin = 0;

    private void ChangeCameraAngle(cameraAngle ca)
    {
        switch (ca)
        {
            case cameraAngle.main:
                Character.Instance.curentCam = CamDef;
                Cam1.gameObject.SetActive(false);
                CamDef.gameObject.SetActive(true);
                break;
            case cameraAngle.tower:
                Character.Instance.curentCam = Cam1;
                Cam1.gameObject.SetActive(true);
                CamDef.gameObject.SetActive(false);
                break;
        }
        currentCameraAngle = ca;
        StartCoroutine(DoDelay());
    }

    private void OnTriggerStay(Collider other)
    {

    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Coins")) // Touché une piece
        {
            nbCoin++;
            other.gameObject.GetComponent<CoinAnim>().OnCollect();
        }
        if (other.gameObject.CompareTag("Cam1"))
            isTriggering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cam1"))
            isTriggering = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.CompareTag("Hurt"))
        {
            print("Aie");
        }
        if (collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponentInParent<MobCollision>().OnDie();
        }
    }

    private void Update()
    {
        if (canSwitchCamera)
        {
            if (isTriggering && currentCameraAngle != cameraAngle.tower)
            {
                ChangeCameraAngle(cameraAngle.tower);
            }
            else if (!isTriggering && currentCameraAngle != cameraAngle.main)
            {
                ChangeCameraAngle(cameraAngle.main);
            }
        }
    }

    private IEnumerator DoDelay()
    {
        canSwitchCamera = false;
        yield return new WaitForSeconds(0.5f);
        canSwitchCamera = true;
    }

}
