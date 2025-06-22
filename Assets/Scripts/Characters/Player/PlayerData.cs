using System.Collections.Generic;

using UnityEngine;

public class PlayerData : CharacterData
{
    public string Role { get; set; }

    private int _exp;
    public int Exp
    {
        get => _exp;
        set
        {
            _exp = value;
            if (Level < ExpList.Length && _exp >= ExpList[Level - 1])
            {
                LevelUp();
                HasLeveledUp = true;
            }
        }
    }
    public int NextExp => Level < ExpList.Length ? ExpList[Level - 1] - Exp : 0;

    private int _sp;
    public int Sp
    {
        get => _sp;
        set
        {
            _sp = Mathf.Clamp(value, 0, MaxSp);
        }
    }
    public int MaxSp { get; set; }

    public bool HasLeveledUp { get; set; } = false;

    // this property should be refined
    public int[] ExpList { get; } = {
        10, 30, 60, 100, 150, 210, 280, 360, 450, 550,
    };

    // properties below should be deleted when class for each character is made
    public Dictionary<int, int> HpList { get; } = new(){
        {1, 21},
        {2, 22},
        {3, 24},
        {4, 26},
        {5, 28},
        {6, 31},
        {7, 34},
        {8, 37},
        {9, 40},
        {10, 44},
    };

    public Dictionary<int, int> SpList { get; } = new(){
        {1, 12},
        {2, 13},
        {3, 14},
        {4, 15},
        {5, 16},
        {6, 17},
        {7, 18},
        {8, 19},
        {9, 21},
        {10, 23},
    };

    public Dictionary<int, int> AtkList { get; } = new(){
        {1, 2},
        {2, 2},
        {3, 3},
        {4, 3},
        {5, 3},
        {6, 3},
        {7, 4},
        {8, 4},
        {9, 4},
        {10, 5},
    };

    public Dictionary<int, int> MagList { get; } = new(){
        {1, 5},
        {2, 6},
        {3, 6},
        {4, 7},
        {5, 7},
        {6, 8},
        {7, 8},
        {8, 9},
        {9, 9},
        {10, 10},
    };

    public Dictionary<int, int> DefList { get; } = new(){
        {1, 2},
        {2, 2},
        {3, 3},
        {4, 3},
        {5, 3},
        {6, 3},
        {7, 4},
        {8, 4},
        {9, 4},
        {10, 4},
    };

    public Dictionary<int, int> ResList { get; } = new(){
        {1, 6},
        {2, 7},
        {3, 7},
        {4, 8},
        {5, 8},
        {6, 8},
        {7, 9},
        {8, 9},
        {9, 10},
        {10, 11},
    };

    public Dictionary<int, int> AgiList { get; } = new(){
        {1, 2},
        {2, 2},
        {3, 2},
        {4, 3},
        {5, 3},
        {6, 3},
        {7, 4},
        {8, 4},
        {9, 4},
        {10, 5},
    };

    public Dictionary<int, int> LukList { get; } = new(){
        {1, 4},
        {2, 4},
        {3, 5},
        {4, 5},
        {5, 6},
        {6, 6},
        {7, 7},
        {8, 8},
        {9, 8},
        {10, 9},
    };

    public PlayerData(string name, int level) : base(name, level)
    {
        SetParametersBasedOnLevel();
    }

    private void SetParametersBasedOnLevel()
    {
        MaxHp = HpList.GetValueOrDefault(Level);
        MaxSp = SpList.GetValueOrDefault(Level);

        Hp = MaxHp;
        Sp = MaxSp;

        Atk = AtkList.GetValueOrDefault(Level);
        Mag = MagList.GetValueOrDefault(Level);
        Def = DefList.GetValueOrDefault(Level);
        Res = ResList.GetValueOrDefault(Level);
        Agi = AgiList.GetValueOrDefault(Level);
        Luk = LukList.GetValueOrDefault(Level);
    }

    private void LevelUp()
    {
        while (Level < ExpList.Length && Exp >= ExpList[Level - 1])
        {
            Level++;
            SetParametersBasedOnLevel();
        }
    }
}
