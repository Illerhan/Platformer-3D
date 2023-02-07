using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;
using System;

public class CoinAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 dir;
    [SerializeField]private ParticleSystem coinParticules;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(dir * Time.deltaTime);
    }

    public void OnCollect()
    {
        coinParticules.Play();
        transform.DOScale(Vector3.zero,.8f).SetEase(Ease.InBack).OnComplete(DestroyCoin);
    }

    private void DestroyCoin()
    {
        coinParticules.Stop();
        Destroy(this.gameObject);
    }
}
