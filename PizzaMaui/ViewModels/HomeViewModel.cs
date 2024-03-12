﻿namespace PizzaMaui.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly PizzaService _pizzaservice;

        public HomeViewModel(PizzaService pizzaservice) 
        {
            _pizzaservice = pizzaservice;
            Pizzas = new(_pizzaservice.GetPopularPizzas());
        }   

        public ObservableCollection<Pizza> Pizzas { get; set; }

        [RelayCommand]
        private async Task GoToAllPizzasPage(bool fromSearch = false)
        {
            var parameters = new Dictionary<string, object> {
                [nameof(AllPizzasViewModel.FromSearch)] = fromSearch
            };
            await Shell.Current.GoToAsync(nameof(AllPizzasPage), animate: true, parameters);
        }

    }
}
