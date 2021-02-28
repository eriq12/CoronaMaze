using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private float infectRadius = 6.0f;

    private ContactFilter2D peopleLayer; 


    // value out of 10,000
    [SerializeField]
    private int chanceInfect = 20;

    // parent player
    private Player_Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = gameObject.GetComponent<Player_Health>();
        peopleLayer = new ContactFilter2D();
        peopleLayer.SetLayerMask(LayerMask.GetMask("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        var colliderList = new List<Collider2D>();
        int enemies = Physics2D.OverlapCircle(transform.position, infectRadius, peopleLayer, colliderList)-1;
        List<Player_Health> peopleList = colliderList.Select(col => col.gameObject.GetComponent<Player_Health>()).ToList();
        peopleList.Remove(gameObject.GetComponent<Player_Health>());
        if(enemies > 0){
            int threshold = 0;
            foreach(Player_Health ph in peopleList){
                threshold += ph.GetInfStage();
            }
            int sample = Random.Range(0,10000);
            if(sample < (threshold * chanceInfect)){
                playerHealth.Infect();
            }
        }
    }
}
