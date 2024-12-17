using Golden.Raspberry.Awards.Entity;
using Golden.Raspberry.Awards.Repository;
using Golden.Raspberry.Awards.Service;
using System.Net.Security;

namespace Golden.Raspberry.Awards.IntegrationTest
{
    [TestClass]
    public sealed class DataValidation
    {
        private MovieService _service;

        [TestInitialize]
        public void Setup()
        {
            var movieRepository = new MovieRepository();
            _service = new MovieService(movieRepository);
        }

        [TestMethod]
        public void TestMovies()
        {
            SetMovies();
        }

        public void SetMovies()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0", ""), "DataSource/movies.csv");
            Assert.IsTrue(File.Exists(file), "Arquivo CSV não encontrado.");

            List<Movie> movies = new();
            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);

            if (stream == null || stream.Length == 0)
                throw new ArgumentException("CSV file is invalid.");

            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');

                int.TryParse(parts[0], out int year);

                var isWinner = false;

                if (parts[4].ToUpper().Equals("YES"))
                {
                    isWinner = true;
                }

                if (year > 0)
                {
                    var movie = new Movie
                    {
                        Year = year,
                        Title = parts[1],
                        Studio = parts[2],
                        Producer = parts[3],
                        IsWinner = isWinner
                    };

                    movies.Add(movie);
                }
            }

            _service.LoadMovies(movies);
        }
    }
}
