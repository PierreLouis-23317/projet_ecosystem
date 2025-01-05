namespace ecosystem;

public class Viande : SimulationObjet
{
    private int tempsAvantDecomposition = 100; // Ticks avant décomposition

    public Viande(double x, double y) : base(x, y) { }

    public override void Update()
    {
        tempsAvantDecomposition--;

        if (tempsAvantDecomposition <= 0)
        {
            Simulation.Instance.Add(new DechetOrganique(X, Y));
            Simulation.Instance.Remove(this);
        }
    }
}
