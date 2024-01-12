namespace User_Management_Application.Service
{
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using User_Management_Application.Database;
    using User_Management_Application.Entities;
    using User_Management_Application.Models;
    using User_Management_Application.Utilities;

    public class UserService
    {
        private readonly DataContext _context;
        private readonly ResponseStatusCode _responseStatusCode;

        public UserService(DataContext context)
        {
            _context = context;
            _responseStatusCode = new ResponseStatusCode();
        }

        public Response<UserModel> CreateUser(UserModel user)
        {
            try
            {
                var existingUser = _context.User.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    return new Response<UserModel>
                    {
                        ResponseMessage = "Email already exists. User not created.",
                        ResponseCode = _responseStatusCode.RECORDEXIST,
                        Data = null,
                    };
                }
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow
                };
                _context.User.Add(newUser);
                _context.SaveChanges();
                if (newUser.Id > 0) 
                {
                    var createdUserModel = new UserModel
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email,
                       
                    };
                    newUser.Status = "ACTIVE";
                    newUser.RegistrationStatus = "SUCCESSFUL";
                    _context.SaveChanges();
                    return new Response<UserModel>
                    {
                        ResponseMessage = "User created successfully.",
                        ResponseCode = _responseStatusCode.SUCCESS,
                        Data = createdUserModel
                    };
                }
                else
                {
                    newUser.RegistrationStatus = "FAILED";
                    newUser.Status = "INACTIVE";
                    _context.SaveChanges();
                    return new Response<UserModel>
                    {
                        ResponseMessage = "Failed to create user.",
                        ResponseCode = _responseStatusCode.FAILED,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<UserModel>
                {
                    ResponseMessage = $"Failed to create user. Exception: {ex.Message}",
                    ResponseCode = _responseStatusCode.FAILED,
                    Data = null,
                };
            }
        }


        public Response<List<User>> GetAllUsers()
        {
            try
            {
                var users = _context.User.ToList();

                if (users.Count > 0)
                {
                    return new Response<List<User>>
                    {
                        ResponseMessage = "Users retrieved successfully.",
                        ResponseCode = _responseStatusCode.SUCCESS,
                        Data = users
                    };
                }
                else
                {
                    return new Response<List<User>>
                    {
                        ResponseMessage = "No users found.",
                        ResponseCode = _responseStatusCode.NORECORDFOUND,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<User>>
                {
                    ResponseMessage = $"Failed to retrieve users. Exception: {ex.Message}",
                    ResponseCode = _responseStatusCode.FAILED,
                    Data = null,
                };
            }
        }


        public Response<UserModel> GetUserById(int userId)
        {
            try
            {
                var user = _context.User
                    .Where(u => u.Id == userId)
                    .SingleOrDefault();


                if (user != null)
                {
                    var userModel = new UserModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                    };

                    return new Response<UserModel>
                    {
                        ResponseMessage = "User retrieved successfully.",
                        ResponseCode = _responseStatusCode.SUCCESS,
                        Data = userModel
                    };
                }
                else
                {
                    return new Response<UserModel>
                    {
                        ResponseMessage = "User not found.",
                        ResponseCode = _responseStatusCode.NORECORDFOUND,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<UserModel>
                {
                    ResponseMessage = $"Failed to retrieve user. Exception: {ex.Message}",
                    ResponseCode = _responseStatusCode.FAILED,
                    Data = null,
                };
            }
        }


        public Response<UserModel> UpdateUser(int userId, UserModel updatedUser)
        {
            try
            {
                var userToUpdate = _context.User.FirstOrDefault(u => u.Id == userId);

                if (userToUpdate != null)
                {
                    var existingUserWithUpdatedEmail = _context.User.FirstOrDefault(u => u.Email == updatedUser.Email && u.Id != userId);

                    if (existingUserWithUpdatedEmail != null)
                    {
                        return new Response<UserModel>
                        {
                            ResponseMessage = "Email already exists for another user. User not updated.",
                            ResponseCode = _responseStatusCode.RECORDEXIST,
                            Data = null,
                        };
                    }
                    userToUpdate.FirstName = updatedUser.FirstName;
                    userToUpdate.LastName = updatedUser.LastName;
                    userToUpdate.Email = updatedUser.Email;
                    userToUpdate.DateLastModified = DateTime.UtcNow;
                    userToUpdate.RegistrationStatus = "SUCCESSFUL";
                    userToUpdate.Status = "ACTIVE";
                    _context.SaveChanges();
                    var updatedUserModel = new UserModel
                    {
                        FirstName = userToUpdate.FirstName,
                        LastName = userToUpdate.LastName,
                        Email = userToUpdate.Email,
                    };

                    return new Response<UserModel>
                    {
                        ResponseMessage = "User updated successfully.",
                        ResponseCode = _responseStatusCode.SUCCESS,
                        Data = updatedUserModel
                    };
                }
                else
                {
                    return new Response<UserModel>
                    {
                        ResponseMessage = "User not found. Cannot update.",
                        ResponseCode = _responseStatusCode.NORECORDFOUND,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<UserModel>
                {
                    ResponseMessage = $"Failed to update user. Exception: {ex.Message}",
                    ResponseCode = _responseStatusCode.FAILED,
                    Data = null,
                };
            }
        }


        public Response<UserModel> DeleteUser(int userId)
        {
            try
            {
                var userToDelete = _context.User.FirstOrDefault(u => u.Id == userId);

                if (userToDelete != null)
                {
                    _context.User.Remove(userToDelete);
                    _context.SaveChanges();
                    return new Response<UserModel>
                    {
                        ResponseMessage = "User deleted successfully.",
                        ResponseCode = _responseStatusCode.SUCCESS,
                        Data = null,
                    };
                }
                else
                {
                    return new Response<UserModel>
                    {
                        ResponseMessage = "User not found. Cannot delete.",
                        ResponseCode = _responseStatusCode.NORECORDFOUND,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<UserModel>
                {
                    ResponseMessage = $"Failed to delete user. Exception: {ex.Message}",
                    ResponseCode = _responseStatusCode.FAILED,
                    Data = null,
                };
            }
        }



    }

}
