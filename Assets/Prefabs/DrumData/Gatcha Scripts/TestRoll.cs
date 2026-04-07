using UnityEngine;
using UnityEngine.InputSystem;


public class TestRoll : MonoBehaviour
{
    public CrateData testCrate;
    public CrateOpener opener;

    void Awake()
    {
        Debug.Log("Testroll working");

    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            DrumData result = opener.OpenCrate(testCrate);

            if (result != null)
            {
                Debug.Log("Got drum: " + result.drumId + " (" + result.rarity + ")");
            }
        }
    }
}
