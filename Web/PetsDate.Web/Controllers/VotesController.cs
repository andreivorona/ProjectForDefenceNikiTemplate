namespace PetsDate.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponceModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetVoteAsync(input.AnimalId, userId, input.Value);

            var averageVotes = this.votesService.GetAverageVotes(input.AnimalId);

            return new PostVoteResponceModel
            {
                AverageVote = averageVotes,
            };
        }
    }
}
