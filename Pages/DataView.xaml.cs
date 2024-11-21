namespace Farm_App.Pages;

public partial class DataView : ContentPage
{
	private MainViewModel vm;
	
	public DataView(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		DataListingView.ItemsSource = vm.Animals;
		this.vm = vm;
	}

}