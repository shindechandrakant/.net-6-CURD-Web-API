﻿namespace ContactAPI.Models;

public class UpdateContactRequest
{
    public string  FullName { get; set; }
    public string  Email { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
}