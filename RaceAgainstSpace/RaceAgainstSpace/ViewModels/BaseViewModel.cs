using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using RaceAgainstSpace.Models;
using RaceAgainstSpace.Services;

#if __IOS__
using RaceAgainstSpace.iOS.Services;
#endif

namespace RaceAgainstSpace.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public IFirestore<Card> DataStore => DependencyService.Get<FirestoreService>() ?? new FirestoreService();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        bool isLarge;
        public bool IsLarge {
            get { return isLarge; }
            set { SetProperty(ref isLarge, value); }
        }
        double width;
        public double Width
        {
            get { return width; }
            set {
                SetProperty(ref width, value);
                if (IsLarge)
                    SetProperty(ref flexWidth, value / 2);
                else
                    SetProperty(ref flexWidth, value);
            }
        }
        double flexWidth;
        public double FlexWidth
        {
            get { return flexWidth; }
        }
        Command tapCommand;
        public Command TapCommand
        {
            get { return tapCommand; }
            set { SetProperty(ref tapCommand, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
