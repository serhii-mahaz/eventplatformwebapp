namespace MSA.EventPlatform.DataSeeder
{
    public class DataSeederSettings
    {
        public DatabaseOptions DatabaseOptions { get; set; } = new DatabaseOptions();
        public GeneratorsOptions GeneratorsOptions { get; set; } = new GeneratorsOptions();
    }

    public class DatabaseOptions
    {
        public bool RecreateDB { get; set; }
        public string SeedData { get; set; } = string.Empty;
    }

    public class GeneratorsOptions
    {
        public int Users { get; set; }
    }
}
