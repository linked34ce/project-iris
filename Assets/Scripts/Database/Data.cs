using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public abstract class Data : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField] private int _id;

    public int Id => _id;
}
