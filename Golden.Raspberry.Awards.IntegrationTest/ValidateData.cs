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
            GetMovies();
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

            Assert.IsNotNull(movies, "A lista de filmes carregada do CSV não deve ser nula.");
            Assert.IsTrue(movies.Count > 0, "A lista de filmes carregada do CSV deve conter elementos.");

            _service.LoadMovies(movies);
        }

        public void GetMovies()
        {
            var movies = _service.GetMovies();

            Assert.IsNotNull(movies, "O retorno da lista de filmes não deve ser nulo.");
            Assert.IsTrue(movies.Any(), "A lista de filmes não deve estar vazia.");

            foreach (var movie in movies)
            {
                Assert.IsTrue(movie.Year >= DateTime.Now.AddYears(-500).Year && movie.Year <= DateTime.Now.Year, $"O ano {movie.Year} não está no intervalo válido.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(movie.Title), "O título do filme não deve ser vazio ou nulo.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(movie.Studio), "O campo Studio não deve ser vazio ou nulo.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(movie.Producer), "O campo Producer não deve ser vazio ou nulo.");
                Assert.IsTrue(movie.IsWinner == true || movie.IsWinner == false, "O campo Winner deve ser um booleano válido.");
            }
        }
    }
}
