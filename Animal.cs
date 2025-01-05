using System;
using System.Linq;

namespace ecosystem;

public abstract class Animal : EtreVivant
{
    public double ZoneDeVision { get; set; } = 50;
    public double ZoneDeContact { get; set; } = 10;
    public bool Sexe { get; private set; } // true = Mâle, false = Femelle

    private bool EnGestation { get; set; } = false;
    private int TempsDeGestation { get; set; } = 0;
    private const int DureeGestation = 50; // Période de gestation en ticks

    protected Animal(double x, double y, int energie, int vie) : base(x, y, energie, vie)
    {
        // Détermine aléatoirement le sexe (mâle ou femelle)
        Sexe = new Random().Next(2) == 0;
    }

    public override void Update()
    {
        // 1. Appel de la logique de base (diminution d'énergie, vie, etc.)
        base.Update();

        // 2. Vérifier si l'animal est mort naturellement après base.Update()
        if (Vie <= 0)
        {
            // On transforme l'animal mort en Viande, puis on le retire de la simulation
            Simulation.Instance.Add(new Viande(X, Y));
            Simulation.Instance.Remove(this);
            return; // Fin de l'Update, l'animal n'existe plus
        }

        // 3. Chercher la nourriture (méthode abstraite à implémenter dans Carnivore / Herbivore)
        ChercherNourriture();

        // 4. Gestion de la reproduction
        if (EnGestation)
        {
            TempsDeGestation++;
            if (TempsDeGestation >= DureeGestation)
            {
                DonnerNaissance();
            }
        }
        else
        {
            var partenaire = TrouverPartenaire();
            if (partenaire != null)
            {
                CommencerGestation();
            }
        }
    }

    protected abstract void ChercherNourriture();

    private Animal? TrouverPartenaire()
    {
        return Simulation.Instance.GetObjects()
            .OfType<Animal>()
            .FirstOrDefault(a =>
                a != this && 
                a.GetType() == this.GetType() && 
                a.Sexe != this.Sexe && 
                Distance(this, a) < ZoneDeContact);
    }

    private void CommencerGestation()
    {
        EnGestation = true;
        TempsDeGestation = 0;
    }

    private void DonnerNaissance()
    {
        EnGestation = false;

        // Crée un petit près de la femelle
        var petit = CreerDescendant();
        petit.X = this.X + new Random().Next(-10, 10);
        petit.Y = this.Y + new Random().Next(-10, 10);

        Simulation.Instance.Add(petit);
    }

    protected abstract Animal CreerDescendant();

    protected double Distance(SimulationObjet a, SimulationObjet b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}
