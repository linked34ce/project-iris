using UnityEngine;

public abstract class Data : ScriptableObject
{
    [SerializeField] private new string name;
    public string Name
    {
        get => name;
        set => name = value;
    }

    [SerializeField] private int id;

    public int Id => id;
}