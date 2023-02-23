namespace ContactAPI.Models;

public class AddContactRequest
{
    public string  FullName { get; set; }
    public string  Email { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
}