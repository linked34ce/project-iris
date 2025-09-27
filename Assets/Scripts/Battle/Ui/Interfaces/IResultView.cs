public interface IBattleResult
{
    bool IsShown { get; }
    event System.Action Confirmed;
    void Show(IPlayer player);
    void Hide();
}
