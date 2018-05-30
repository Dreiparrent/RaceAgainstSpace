using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RaceAgainstSpace.ViewModels;
using RaceAgainstSpace.Models;

namespace RaceAgainstSpace.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DecksPage : ContentPage
	{
        ItemsViewModel viewModel;

        // float flexBasis = 100;

        public DecksPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ItemsViewModel();

            if (viewModel.IsLarge)
                Resources["flexStyle"] = Resources["wrapStyle"];
            else
                Resources["flexStyle"] = Resources["noWrapStyle"];

            viewModel.TapCommand = new Command(OnTapped);
            Console.WriteLine($"large: {viewModel.IsLarge}, width:{viewModel.FlexWidth}");
        }
        
        // async?
        public async void OnTapped(object p)
        {
            var card = await viewModel.GetCard((string)p);
            // Navigation.PushAsync
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(card)));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            Resources["stackStyle"] = Resources["widthStyle"];
            viewModel.Width = width;
            Console.WriteLine($"{width}, {viewModel.FlexWidth}");
            base.OnSizeAllocated(width, height);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Cards.Count == 0)
                viewModel.LoadCardsCommand.Execute(null);
        }
    }
}
    