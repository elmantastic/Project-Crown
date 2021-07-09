using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;
    private PlayerHealth health;
    public float dashTimer {get; private set;}
    public float dashCooldown {get; private set;}
    private bool isDashing;
    public float healCooldown = 5f;
    public float healTime {get; private set;}
    private bool canHeal;

    private void Start() {
        player = GetComponent<Player>();
        health = GetComponent<PlayerHealth>();
        dashCooldown = player.dashCooldown;
        dashTimer = dashCooldown;
        healTime = healCooldown;
    }

    private void Update() {
        CheckIfDashing();
        CheckIfCanHeal();

    }

    public void Dashing(){
        isDashing = true;
        dashTimer = 0;
        GameManager.Instance.SoundPlayerDash();
    }

    private void CheckIfDashing(){
        if(isDashing == true && dashTimer <= player.dashCooldown){
            dashTimer += Time.deltaTime;

        } else {
            isDashing = false;
        }
    }

    private void CheckIfCanHeal(){
        
        if(healTime <= healCooldown){
            healTime += Time.deltaTime;
            canHeal = false;
        } else if (!health.isFullHP()) {
            canHeal = true;
            
        }
        
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && canHeal){
            // Healing skill
            health.Heal();
            healTime = 0;
        }
    }
}
