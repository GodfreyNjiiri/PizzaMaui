namespace PizzaMaui.Pages;

public partial class AllPizzasPage : ContentPage
{
	private readonly AllPizzasViewModel _allPizzaViewModel;
	public AllPizzasPage(AllPizzasViewModel allPizzaViewModel)
	{

		InitializeComponent();
        _allPizzaViewModel = allPizzaViewModel;
		BindingContext = _allPizzaViewModel;
    }
}