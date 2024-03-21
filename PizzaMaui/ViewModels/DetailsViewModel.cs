using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toast = CommunityToolkit.Maui.Alerts.Toast;

namespace PizzaMaui.ViewModels
{
    [QueryProperty(nameof(Pizza), nameof(Pizza))]
    public partial class DetailsViewModel : ObservableObject, IDisposable
    {
        private readonly CartViewModel _cartViewModel;
        public DetailsViewModel(CartViewModel cartViewModel) 
        {
            _cartViewModel = cartViewModel;

            cartViewModel.CartCleared += onCartCleared;
            _cartViewModel.CartItemUpdated += onCartItemUpdated;
            _cartViewModel.CartItemRemoved += onCartItemRemoved;

        }


        private void onCartCleared(object? _, EventArgs e)
        {
            Pizza.CartQuantity = 0;
        }

        private void onCartItemRemoved(object? _, Pizza p) => onCartItemChanged(p, 0);

        private void onCartItemUpdated(object? _, Pizza p) => onCartItemChanged(p, p.CartQuantity);
        private void onCartItemChanged(Pizza p, int quantity)
        {
            if(p.Name == Pizza.Name)
            {
                Pizza.CartQuantity = quantity;
            }
        }

        [ObservableProperty]
        private Pizza _pizza;

        [RelayCommand]
        private void AddToCart()
        {
            Pizza.CartQuantity++;
            _cartViewModel.UpdateCartItemCommand.Execute(Pizza);
        }

        [RelayCommand]
        private void RemoveFromCart()
        {
            if(Pizza.CartQuantity > 0)
            {
                Pizza.CartQuantity--;
                _cartViewModel.RemoveCartItemCommand.Execute(Pizza);
            }
            
        }

        // Go to Cart Page
        [RelayCommand]
        private async Task ViewCart()
        {
            if (Pizza.CartQuantity > 0)
            {
                // go to cart page
                await Shell.Current.GoToAsync(nameof(CartPage), animate: true);
            }
            else
            {
                await Toast.Make("Please select the Quantity", ToastDuration.Short).Show();
            }
        }

        public void Dispose()
        {
            _cartViewModel.CartCleared -= onCartCleared;
            _cartViewModel.CartItemUpdated -= onCartItemUpdated;
            _cartViewModel.CartItemRemoved -= onCartItemRemoved;

        }
    }  
}
