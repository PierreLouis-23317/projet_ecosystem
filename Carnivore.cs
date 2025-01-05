using System;
using System.Linq;

namespace ecosystem;

public abstract class Carnivore : Animal
{
    protected Carnivore(double x, double y, int energie, int vie) : base(x, y, energie, vie) { }

    protected override void ChercherNourriture()
    {
        // Chercher de la viande dans la zone de contact
        var viandeEnContact = Simulation.Instance.GetObjects()
            .OfType<Viande>()
            .FirstOrDefault(v => Distance(this, v) < ZoneDeContact);

        if (viandeEnContact != null)
        {
            Simulation.Instance.Remove(viandeEnContact);
            Energie += 50; // Récupère de l'énergie en mangeant
            return;
        }

        // Chercher de la viande dans la zone de vision
        var viandeProche = Simulation.Instance.GetObjects()
            .OfType<Viande>()
            .Where(v => Distance(this, v) < ZoneDeVision)
            .OrderBy(v => Distance(this, v))
            .FirstOrDefault();

        if (viandeProche != null)
        {
            SeDirigerVers(viandeProche);
            return;
        }

        // Trouver une proie dans la zone de contact
        var proieEnContact = Simulation.Instance.GetObjects()
            .OfType<Herbivore>()
            .FirstOrDefault(p => Distance(this, p) < ZoneDeContact);

        if (proieEnContact != null)
        {
            AttaquerProie(proieEnContact);
            return;
        }

        // Sinon, chercher une proie dans la zone de vision
        var proieProche = Simulation.Instance.GetObjects()
            .OfType<Herbivore>()
            .Where(p => Distance(this, p) < ZoneDeVision)
            .OrderBy(p => Distance(this, p))
            .FirstOrDefault();

        if (proieProche != null)
        {
            SeDirigerVers(proieProche);
        }
    }

    private void AttaquerProie(Herbivore proie)
    {
        proie.Vie -= 10; // Réduit les points de vie de la proie

        if (proie.Vie <= 0)
        {
            // Transforme la proie en viande
            Simulation.Instance.Remove(proie);
            Simulation.Instance.Add(new Viande(proie.X, proie.Y));
        }
    }

    private void SeDirigerVers(SimulationObjet cible)
    {
        // Calculer le vecteur de déplacement
        double deltaX = cible.X - this.X;
        double deltaY = cible.Y - this.Y;

        // Normaliser le vecteur
        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        double step = 5; // Vitesse de déplacement

        if (distance > 0)
        {
            this.X += (deltaX / distance) * step;
            this.Y += (deltaY / distance) * step;
        }
    }
}
