using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Server.Net.Common.Specifications;
using Server.Net.Identity.Domain.Models.UserAggregate;
using System.Reflection;

namespace Server.Net.Identity.API.APIs
{
    public class CompositeSearchFilterInputCommand
    {
        public List<FilterCriteria> Filters { get; set; } = new List<FilterCriteria>();
        public PaginationCriteria Pagination { get; set; } = new PaginationCriteria();
    }

    public class UserDto
    {
        public string? UserEmail { get; set; }

        public string? DisplayName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public UserDto(string? userEmail, string? displayName, string? firstName, string? lastName)
        {
            UserEmail = userEmail;
            DisplayName = displayName;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class CompositeSearchFilterInputCommandValidator : Validator<CompositeSearchFilterInputCommand>
    {
        public CompositeSearchFilterInputCommandValidator()
        {
            RuleForEach(x => x.Filters)
                .Must(HaveMatchingPropertyInUserDto)
                .WithMessage("Property '{PropertyValue.PropertyName}' does not exist in UserDto");
        }

        // TODO: create an abstraction to inherit this method, specifying the class we will validate
        private bool HaveMatchingPropertyInUserDto(FilterCriteria filterCriteria)
        {
            var userDtoProperties = typeof(UserDto).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                   .Select(prop => prop.Name);

            return userDtoProperties.Contains(filterCriteria.PropertyName, StringComparer.OrdinalIgnoreCase);
        }
    }

    public class TestFilter : Endpoint<CompositeSearchFilterInputCommand, List<UserDto>>
    {
        public override void Configure()
        {
            Post("/api/user/test");
            AllowAnonymous();
            //DontThrowIfValidationFails();
        }

        public override async Task HandleAsync(CompositeSearchFilterInputCommand req, CancellationToken ct)
        {
            var users = new List<User>
            {
                new User(
                    "user1@email.com",
                    true,
                    "User1",
                    new byte[] { 0, 1, 2, 3 }, 
                    new byte[] { 4, 5, 6, 7 },
                    "FirstName1",
                    "LastName1",
                    new byte[] { 8, 9, 10, 11 } 
                ),
                new User(
                    "user2@email.com",
                    false,
                    "User2",
                    new byte[] { 12, 13, 14, 15 },
                    new byte[] { 16, 17, 18, 19 },
                    "FirstName2",
                    "LastName2",
                    new byte[] { 20, 21, 22, 23 }
                ),
                new User(
                    "user2@email.com",
                    false,
                    "User2",
                    new byte[] { 12, 13, 14, 15 },
                    new byte[] { 16, 17, 18, 19 },
                    "FirstName2",
                    "LastName2",
                    new byte[] { 20, 21, 22, 23 }
                )
            };

            var spec = new BaseSpecification<User>(req.Filters, req.Pagination);

            var query = users.AsQueryable();
            query = spec.Apply(query);

            var searchResult = query.ToList();

            // TODO: use mapper
            var result = searchResult.Select(user => new UserDto(
                userEmail: user.UserEmail,
                displayName: user.DisplayName,
                firstName: user.FirstName,
                lastName: user.LastName
                )).ToList();

            await SendAsync(result);
        }
    }
}
