using Systems.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "New SceneGroup", menuName = "SceneGroup", order = 0)]
public class SceneGroupSO : ScriptableObject
{
    public SceneGroup sceneGroup;
}