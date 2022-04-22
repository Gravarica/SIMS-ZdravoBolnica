// File:    User.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:03:35
// Purpose: Definition of Class User

using Model;
using System;

public abstract class User
{
   protected String username;
   protected String firstName;
   protected String lastName;
   
   public UserType userType;

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
    
}