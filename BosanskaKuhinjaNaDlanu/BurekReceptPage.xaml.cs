using Microsoft.Maui.Controls;
using System;

namespace BosanskaKuhinjaNaDlanu
{
    public partial class BurekReceptPage : ContentPage
    {
        public BurekReceptPage()
        {
            InitializeComponent();
        }


        private void OnAddCommentButtonClicked(object sender, EventArgs e)
        {
            var commentText = NewCommentEntry.Text;
            if (!string.IsNullOrWhiteSpace(commentText))
            {
                var commentLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 5) };

                var userImage = new Image
                {
                    Source = "zena.png",
                    Style = (Style)Resources["CommentUserImageStyle"]
                };

                var commentTextLayout = new StackLayout { Orientation = StackOrientation.Vertical };

                var userNameLabel = new Label
                {
                    Text = "Korisnik",
                    Style = (Style)Resources["CommentUserNameStyle"]
                };

                var commentLabel = new Label
                {
                    Text = commentText,
                    Style = (Style)Resources["CommentTextStyle"]
                };

                commentTextLayout.Children.Add(userNameLabel);
                commentTextLayout.Children.Add(commentLabel);

                commentLayout.Children.Add(userImage);
                commentLayout.Children.Add(commentTextLayout);

                CommentsStackLayout.Children.Add(commentLayout);
                NewCommentEntry.Text = string.Empty;
            }
        }

        private void OnRateButtonClicked(object sender, EventArgs e)
        {
            string selectedRating = RatingPicker.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedRating))
            {
                DisplayAlert("Hvala na ocjeni", $"Ocijenili ste recept sa {selectedRating} zvjezdica.", "OK");
            }
        }
    }
}

