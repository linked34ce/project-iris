using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Initiate.Fade("Scenes/Menu/Before Entering Dungeon", Color.black, 1f);
        }
    }
}
