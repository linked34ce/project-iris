using System;
using System.Security.Cryptography;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    const float STEP = 10f;
    const int MIN_STEPS_AFTER_ENCOUNT = 5;
    private int stepsAfterEncount = 3;
    private readonly Location location = new(9, 0);
    private Direction direction = Direction.north;
    private readonly Dungeon dungeon = new(0.1f);

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        GetComponent<BattleManager>().enabled = false;
    }
    // Update is called once per frame
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
        Walls walls = dungeon.GetMap()[location.GetY()][location.GetX()];

        if (direction == Direction.east)
        {
            if (walls.GetEast() == 0)
            {
                location.IncrementX();
                StepForward();
            }
            else if (walls.GetEast() == 2)
            {
                GoUpstairs();
            }
        }

        if (direction == Direction.south)
        {
            if (walls.GetSouth() == 0)
            {
                location.DecrementY();
                StepForward();
            }
            else if (walls.GetSouth() == 2)
            {
                GoUpstairs();
            }
        }

        if (direction == Direction.west)
        {
            if (walls.GetWest() == 0)
            {
                location.DecrementX();
                StepForward();
            }
            else if (walls.GetWest() == 2)
            {
                GoUpstairs();
            }
        }

        if (direction == Direction.north)
        {
            if (walls.GetNorth() == 0)
            {
                location.IncrementY();
                StepForward();
            }
            else if (walls.GetNorth() == 2)
            {
                GoUpstairs();
            }
        }
    }

    public void GoUpstairs()
    {
        GetComponent<Footsteps>().PlayStairs();

        string ordinal = ConvertNumberFromCardinalToOrdinal(++Status.floor);
        Initiate.Fade($"Scenes/Dungeons/Nijigasaki Old School Building/{ordinal} Floor", Color.black, 0.4f);
    }

    public void StepForward()
    {
        transform.Translate(0f, 0f, STEP);
        transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, (float)Math.Round(transform.position.z));

        GetComponent<Footsteps>().PlayFootStep();

        IncrementStepsAfterEncount();
        Encount();
    }

    public void Encount()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        if (stepsAfterEncount >= MIN_STEPS_AFTER_ENCOUNT && UnityEngine.Random.value < dungeon.GetEncountRate())
        {
            GetComponent<BattleManager>().enabled = true;
            ResetStepsAfterEncount();
        }
        // Debug.Log("(" + location.GetX() + ", " + location.GetY() + ")");
        // Debug.Log("(" + walls.GetEast() + ", " + walls.GetSouth() + ", " + walls.GetWest() + ", " + walls.GetNorth() + ")");
        Debug.Log(stepsAfterEncount);
    }

    public void IncrementStepsAfterEncount()
    {
        stepsAfterEncount++;
    }

    public void ResetStepsAfterEncount()
    {
        stepsAfterEncount = 0;
    }


    private void TurnAround()
    {
        Footsteps footsteps = GetComponent<Footsteps>();

        footsteps.PlayTurn();
        transform.Rotate(0f, 180f, 0f);
        transform.Translate(0f, 0f, -STEP);

        switch (direction)
        {

            case Direction.east:
                direction = Direction.west;
                break;
            case Direction.south:
                direction = Direction.north;
                break;
            case Direction.west:
                direction = Direction.east;
                break;
            case Direction.north:
                direction = Direction.south;
                break;
        }
    }

    private void TurnLeft()
    {
        Footsteps footsteps = GetComponent<Footsteps>();

        footsteps.PlayTurn();
        transform.Rotate(0f, -90f, 0f);
        transform.Translate(STEP / 2, 0f, -STEP / 2);

        switch (direction)
        {
            case Direction.east:
                direction = Direction.north;
                break;
            case Direction.south:
                direction = Direction.east;
                break;
            case Direction.west:
                direction = Direction.south;
                break;
            case Direction.north:
                direction = Direction.west;
                break;

        }
    }

    private void TurnRight()
    {
        Footsteps footsteps = GetComponent<Footsteps>();

        footsteps.PlayTurn();
        transform.Rotate(0f, 90f, 0f);
        transform.Translate(-STEP / 2, 0f, -STEP / 2);

        switch (direction)
        {
            case Direction.east:
                direction = Direction.south;
                break;
            case Direction.south:
                direction = Direction.west;
                break;
            case Direction.west:
                direction = Direction.north;
                break;
            case Direction.north:
                direction = Direction.east;
                break;

        }
    }

    public string ConvertNumberFromCardinalToOrdinal(int num)
    {
        int onesPlace = num % 10;
        switch (onesPlace)
        {
            case 1:
                return $"{num}st";
            case 2:
                return $"{num}nd";
            case 3:
                return $"{num}rd";
            default:
                return $"{num}th";
        }
    }
}