namespace Farm_App.Pages;

public partial class Query : ContentPage
{
	MainViewModel vm;
	public Query(MainViewModel vm)
	{
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
        

    }

    private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedType = Typ_Pkr.SelectedItem.ToString();
        Clr_Pkr.SelectedIndex = 0;

        if (selectedType == "Cow" || selectedType == "Sheep")
        {
            Clr_Pkr.IsEnabled = true;
        }
    }

    private void Search_Btn(object sender, EventArgs e)
    {
        if (Typ_Pkr.SelectedItem is null)
        {
            DisplayAlert("Error", "Select Animal", "OK");
            return;
        }
        if (Clr_Pkr.SelectedItem is null)
        {
            DisplayAlert("Error", "Select Color", "OK");
            return;
        }

        string selectedType = Typ_Pkr.SelectedItem.ToString();
        string Selected_Clr = Clr_Pkr.SelectedItem.ToString();

       
        List<Animal> Match_Animal = vm.Animals.Where(animal =>
        {
            bool isMatch = false;
            if (selectedType == "Cow" && animal is Cow cow)
            {
                isMatch = Selected_Clr == "All" || cow.Colour.ToLower() == Selected_Clr.ToLower();
            }
            else if (selectedType == "Sheep" && animal is Sheep sheep)
            {
                isMatch = Selected_Clr == "All" || sheep.Colour.ToLower() == Selected_Clr.ToLower();
            }
            return isMatch;
        }).ToList();

        int totalLivestockCount = Match_Animal.Count;
        double percentage = (double)totalLivestockCount / vm.Animals.Count * 100;
       

        double totalIncome = 0;
        double totalCost = 0;
        double totalTax = 0;
        double totalWeight = 0;
        double totalProduceAmount = 0;

        foreach (var animal in Match_Animal)
        {
            double weight = animal.Weight;
            double produceAmountPerDay = CalculateProduceAmount(animal);

            double income = 0;
            if (animal is Cow cow)
            {
                income = produceAmountPerDay * 9.4;
            }
            else if (animal is Sheep sheep)
            {
                income = produceAmountPerDay * 6.2;
            }

            double cost = animal.Cost; // Cost per day
            double tax = produceAmountPerDay * 0.02; // Tax per day on produce amount

            totalIncome += income;
            totalCost += cost;
            totalTax += tax;
            totalWeight += weight;
            totalProduceAmount += produceAmountPerDay;
        }

        double averageWeight = totalWeight / totalLivestockCount;
        double profit = totalIncome - totalCost - totalTax;
        
        Ttl_Count_Lbl.Text = $"{totalLivestockCount}";
        Per_Lbl.Text = $"{percentage:F0}%";
        Ttl_Tax_Lbl.Text = $"${totalTax:F1}";
        Pft_Lbl.Text = $"${profit:F1}";
        Avrg_Wgh_lbl.Text = $"{averageWeight:F1} kg";
        Ttl_Prdc_Lbl.Text = $"{totalProduceAmount:F1} kg";
        label1.Text = $"Number of livestock ({selectedType} in {Selected_Clr} colour):";
    }

    public static double CalculateProduceAmount(Animal animal)
    {
        if (animal is Cow cow)
        {
            return cow.Milk;
        }
        else if (animal is Sheep sheep)
        {
            return sheep.Wool;
        }
        return 0;
    }

    private void Reset_Btn(object sender, EventArgs e)
    {
        Ttl_Count_Lbl.Text = string.Empty;
        Per_Lbl.Text = string.Empty;
        Ttl_Tax_Lbl.Text = string.Empty;
        Pft_Lbl.Text = string.Empty;
        Avrg_Wgh_lbl.Text = string.Empty;
        Ttl_Prdc_Lbl.Text = string.Empty;
        label1.Text = string.Empty;
    }
}