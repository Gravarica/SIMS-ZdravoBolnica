// File:    User.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:03:35
// Purpose: Definition of Class User

using HospitalProject.Model;
using Model;
using System;

public class User : ViewModelBase
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
        get 
        { 
            return username;
        }
        set 
        { 
            username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    public String FirstName 
    {
        get 
        { 
            return firstName;
        }
        set 
        { 
            firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get 
        { 
            return lastName;
        }
        set 
        { 
            lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    
    public UserType UserType
    {
        get 
        { 
            return userType;
        }
        set 
        { 
            userType = value;
            OnPropertyChanged(nameof(UserType));
        }
    }

    public String Password
    { 
        get
        {
            return password;
        }
        set
        {
            password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public User(String username,String firstName, String lastName) { 
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }


    public User(string username, string password, UserType userType)
    {
        Username = username;
        Password = password;
        UserType = userType;
    }

    public User(String username, String password, String firstName, String lastName, UserType userType)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public User(String firstName, String lastName, int jmbg) { 
        Username = username;
        FirstName = firstName;
        Jmbg = jmbg;
        
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