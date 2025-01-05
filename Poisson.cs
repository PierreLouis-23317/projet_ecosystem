using System.Linq;

namespace ecosystem;

public class Poisson : Herbivore
{
    public Poisson(double x, double y) : base(x, y, 80, 40) { }

    public override void Update()
    {
        base.Update();

        // Si aucune plante n'est visible, effectuer un déplacement aléatoire
        if (!PlanteEnVue())
        {
            DeplacementAleatoire();
        }
    }

    private bool PlanteEnVue()
    {
        return Simulation.Instance.GetObjects()
            .OfType<Vegetaux>()
            .Any(p => Distance(this, p) < ZoneDeVision);
    }

    protected override Animal CreerDescendant()
    {
        return new Poisson(X + 10, Y + 10);
    }
}
