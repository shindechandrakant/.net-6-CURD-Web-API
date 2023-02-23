using ContactAPI.Data;
using ContactAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ContactsController : Controller
{
    private readonly ContactsAPIDbContext _dbContext;

    public ContactsController(ContactsAPIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var contactList = await _dbContext.Contacts.ToListAsync();
        return Ok(contactList);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetContactById([FromRoute] Guid id)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound("Contact Doesn't exist");
        }

        return Ok(contact);
    }


    [HttpPost]
    public async Task<IActionResult> AddContact([FromForm] AddContactRequest contactRequest)
    {
        var newContact = new Contact
        {
            Id = Guid.NewGuid(),
            Address = contactRequest.Address,
            Email = contactRequest.Email,
            FullName = contactRequest.FullName,
            PhoneNo = contactRequest.FullName
        };

        await _dbContext.Contacts.AddAsync(newContact);
        await _dbContext.SaveChangesAsync();
        return Ok(new { Message = "Contact Registered Successfully", Contact = newContact });
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromForm] UpdateContactRequest updateContactRequest)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);
        if (contact == null)
            return NotFound(new { message = $"Contact ID {id} doesn't exist" });

        contact.FullName = updateContactRequest.FullName;
        contact.Email = updateContactRequest.Email;
        contact.PhoneNo = updateContactRequest.PhoneNo;
        contact.Address = updateContactRequest.Address;
        await _dbContext.SaveChangesAsync();

        return Ok(new { message = "Contact Update Successfully", updatedContact = contact });
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound("Contact Doesn't exist");
        }

        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync();
        return Ok("Contact Deleted");
    }
}