using Golden.Raspberry.Awards.Entity;

namespace Golden.Raspberry.Awards.Repository.Interface
{
    public interface IMovieRepository
    {
        void Add(Movie movie);

        IEnumerable<Movie> GetAll();
    }
}
