using System;
using System.Linq;

namespace ecosystem;

public abstract class Herbivore : Animal
{
    protected Herbivore(double x, double y, int energie, int vie) : base(x, y, energie, vie) { }

    protected override void ChercherNourriture()
    {
        // Chercher une plante à proximité
        var planteProche = Simulation.Instance.GetObjects()
            .OfType<Vegetaux>()
            .FirstOrDefault(p => Distance(this, p) < ZoneDeContact);

        if (planteProche != null)
        {
            Simulation.Instance.Remove(planteProche);
            Energie += 30; // Gain d'énergie
            return;
        }

        // Chercher une plante dans la zone de vision
        var planteDansVision = Simulation.Instance.GetObjects()
            .OfType<Vegetaux>()
            .Where(p => Distance(this, p) < ZoneDeVision)
            .OrderBy(p => Distance(this, p))
            .FirstOrDefault();

        if (planteDansVision != null)
        {
            SeDirigerVers(planteDansVision);
        }
    }

    private void SeDirigerVers(SimulationObjet cible)
    {
        // Calculer le vecteur de déplacement
        double deltaX = cible.X - this.X;
        double deltaY = cible.Y - this.Y;

        // Normaliser le vecteur
        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        double step = 4; // Vitesse de déplacement (légèrement plus lente que les carnivores)

        if (distance > 0)
        {
            this.X += (deltaX / distance) * step;
            this.Y += (deltaY / distance) * step;
        }
    }
}
