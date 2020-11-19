namespace PetsDate.Services.Data
{
    using PetsDate.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        // Use View model
        IndexViewModel GetCounts();
    }
}
