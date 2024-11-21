namespace Farm_App.Pages;

public partial class Predictions : ContentPage
{
    MainViewModel vm;

    public Predictions(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }
    private void Stats_Btn(object sender, EventArgs e)
    {
        double avgCowProfit = vm.Avg_Cow_Dly_Pft();
        double avgSheepProfit = vm.Avg_Sheep_Dly_Pft();
        double avgTax = vm.Calc_Gov_Tax();

        Avg_Cow_Pft.Text = $"$ {avgCowProfit:F2}";
        Avg_Sheep_Pft.Text = $"$ {avgSheepProfit:F2}";
        Gov_Tax.Text = $"$ {avgTax * 30:F2}";
        Daily_Pft.Text = $"$ {vm.Ttl_Farm_Pft():F2}";
        Avg_Wgt.Text = $"{vm.Avg_Wght():F2} kg";

        if (avgCowProfit > avgSheepProfit)
        {
            Profit_Stat.Text="Cows are more profitable than sheep";
        }
        else if  (avgSheepProfit > avgCowProfit)
        {
            Profit_Stat.Text="Sheep are more profitable than Cows";
        }
    }


}
