using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heal : MonoBehaviour
{
    [SerializeField] private PlayerSkill skill;
    [SerializeField] private Image healSkill;

    private void Start() {
        healSkill.fillAmount = skill.healCooldown / skill.healCooldown;
    }

    private void Update() {
        healSkill.fillAmount = skill.healTime / skill.healCooldown;
        //print(skill.dashCooldown);
    }
}
