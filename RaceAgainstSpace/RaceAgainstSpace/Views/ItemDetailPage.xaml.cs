using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RaceAgainstSpace.Models;
using RaceAgainstSpace.ViewModels;

namespace RaceAgainstSpace.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var card = new Card
            {
                Text = "Item 1",
                Subtext = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(card);
            BindingContext = viewModel;
        }
    }
}