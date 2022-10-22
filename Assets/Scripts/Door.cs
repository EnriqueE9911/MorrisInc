using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextsRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x) 
            {
                Debug.Log("cam " + cam, cam.gameObject);
                Debug.Log("room " + nextsRoom, nextsRoom.gameObject);
                cam.MoveToNewRoom(nextsRoom);
            }
                
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }

}