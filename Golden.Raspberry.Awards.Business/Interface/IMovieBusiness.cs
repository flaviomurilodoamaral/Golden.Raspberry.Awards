using Golden.Raspberry.Awards.Entity;

namespace Golden.Raspberry.Awards.Business.Interface
{
    public interface IMovieBusiness
    {
        void ProcessCsv(Stream stream);

        (List<AwardInterval> min, List<AwardInterval> max) GetAwardIntervals();
    }
}
