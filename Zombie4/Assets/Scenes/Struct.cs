/// <summary>
/// almacenas los datos del zombie
/// </summary>
public struct InfoZombie
{
    public string gusto;
    public string nombre;
    public int edad;
}
/// <summary>
/// almacena los datos del todo y hace conversiones
/// </summary>
public struct InfoAlde
{
    public string name;
    public int edad;
    static public implicit operator InfoZombie(InfoAlde c)
    {
        InfoZombie z = new InfoZombie();
        z.gusto = "Cerebros";
        z.edad = c.edad;
        z.nombre = "Zombie " + c.name;
        return z;
    }
}