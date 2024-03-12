
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaui.ViewModels
{
    [QueryProperty(nameof(FromSearch), nameof(FromSearch))]
    public partial class AllPizzasViewModel : ObservableObject
    {
        private readonly PizzaService _pizzaService;
        public AllPizzasViewModel(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
            Pizzas = new(_pizzaService.GetAllPizzas());
        }
        public ObservableCollection<Pizza> Pizzas { get; set; }

        [ObservableProperty]
        private bool _fromSearch;

        [ObservableProperty]
        private bool _searching;

        [RelayCommand]
        private async Task SearchPizzas(string searchTerm)
        {
            Pizzas.Clear();
            Searching = true;
            foreach (var pizza in _pizzaService.GetPizzas(searchTerm))
            {
                Pizzas.Add(pizza);
            }
            Searching = false;
        }
    }
}
