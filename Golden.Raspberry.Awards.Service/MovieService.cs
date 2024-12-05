using Golden.Raspberry.Awards.Entity;
using Golden.Raspberry.Awards.Repository;
using Golden.Raspberry.Awards.Repository.Interface;
using Golden.Raspberry.Awards.Service.Interface;

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
            foreach (Movie movie in movies)
            {
                _repository.Add(movie);
            }
        }

        public List<Movie> GetMovies()
        {
            return _repository.GetAll().ToList();
        }
    }
}
