namespace Farm_App.Pages;

public partial class InsertAnimal : ContentPage
{
 public MainViewModel vm;
    private Animal animal; 
    public InsertAnimal(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedType = Typ_Pkr.SelectedItem.ToString();
        Clr_Pkr.SelectedIndex = 0;

        if (selectedType == "Cow")
        {
            Typ_Pkr.SelectedIndex = 0;
            Clr_Pkr.IsEnabled = true;
            Mlk_Entry.IsVisible = true;

            Wool_Entry.IsVisible = false;

            animal = new Cow(); 
        }
        else if (selectedType == "Sheep")
        {
            Typ_Pkr.SelectedIndex = 1;
            Clr_Pkr.IsEnabled = true;
            Clr_Pkr.SelectedIndex = 0;
            Wool_Entry.IsVisible = true;

            Mlk_Entry.IsVisible = false;

            animal = new Sheep(); 
        }
    }

    private void Insert_Btn(object sender, EventArgs e)
    {
        double milkAmount = 0;
        double woolAmount = 0;

        if (!double.TryParse(Cst_Entry.Text, out double cost))
        {
            DisplayAlert("Error", "Cost must be a valid number", "OK");
            return;
        }

        if (!double.TryParse(Wgt_Entry.Text, out double weight))
        {
            DisplayAlert("Error", "Weight must be a valid number", "OK");
            return;
        }

        if (Mlk_Entry.IsVisible && !double.TryParse(Mlk_Entry.Text, out milkAmount))
        {
            DisplayAlert("Error", "Milk amount must be a valid number", "OK");
            return;
        }

        if (Wool_Entry.IsVisible && !double.TryParse(Wool_Entry.Text, out woolAmount))
        {
            DisplayAlert("Error", "Wool amount must be a valid number", "OK");
            return;
        }

        // Set common properties
        animal.Colour = Clr_Pkr.SelectedItem.ToString();
        animal.Cost = cost;
        animal.Weight = weight;

        // Set specific properties based on type
        if (animal is Cow)
        {
            ((Cow)animal).Milk = milkAmount;
        }
        else if (animal is Sheep)
        {
            ((Sheep)animal).Wool = woolAmount;
        }

        // Insert the animal into the list and database
        vm.AddItem(animal);
        vm.database.InsertItem(animal);
        DisplayAlert("Success", "Animal Inserted", "Continue");

    }

    private void Reset_Btn(object sender, EventArgs e)
    {
        // Clear all entries
        Wgt_Entry.Text = string.Empty;
        Cst_Entry.Text = string.Empty;
        Mlk_Entry.Text = string.Empty;
        Wool_Entry.Text = string.Empty;

        // Reset pickers
        Typ_Pkr.SelectedIndex = -1;
        Clr_Pkr.SelectedIndex = -1;

    }
}