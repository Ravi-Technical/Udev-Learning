using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Interface.Admin
{
    public interface IAdminInterfaces
    {
        Task<AdminCommonResponseModel<CountryDto>> AdminAddCountryAsync(AdminCountryAddModel data); // Dummy Test
        Task<AdminCommonResponseModel<bool>> AdminAddStatesAsync(AdminStateAddModel data);
        Task<AdminCommonResponseModel<List<CountryDto>>> AdminGetAllCountryAsync();
        Task<AdminCommonResponseModel<List<StateDto>>> AdminGetAllStateAsync();   
        Task<AdminCommonResponseModel<CountryDto>> AdminGetCountryById(Guid Id);
        Task<AdminCommonResponseModel<StateDto>> AdminGetStateById(Guid Id);
        Task<AdminCommonResponseModel<CountryDto>> AdminUpdateCountryById(Guid Id, AdminCountryAddModel data);
        Task<AdminCommonResponseModel<StateDto>> AdminUpdateStateById(Guid Id, AdminStateAddModel data);
        Task<AdminCommonResponseModel<bool>> AdminDeleteCountryById(Guid Id);
        Task<AdminCommonResponseModel<bool>> AdminDeleteStateById(Guid Id);

    }

}
