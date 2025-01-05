using System;

namespace ecosystem;

public class Algue : Vegetaux
{
    public Algue(double x, double y) : base(x, y, 30, 40) // Ajuster les valeurs ici
    {
        //ici, ZoneDeRacines = 30 et ZoneDeSemis = 40 par Algue
    }

    protected override Vegetaux CreerNouvellePlante(double x, double y)
    {
        return new Algue(x, y);
    }
}
