using Microsoft.Maui.Controls;
using System;

namespace BosanskaKuhinjaNaDlanu
{
    public partial class MyRecipesPage : ContentPage
    {
        public MyRecipesPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            // Provjera korisničkih podataka
            if (email == "ramu@gmail.com" && password == "projekat")
            {
                loginResultLabel.Text = "Uneseni podaci su tačni. Uspješno ste se prijavili!";
                await Navigation.PushAsync(new RecipePage());
            }
            else
            {
                loginResultLabel.Text = "Uneseni podaci su netačni. Pokušajte se ponovo prijaviti!";
            }
        }
    }
}