using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;

namespace ecosystem;

public partial class MainWindow : Window
{
    private Simulation simulation;
    private TimerManager timer;

    public MainWindow()
    {
        InitializeComponent();
        simulation = Simulation.Instance;
        timer = new TimerManager(UpdateSimulation, 500);
    }

    private void UpdateSimulation()
    {
        simulation.Update();
        RefreshCanvas();
    }

    private void RefreshCanvas()
    {
        var canvas = this.FindControl<Canvas>("SimulationCanvas");
        if (canvas == null) return;

        canvas.Children.Clear();

        foreach (var obj in simulation.GetObjects())
        {
            var ellipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = obj switch
                {
                    Carnivore => Brushes.Red,
                    Herbivore => Brushes.Blue,
                    Vegetaux => Brushes.Green,
                    Viande => Brushes.Brown,
                    DechetOrganique => Brushes.Gray,
                    _ => Brushes.Black
                }
            };

            Canvas.SetLeft(ellipse, obj.X);
            Canvas.SetTop(ellipse, obj.Y);

            canvas.Children.Add(ellipse);
        }
    }

    private void OnAddRequin(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var random = new Random();
        double x = random.NextDouble() * 800; // Largeur de la grille
        double y = random.NextDouble() * 500; // Hauteur de la grille

        simulation.Add(new Requin(x, y));
        RefreshCanvas();
    }

    private void OnAddPoisson(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var random = new Random();
        double x = random.NextDouble() * 800; // Largeur de la grille
        double y = random.NextDouble() * 500; // Hauteur de la grille

        simulation.Add(new Poisson(x, y));
        RefreshCanvas();
    }

    private void OnAddAlgue(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var random = new Random();
        double x = random.NextDouble() * 800; // Largeur de la grille
        double y = random.NextDouble() * 500; // Hauteur de la grille

        simulation.Add(new Algue(x, y));
        RefreshCanvas();
    }


    private void OnAccelerate(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        double currentInterval = timer.GetInterval();
        if (currentInterval > 100) // securité, empeche >= 0
        {
            timer.SetInterval(currentInterval - 100); // Réduit l’intervalle de 100 ms
        }
    }

    private void OnDecelerate(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        double currentInterval = timer.GetInterval();
        timer.SetInterval(currentInterval + 100); // Augmente l’intervalle de 100 ms
    }



}

