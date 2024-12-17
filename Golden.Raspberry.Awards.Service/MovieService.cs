using Golden.Raspberry.Awards.Entity;
using Golden.Raspberry.Awards.Repository;
using Golden.Raspberry.Awards.Repository.Interface;
using Golden.Raspberry.Awards.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Golden.Raspberry.Awards.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public void LoadMovies(List<Movie> movies)
        {
            Assert.IsNotNull(movies, "O retorno da lista de filmes não deve ser nulo.");
            Assert.IsTrue(movies.Any(), "A lista de filmes não deve estar vazia.");

            foreach (Movie movie in movies)
            {
                Assert.IsTrue(movie.Year >= DateTime.Now.AddYears(-500).Year && movie.Year <= DateTime.Now.Year, $"O ano {movie.Year} não está no intervalo válido.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(movie.Title), "O título do filme não deve ser vazio ou nulo.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(movie.Studio), "O campo Studio não deve ser vazio ou nulo.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(movie.Producer), "O campo Producer não deve ser vazio ou nulo.");
                Assert.IsTrue(movie.IsWinner == true || movie.IsWinner == false, "O campo Winner deve ser um booleano válido.");

                _repository.Add(movie);
            }
        }

        public List<Movie> GetMovies()
        {
            return _repository.GetAll().ToList();
        }
    }
}
