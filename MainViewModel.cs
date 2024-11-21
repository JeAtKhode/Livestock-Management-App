namespace Farm_App;

public class MainViewModel
{
    public ObservableCollection<Animal> Animals { get; set; }
    public readonly Database database;
    public double Milk_Prc = 9.4; // $ per kg
    public double Wool_Prc = 6.2; // $ per kg
    public double Tax_Rate = 0.02; // Government tax rate per kg per day

    public MainViewModel()
    {
        database = new Database(); 
        Animals = new ObservableCollection<Animal>();
        database.ReadItems().ForEach(x => Animals.Add(x));
    }

    //universally available
    public double Calc_Cow_Pft(Cow cow)
    {
        double income = cow.Milk * Milk_Prc;
        double tax = cow.Milk * Tax_Rate;
        return income - cow.Cost - tax;
    }

    public double Calc_Sheep_Pft(Sheep sheep)
    {
        double income = sheep.Wool * Wool_Prc;
        double tax = sheep.Wool * Tax_Rate;
        return income - sheep.Cost - tax;
    }

    public double Avg_Cow_Dly_Pft()
    {
        var cows = Animals.OfType<Cow>().ToList();
        if (!cows.Any()) return 0;

        double totalCowProfit = cows.Sum(cow => Calc_Cow_Pft(cow));
        return totalCowProfit / cows.Count;
    }

    public double Avg_Sheep_Dly_Pft()
    {
        var sheep = Animals.OfType<Sheep>().ToList();
        if (!sheep.Any()) return 0;

        double totalSheepProfit = sheep.Sum(sheep => Calc_Sheep_Pft(sheep));
        return totalSheepProfit / sheep.Count;
    }

    public double Calc_Gov_Tax()
    {
        double totalWeight = Animals.Sum(animal => animal.Weight);
        return Tax_Rate * totalWeight;
    }

    public double Ttl_Cow_Pft()
    {
        var cows = Animals.OfType<Cow>().ToList();
        return cows.Sum(cow => Calc_Cow_Pft(cow));
    }

    public double Ttl_Sheep_Pft()
    {
        var sheep = Animals.OfType<Sheep>().ToList();
        return sheep.Sum(sheep => Calc_Sheep_Pft(sheep));
    }

    public double Ttl_Farm_Pft()
    {
        return Ttl_Cow_Pft() + Ttl_Sheep_Pft();
    }

    public double Avg_Wght()
    {
        if (!Animals.Any()) return 0;
        return Animals.Average(animal => animal.Weight);
    }

    public void AddItem(Animal animal)
    {
        int r = database.InsertItem(animal);
        if (r > 0)
        {
            Animals.Add(animal);
        }
    }
}

