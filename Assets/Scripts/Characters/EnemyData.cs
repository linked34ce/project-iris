public class EnemyData : CharacterData
{
    public int DropExp { get; set; }

    public EnemyData(
        string name,
        int level,
        int hp,
        int atk,
        int mag,
        int def,
        int res,
        int agi,
        int luk,
        int dropExp
    ) : base(name, level, hp, atk, mag, def, res, agi, luk)
    {
        DropExp = dropExp;
    }
}
