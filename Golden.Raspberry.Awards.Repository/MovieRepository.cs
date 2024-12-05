using Golden.Raspberry.Awards.Entity;
using Golden.Raspberry.Awards.Repository.Interface;

namespace Golden.Raspberry.Awards.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies = new();

        public void Add(Movie movie) => _movies.Add(movie);

        public IEnumerable<Movie> GetAll() => _movies;
    }
}
