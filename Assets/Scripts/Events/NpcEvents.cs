using UnityEngine;

namespace Events.Npc
{
    public struct OnNpcSpawned
    {
        public GameObject npcObject;
    }
    
    public struct OnNpcReachedEndPoint
    {
        public GameObject npcObject;
    }
}