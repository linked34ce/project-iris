public interface IDamageable : IHpShowable
{
    public void TakeDamage(int damage);
    public bool IsAlive { get; set; }
}
