using Microsoft.Maui.Controls;
using System.Linq;

namespace BosanskaKuhinjaNaDlanu
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();
            LoadFavorites();
            FavoritesService.Instance.FavoritesChanged += OnFavoritesChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadFavorites();
        }

        private void OnFavoritesChanged(object sender, EventArgs e)
        {
            LoadFavorites();
        }

        private void LoadFavorites()
        {
            var favorites = FavoritesService.Instance.GetFavorites();
            FavoritesStackLayout.Children.Clear();

            foreach (var recept in favorites)
            {
                var recipeLayout = new StackLayout { Padding = new Thickness(0, 10) };

                // Provera putanje slike
                var imageSource = ImageSource.FromFile($"Resources/Images/{recept.Slika}");
                if (imageSource == null)
                {
                    // Ako slika nije pronađena, postavi default sliku
                    imageSource = ImageSource.FromFile("Resources/Images/noimg.png"); // Ova slika mora biti u projektu
                }

                var image = new Image { Source = imageSource, HeightRequest = 200, Aspect = Aspect.AspectFill }; // Povećana visina slike

                var nameLabel = new Label
                {
                    Text = recept.Naziv,
                    FontSize = 16,
                    TextColor = Color.FromHex("#333"),
                    HorizontalOptions = LayoutOptions.Center, // Postavlja tekst u centar
                    Margin = new Thickness(0, 10, 0, 0) // Postavlja razmak između slike i naslova
                };

                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                var favoriteButton = new ImageButton
                {
                    Source = "favorite_icon_selected.png",
                    HeightRequest = 20,
                    WidthRequest = 20,
                    Margin = new Thickness(5, 0, 0, 5)
                };

                favoriteButton.Clicked += (s, e) =>
                {
                    FavoritesService.Instance.RemoveFromFavorites(recept);
                };

                var viewRecipeLabel = new Label
                {
                    Text = "Vidi recept",
                    FontSize = 12,
                    TextColor = Color.FromHex("#EFAB3D"),
                    TextDecorations = TextDecorations.Underline,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) => {
                    var page = (Page)Activator.CreateInstance(recept.ReceptPageType);
                    await Navigation.PushAsync(page);
                };

                viewRecipeLabel.GestureRecognizers.Add(tapGestureRecognizer);

                grid.Children.Add(favoriteButton);
                Grid.SetColumn(viewRecipeLabel, 1);
                Grid.SetRow(viewRecipeLabel, 0);
                grid.Children.Add(viewRecipeLabel);

                recipeLayout.Children.Add(image);
                recipeLayout.Children.Add(nameLabel);
                recipeLayout.Children.Add(grid);

                FavoritesStackLayout.Children.Add(recipeLayout);
            }
        }
    }
}








