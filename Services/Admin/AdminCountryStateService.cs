using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Services.Admin
{
    public class AdminCountryStateService : IAdminInterfaces, ITestInterface
    {
        private readonly MyDbContext database;
        public AdminCountryStateService(MyDbContext db)
        {
            database = db;
        }

        //**************************************** Add New Country ************************************// 
        public async Task<AdminCommonResponseModel> AdminAddCountryAsync(AdminCountryAddModel data)
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
                        return new AdminCommonResponseModel
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
                return new AdminCommonResponseModel
                {
                    Success = false,
                    Message = "Country Code is too long. Please enter a valid code (max 10 characters).",
                };
            } 
            return new AdminCommonResponseModel { Success = false, Message = "Something went wrong! please try again" };
        }

        //**************************************** Add New State ************************************// 
        public async Task<AdminCommonResponseModel> AdminAddStatesAsync(AdminStateAddModel data)
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
                    return new AdminCommonResponseModel
                    {
                        Success = true,
                        Message = "State has been added successfully"
                    };
                }
            }
            return new AdminCommonResponseModel
            {
                Success = false,
                Message = "Something went wrong! please try again!",
            };
        }

        //**************************************** Delete Country By Id ************************************// 
        public async Task<AdminCommonResponseModel> AdminDeleteCountryById(Guid Id)
        {
            var isCountryAvail = await database.Countries.FindAsync(Id);
            if (isCountryAvail != null)
            {
                database.Countries.Remove(isCountryAvail);
                await database.SaveChangesAsync();
                return new AdminCommonResponseModel
                {
                    Success = true,
                    Message = "Country has been deleted successfully"
                };
            }
            return new AdminCommonResponseModel
            {
                Success = false,
                Message = "Country not found! please try again",
            };
        }

        //**************************************** Delete State By Id ************************************//
        public async Task<AdminCommonResponseModel> AdminDeleteStateById(Guid Id)
        {
            var isValid = await database.States.FindAsync(Id);
            if (isValid != null)
            {
                database.States.Remove(isValid);
                await database.SaveChangesAsync();
                return new AdminCommonResponseModel
                {
                    Success = true,
                    Message = "State has been deleted successfully"
                };
            }
            return new AdminCommonResponseModel
            {
                Success = false,
                Message = "State not found! please try again",
            };
        }

        //**************************************** Get All Countries ************************************//
        public async Task<List<CountryDto>> AdminGetAllCountryAsync()
        {
            return await database.Countries.Include(x => x.State).AsNoTracking()
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryCode = c.CountryCode,
                    State = c.State,
                    Timestamp = c.Timestamp,
                    IsActive = c.IsActive,
                }).ToListAsync();
        }
        //**************************************** Get All States ************************************//
        public async Task<List<StateDto>> AdminGetAllStateAsync()
        {
            return await database.States.Include(c => c.Country)
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
        }

        //**************************************** Get Single Country By Id ************************************//
        public async Task<Country?> AdminGetCountryById(Guid Id)
        {
            return await database.Countries.FirstOrDefaultAsync(c => c.Id == Id);
        }

        //**************************************** Get Single State By Id ************************************//
        public async Task<State?> AdminGetStateById(Guid Id)
        {
            return await database.States.FirstOrDefaultAsync(s => s.Id == Id);
        }

        //**************************************** Update Single Country By Id ************************************//
        public async Task<AdminCommonResponseModel> AdminUpdateCountryById(Guid Id, AdminCountryAddModel data)
        {
            if (!string.IsNullOrWhiteSpace(data.CountryCode) && !string.IsNullOrWhiteSpace(data.Name))
            {
                var isCountryExist = await database.Countries.FirstOrDefaultAsync(x => x.Id == Id);
                if (isCountryExist != null)
                {
                    isCountryExist.Name = data.Name;
                    isCountryExist.CountryCode = data.CountryCode;
                    await database.SaveChangesAsync();
                    return new AdminCommonResponseModel
                    {
                        Success = true,
                        Message = "Country has been updated successfully",
                        Data = isCountryExist
                    };
                }
            }
            return new AdminCommonResponseModel
            {
                Success = false,
                Message = "Something went wrong! please try again"
            };
        }

        //**************************************** Update Single State By Id ************************************//
        public async Task<AdminCommonResponseModel> AdminUpdateStateById(Guid Id, AdminStateAddModel data)
        {
            if (!string.IsNullOrWhiteSpace(data.StateCode) && !string.IsNullOrWhiteSpace(data.StateName))
            {
                var isStateExist = await database.States.FirstOrDefaultAsync(y => y.Id == Id);
                if (isStateExist != null)
                {
                    isStateExist.StateName = data.StateName;
                    isStateExist.StateCode = data.StateCode;
                    await database.SaveChangesAsync();
                    return new AdminCommonResponseModel
                    {
                        Success = true,
                        Message = "State has been updated successfully"
                    };
                }
            }
            return new AdminCommonResponseModel
            {
                Success = false,
                Message = "Something went wrong! please try again"
            };
        }

        public Task<bool> Fly()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Sound()
        {
            throw new NotImplementedException();
        }

    } // END CLASS HERE


    


}

       
    
