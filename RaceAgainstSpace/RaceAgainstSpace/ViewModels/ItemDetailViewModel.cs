using System;

using RaceAgainstSpace.Models;

namespace RaceAgainstSpace.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Card Card { get; set; }
        public ItemDetailViewModel(Card card = null)
        {
            Title = Capitolize(card?.Type);
            Card = card;
        }

        private string Capitolize(string type)
        {
            if (type == null)
                return null;
            if (type.Length > 1)
                return char.ToUpper(type[0]) + type.Substring(1);
            return type.ToUpper();
        }
    }
}
