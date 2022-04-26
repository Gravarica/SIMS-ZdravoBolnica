// File:    User.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:03:35
// Purpose: Definition of Class User

using Model;
using System;

public abstract class User
{
   protected String username;
   protected String password;
   protected String firstName;
   protected String lastName;
   public UserType userType;
   protected int jmbg;
   protected int phoneNumber;
   protected String email;
   protected String adress;
   protected DateTime dateOfBirth;
   protected Gender gender;

   public String Username
    {
        get { return username; }
        set { username = value; }
    }
    public String FirstName 
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public UserType UserType
    {
        get { return userType; }
        set { userType = value; }
    }

    public User(String username, String firstName, String lastName) { 
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }

    public User() { }
    
    public Int32 Jmbg
    {
         get { return jmbg; }
         set { jmbg= value; }
    }
    
    public int PhoneNumber
    { 
        get { return phoneNumber; }
        set { phoneNumber = value; }
    }
    
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    
    public string Adress
    {
        get { return adress; }
        set { adress = value; }
    }
    
    public DateTime DateOfBirth 
    {
        get { return dateOfBirth; }
        set { dateOfBirth = value; }
    }
    
    public Gender Gender
    {
        get { return gender; }
        set { gender = value; }
    }
            
            
            
          public User(String username,
                      String firstName,
                      String lastName,
                      UserType userType,
                      int jmbg,
                      int phoneNumber,
                      String email,
                      String adress,
                      DateTime dateOfBirth,
                      Gender gender)
          {
              Username = username;
              FirstName = firstName;
              LastName = lastName;
              UserType = userType;
              Jmbg = jmbg;
              PhoneNumber = phoneNumber;
              Email = email;
              Adress = adress;
              DateOfBirth = dateOfBirth;
              Gender = this.gender;
          }
          public User(string firstName, string lastName)
          {
              this.firstName = FirstName;
              this.lastName = LastName;
          }
}