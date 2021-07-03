using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private float rechargeHealthTime;

    private PlayerDeath death;
    private ParticleSystem ps;

    private void Start() {
        death = GetComponent<PlayerDeath>();
        ps = GetComponent<ParticleSystem>();
    }

    private void Awake() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage){
        // make sure the health doesn't go under 0 and above the max
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0){
            // player get hurt
            // animation player hurt
        } else {
            // player death
            // animation player die
            death.playerDeath();
            currentHealth = startingHealth;
        }
    }

    private void Update() {
        if(rechargeHealthTime > 2f){
            currentHealth = Mathf.Clamp(currentHealth + 0.3f, 0, startingHealth);
            rechargeHealthTime = 0;
        } else {
            rechargeHealthTime += Time.deltaTime;
        }
    }

    public void Heal(){
        StartCoroutine(Healing());

    }

    public bool isFullHP(){
        return currentHealth == startingHealth;
    }

    private IEnumerator Healing(){
        ps.Play();
        currentHealth = Mathf.Clamp(currentHealth + 2f, 0, startingHealth);

        yield return new WaitForSeconds(ps.main.startLifetime.constantMax);
    }
}
