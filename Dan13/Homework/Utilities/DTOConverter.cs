using Homework.Models;
using Homework.Models.DTOs.Account;
using Homework.Models.DTOs.Address;
using Homework.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Utilities
{
    public static class DTOConverter
    {
        #region UserDTO converters
        public static User UserFromDTO(object dto)
        {
            switch (dto)
            {
                case AdminUserDTO adminDto:
                    {
                        return new User()
                        {
                            Id = adminDto.Id,
                            Name = adminDto.Name,
                            Email = adminDto.Email,
                            DateOfBirth = adminDto.DateOfBirth,
                            // setting the navigation properties like this is almost certainly wrong!
                            // the proper way is something like in Mladen's code and only for single objects
                            // collections should never be changed from the navigation property if you don't want trouble...
                            Address = DTOConverter.AddressFromDTO(adminDto.Address),
                            Accounts = adminDto.Accounts
                                .ToList()
                                .ConvertAll(new Converter<AdminAccountDTO, Account>(DTOConverter.AccountFromDTO))
                        };
                    }
                case PrivateUserDTO privDto:
                    {
                        return new User()
                        {
                            Id = privDto.Id,
                            Name = privDto.Name,
                            Accounts = privDto.Accounts
                                .ToList()
                                .ConvertAll(new Converter<PrivateAccountDTO, Account>(DTOConverter.AccountFromDTO)),
                            Address = DTOConverter.AddressFromDTO(privDto.Address)
                        };
                    }
                default: return null;
            }
        }

        public static User UserFromRegisterUserDTO(RegisterUserDTO user)
        {
            return new User()
            {

            };
        }

        public static PublicUserDTO PublicUserDTO(User user)
        {
            return new PublicUserDTO()
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public static PrivateUserDTO PrivateUserDTO(User user)
        {
            return new PrivateUserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Address = DTOConverter.PrivateAddressDTO(user.Address),
                Accounts = user.Accounts
                    .ToList()
                    .ConvertAll(new Converter<Account, PrivateAccountDTO>(DTOConverter.PrivateAccountDTO))
            };
        }

        public static AdminUserDTO AdminUserDTO(User user)
        {
            return new AdminUserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Address = DTOConverter.AdminAddressDTO(user.Address),
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Accounts = user.Accounts
                    .ToList()
                    .ConvertAll(new Converter<Account, AdminAccountDTO>(DTOConverter.AdminAccountDTO))
            };
        }
        #endregion

        #region AddressDTO converters
        public static Address AddressFromDTO(object dto)
        {
            switch (dto)
            {
                case AdminAddressDTO adminDto:
                    {
                        return new Address()
                        {
                            Id = adminDto.Id,
                            Street = adminDto.Street,
                            City = adminDto.City,
                            Country = adminDto.Country
                        };
                    }
                case PrivateAddressDTO privDto:
                    {
                        return new Address()
                        {
                            Id = privDto.Id,
                            Street = privDto.Street,
                            City = privDto.City,
                            Country = privDto.Country
                        };
                    }
                case PublicAddressDTO pubDto:
                    {
                        return new Address()
                        {

                        };
                    }
                default: return null;
            }
        }

        public static PublicAddressDTO PublicAddressDTO(Address address)
        {
            return new PublicAddressDTO() {};
        }

        public static PrivateAddressDTO PrivateAddressDTO(Address address)
        {
            return new PrivateAddressDTO()
            {
                Id = address.Id,
                City = address.City,
                Country = address.Country,
                Street = address.Street
            };
        }

        public static AdminAddressDTO AdminAddressDTO(Address address)
        {
            return new AdminAddressDTO()
            {
                Id = address.Id,
                City = address.City,
                Country = address.Country,
                Street = address.Street
            };
        }
        #endregion

        #region AccountDTO converters
        public static Account AccountFromDTO(object dto)
        {
            return null;
        }

        public static PublicAccountDTO PublicAccountDTO(Account account)
        {
            return new PublicAccountDTO() {};
        }

        public static PrivateAccountDTO PrivateAccountDTO(Account account)
        {
            return new PrivateAccountDTO()
            {
                Id = account.Id,
                Description = account.Description,
                Provider = account.Provider
            };
        }

        public static AdminAccountDTO AdminAccountDTO(Account account)
        {
            return new AdminAccountDTO()
            {
                Id = account.Id,
                Description = account.Description,
                Provider = account.Provider,
                Link = account.Link,
                // Owner = DTOConverter.AdminUserDTO(account.Owner)
            };
        }
        #endregion
    }
}