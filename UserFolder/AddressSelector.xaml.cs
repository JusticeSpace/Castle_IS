using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Castle.UserFolder
{
    public partial class AddressSelector : UserControl
    {
        private Amo_CastleEntities1 _context;
        public int? SelectedAddressID { get; private set; }

        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<Street> Streets { get; set; }
        public List<Address> Houses { get; set; }
        public List<Address> Buildings { get; set; }
        public List<Address> Entrances { get; set; }
        public List<Address> Flats { get; set; }

        public AddressSelector(Amo_CastleEntities1 context)
        {
            InitializeComponent();
            _context = context;
            LoadCountries();
            DataContext = this;
        }

        private void LoadCountries()
        {
            Countries = _context.Country.ToList();
            if (Countries == null || Countries.Count == 0)
            {
                MessageBox.Show("Нет стран в базе данных! Пожалуйста, добавьте страны.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            CountryComboBox.ItemsSource = Countries;
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem is Country selectedCountry)
            {
                // Находим все записи в Address, где CountryID соответствует выбранной стране
                var cityIds = _context.Address
                    .Where(a => a.CountryID == selectedCountry.CountryID)
                    .Select(a => a.CityID)
                    .Distinct()
                    .ToList();

                // Загружаем города, у которых CityID есть в списке cityIds
                Cities = _context.City
                    .Where(c => cityIds.Contains(c.CityID))
                    .ToList();

                if (Cities == null || Cities.Count == 0)
                {
                    MessageBox.Show($"Нет городов для страны {selectedCountry.Country1}!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                CityComboBox.ItemsSource = Cities;
                CityComboBox.IsEnabled = true;

                // Сбрасываем последующие выборы
                Streets = null;
                Houses = null;
                Buildings = null;
                Entrances = null;
                Flats = null;

                StreetComboBox.ItemsSource = null;
                HouseComboBox.ItemsSource = null;
                BuildingComboBox.ItemsSource = null;
                EntranceComboBox.ItemsSource = null;
                FlatComboBox.ItemsSource = null;

                StreetComboBox.IsEnabled = false;
                HouseComboBox.IsEnabled = false;
                BuildingComboBox.IsEnabled = false;
                EntranceComboBox.IsEnabled = false;
                FlatComboBox.IsEnabled = false;
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedItem is City selectedCity)
            {
                Streets = _context.Street
                    .Where(s => _context.Address.Any(a => a.StreetID == s.StreetID && a.CityID == selectedCity.CityID))
                    .ToList();
                if (Streets == null || Streets.Count == 0)
                {
                    MessageBox.Show($"Нет улиц для города {selectedCity.City1}!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                StreetComboBox.ItemsSource = Streets;
                StreetComboBox.IsEnabled = true;

                Houses = null;
                Buildings = null;
                Entrances = null;
                Flats = null;

                HouseComboBox.ItemsSource = null;
                BuildingComboBox.ItemsSource = null;
                EntranceComboBox.ItemsSource = null;
                FlatComboBox.ItemsSource = null;

                HouseComboBox.IsEnabled = false;
                BuildingComboBox.IsEnabled = false;
                EntranceComboBox.IsEnabled = false;
                FlatComboBox.IsEnabled = false;
            }
        }

        private void StreetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem is Country selectedCountry &&
                CityComboBox.SelectedItem is City selectedCity &&
                StreetComboBox.SelectedItem is Street selectedStreet)
            {
                Houses = _context.Address
                    .Where(a => a.CountryID == selectedCountry.CountryID &&
                                a.CityID == selectedCity.CityID &&
                                a.StreetID == selectedStreet.StreetID)
                    .ToList();
                if (Houses == null || Houses.Count == 0)
                {
                    MessageBox.Show($"Нет домов для улицы {selectedStreet.Street1} в городе {selectedCity.City1}!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                HouseComboBox.ItemsSource = Houses;
                HouseComboBox.IsEnabled = true;

                Buildings = null;
                Entrances = null;
                Flats = null;

                BuildingComboBox.ItemsSource = null;
                EntranceComboBox.ItemsSource = null;
                FlatComboBox.ItemsSource = null;

                BuildingComboBox.IsEnabled = false;
                EntranceComboBox.IsEnabled = false;
                FlatComboBox.IsEnabled = false;
            }
        }

        private void HouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HouseComboBox.SelectedItem is Address selectedHouse)
            {
                Buildings = _context.Address
                    .Where(a => a.CountryID == selectedHouse.CountryID &&
                                a.CityID == selectedHouse.CityID &&
                                a.StreetID == selectedHouse.StreetID &&
                                a.HouseNum == selectedHouse.HouseNum)
                    .ToList();
                if (Buildings == null || Buildings.Count == 0)
                {
                    MessageBox.Show($"Нет корпусов для дома {selectedHouse.HouseNum}!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                BuildingComboBox.ItemsSource = Buildings;
                BuildingComboBox.IsEnabled = true;

                Entrances = null;
                Flats = null;

                EntranceComboBox.ItemsSource = null;
                FlatComboBox.ItemsSource = null;

                EntranceComboBox.IsEnabled = false;
                FlatComboBox.IsEnabled = false;
            }
        }

        private void BuildingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HouseComboBox.SelectedItem is Address selectedHouse &&
                BuildingComboBox.SelectedItem is Address selectedBuilding)
            {
                Entrances = _context.Address
                    .Where(a => a.CountryID == selectedHouse.CountryID &&
                                a.CityID == selectedHouse.CityID &&
                                a.StreetID == selectedHouse.StreetID &&
                                a.HouseNum == selectedHouse.HouseNum &&
                                a.Building == selectedBuilding.Building)
                    .ToList();
                if (Entrances == null || Entrances.Count == 0)
                {
                    MessageBox.Show($"Нет подъездов для корпуса {selectedBuilding.Building} дома {selectedHouse.HouseNum}!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                EntranceComboBox.ItemsSource = Entrances;
                EntranceComboBox.IsEnabled = true;

                Flats = null;
                FlatComboBox.ItemsSource = null;
                FlatComboBox.IsEnabled = false;
            }
        }

        private void EntranceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HouseComboBox.SelectedItem is Address selectedHouse &&
                BuildingComboBox.SelectedItem is Address selectedBuilding &&
                EntranceComboBox.SelectedItem is Address selectedEntrance)
            {
                Flats = _context.Address
                    .Where(a => a.CountryID == selectedHouse.CountryID &&
                                a.CityID == selectedHouse.CityID &&
                                a.StreetID == selectedHouse.StreetID &&
                                a.HouseNum == selectedHouse.HouseNum &&
                                a.Building == selectedBuilding.Building &&
                                a.Entrance == selectedEntrance.Entrance)
                    .ToList();
                if (Flats == null || Flats.Count == 0)
                {
                    MessageBox.Show($"Нет квартир для подъезда {selectedEntrance.Entrance} корпуса {selectedBuilding.Building} дома {selectedHouse.HouseNum}!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                FlatComboBox.ItemsSource = Flats;
                FlatComboBox.IsEnabled = true;
            }
        }

        private void SelectAddress_Click(object sender, RoutedEventArgs e)
        {
            if (FlatComboBox.SelectedItem is Address selectedAddress)
            {
                SelectedAddressID = selectedAddress.AddressID;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите полный адрес (включая квартиру)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedAddressID = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}