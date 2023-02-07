using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem mobParticle;
    private bool isAllive = true;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDie()
    {
        if (!isAllive) return; 
        isAllive= false;
        mobParticle.Play();
        transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack).OnComplete(DestroyMob);
    }

    private void DestroyMob()
    {
        mobParticle.Stop();
        //print(this.gameObject);
        //print(this.gameObject);
        //Destroy(this.gameObject);
    }
}
