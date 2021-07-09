using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attackRadius, attackDamage;
    [SerializeField]
    private Transform attackHitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    private bool gotInput, isAttacking, isFirstAttack;
    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update() {
        CheckCombatInput();
        CheckAttack();
    }
    
    private void CheckCombatInput(){
        if(Input.GetMouseButtonDown(0)){
            if(combatEnabled){ // agar ketika klik pas attacking, maka akan ttap melakukan attack setelah attack sebelumnya
                //attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttack(){
        if(gotInput){
            //perform attack
            if(!isAttacking){
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAtacking", isAttacking);
            }
        }

        if(Time.time >= lastInputTime + inputTimer){
            //wait for new input
            gotInput = false;
        }

    }

    private void CheckAttackHitBox(){
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsDamageable);

        foreach(Collider2D collider in detectedObjects){
            collider.transform.SendMessage("Damage", attackDamage);
            //Instantiate hit particle
        }
    }

    private void FinishAttack1(){
        isAttacking = false;
        anim.SetBool("isAttacking",isAttacking);
        anim.SetBool("attack1",false);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }

    private void PlaySoundAttack1(){
        GameManager.Instance.SoundPlayerAttack1();
    }
    private void PlaySoundAttack2(){
        GameManager.Instance.SoundPlayerAttack2();
    }

}
