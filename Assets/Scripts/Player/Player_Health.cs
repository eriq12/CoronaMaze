using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    // health section

    // visualization of health
    [SerializeField]
    private Slider hpBar;
    
    // holds current health value, will be set to max when initialized
    private float currentHP;

    // the max hp for character, determines division/max value for hpBar
    [SerializeField]
    private int maxHP = 100;

    // infection section

    // bar to visualize infection stage
    [SerializeField]
    private Slider infectionBar;
    
    // determines the max stages of infection
    [SerializeField]
    private int maxInfectStages = 3;

    // holds current stages of infection
    private int currentStageInfect;

    // base health damage over time while infected (to be multiplied by how many stages of infection)
    private int baseInfectDOT = 1;

    // recover section

    // time before an infection stage will recover
    [SerializeField]
    private float recoverTime = 5.0f;

    // timer that holds time left until a stage is depleted
    private float recoveryTimer;

    // amount of health that will be recovered per second
    [SerializeField]
    private float recoverRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentStageInfect = 0;
        hpBar.maxValue = maxHP;
        infectionBar.maxValue = maxInfectStages;
        currentHP = maxHP;
        infectionBar.value = currentStageInfect;
    }

    // Update is called once per frame
    void Update()
    {
        // infection damage
        if(currentStageInfect > 0){
            currentHP -= baseInfectDOT * currentStageInfect * Time.deltaTime;
        }

        // handles infection stages
        // is timer still running?
        if(recoveryTimer > 0){
            recoveryTimer -= Time.deltaTime;
        }
        // are there still stages
        else if(currentStageInfect > 0){
            currentStageInfect--;
            infectionBar.value = currentStageInfect;
            recoveryTimer = recoverTime;
        }
        // recover health
        else {
            currentHP += recoverRate * Time.deltaTime;
        }

        // handles visual
        if(hpBar){
            hpBar.value = currentHP;
        }
    }

    // increases infection stage
    public void Infect(){
        if(currentStageInfect < maxInfectStages){
            infectionBar.value = ++currentStageInfect;
        }
        recoveryTimer = recoverTime;
    }
}
