using System;
using System.Linq;

namespace ecosystem;

public abstract class Vegetaux : EtreVivant
{
    public double ZoneDeRacines { get; protected set; }
    public double ZoneDeSemis { get; protected set; }

    protected Vegetaux(double x, double y, double zoneDeRacines, double zoneDeSemis) : base(x, y, 0, 20)
    {
        ZoneDeRacines = zoneDeRacines;
        ZoneDeSemis = zoneDeSemis;
    }

    public override void Update()
    {
        base.Update();

        // Manger les déchets organiques dans la zone de racines.
        MangerDechets();

        // Propagation de nouvelles plantes (2 % de chance par tick).
        if (new Random().Next(100) < 2)
        {
            Propager();
        }
    }

    private void MangerDechets()
    {
        var dechetProche = Simulation.Instance.GetObjects()
            .OfType<DechetOrganique>()
            .FirstOrDefault(dechet => DistanceTo(dechet) <= ZoneDeRacines);

        if (dechetProche != null)
        {
            // Consommer le déchet
            Simulation.Instance.Remove(dechetProche);
            Energie += 5;
        }
    }

    private void Propager()
    {
        var random = new Random();

        double offsetX = random.NextDouble() * ZoneDeSemis * 2 - ZoneDeSemis;
        double offsetY = random.NextDouble() * ZoneDeSemis * 2 - ZoneDeSemis;

        Simulation.Instance.Add(CreerNouvellePlante(X + offsetX, Y + offsetY));
    }

    protected abstract Vegetaux CreerNouvellePlante(double x, double y);

    private double DistanceTo(SimulationObjet objet)
    {
        return Math.Sqrt(Math.Pow(X - objet.X, 2) + Math.Pow(Y - objet.Y, 2));
    }
}
