namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Event;

    public interface IEventService
    {
        Task CreateAsync(CreateEventInputModel input);
    }
}
