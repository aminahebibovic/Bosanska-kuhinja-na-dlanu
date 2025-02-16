using System;
using System.Collections.Generic;
using System.Linq;

namespace BosanskaKuhinjaNaDlanu
{
    public class Recept
    {
        public string Naziv { get; set; }
        public string Slika { get; set; }
        public string Opis { get; set; }
        public Type ReceptPageType { get; set; }
    }

    public class FavoritesService
    {
        private static FavoritesService _instance;
        private List<Recept> _favorites;

        public event EventHandler FavoritesChanged;

        private FavoritesService()
        {
            _favorites = new List<Recept>();
        }

        public static FavoritesService Instance => _instance ??= new FavoritesService();

        public void AddToFavorites(Recept recept)
        {
            if (!_favorites.Any(r => r.Naziv == recept.Naziv))
            {
                _favorites.Add(recept);
                OnFavoritesChanged();
            }
        }

        public void RemoveFromFavorites(Recept recept)
        {
            var item = _favorites.FirstOrDefault(r => r.Naziv == recept.Naziv);
            if (item != null)
            {
                _favorites.Remove(item);
                OnFavoritesChanged();
            }
        }

        public List<Recept> GetFavorites()
        {
            return new List<Recept>(_favorites);
        }

        protected virtual void OnFavoritesChanged()
        {
            FavoritesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}


