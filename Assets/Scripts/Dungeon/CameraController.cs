using System;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private UIStateManager _uiStateManager;
    public UIStateManager UIStateManager => _uiStateManager;
    [SerializeField] private DungeonSounds _dungeonSounds;
    public DungeonSounds DungeonSounds => _dungeonSounds;

    public int StepsAfterEncount { get; private set; } = 3;
    public Direction Direction { get; private set; } = Direction.north;
    public Location Location { get; } = new();
    public Dungeon Dungeon { get; } = new();
    public bool HasGoneUpstairs { get; private set; } = false;

    private const float ZERO_TRANSLATION = 0f;
    private const float STEP = 10f;
    private const float HALF_STEP = 5f;
    private const float ZERO_ROTATION = 0f;
    private const float QUAURTER_ROTATION = 90f;
    private const float HALF_ROTATION = 180f;
    private const int MIN_STEPS_AFTER_ENCOUNT = 5;
    private const int DEFAULT_STEPS = 0;
    private const int DECIMAL_BASE = 10;
    private const float FADE_DURATION = 0.4f;

    private static CameraController instance;

    public static CameraController Instance
    {
        get
        {
            if (null == instance)
            {
                instance = (CameraController)FindAnyObjectByType(typeof(CameraController));
                if (null == instance)
                {
                    Debug.Log("CameraController Instance Error");
                }
            }
            return instance;
        }
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

            DungeonSounds.PlayStairs();

            Status.IncrementFloor();
            string ordinal = ConvertNumberFromCardinalToOrdinal(Status.Floor);
            Initiate.Fade($"Scenes/Dungeons/ToOhGakuenOldBuilding/{ordinal}Floor", Color.black, FADE_DURATION);
        }
    }

    public void StepForward()
    {
        transform.Translate(ZERO_TRANSLATION, ZERO_TRANSLATION, STEP);
        transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, (float)Math.Round(transform.position.z));

        DungeonSounds.PlayWalk();

        IncrementStepsAfterEncount();
        Encount();
    }

    public void Encount()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        if (StepsAfterEncount >= MIN_STEPS_AFTER_ENCOUNT && UnityEngine.Random.value < Dungeon.EncountRate)
        {
            UIStateManager.UIState = UIState.Battle;
            ResetStepsAfterEncount();
        }
    }

    public void IncrementStepsAfterEncount() => StepsAfterEncount++;

    public void ResetStepsAfterEncount() => StepsAfterEncount = DEFAULT_STEPS;

    public void TurnAround()
    {
        DungeonSounds.PlayTurn();

        transform.Rotate(ZERO_ROTATION, HALF_ROTATION, ZERO_ROTATION);
        transform.Translate(ZERO_TRANSLATION, ZERO_TRANSLATION, -STEP);

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
        DungeonSounds.PlayTurn();

        transform.Rotate(ZERO_ROTATION, -QUAURTER_ROTATION, ZERO_ROTATION);
        transform.Translate(HALF_STEP, ZERO_TRANSLATION, -HALF_STEP);

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
        DungeonSounds.PlayTurn();

        transform.Rotate(ZERO_ROTATION, QUAURTER_ROTATION, ZERO_ROTATION);
        transform.Translate(-HALF_STEP, ZERO_ROTATION, -HALF_STEP);

        Direction = Direction switch
        {
            Direction.east => Direction.south,
            Direction.south => Direction.west,
            Direction.west => Direction.north,
            Direction.north => Direction.east,
            _ => Direction,
        };
    }

    public string ConvertNumberFromCardinalToOrdinal(int num) => (num % DECIMAL_BASE) switch
    {
        1 => $"{num}st",
        2 => $"{num}nd",
        3 => $"{num}rd",
        _ => $"{num}th",
    };
}