using System;
using System.Collections.Generic;
using System.Linq;

namespace ecosystem;

public class Simulation
{
    private readonly List<SimulationObjet> objects = new();
    public static Simulation Instance { get; } = new Simulation();

    public void Add(SimulationObjet obj) => objects.Add(obj);

    public void Remove(SimulationObjet obj) => objects.Remove(obj);

    public IEnumerable<SimulationObjet> GetObjects() => objects;

    public void Update()
    {
        foreach (var obj in objects.ToList()) // Utiliser une copie pour éviter des modifications concurrentes
        {
            obj.Update();
        }
    }
}
