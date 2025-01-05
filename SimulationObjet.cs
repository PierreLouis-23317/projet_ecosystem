namespace ecosystem;

public abstract class SimulationObjet
{
    public double X { get; set; }
    public double Y { get; set; }

    protected SimulationObjet(double x, double y)
    {
        X = x;
        Y = y;
    }

    public abstract void Update(); // MAJ object
}
