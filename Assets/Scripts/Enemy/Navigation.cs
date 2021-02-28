using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    /* Field values */
    private Rigidbody2D rb2d;

    [SerializeField]
    private float speed=7;

    /* Cohesion */

    // radius for circle cover
    [SerializeField]
    private float radarRadius = 10;

    // multiplier for coehesion vector
    [SerializeField]
    private float cohesionCoeff = 0.5f;

    // ContactFilter2D for collision
    private ContactFilter2D cohesionCircleLayers;

    /* Separation */

    //DEBUG
    //compass
    [SerializeField]
    private GameObject compass;

    // ray cast layer collisions
    [SerializeField]
    private string[] sepLayerNames = new string [] {"Default", "Enemy"};

    // raycast distance
    [SerializeField]
    private float raycastDistance = 8;

    [SerializeField]
    private int numRaycasts = 8;

    // Start is called before the first frame update
    void Start()
    {
        // should be ronaradar
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        // set the Layer mask and initialize contact filter 2d
        cohesionCircleLayers = new ContactFilter2D();
        LayerMask mask = LayerMask.GetMask("Enemy");
        cohesionCircleLayers.SetLayerMask(mask);
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        // inverse cohesion
        Vector2 Coeh = Cohesion();

        Vector2 direction = Coeh * (radarRadius - Coeh.magnitude) / Coeh.magnitude * cohesionCoeff + Separation();
        if(direction.magnitude > 0.5f){
            direction /= direction.magnitude;
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, direction*speed, 0.1f);
        }
        
    }

    /* The three parts of boids (sorta)*/

    /* Alignment (maybe?) */

    /* Cohesion */
    Vector2 Cohesion() {
        List<Collider2D> nearbyPeople = new List<Collider2D>();
        int numPeople = Physics2D.OverlapCircle((Vector2)transform.position, radarRadius, cohesionCircleLayers, nearbyPeople) - 1;
        Vector2 result = Vector2.zero;
        if (numPeople == 0) {
            return result;
        }
        foreach (Collider2D boid in nearbyPeople){
            result += (Vector2) boid.transform.position - (Vector2) transform.position;
        }
        Vector2 dir = result / numPeople;
        return dir;
    }

    /* Separation */

    // main separation method, returns a vector in the direction away from entities and obstacles alike
    Vector2 Separation(){
        // calculates the radians between each raycast
        float radIncrement = 2 * Mathf.PI / numRaycasts;
        // starting angle
        float currentAngle = 0;
        // the opposing forces to add to get to resulting vector
        Vector2 result = Vector2.zero;
        for(int i = 0; i < numRaycasts; i++){
            // add from certain angle
            result += InfluenceFromAngle(currentAngle);
            // rotate angle by even portion of a circle
            currentAngle+=radIncrement;
        }
        return result;
    }

    // helper converter from Radian to a vector
    Vector2 RadianToVector2(float radians){
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    // helper method: raycast to vector "force"
    Vector2 InfluenceFromAngle(float radians)
    {
        Vector2 direction = RadianToVector2(radians);
        Vector2 raycastOrigin = (Vector2)transform.position+direction*4;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, direction, (raycastDistance-4.0f), LayerMask.GetMask(sepLayerNames));

        // if the hit returns something
        if(hit.collider){
            // calculate the magnitude of the distance
            float dist = (((Vector2)transform.position) - hit.point).magnitude;
            // Debug.Log("Hit! At " + radians + " angle and " + dist + " units away.");
            // and invert it with multiplying by the direction unit vector
            return direction * (dist-raycastDistance);
        }
        return Vector2.zero;
    }


}
