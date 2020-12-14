namespace PetsDate.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task SetVoteAsync(int animalId, string userId, int value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.AnimalId == animalId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    UserId = userId,
                    AnimalId = animalId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;

            await this.votesRepository.SaveChangesAsync();
        }

        public double GetAverageVotes(int animalId)
        {
            var averageVotes = this.votesRepository.All()
                 .Where(x => x.AnimalId == animalId);

            if (averageVotes.Count() == 0)
            {
                return 0;
            }

            return averageVotes.Average(x => x.Value);
        }
    }
}
