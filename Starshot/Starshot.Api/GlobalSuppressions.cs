// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>", Scope = "type", Target = "~T:Starshot.Api.Source.Domain.BusinessRules.Base.BusinessRuleException")]
[assembly: SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>", Scope = "type", Target = "~T:Starshot.Api.Source.Domain.BusinessRules.ResourceNotFoundException")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "<Pending>", Scope = "type", Target = "~T:Starshot.Api.Source.Domain.Features.EditUser.EditUserParameters")]
[assembly: SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "<Pending>", Scope = "member", Target = "~M:Starshot.Api.Source.Infrastructure.DatabaseProvider.EntityFramework.DataContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "<Pending>", Scope = "type", Target = "~T:Starshot.Api.Source.Domain.Features.AddUser.AddUserParameters")]
[assembly: SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>", Scope = "type", Target = "~T:Starshot.Api.Source.Domain.BusinessRules.UsernamePasswordIncorrectException")]
