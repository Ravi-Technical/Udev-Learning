using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Services.Admin
{
    public class AdminCountryStateService : IAdminInterfaces
    {
        private readonly MyDbContext database;
        public AdminCountryStateService(MyDbContext db)
        {
            database = db;
        }

        //**************************************** Add New Country ************************************// 
        public async Task<AdminCommonResponseModel<CountryDto>> AdminAddCountryAsync(AdminCountryAddModel data)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(data.CountryCode) && !string.IsNullOrWhiteSpace(data.Name))
                {
                    var isExist = await database.Countries.FirstOrDefaultAsync(x => x.CountryCode == data.CountryCode);
                    if (isExist == null)
                    {
                        var mergeData = new Country
                        {
                            Name = data.Name,
                            CountryCode = data.CountryCode,
                        };
                        await database.Countries.AddAsync(mergeData);
                        await database.SaveChangesAsync();
                        return new AdminCommonResponseModel<CountryDto>
                        {
                            Success = true,
                            Message = "Country has been added successfully",
                            Data = new CountryDto
                            {
                                Id = mergeData.Id,
                                Name = mergeData.Name,
                                CountryCode = mergeData.CountryCode,
                                IsActive = mergeData.IsActive,
                                Timestamp = mergeData.Timestamp,
                            }
                        };
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                return new AdminCommonResponseModel<CountryDto>
                {
                    Success = false,
                    Message = "Country Code is too long. Please enter a valid code (max 10 characters).",
                };
            } 
            return new AdminCommonResponseModel<CountryDto> { Success = false, Message = "Something went wrong! please try again" };
        }

        //**************************************** Add New State ************************************// 
        public async Task<AdminCommonResponseModel<bool>> AdminAddStatesAsync(AdminStateAddModel data)
        {
            if (!string.IsNullOrWhiteSpace(data.StateName))
            {
                var isExist = await database.States.FirstOrDefaultAsync(x => x.StateName == data.StateName);
                if (isExist == null)
                {
                    var dataModify = new State
                    {
                        StateName = data.StateName,
                        StateCode = data.StateCode,
                        CountryId = data.CountryId,
                    };
                    await database.States.AddAsync(dataModify);
                    await database.SaveChangesAsync();
                    return new AdminCommonResponseModel<bool>
                    {
                        Success = true,
                        Message = "State has been added successfully"
                    };
                }
            }
            return new AdminCommonResponseModel<bool>
            {
                Success = false,
                Message = "Something went wrong! please try again!",
            };
        }

        //**************************************** Delete Country By Id ************************************// 
        public async Task<AdminCommonResponseModel<bool>> AdminDeleteCountryById(Guid Id)
        {
            var isCountryAvail = await database.Countries.FindAsync(Id);
            if (isCountryAvail != null)
            {
                database.Countries.Remove(isCountryAvail);
                await database.SaveChangesAsync();
                return new AdminCommonResponseModel<bool>
                {
                    Success = true,
                    Message = "Country has been deleted successfully"
                };
            }
            return new AdminCommonResponseModel<bool>
            {
                Success = false,
                Message = "Country not found! please try again",
            };
        }

        //**************************************** Delete State By Id ************************************//
        public async Task<AdminCommonResponseModel<bool>> AdminDeleteStateById(Guid Id)
        {
            var isValid = await database.States.FindAsync(Id);
            if (isValid != null)
            {
                database.States.Remove(isValid);
                await database.SaveChangesAsync();
                return new AdminCommonResponseModel<bool>
                {
                    Success = true,
                    Message = "State has been deleted successfully"
                };
            }
            return new AdminCommonResponseModel<bool>
            {
                Success = false,
                Message = "State not found! please try again",
            };
        }

        //**************************************** Get All Countries ************************************//
        public async Task<AdminCommonResponseModel<List<CountryDto>>> AdminGetAllCountryAsync()
        {
           var getAllCountry = await database.Countries.Include(x => x.State).AsNoTracking()
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryCode = c.CountryCode,
                    State = c.State,
                    Timestamp = c.Timestamp,
                    IsActive = c.IsActive,
                }).ToListAsync();
            return new AdminCommonResponseModel<List<CountryDto>>
            {
                Success = true,
                Message = "All countries fetch successfully",
                Data = getAllCountry
            };
        }
        //**************************************** Get All States ************************************//
        public async Task<AdminCommonResponseModel<List<StateDto>>> AdminGetAllStateAsync()
        {
           var getAllState = await database.States.Include(c => c.Country)
                .AsNoTracking().Select(s =>
              new StateDto
              {
                  Id = s.Id,
                  StateName = s.StateName,
                  StateCode = s.StateCode,
                  Country = s.Country == null ? null : new CountryMiniDto
                  {
                      Id = s.Country.Id,
                      Name= s.Country.Name,
                      CountryCode = s.Country.CountryCode,
                  },
                  CountryId = s.CountryId,
                  Timestamp = s.Timestamp,
                  IsActive = s.IsActive,
              }).ToListAsync();
            return new AdminCommonResponseModel<List<StateDto>>
            {
                Success=true,
                Message="States fetched successfully",
                Data = getAllState
            };
        }

        //**************************************** Get Single Country By Id ************************************//
        public async Task<AdminCommonResponseModel<CountryDto>> AdminGetCountryById(Guid Id)
        {
            var getSingleCategory = await database.Countries.FirstOrDefaultAsync(c => c.Id == Id);
            if (getSingleCategory == null)
            {
                return new AdminCommonResponseModel<CountryDto>
                {
                    Success = false,
                    Message = "Country not found!",
                };
            } 
            return new AdminCommonResponseModel<CountryDto>
            {
                Success = true,
                Message = "Fetched country successfully",
                Data = new CountryDto
                {
                     Id = getSingleCategory.Id,
                     Name = getSingleCategory.Name,
                     CountryCode = getSingleCategory.CountryCode,
                     State = getSingleCategory.State,
                     Timestamp = getSingleCategory.Timestamp,
                     IsActive = getSingleCategory.IsActive
                }
            };
        }

        //**************************************** Get Single State By Id ************************************//
        public async Task<AdminCommonResponseModel<StateDto>> AdminGetStateById(Guid Id)
        {
            var getState = await database.States.Include(x=>x.Country).FirstOrDefaultAsync(s => s.Id == Id);
            if(getState == null)
            {
                return new AdminCommonResponseModel<StateDto>
                {
                    Success = false,
                    Message = "State not found!"
                };
            }
            return new AdminCommonResponseModel<StateDto>
            {
                 Success = true,
                 Message = "",
                 Data = new StateDto
                 {
                     Id = getState.Id,
                     StateName = getState.StateName,
                     StateCode = getState.StateCode,
                     Country = getState.Country==null ? null : new CountryMiniDto { 
                        Id = getState.Country.Id,
                        Name = getState.Country.Name,
                        CountryCode = getState.Country.CountryCode
                     },
                     CountryId = getState.CountryId,
                     Timestamp = getState.Timestamp,
                     IsActive = getState.IsActive
                 }
            };
        }

        //**************************************** Update Single Country By Id ************************************//
        public async Task<AdminCommonResponseModel<CountryDto>> AdminUpdateCountryById(Guid Id, AdminCountryAddModel data)
        {
            if (!string.IsNullOrWhiteSpace(data.CountryCode) && !string.IsNullOrWhiteSpace(data.Name))
            {
                var isCountryExist = await database.Countries.FirstOrDefaultAsync(x => x.Id == Id);
                if (isCountryExist != null)
                {
                    isCountryExist.Name = data.Name;
                    isCountryExist.CountryCode = data.CountryCode;
                    await database.SaveChangesAsync();
                    return new AdminCommonResponseModel<CountryDto>
                    {
                        Success = true,
                        Message = "Country has been updated successfully",
                        Data = new CountryDto
                        {
                            Id = isCountryExist.Id,
                            Name = isCountryExist.Name,
                            CountryCode = isCountryExist.CountryCode,
                            State = isCountryExist.State,
                            Timestamp = isCountryExist.Timestamp,
                            IsActive = isCountryExist.IsActive,
                        }
                    };
                }
            }
            return new AdminCommonResponseModel<CountryDto>
            {
                Success = false,
                Message = "Something went wrong! please try again"
            };
        }

        //**************************************** Update Single State By Id ************************************//
        public async Task<AdminCommonResponseModel<StateDto>> AdminUpdateStateById(Guid Id, AdminStateAddModel data)
        {
            if (!string.IsNullOrWhiteSpace(data.StateCode) && !string.IsNullOrWhiteSpace(data.StateName))
            {
                var isStateExist = await database.States.Include(s=>s.Country).FirstOrDefaultAsync(y => y.Id == Id);
                if (isStateExist != null)
                {
                    isStateExist.StateName = data.StateName;
                    isStateExist.StateCode = data.StateCode;
                    await database.SaveChangesAsync();
                    return new AdminCommonResponseModel<StateDto>
                    {
                        Success = true,
                        Message = "State has been updated successfully",
                        Data = new StateDto
                        {
                            Id = isStateExist.Id,
                            StateName = isStateExist.StateName,
                            StateCode = isStateExist.StateCode,
                            Country = isStateExist.Country == null ? null : 
                            new CountryMiniDto { 
                               Id = isStateExist.Country.Id,
                               Name = isStateExist.Country.Name,
                               CountryCode = isStateExist.Country.CountryCode,
                            },
                            CountryId = isStateExist.CountryId,
                            Timestamp = isStateExist.Timestamp,
                            IsActive = isStateExist.IsActive,
                        }
                    };
                }
            }
            return new AdminCommonResponseModel<StateDto>
            {
                Success = false,
                Message = "Something went wrong! please try again"
            };
        }
 
 

    } // END CLASS HERE


    


}

       
    
