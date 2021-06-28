using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    private void Awake() {
        particle = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            LevelManager.instance.AddDiamond();
            StartCoroutine(BreakDiamond());
            
        }
    }

    private IEnumerator BreakDiamond(){
        particle.Play();
        sr.enabled = false;
        cc.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
