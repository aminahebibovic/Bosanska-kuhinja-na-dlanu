using Microsoft.Maui.Controls;

namespace BosanskaKuhinjaNaDlanu
{
    public partial class RecipePage : ContentPage
    {
        public RecipePage()
        {
            InitializeComponent();
        }

        private void OnAddRecipeButtonClicked(object sender, EventArgs e)
        {
            var recept = new Recept
            {
                Naziv = RecipeNameEntry.Text,
                Slika = RecipeImageEntry.Text,
                Opis = RecipeDescriptionEditor.Text
                
            };

            
            DisplayAlert("Recept je dodan.", "Uspješno ste dodali svoj novi recept!", "OK");
        }
    }
}