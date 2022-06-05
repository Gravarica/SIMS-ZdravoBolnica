using System;
using Model;

namespace HospitalProject.DataTransferObjects;

public class NewUserDTO
{
    private string _username;
    private string _password;
    private string _firstName;
    private string _lastName;
    private UserType _userType;
    private int _jmbg;
    private int _phoneNumber;
    private string _email;
    private string _adress;
    private DateTime _dateOfBirth;
    private Gender _gender;

    public NewUserDTO(string username, string password, string firstName, string lastName,
        UserType userType, int jmbg, int phoneNumber, string email, string address,
        DateTime dateOfBirth, Gender gender)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        Jmbg = jmbg;
        PhoneNumber = phoneNumber;
        Email = email;
        Adress = address;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }

    public String Username
    {
        get 
        { 
            return _username;
        }
        set 
        { 
            _username = value;
        }
    }
    public String FirstName 
    {
        get 
        { 
            return _firstName;
        }
        set 
        { 
            _firstName = value;
        }
    }

    public string LastName
    {
        get 
        { 
            return _lastName;
        }
        set 
        { 
            _lastName = value;
        }
    }

    
    public UserType UserType
    {
        get 
        { 
            return _userType;
        }
        set 
        { 
            _userType = value;
        }
    }

    public String Password
    { 
        get
        {
            return _password;
        }
        set
        {
            _password = value;
        }
    }
    
    public Int32 Jmbg
    {
        get { return _jmbg; }
        set { _jmbg= value; }
    }
    
    public int PhoneNumber
    { 
        get { return _phoneNumber; }
        set { _phoneNumber = value; }
    }
    
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    
    public string Adress
    {
        get { return _adress; }
        set { _adress = value;}
    }
    
    public DateTime DateOfBirth 
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value;}
    }
    
    public Gender Gender
    {
        get { return _gender; }
        set { _gender = value;}
    }
}