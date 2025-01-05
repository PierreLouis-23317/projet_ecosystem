using System.Linq;


namespace ecosystem;

public class Requin : Carnivore
{
    public Requin(double x, double y) : base(x, y, 100, 50) { }

    public override void Update()
    {
        base.Update();

        // Si aucune proie n'est visible, effectuer un déplacement aléatoire
        if (!ProieEnVue())
        {
            DeplacementAleatoire();
        }
    }

    private bool ProieEnVue()
    {
        return Simulation.Instance.GetObjects()
            .OfType<Herbivore>()
            .Any(p => Distance(this, p) < ZoneDeVision);
    }

    protected override Animal CreerDescendant()
    {
        return new Requin(X + 10, Y + 10);
    }
}
