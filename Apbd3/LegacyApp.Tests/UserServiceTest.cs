using System;
using Xunit;

namespace LegacyApp.Tests;

public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Null_Or_Empty()
    {
        //Arrange
        UserService userService = new UserService();
        //Act
        bool res = userService.AddUser("", "Doe", "doe@gmail.com", DateTime.Now, 1);
        //Assert
        Assert.False(res);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Null_Or_Empty()
    {
        UserService userService = new UserService();
        
        bool res = userService.AddUser("John", null, "doe@gmail.com", DateTime.Now, 1);
        
        Assert.False(res);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Not_Correct()
    {
        UserService userService = new UserService();
        
        bool res = userService.AddUser("John", "Doe", "doegmailcom", DateTime.Now, 1);
        
        Assert.False(res);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Under_21()
    {
        UserService userService = new UserService();
        
        bool res = userService.AddUser("John", "Doe", "doe@gmail.com", DateTime.Parse(
            "2005-10-10"), 1);
        
        Assert.False(res);
    }

    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_UserId_Not_In_Db()
    {
        UserService userService = new UserService();
        
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Doe", "doe@gmail.com", DateTime.Parse(
                "1980-10-10"), 1000);
        });
    }

    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_User_LastName_Not_In_Db()
    {
        UserService userService = new UserService();

        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Bartosiak", "doe@gmail.com", DateTime.Parse(
                "1980-10-10"), 1);
        });
    }

    [Fact]
    public void AddUser_Should_Return_False_When_User_Has_Credit_Limit_And_Credit_Limit_Under_500()
    {
        var userService = new UserService();

        bool res = userService.AddUser("John", "Kowalski", "doe@gmail.com", DateTime.Parse(
            "1980-10-10"), 1);
        
        Assert.False(res);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_User_Is_Important_Or_Very_Important_Client()
    {
        var userService = new UserService();

        bool res = userService.AddUser("John", "Doe", "doe@gmail.com", DateTime.Parse(
            "1980-10-10"), 1);
        
        Assert.True(res);
    }
}