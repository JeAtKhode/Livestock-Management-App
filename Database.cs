namespace Farm_App;

public class Database
{
    private readonly SQLiteConnection _connection;

    public Database()
{
    var dbName = "FarmDataOriginal.db";
    var dbPath = Path.Combine(Current.AppDataDirectory, dbName);

    if (File.Exists(dbPath))
    {
        File.Delete(dbPath);
    }

    using Stream stream = Current.OpenAppPackageFileAsync(dbName).Result;
    using MemoryStream memoryStream = new();
    stream.CopyTo(memoryStream);
    File.WriteAllBytes(dbPath, memoryStream.ToArray());

    _connection = new SQLiteConnection(dbPath);
    _connection.CreateTables<Cow, Sheep>();
}

    public List<Animal> ReadItems()
    {
        var Animals = new List<Animal>();
        var lst1 = _connection.Table<Cow>().ToList();
        Animals.AddRange(lst1);
        var lst2 = _connection.Table<Sheep>().ToList();
        Animals.AddRange(lst2);
        return Animals;
    }

    public int InsertItem(Animal item)
    {
        return _connection.Insert(item);
    }

    public int DeleteItem(Animal item)
    {
        return _connection.Delete(item);
    }

    public int UpdateItem(Animal item)
    {
        return _connection.Update(item);
    }
}
