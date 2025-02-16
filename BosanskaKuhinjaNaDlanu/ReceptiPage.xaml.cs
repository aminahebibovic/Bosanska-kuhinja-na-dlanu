using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BosanskaKuhinjaNaDlanu
{
    public partial class ReceptiPage : ContentPage
    {
        private string favoriteIconPath = "favorite_icon.png";
        private string favoriteIconSelectedPath = "favorite_icon_selected.png";

        public ReceptiPage()
        {
            InitializeComponent();
            FavoritesService.Instance.FavoritesChanged += OnFavoritesChanged;
            UpdateFavoriteIcons();
        }

        private void OnFavoritesChanged(object sender, EventArgs e)
        {
            UpdateFavoriteIcons();
        }

        private void UpdateFavoriteIcons()
        {
            var favorites = FavoritesService.Instance.GetFavorites().Select(f => f.Naziv).ToList();
            var recipeItems = new List<StackLayout>
            {
                BurekItem,
                CevapiItem,
                BaklavaItem,
                HurmasiceItem,
                BegovaCorbaItem,
                GrahItem
            };

            foreach (var item in recipeItems)
            {
                var label = item.Children.OfType<Label>().FirstOrDefault();
                if (label != null)
                {
                    var imageButton = item.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<ImageButton>().FirstOrDefault();
                    if (imageButton != null)
                    {
                        imageButton.Source = favorites.Contains(label.Text) ? favoriteIconSelectedPath : favoriteIconPath;
                    }
                }
            }
        }

        private async void HandleBurekTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BurekReceptPage());
        }

        private async void HandleCevapiTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CevapiReceptPage());
        }

        private async void HandleBaklavaTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BaklavaReceptPage());
        }

        private async void HandleHurmasiceTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HurmasiceReceptPage());
        }

        private async void HandleBegovaCorbaTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BegovaCorbaReceptPage());
        }

        private async void HandleGrahTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GrahReceptPage());
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();

            var recipeItems = new List<StackLayout>
            {
                BurekItem,
                CevapiItem,
                BaklavaItem,
                HurmasiceItem,
                BegovaCorbaItem,
                GrahItem
            };

            foreach (var item in recipeItems)
            {
                var label = item.Children.OfType<Label>().FirstOrDefault();
                if (label != null)
                {
                    bool isVisible = label.Text.ToLower().Contains(searchText);
                    item.IsVisible = isVisible;
                }
            }
        }

        private void OnFavoriteIconClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton)
            {
                var parentStackLayout = (StackLayout)imageButton.Parent.Parent;
                var label = parentStackLayout.Children.OfType<Label>().FirstOrDefault();
                var naziv = label?.Text;
                var slika = ((Image)parentStackLayout.Children[0]).Source.ToString().Replace("File: ", "");

                var recept = new Recept
                {
                    Naziv = naziv,
                    Slika = slika,
                    Opis = "Opis recepta",
                    ReceptPageType = GetReceptPageType(naziv)
                };

                if (imageButton.Source.ToString().Contains(favoriteIconPath))
                {
                    imageButton.Source = favoriteIconSelectedPath;
                    FavoritesService.Instance.AddToFavorites(recept);
                }
                else
                {
                    imageButton.Source = favoriteIconPath;
                    FavoritesService.Instance.RemoveFromFavorites(recept);
                }
            }
        }

        private Type GetReceptPageType(string naziv)
        {
            return naziv switch
            {
                "Burek" => typeof(BurekReceptPage),
                "Ćevapi" => typeof(CevapiReceptPage),
                "Baklava" => typeof(BaklavaReceptPage),
                "Hurmašice" => typeof(HurmasiceReceptPage),
                "Begova Čorba" => typeof(BegovaCorbaReceptPage),
                "Grah" => typeof(GrahReceptPage),
                _ => typeof(MainPage),
            };
        }
    }
}

