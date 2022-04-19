using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform perviousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraControler cam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
            } else
                cam.MoveToNewRoom(perviousRoom);
        }
    }


}
