
using UnityEngine;

public class Door : MonoBehaviour
{
   [SerializeField] private Transform previousRoom;
   [SerializeField] private Transform NextRoom;
   [SerializeField] private CameraMovement cam;

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.tag == "Player")
       {
           if (collision.transform.position.x < transform.position.x)
           {
               cam.MoveToNewRoom(NextRoom);
           }
           else
           {
              cam.MoveToNewRoom(previousRoom);
           }
       }
   }
}
