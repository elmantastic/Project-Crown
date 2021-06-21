using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [SerializeField] private PlayerSkill skill;
    [SerializeField] private Image dashSkill;

    private void Start() {
        dashSkill.fillAmount = skill.dashCooldown / skill.dashCooldown;
    }

    private void Update() {
        dashSkill.fillAmount = skill.dashTimer / skill.dashCooldown;
        //print(skill.dashCooldown);
    }
}
