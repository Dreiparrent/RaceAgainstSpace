using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RaceAgainstSpace.Models;
using RaceAgainstSpace.Views;
using RaceAgainstSpace.ViewModels;

namespace RaceAgainstSpace.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var card = args.SelectedItem as Card;
            if (card == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(card)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Cards.Count == 0)
                viewModel.LoadCardsCommand.Execute(null);
        }
    }
}