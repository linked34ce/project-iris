using UnityEngine;

public class PlayerContainer : CharacterContainer
{
    [SerializeField] private string _role;

    [SerializeField] private PlayerView _view;
    [SerializeField] private BattleSoundProvider _soundProvider;

    public IPlayer Player;

    public override void Initialize() =>
        Player = new Player(
            _name,
            _level,
            _role,
            _view,
            _soundProvider
        );
}
