using UnityEngine;

public class EventManager : MonoBehaviour
{
    // called on object pickup ()
   public void onObjectPickup(GameObject obj)
   {
      Debug.Log("Object picked up: " + obj.name);
      // Additional logic for when an object is picked up can be added here
   }

    public void onObjectDropped(GameObject obj)
    {
        Debug.Log("Object Dropped: " + obj.name);
        // Additional logic for when an object is dropped can be added here
    }

    public void onHoverEnter(GameObject obj)
    {
        Debug.Log("Hover enter: " + obj.name);
        // Additional logic for when the user starts hovering over an object can be added here
    }

    public void onHoverExit(GameObject obj)
    {
        Debug.Log("Hover exit: " + obj.name);
        // Additional logic for when the user stops hovering over an object can be added here
    }

}
