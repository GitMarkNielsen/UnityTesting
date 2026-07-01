using UnityEngine;

public class TimelineManager : MonoBehaviour 
{
    public GameObject player;
    public void FixedUpdate()
    {
        player.GetComponent<Movement>().CalculateMovement();
    }

    public void Update()
    {
        
    }
}
