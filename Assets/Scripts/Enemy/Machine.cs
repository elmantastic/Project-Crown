using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    private Player player;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject damageParticle;
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private void Start() {
        currentHealth = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        particle = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Damage(float _damage){
        currentHealth -= _damage;

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(damageParticle, transform.position, Quaternion.Euler(0.0f, Random.Range(0.0f, 10.0f), 0.0f));

        // camera shake

        if(currentHealth <= 0.0f){
            // die
            StartCoroutine(BreakMachine());
        } else {
            // sound hit machine
            GameManager.Instance.SoundPlayerHitMachine();
        }
    }

    private IEnumerator BreakMachine(){
        particle.Play();
        // sound machine destroyed
        GameManager.Instance.SoundPlayerDestroyMachine();
        sr.enabled = false;
        bc.enabled = false;
        LevelManager.instance.AddMachineDestroyed();

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
