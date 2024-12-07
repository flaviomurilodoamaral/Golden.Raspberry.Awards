using Golden.Raspberry.Awards.Business.Interface;
using Golden.Raspberry.Awards.Entity;
using Golden.Raspberry.Awards.Service.Interface;
using System.Text.RegularExpressions;

namespace Golden.Raspberry.Awards.Business
{
    public class MovieBusiness : IMovieBusiness
    {
        private readonly IMovieService _service;

        public MovieBusiness(IMovieService service)
        {
            _service = service;
        }

        public void ProcessCsv(Stream csvStream)
        {
            List<Movie> movies = new();

            if (csvStream == null || csvStream.Length == 0)
                throw new ArgumentException("CSV file is invalid.");

            using var reader = new StreamReader(csvStream);
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

        public (List<AwardInterval> min, List<AwardInterval> max) GetAwardIntervals()
        {
            // Obtém todos os prêmios ordenados por produtor e ano
            var awards = _service.GetMovies()
                .Where(m => m.IsWinner)
                .SelectMany(m =>
                    Regex.Split(m.Producer, @"\s*(?:,|and)\s*")
                    .Select(p => new
                    {
                        Producer = p.Trim(),
                        Year = m.Year
                    }))
                .OrderBy(a => a.Producer)
                .ThenBy(a => a.Year)
                .ToList();

            // Dicionário para armazenar os intervalos entre prêmios consecutivos por produtor
            var producerIntervals = new List<AwardInterval>();

            // Agrupa prêmios por produtor
            var groupedByProducer = awards.GroupBy(a => a.Producer);

            foreach (var group in groupedByProducer)
            {
                var producerAwards = group.OrderBy(a => a.Year).ToList();

                for (int i = 1; i < producerAwards.Count; i++)
                {
                    var interval = producerAwards[i].Year - producerAwards[i - 1].Year;

                    if (interval >= 1)
                    {
                        producerIntervals.Add(new AwardInterval
                        {
                            Producer = group.Key,
                            Interval = interval,
                            PreviousWin = producerAwards[i - 1].Year,
                            FollowingWin = producerAwards[i].Year
                        });
                    }
                }
            }

            // Filtra os intervalos para encontrar os valores mínimos e máximos
            var minIntervalValue = producerIntervals.Min(i => i.Interval);
            var maxIntervalValue = producerIntervals.Max(i => i.Interval);

            var minIntervals = producerIntervals
                .Where(i => i.Interval == minIntervalValue)
                .ToList();

            var maxIntervals = producerIntervals
                .Where(i => i.Interval == maxIntervalValue)
                .ToList();

            // Retorna as listas de intervalos mínimos e máximos
            return (minIntervals, maxIntervals);
        }
    }
}
