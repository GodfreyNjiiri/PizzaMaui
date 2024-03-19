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

    void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(e.OldTextValue) && string.IsNullOrWhiteSpace(e.NewTextValue)) 
        { 
            _allPizzaViewModel.SearchPizzasCommand.Execute(null); 
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if(_allPizzaViewModel.FromSearch)
        {
            searchBar.Focus();
        }
    }
}