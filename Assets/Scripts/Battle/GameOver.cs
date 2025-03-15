using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool Restarted { get; private set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !Restarted)
        {
            Restarted = true;
            Initiate.DoneFading();
            Initiate.Fade("Scenes/Menu/BeforeEnteringDungeon", Color.black, 1f);
        }
    }
}
