using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace travel_planner;

public partial class MainWindow : Window
{
    private List<string> selectedCities = new List<string>();

    public MainWindow()
    {
        InitializeComponent();
        this.Opened += (_, _) =>
        {
            CategoryComboBox.SelectionChanged += CategoryComboBox_OnSelectionChanged;
        };
    }

    private void OnAddCity(object sender, RoutedEventArgs e)
    {
        var city = CityTextBox.Text?.Trim();
        if (!string.IsNullOrWhiteSpace(city) && !selectedCities.Contains(city))
        {
            selectedCities.Add(city);
            CitiesListBox.ItemsSource = selectedCities.ToList();
            CityTextBox.Clear();
        }
    }

    private void OnShowSummary(object sender, RoutedEventArgs e)
    {
        var name = TaskTextBox.Text ?? "";
        var country = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";
        if (country == "Wybierz kraj docelowej podrozy")
            country = "";

        string transport = "Brak";
        if (PlaneRadio.IsChecked == true) transport = "Samolot";
        else if (CarRadio.IsChecked == true) transport = "Samochód";
        else if (TrainRadio.IsChecked == true) transport = "Pociąg";
        else if (ShipRadio.IsChecked == true) transport = "Statek";

        List<string> attractions = new List<string>();
        if (MuseumCheckBox.IsChecked == true) attractions.Add("Muzea");
        if (ParksCheckBox.IsChecked == true) attractions.Add("Parki Narodowe");
        if (MonumentsCheckBox.IsChecked == true) attractions.Add("Zabytki");
        if (RestaurantsCheckBox.IsChecked == true) attractions.Add("Restauracje");
        if (GalleriesCheckBox.IsChecked == true) attractions.Add("Galerie sztuki");
        if (FestivalsCheckBox.IsChecked == true) attractions.Add("Festiwale i koncerty");

        var summaryWindow = new SummaryWindow(name, country, transport, attractions, selectedCities);
        summaryWindow.ShowDialog(this);
    }

    private void CategoryComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var country = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";
        if (country == "Wybierz kraj docelowej podrozy")
            country = "";

        var imagePath = "";

        switch (country.ToLower())
        {
            case "włochy":
                imagePath = "avares://travel_planner/Assets/wlochy.jpg";
                break;
            case "japonia":
                imagePath = "avares://travel_planner/Assets/japonia.jpg";
                break;
            case "norwegia":
                imagePath = "avares://travel_planner/Assets/norwegia.jpg";
                break;
            case "usa":
                imagePath = "avares://travel_planner/Assets/usa.webp";
                break;
            case "francja":
                imagePath = "avares://travel_planner/Assets/francja.jpg";
                break;
        }

        try
        {
            using var stream = AssetLoader.Open(new Uri(imagePath));
            CountryImage.Source = new Bitmap(stream);
        }
        catch (Exception)
        {
            CountryImage.Source = null;
        }
    }
}
