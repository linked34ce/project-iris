using System;

using UnityEngine;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    public int StepsAfterEncount { get; private set; } = 3;
    public Direction Direction { get; private set; } = Direction.north;
    public Location Location { get; } = new();
    public Dungeon Dungeon { get; } = new();
    public bool HasGoneUpstairs { get; private set; } = false;

    private const float ZeroTranslation = 0f;
    private const float Step = 10f;
    private const float HalfStep = 5f;
    private const float ZeroRotation = 0f;
    private const float QuarterRotation = 90f;
    private const float HalfRotation = 180f;
    private const int MinStepsAfterEncount = 5;
    private const int DefaultSteps = 0;
    private const int DecimalBase = 10;
    private const float FadeDuration = 0.4f;
    private const string NextFloorScenePrefix = "Scenes/Dungeons/TohoGakuenOldBuilding/";
    private const string NextFloorSceneSuffix = "Floor";

    protected override void Awake()
    {
        base.Awake();
        UnityEngine.Random.InitState(Environment.TickCount);
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
        if (!HasGoneUpstairs)
        {
            HasGoneUpstairs = true;
            DungeonSounds.Instance.PlayStairs();
            Status.IncrementFloor();

            string ordinal = ConvertNumberFromCardinalToOrdinal(Status.Floor);
            Initiate.Fade(
                $"{NextFloorScenePrefix}{ordinal}{NextFloorSceneSuffix}",
                Color.black,
                FadeDuration
            );
        }
    }

    public void StepForward()
    {
        transform.Translate(ZeroTranslation, ZeroTranslation, Step);
        transform.position = new Vector3(
            (float)Math.Round(transform.position.x),
            transform.position.y,
            (float)Math.Round(transform.position.z)
        );
        DungeonSounds.Instance.PlayWalk();

        IncrementStepsAfterEncount();
        Encount();
    }

    public void Encount()
    {
        if (
            StepsAfterEncount >= MinStepsAfterEncount
            && UnityEngine.Random.value < Dungeon.EncountRate
        )
        {
            UIStateManager.Instance.UIState = UIState.Battle;
            ResetStepsAfterEncount();
        }
    }

    public void IncrementStepsAfterEncount() => StepsAfterEncount++;

    public void ResetStepsAfterEncount() => StepsAfterEncount = DefaultSteps;

    public void TurnAround()
    {
        DungeonSounds.Instance.PlayTurn();

        transform.Rotate(ZeroRotation, HalfRotation, ZeroRotation);
        transform.Translate(ZeroTranslation, ZeroTranslation, -Step);

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
        DungeonSounds.Instance.PlayTurn();

        transform.Rotate(ZeroRotation, -QuarterRotation, ZeroRotation);
        transform.Translate(HalfStep, ZeroTranslation, -HalfStep);

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
        DungeonSounds.Instance.PlayTurn();

        transform.Rotate(ZeroRotation, QuarterRotation, ZeroRotation);
        transform.Translate(-HalfStep, ZeroTranslation, -HalfStep);

        Direction = Direction switch
        {
            Direction.east => Direction.south,
            Direction.south => Direction.west,
            Direction.west => Direction.north,
            Direction.north => Direction.east,
            _ => Direction,
        };
    }

    public string ConvertNumberFromCardinalToOrdinal(int num) => (num % DecimalBase) switch
    {
        1 => $"{num}st",
        2 => $"{num}nd",
        3 => $"{num}rd",
        _ => $"{num}th",
    };
}
