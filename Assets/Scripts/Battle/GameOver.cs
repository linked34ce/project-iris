using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool Restarted { get; private set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!Restarted)
            {
                Initiate.DoneFading();
                Initiate.Fade("Scenes/Menu/Before Entering Dungeon", Color.black, 1f);
                Restarted = true;
            }
        }
    }
}
