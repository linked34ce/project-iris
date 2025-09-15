using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class EnemyContainer : CharacterContainer
{
    [SerializeField] private int _hp;
    [SerializeField] private int _atk;
    [SerializeField] private int _mag;
    [SerializeField] private int _def;
    [SerializeField] private int _res;
    [SerializeField] private int _agi;
    [SerializeField] private int _luk;
    [SerializeField] private int _dropExp;
    [SerializeField] private EnemyView _view;

    public IEnemy Enemy;

    private readonly Logger _logger = new();

    public override void Initialize() =>
        Enemy = new Enemy(
            _name,
            _level,
            _hp,
            _atk,
            _mag,
            _def,
            _res,
            _agi,
            _luk,
            _dropExp,
            _view,
            _logger
        );
}
