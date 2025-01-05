using System;

namespace ecosystem;

public abstract class EtreVivant : SimulationObjet
{
    public int Energie { get; set; }
    public int Vie { get; set; }

    private Random random = new Random();

    protected EtreVivant(double x, double y, int energie, int vie) : base(x, y)
    {
        Energie = energie;
        Vie = vie;
    }

    public override void Update()
    {
        Energie--;
        if (Energie <= 0)
        {
            Vie--;
        }

        if (Vie > 0 && Energie > 0)
        {
            DeplacementAleatoire(); // Ajout du déplacement aléatoire par défaut
        }
    }

    protected virtual void DeplacementAleatoire()
    {
        // Déplacement aléatoire dans une zone limitée
        double deltaX = random.Next(-10, 11); // Déplacement entre -10 et 10
        double deltaY = random.Next(-10, 11);

        // Mise à jour des coordonnées
        X = Math.Max(0, Math.Min(800, X + deltaX)); // Garde dans les limites de l'écran
        Y = Math.Max(0, Math.Min(500, Y + deltaY));
    }
}

