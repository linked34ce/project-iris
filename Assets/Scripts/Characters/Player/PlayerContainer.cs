using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class PlayerContainer : CharacterContainer
{
    [SerializeField] private string _role;

    [SerializeField] private PlayerView _view;
    [SerializeField] private BattleSoundProvider _soundProvider;
    [SerializeField] private BattleEffectController _effectController;

    public IPlayer Player;

    private readonly Logger _logger = new();

    public override void Initialize() =>
        Player = new Player(
            _name,
            _level,
            _role,
            _view,
            _soundProvider,
            _effectController,
            _logger
        );
}
