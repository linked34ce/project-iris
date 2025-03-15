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
        GameObject.Find("/DungeonUI/LocationName/Text").GetComponent<TMP_Text>().SetText($"{Dungeon.Name} {Status.Floor}F");
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

        switch (Direction)
        {
            case Direction.east:
                switch (walls.East)
                {
                    case Wall.air:
                        Location.IncrementX();
                        StepForward();
                        break;
                    case Wall.stairs:
                        GoUpstairs();
                        break;
                }
                break;
            case Direction.south:
                switch (walls.South)
                {
                    case Wall.air:
                        Location.DecrementY();
                        StepForward();
                        break;
                    case Wall.stairs:
                        GoUpstairs();
                        break;
                }
                break;
            case Direction.west:
                switch (walls.West)
                {
                    case Wall.air:
                        Location.DecrementX();
                        StepForward();
                        break;
                    case Wall.stairs:
                        GoUpstairs();
                        break;
                }
                break;
            case Direction.north:
                switch (walls.North)
                {
                    case Wall.air:
                        Location.IncrementY();
                        StepForward();
                        break;
                    case Wall.stairs:
                        GoUpstairs();
                        break;
                }
                break;
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
            Initiate.Fade($"Scenes/Dungeons/ToOhGakuenOldBuilding/{ordinal}Floor", Color.black, 0.4f);
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

        Direction = Direction switch
        {
            Direction.east => Direction.west,
            Direction.south => Direction.north,
            Direction.west => Direction.east,
            Direction.north => Direction.south,
            _ => Direction,
        };
    }

    public void TurnLeft()
    {
        GetComponent<FootstepsSound>().PlayTurn();

        transform.Rotate(0f, -90f, 0f);
        transform.Translate(STEP / 2, 0f, -STEP / 2);

        Direction = Direction switch
        {
            Direction.east => Direction.north,
            Direction.south => Direction.east,
            Direction.west => Direction.south,
            Direction.north => Direction.west,
            _ => Direction,
        };
    }

    public void TurnRight()
    {
        GetComponent<FootstepsSound>().PlayTurn();

        transform.Rotate(0f, 90f, 0f);
        transform.Translate(-STEP / 2, 0f, -STEP / 2);

        Direction = Direction switch
        {
            Direction.east => Direction.south,
            Direction.south => Direction.west,
            Direction.west => Direction.north,
            Direction.north => Direction.east,
            _ => Direction,
        };
    }

    public string ConvertNumberFromCardinalToOrdinal(int num) => (num % 10) switch
    {
        1 => $"{num}st",
        2 => $"{num}nd",
        3 => $"{num}rd",
        _ => $"{num}th",
    };
}