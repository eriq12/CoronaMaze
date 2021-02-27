using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = hpBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHP;
    }

    //takes health away
    public void Damage(float hpDamage){
        currentHP-=hpDamage;
        if(currentHP < 0){
            currentHP = 0;
        }
    }
}
