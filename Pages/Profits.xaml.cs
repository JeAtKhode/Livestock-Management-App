namespace Farm_App.Pages;

public partial class Profits : ContentPage
{
	MainViewModel vm;
	public Profits(MainViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
        BindingContext = vm;
	}
	private void Calc_Btn(object sender, EventArgs e)
    {
        string selectedType = Typ_Pkr.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedType))
        {
            DisplayAlert("Error", "Please select an animal type.", "OK");
            return;
        }

        if (!int.TryParse(Animal_Amt.Text, out int animalAmount) || animalAmount <= 0)
        {
            DisplayAlert("Error", "Invalid input. Please enter a valid integer greater than 0.", "OK");
            return;
        }

        double avgProfit;
        
        if (selectedType.Equals("Cow", StringComparison.OrdinalIgnoreCase))
        {
            avgProfit = vm.Avg_Cow_Dly_Pft();
            double totalProfit = avgProfit * animalAmount;
            Result_Lbl.Text = $"Average Profit per Cow: ${avgProfit:F2}\n"
               + $"Total daily profit for {animalAmount} Cows: ${totalProfit:F2}";
        }
        else if (selectedType.Equals("Sheep", StringComparison.OrdinalIgnoreCase))
        {
            avgProfit = vm.Avg_Sheep_Dly_Pft();
            double totalProfit = avgProfit * animalAmount;
            Result_Lbl.Text = $"Average Profit per Sheep: ${avgProfit:F2}\n" +
                $"Total daily profit for {animalAmount} Sheep: ${totalProfit:F2}";
        }
        else
        {
            Result_Lbl.Text = "Animal type not found";
        }

    }

    private void Reset_Btn(object sender, EventArgs e)
    {
        Animal_Amt.Text = string.Empty;
        Result_Lbl.Text = string.Empty;
       
    }
    

}