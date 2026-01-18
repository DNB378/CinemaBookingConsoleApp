using System.Text.Json;

namespace CinemaBookingConsoleApp
{
    class JsonStorage
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options;

        public JsonStorage(string path)
        {
            _path = path;
            _options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        public bool TryLoad(out InMemoryDb db)
        {
            db = new InMemoryDb();

            if (!File.Exists(_path))
                return false;

            try
            {
                var json = File.ReadAllText(_path);
                var loaded = JsonSerializer.Deserialize<InMemoryDb>(json, _options);
                if (loaded == null)
                    return false;

                loaded.RecalculateIds();
                db = loaded;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save(InMemoryDb db)
        {
            var json = JsonSerializer.Serialize(db, _options);
            File.WriteAllText(_path, json);
        }
    }
}
