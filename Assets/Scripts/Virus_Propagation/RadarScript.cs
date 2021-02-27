using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private int enemies = 0;

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
            playerHealth.Damage(enemies * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        enemies++;
    }

    void OnTriggerExit2D(Collider2D other){
        enemies--;
    }
}
