using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private int enemies = 0;

    // value out of 10,000
    [SerializeField]
    private int chanceInfect = 2;

    // parent player
    [SerializeField]
    private Player_Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = transform.parent.gameObject.GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies > 0){
            int sample = Random.Range(0,10000);
            if(sample < (chanceInfect * enemies)){
                playerHealth.Infect();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        enemies++;
    }

    void OnTriggerExit2D(Collider2D other){
        enemies--;
    }
}
