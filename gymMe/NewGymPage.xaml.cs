using System;
using System.Collections.Generic;
using System.Linq;
using gymMe.Logic;
using gymMe.Model;
using Plugin.Geolocator;
using SQLite;
using Xamarin.Forms;

namespace gymMe
{
    public partial class NewGymPage : ContentPage
    {
        public NewGymPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;

        }

        private void Save_Clicked(object sender, System.EventArgs e)
        {
            try
            {

                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Insert(post);

                    if (rows > 0)
                        DisplayAlert("Success", "Experience successfully updated", "OK");
                    else
                        DisplayAlert("Failure", "Experience failed to update", "OK");

                }
            }
            catch (NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
