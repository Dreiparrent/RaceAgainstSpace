using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RaceAgainstSpace.Models;

namespace RaceAgainstSpace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Card Card { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Card = new Card
            {
                Text = "Item name",
                Subtext = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Card);
            await Navigation.PopModalAsync();
        }
    }
}