﻿using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ValidationException = Domain.Exceptions.ValidationException;

namespace Services
{
    public class AuthenticationServices(UserManager<User> userManager,
        IMapper mapper,
        IOptions<JwtOptions> options) : IAuthenticationServices
    {
        public async Task<IdentityAddressDto> GetUserAddressAsync(string email)
        {
            var user =  await userManager.Users.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == email);

            if(user is null)
                throw new UserNotFoundException(email);

            return mapper.Map<IdentityAddressDto>(user.Address);
        }

        public async Task<UserResultDto> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                throw new UserNotFoundException(email);

            return new UserResultDto
                (
                DisplayName: user.DisplayName,
                Email: user.Email!,
                null
                );
            
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user != null;

        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
         {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                throw new UnauthorizedAccessException($"Email : {loginDto.Email} is not Exist!");

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
                throw new UnauthorizedAccessException();

            return new UserResultDto
            (
                user.DisplayName,
                user.Email!,
               await CreateTokenAsync(user)
            );

        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
            {
                 var user = new User
                {
                    DisplayName = registerDto.DisplayName,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                throw new ValidationException(errors);
            }

            return new UserResultDto
           (
               user.DisplayName,
               user.Email!,
               await CreateTokenAsync(user)
           );
        }

        public async Task<IdentityAddressDto> UpdateUserAddressAsync(string email, IdentityAddressDto addressDto)
        {
            var user = await userManager.Users.Include(x => x.Address)
                      .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                throw new UserNotFoundException(email);

                var mappedAddress = mapper.Map<Address>(addressDto);
                user.Address = mappedAddress;
                await userManager.UpdateAsync(user);
                return addressDto;
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var JwtOptions = options.Value;

            //Claims

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,  user.Email!),
            };

            //Roles
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecurityKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(JwtOptions.DurationInDays),
                audience: JwtOptions.Audience,
                issuer: JwtOptions.Issuer
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
 }
