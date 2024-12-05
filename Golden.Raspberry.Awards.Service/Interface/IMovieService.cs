using Golden.Raspberry.Awards.Entity;

namespace Golden.Raspberry.Awards.Service.Interface
{
    public interface IMovieService
    {
        void LoadMovies(List<Movie> movies);

        List<Movie> GetMovies();
    }
}
