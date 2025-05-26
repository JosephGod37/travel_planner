using Avalonia.Controls;
using System.Collections.Generic;

namespace travel_planner;

public partial class SummaryWindow : Window
{
    public SummaryWindow(string name, string country, string transport, List<string> attractions, List<string> cities)
    {
        InitializeComponent();

        NameText.Text = $"Podróżujący: {name}";
        CountryText.Text = $"Kraj docelowy: {country}";
        TransportText.Text = $"Środek transportu: {transport}";

        AttractionsList.ItemsSource = attractions;
        CitiesList.ItemsSource = cities;
    }
}