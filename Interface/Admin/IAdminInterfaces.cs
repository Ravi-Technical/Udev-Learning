using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Interface.Admin
{
    public interface IAdminInterfaces
    {
        Task<AdminCommonResponseModel> AdminAddCountryAsync(AdminCountryAddModel data); // Dummy Test
        Task<AdminCommonResponseModel> AdminAddStatesAsync(AdminStateAddModel data);
        Task<List<CountryDto>> AdminGetAllCountryAsync();
        Task<List<StateDto>> AdminGetAllStateAsync();   
        Task<Country?> AdminGetCountryById(Guid Id);
        Task<State?> AdminGetStateById(Guid Id);
        Task<AdminCommonResponseModel> AdminUpdateCountryById(Guid Id, AdminCountryAddModel data);
        Task<AdminCommonResponseModel> AdminUpdateStateById(Guid Id, AdminStateAddModel data);
        Task<AdminCommonResponseModel> AdminDeleteCountryById(Guid Id);
        Task<AdminCommonResponseModel> AdminDeleteStateById(Guid Id);
    }
    public interface ITestInterface
    {
        Task<bool> Fly();
        Task<bool> Sound();
    }
}
