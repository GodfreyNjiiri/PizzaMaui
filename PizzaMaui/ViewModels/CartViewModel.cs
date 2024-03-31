using PizzaMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaui.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public CartViewModel() { }

        public event EventHandler<Pizza> CartItemRemoved;
        public event EventHandler<Pizza> CartItemUpdated;
        public event EventHandler CartCleared;

        public ObservableCollection<Pizza> Items { get; set; } = new();

        [ObservableProperty]
        private double _totalAmount;


        private void RecalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);


        // Add to cart
        [RelayCommand]
        private void UpdateCartItem(Pizza pizza)
        {
            var item = Items.FirstOrDefault(i => i.Name == pizza.Name);
            if (item != null)
            {
                item.CartQuantity = pizza.CartQuantity;
            }
            else
            {
                Items.Add(pizza.Clone());
            }
            RecalculateTotalAmount();
        }

        // Remove Cart Items
        [RelayCommand]
        private async void RemoveCartItem(string name)
        {
            var item = Items.FirstOrDefault(i => i.Name == name);
            if (item != null)
            {
                Items.Remove(item);
                RecalculateTotalAmount();
                CartItemRemoved?.Invoke(this, item);

                var snackbaroptions = new SnackbarOptions
                {
                    CornerRadius = 10,
                    BackgroundColor = Colors.PaleGoldenrod
                };

                var snackbar = Snackbar.Make($"'{item.Name}' Removed from cart", () => 
                { 
                    Items.Add(item);
                    RecalculateTotalAmount();
                    CartItemUpdated?.Invoke(this, item);
                }, "Undo", TimeSpan.FromSeconds(5), snackbaroptions );
                await snackbar.Show();
            }
        }


        // Clear the Cart
        [RelayCommand]
        private async Task ClearCart()
        {
            if (await Shell.Current.DisplayAlert("Confrim Clear Cart?", "Do you really want to clear the cart items?", "Yes", "No")) 
            {
                Items.Clear();
                RecalculateTotalAmount();
                CartCleared?.Invoke(this, EventArgs.Empty);

                await Toast.Make("Cart cleared", ToastDuration.Short).Show();
            }
        }

        // Place the order
        [RelayCommand]
        private async Task PlaceOrder()
        {
            Items.Clear();
            CartCleared?.Invoke(this, EventArgs.Empty);
            RecalculateTotalAmount();

            // go to checkout page
            await Shell.Current.GoToAsync(nameof(CheckoutPage), animate: true);
        }
    }
}
