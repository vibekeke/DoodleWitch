using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    private Vector2 startPos;
    private float length;
    public GameObject cam;
    public float parallaxEffect_x; //The speed at which the BG should move relative to the camera
                                //0 = moves a lot (close), 1 = doesn't move at all (far)
    public float parallaxEffect_y; 
    void Start() {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        //Moved 
        float distance_x = cam.transform.position.x * parallaxEffect_x;
        float distance_y = cam.transform.position.y * parallaxEffect_y;


        transform.position = new Vector3(startPos.x + distance_x, startPos.y + distance_y, transform.position.z);


        //Repetition logic
        float movement_x = cam.transform.position.x * (1 - parallaxEffect_x);
        //TODO: add booleans vertical_repeat, horizontal_repeats
        
        if(movement_x < startPos.x - length) {
            startPos.x -= length; //Snaps background image to the middle if player goes far away
        }
        if(movement_x > startPos.x + length) {
            startPos.x += length; //Snaps background image to the middle if player goes far away
        }
    }
}
