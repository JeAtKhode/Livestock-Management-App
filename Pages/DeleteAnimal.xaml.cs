namespace Farm_App.Pages;

public partial class DeleteAnimal : ContentPage
{
    private readonly MainViewModel vm;

    public DeleteAnimal(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private void Reset_Btn(object sender, EventArgs e)
    {
        Id_Entry.Text = string.Empty;

    }

    private void Delete_Btn(object sender, EventArgs e)
    {
        if (!TryGetAnimalById(out Animal animal))
        {
            DisplayAlert("Error", "No Animal Found", "OK");
            return;
        }

        DisplayAlert("Success", $"One {animal.GetType().Name} Deleted", "OK");
        vm.database.DeleteItem(animal);
        vm.Animals.Remove(animal);
        
    }

    private bool TryGetAnimalById(out Animal animal)
    {
        animal = null;
        if (!int.TryParse(Id_Entry.Text, out int animalId))
        {
            DisplayAlert("Error", "Invalid input", "OK");
            return false;
        }

        animal = vm.Animals.FirstOrDefault(a => a.Id == animalId);
        return animal != null;
    }
}
