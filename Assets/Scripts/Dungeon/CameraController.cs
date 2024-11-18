using System;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float STEP = 10f;
    const int MIN_STEPS_AFTER_ENCOUNT = 5;
    public int StepsAfterEncount { get; private set; } = 3;
    public Direction Direction { get; private set; } = Direction.north;
    public Location Location { get; } = new();
    public Dungeon Dungeon { get; } = new();
    public bool GoneUpstairs { get; private set; } = false;

    void Awake()
    {
        GetComponent<BattleManager>().enabled = false;
        GameObject.Find("/Dungeon UI/Location Name/Text").GetComponent<TMP_Text>().SetText($"{Dungeon.Name} {Status.Floor}F");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ForwardPressed();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TurnAround();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TurnLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TurnRight();
        }

    }

    public void ForwardPressed()
    {
        Walls walls = Dungeon.Map[Location.Y][Location.X];

        if (Direction == Direction.east)
        {
            if (walls.East == 0)
            {
                Location.IncrementX();
                StepForward();
            }
            else if (walls.East == 2)
            {
                GoUpstairs();
            }
        }

        if (Direction == Direction.south)
        {
            if (walls.South == 0)
            {
                Location.DecrementY();
                StepForward();
            }
            else if (walls.South == 2)
            {
                GoUpstairs();
            }
        }

        if (Direction == Direction.west)
        {
            if (walls.West == 0)
            {
                Location.DecrementX();
                StepForward();
            }
            else if (walls.West == 2)
            {
                GoUpstairs();
            }
        }

        if (Direction == Direction.north)
        {
            if (walls.North == 0)
            {
                Location.IncrementY();
                StepForward();
            }
            else if (walls.North == 2)
            {
                GoUpstairs();
            }
        }
    }

    public void GoUpstairs()
    {
        if (!GoneUpstairs)
        {
            GoneUpstairs = true;

            GetComponent<FootstepsSound>().PlayStairs();

            Status.IncrementFloor();
            string ordinal = ConvertNumberFromCardinalToOrdinal(Status.Floor);
            Initiate.Fade($"Scenes/Dungeons/To-o Gakuen Old Building/{ordinal} Floor", Color.black, 0.4f);
        }
    }

    public void StepForward()
    {
        transform.Translate(0f, 0f, STEP);
        transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, (float)Math.Round(transform.position.z));

        GetComponent<FootstepsSound>().PlayWalk();

        IncrementStepsAfterEncount();
        Encount();
    }

    public void Encount()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        if (StepsAfterEncount >= MIN_STEPS_AFTER_ENCOUNT && UnityEngine.Random.value < Dungeon.EncountRate)
        {
            GetComponent<BattleManager>().enabled = true;
            ResetStepsAfterEncount();
        }
    }

    public void IncrementStepsAfterEncount() => StepsAfterEncount++;

    public void ResetStepsAfterEncount() => StepsAfterEncount = 0;

    public void TurnAround()
    {
        GetComponent<FootstepsSound>().PlayTurn();

        transform.Rotate(0f, 180f, 0f);
        transform.Translate(0f, 0f, -STEP);

        switch (Direction)
        {

            case Direction.east:
                Direction = Direction.west;
                break;
            case Direction.south:
                Direction = Direction.north;
                break;
            case Direction.west:
                Direction = Direction.east;
                break;
            case Direction.north:
                Direction = Direction.south;
                break;
        }
    }

    public void TurnLeft()
    {
        GetComponent<FootstepsSound>().PlayTurn();

        transform.Rotate(0f, -90f, 0f);
        transform.Translate(STEP / 2, 0f, -STEP / 2);

        switch (Direction)
        {
            case Direction.east:
                Direction = Direction.north;
                break;
            case Direction.south:
                Direction = Direction.east;
                break;
            case Direction.west:
                Direction = Direction.south;
                break;
            case Direction.north:
                Direction = Direction.west;
                break;
        }
    }

    public void TurnRight()
    {
        GetComponent<FootstepsSound>().PlayTurn();

        transform.Rotate(0f, 90f, 0f);
        transform.Translate(-STEP / 2, 0f, -STEP / 2);

        switch (Direction)
        {
            case Direction.east:
                Direction = Direction.south;
                break;
            case Direction.south:
                Direction = Direction.west;
                break;
            case Direction.west:
                Direction = Direction.north;
                break;
            case Direction.north:
                Direction = Direction.east;
                break;
        }
    }

    public string ConvertNumberFromCardinalToOrdinal(int num) => (num % 10) switch
    {
        1 => $"{num}st",
        2 => $"{num}nd",
        3 => $"{num}rd",
        _ => $"{num}th",
    };
}