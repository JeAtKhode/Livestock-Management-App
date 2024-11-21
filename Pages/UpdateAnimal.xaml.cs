namespace Farm_App.Pages;

public partial class UpdateAnimal : ContentPage
{
    MainViewModel vm;
    private Animal animal; // Declare the animal variable at the class level

    public UpdateAnimal(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private void Reset_Btn(object sender, EventArgs e)
    {
        Id_Entry.Text = string.Empty;
        Result_Lbl.Text = string.Empty;
        Wgt_Entry.Text = string.Empty;
        Cst_Entry.Text = string.Empty;
        Mlk_Entry.Text = string.Empty;
        Wool_Entry.Text = string.Empty;
        Clr_Pkr.SelectedIndex = -1;
        Wgt_Entry.IsVisible = false;
        Cst_Entry.IsVisible = false;
        Mlk_Entry.IsVisible = false;
        Wool_Entry.IsVisible = false;
        milkLabel.IsVisible = false;
        woolLabel.IsVisible = false;
        Clr_Pkr.IsVisible = false;
        UpdateBtn.IsVisible = false;
    }

    private void Search_Btn(object sender, EventArgs e)
    {
        if (!int.TryParse(Id_Entry.Text, out int animalId))
        {
            DisplayAlert("Error", "Invalid input", "OK");
            return;
        }

        animal = vm.Animals.FirstOrDefault(a => a.Id == animalId);

        if (animal != null)
        {
            Clr_Pkr.IsVisible = true;
            Wgt_Entry.IsVisible = true;
            Cst_Entry.IsVisible = true;
            Wgt_Entry.Text = animal.Weight.ToString();
            Cst_Entry.Text = animal.Cost.ToString();
            Clr_Pkr.SelectedItem = animal.Colour;

            if (animal is Cow cow)
            {
                milkLabel.IsVisible = true;
                Mlk_Entry.IsVisible = true;
                Mlk_Entry.Text = cow.Milk.ToString();
                woolLabel.IsVisible = false;
                Wool_Entry.IsVisible = false;
            }
            else if (animal is Sheep sheep)
            {
                woolLabel.IsVisible = true;
                Wool_Entry.IsVisible = true;
                Wool_Entry.Text = sheep.Wool.ToString();
                milkLabel.IsVisible = false;
                Mlk_Entry.IsVisible = false;
            }

            Result_Lbl.Text = "Animal found. Edit the details below.";
            UpdateBtn.IsVisible = true;
        }
        else
        {
            Result_Lbl.Text = "Animal not found";
        }
    }

    private void UpdateBtn_Click(object sender, EventArgs e)
    {
        if (animal == null) return;

        if (!double.TryParse(Wgt_Entry.Text, out double weight) ||
            !double.TryParse(Cst_Entry.Text, out double cost))
        {
            DisplayAlert("Error", "Weight and cost must be valid numbers", "OK");
            return;
        }

        animal.Weight = weight;
        animal.Cost = cost;
        animal.Colour = Clr_Pkr.SelectedItem.ToString();

        if (animal is Cow cow)
        {
            if (!double.TryParse(Mlk_Entry.Text, out double milkAmount))
            {
                DisplayAlert("Error", "Milk amount must be a valid number", "OK");
                return;
            }
            cow.Milk = milkAmount;
        }
        else if (animal is Sheep sheep)
        {
            if (!double.TryParse(Wool_Entry.Text, out double woolAmount))
            {
                DisplayAlert("Error", "Wool amount must be a valid number", "OK");
                return;
            }
            sheep.Wool = woolAmount;
        }

        // Update the animal in the database
        vm.database.UpdateItem(animal);

        // Reflect the changes in the UI
        int index = vm.Animals.IndexOf(vm.Animals.First(a => a.Id == animal.Id));
        if (index != -1)
        {
            vm.Animals[index] = animal;
        }

        DisplayAlert("Success", "Animal updated successfully", "OK");
    }
}
