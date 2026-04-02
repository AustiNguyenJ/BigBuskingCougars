using UnityEngine;

[CreateAssetMenu(fileName = "NpcSettings", menuName = "Config/Npc", order = 0)]
public class NpcSettings : ScriptableObject
{
    [Header("Settings")] 
    [Tooltip("Range (in seconds) for how long an npc will listen to player's performance before leaving. X = Min, Y = Max")]
    public Vector2 listeningDuration = new Vector2(5f, 10f);
    
    
}