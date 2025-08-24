using System;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private UiStateManager _uiStateManager;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private DungeonSoundProvider _soundProvider;
    private int _stepsAfterEncount = 3;
    private Direction _direction = Direction.north;
    private readonly Location _location = new();
    private readonly Dungeon _dungeon = new();
    private bool _hasGoneUpstairs = false;

    private const float ZeroTranslation = 0f;
    private const float Step = 10f;
    private const float HalfStep = 5f;
    private const float ZeroRotation = 0f;
    private const float QuarterRotation = 90f;
    private const float HalfRotation = 180f;
    private const int MinStepsAfterEncount = 5;
    private const int DefaultSteps = 0;
    private const string NextFloorScenePrefix = "Scenes/Dungeons/TohoGakuenOldBuilding/";
    private const string NextFloorSceneSuffix = "Floor";

    void Awake() => UnityEngine.Random.InitState(Environment.TickCount);

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

    private void ForwardPressed()
    {
        Walls walls = _dungeon.Map[_location.Y][_location.X];

        switch (_direction)
        {
            case Direction.east:
                switch (walls.East)
                {
                    case Wall.air:
                        _location.X++;
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
                        _location.Y--;
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
                        _location.X--;
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
                        _location.Y++;
                        StepForward();
                        break;
                    case Wall.stairs:
                        GoUpstairs();
                        break;
                }
                break;
        }
    }

    private void GoUpstairs()
    {
        if (!_hasGoneUpstairs)
        {
            _hasGoneUpstairs = true;
            _soundProvider.PlayStairs();
            Status.Floor++;

            string ordinal = Converter.ToOrdinal(Status.Floor);
            _sceneLoader.LoadScene($"{NextFloorScenePrefix}{ordinal}{NextFloorSceneSuffix}");
        }
    }

    private void StepForward()
    {
        transform.Translate(ZeroTranslation, ZeroTranslation, Step);
        transform.position = new Vector3(
            (float)Math.Round(transform.position.x),
            transform.position.y,
            (float)Math.Round(transform.position.z)
        );
        _soundProvider.PlayWalk();

        _stepsAfterEncount++;
        Encount();
    }

    private void Encount()
    {
        if (
            _stepsAfterEncount >= MinStepsAfterEncount
            && UnityEngine.Random.value < _dungeon.EncountRate
        )
        {
            _stepsAfterEncount = DefaultSteps;
            _uiStateManager.UiState = UiState.Battle;
        }
    }

    private void TurnAround()
    {
        _soundProvider.PlayTurn();

        transform.Rotate(ZeroRotation, HalfRotation, ZeroRotation);
        transform.Translate(ZeroTranslation, ZeroTranslation, -Step);

        _direction = _direction switch
        {
            Direction.east => Direction.west,
            Direction.south => Direction.north,
            Direction.west => Direction.east,
            Direction.north => Direction.south,
            _ => _direction,
        };
    }

    private void TurnLeft()
    {
        _soundProvider.PlayTurn();

        transform.Rotate(ZeroRotation, -QuarterRotation, ZeroRotation);
        transform.Translate(HalfStep, ZeroTranslation, -HalfStep);

        _direction = _direction switch
        {
            Direction.east => Direction.north,
            Direction.south => Direction.east,
            Direction.west => Direction.south,
            Direction.north => Direction.west,
            _ => _direction,
        };
    }

    private void TurnRight()
    {
        _soundProvider.PlayTurn();

        transform.Rotate(ZeroRotation, QuarterRotation, ZeroRotation);
        transform.Translate(-HalfStep, ZeroTranslation, -HalfStep);

        _direction = _direction switch
        {
            Direction.east => Direction.south,
            Direction.south => Direction.west,
            Direction.west => Direction.north,
            Direction.north => Direction.east,
            _ => _direction,
        };
    }

}
