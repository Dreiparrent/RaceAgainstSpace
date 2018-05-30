using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using RaceAgainstSpace.Models;
using RaceAgainstSpace.Views;

namespace RaceAgainstSpace.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        // public ObservableCollection<Item> Items { get; set; }
        // public Command LoadItemsCommand { get; set; }
        public ObservableCollection<Card> Cards { get; set; }
        public Command LoadCardsCommand { get; set; }

        private List<Card> _planets;
        private List<Card> _stars;
        private List<Card> _anomalies;

        public ItemsViewModel()
        {
            Title = "Decks";
            IsLarge = (App.ScreenHeight > 1000);
            Width = App.ScreenWidth;

            _planets = new List<Card>();
            _anomalies = new List<Card>();
            _stars = new List<Card>();

            Cards = new ObservableCollection<Card>();
            LoadCardsCommand = new Command(async () => await ExecuteLoadCardsCommand());

            MessagingCenter.Subscribe<NewItemPage, Card>(this, "AddCard", async (obj, card) =>
            {
                var _card = card as Card;
                Cards.Add(_card);
                await DataStore.AddCardAsync(_card);
            });
        }

        private double WidthSet {
            get { return IsLarge ? Width / 2 : Width; }
        }


        async Task ExecuteLoadCardsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Cards.Clear();
                var cards = await DataStore.GetCardsAsync(true);
                foreach (var card in cards)
                {
                    switch(card.Type)
                    {
                        case "star":
                            _stars.Add(card);
                            break;
                        case "anomaly":
                            _anomalies.Add(card);
                            break;
                        default:
                            _planets.Add(card);
                            break;
                    }
                    Cards.Add(card);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<Card> GetCard(string type)
        {
            if (_planets.Count == 0 || _stars.Count == 0 || _anomalies.Count == 0)
            {
                await ExecuteLoadCardsCommand();
            }
            return DrawCard(type);
        }
       
        private Card DrawCard(string type)
        {
            Card drawCard;
            switch (type)
            {
                case "star":
                    drawCard = _stars.PopAt(0);
                    break;
                case "anomaly":
                    drawCard = _anomalies.PopAt(0);
                    break;
                default:
                    drawCard = _planets.PopAt(0);
                    break;
            }
            return drawCard;
        }

        /*
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        */
    }
    static class ListExtension
    {
        public static T PopAt<T>(this List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }
    }
}