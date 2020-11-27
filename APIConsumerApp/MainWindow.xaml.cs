using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace APIDemoConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string APIUri = $@"https://fzhou26-eval-test.apigee.net/apidemo/api/city";
        private static readonly HttpClient webClient = new HttpClient();
        private List<City> cityList;
        private City selectedCity;

        public MainWindow()
        {
            InitializeComponent();
            cityList = new List<City>();
        }

        private async Task<bool> getCityByIDAsync(string id)
        {
            try
            {
                //var request = new HttpRequestMessage()
                //{
                //    Method = HttpMethod.Get,
                //    RequestUri = new Uri($@"{ APIUri }/{ id }"),
                //    Headers = {
                //    { "X-apikey", txtBxAPIKey.Text }
                //}
                //};

                var response = await webClient.GetAsync($@"{ APIUri }/{ id }?apikey={ txtBxAPIKey.Text }");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var payload = await response.Content.ReadAsStringAsync();
                selectedCity = JsonConvert.DeserializeObject<City>(payload);
                updateSelectedCity();
                return true;
            }
            catch (Exception) { return false; }
        }

        private async Task<bool> updateCityAsync()
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(selectedCity), Encoding.UTF8, "application/json");
                var response = await webClient.PutAsync($@"{ APIUri }/{ selectedCity.ID }?apikey={ txtBxAPIKey.Text }", content);
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    return false;
                };
                return true;
            }
            catch (Exception) { return false; }
        }

        private async Task<bool> addCityAsync()
        {
            try
            {
                var temp = selectedCity.ID;
                selectedCity.ID = string.Empty;
                var content = new StringContent(JsonConvert.SerializeObject(selectedCity), Encoding.UTF8, "application/json");
                var response = await webClient.PostAsync($@"{ APIUri }/?apikey={ txtBxAPIKey.Text }", content);
                selectedCity.ID = temp;
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    return false;
                };
                return true;
            }
            catch (Exception) { return false; }
        }

        private async Task<bool> deleteCityAsync(string id)
        {
            try
            {
                var response = await webClient.DeleteAsync($@"{ APIUri }/{ id }?apikey={ txtBxAPIKey.Text }");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                };
                return true;
            }
            catch (Exception) { return false; }
        }

        private async Task<bool> updateCityListAsync()
        {
            cityList.Clear();
            try
            {
                var response = await webClient.GetAsync($@"{ APIUri }?apikey={ txtBxAPIKey.Text }");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var payload = await response.Content.ReadAsStringAsync();
                cityList = JsonConvert.DeserializeObject<List<City>>(payload);
            }
            catch (Exception)
            {
                return false;
            }
            dtGrdCityList.ItemsSource = cityList;
            dtGrdCityList.Items.Refresh();
            return true;
        }

        private void updateSelectedCity()
        {
            if (selectedCity != null)
            {
                txtBxID.Text = selectedCity.ID;
                txtBxName.Text = selectedCity.Name;
                txtBxCountry.Text = selectedCity.Country;
                txtBxCurrency.Text = selectedCity.Currency;
                txtBxImageURI.Text = selectedCity.ImageURI;
                txtBxGuideURI.Text = selectedCity.GuideURI;
                txtBxRegion.Text = selectedCity.Region;
                txtBxClimate.Text = selectedCity.Climate;
            }
        }

        private void clearSelectedBook()
        {
            selectedCity = null;
            txtBxID.Text = string.Empty;
            txtBxName.Text = string.Empty;
            txtBxCountry.Text = string.Empty;
            txtBxCurrency.Text = string.Empty;
            txtBxImageURI.Text = string.Empty;
            txtBxGuideURI.Text = string.Empty;
            txtBxRegion.Text = string.Empty;
            txtBxClimate.Text = string.Empty;
        }

        private void updateUserSelection()
        {
            selectedCity.Name = txtBxName.Text;
            selectedCity.Climate = txtBxClimate.Text;
            selectedCity.Country = txtBxCountry.Text;
            selectedCity.Currency = txtBxCurrency.Text;
            selectedCity.ImageURI = txtBxImageURI.Text;
            selectedCity.GuideURI = txtBxGuideURI.Text;
            selectedCity.Region = txtBxRegion.Text;
        }

        private void dtGrdCityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((City)dtGrdCityList.SelectedItem != null)
                {
                    selectedCity = (City)dtGrdCityList.SelectedItem;
                    updateSelectedCity();
                }
            }
            catch (Exception) { }
        }

        private async void btnGetCities_Click(object sender, RoutedEventArgs e)
        {
            btnGetCities.IsEnabled = false;
            if (!await updateCityListAsync())
            {
                MessageBox.Show("Something went wrong.");
            }
            btnGetCities.IsEnabled = true;
        }

        private void btnClearCity_Click(object sender, RoutedEventArgs e)
        {
            clearSelectedBook();
        }

        private async void btnGetCity_Click(object sender, RoutedEventArgs e)
        {
            btnGetCity.IsEnabled = false;
            if (!await getCityByIDAsync(txtBxCityID.Text))
            {
                MessageBox.Show("Something went wrong.");
            }
            btnGetCity.IsEnabled = true;
        }

        private async void btnAddCity_Click(object sender, RoutedEventArgs e)
        {
            btnAddCity.IsEnabled = false;
            updateUserSelection();
            if (!await addCityAsync())
            {
                MessageBox.Show("Something went wrong.");
            }
            await updateCityListAsync();
            btnAddCity.IsEnabled = true;
        }

        private async void btnUpdateCity_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateCity.IsEnabled = false;
            updateUserSelection();
            if (!await updateCityAsync())
            {
                MessageBox.Show("Something went wrong.");
            }
            await updateCityListAsync();
            btnUpdateCity.IsEnabled = true;
        }

        private async void btnDeleteCity_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteCity.IsEnabled = false;
            if (!await deleteCityAsync(selectedCity.ID))
            {
                MessageBox.Show("Something went wrong.");
            }
            await updateCityListAsync();
            btnDeleteCity.IsEnabled = true;
        }
    }
}
