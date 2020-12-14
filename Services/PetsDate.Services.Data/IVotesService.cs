namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int animalId, string userId, int value);

        double GetAverageVotes(int animalId);
    }
}
