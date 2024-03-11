namespace PizzaMaui.ViewModels
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

    }
}
