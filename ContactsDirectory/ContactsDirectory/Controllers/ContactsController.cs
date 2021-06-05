using ContactsDirectory.BusinessLayer;
using ContactsDirectory.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IContactsBAL _contactsBAL;

        public ContactsController(ILogger<ContactsController> logger,
            IContactsBAL contactsBAL)
        {
            _logger = logger;
            _contactsBAL = contactsBAL;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            try
            {
                var contacts = _contactsBAL.GetContacts();

                _logger.LogInformation($"{contacts.Count()} Contacts fetched.");

                return contacts;
            }
            catch(Exception ex)
            {
                _logger.LogError("Contacts fetch failed");
                _logger.LogError(ex.StackTrace);
            }

            return null;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Model State");
                    return BadRequest(ModelState);
                }

                var contact = await _contactsBAL.GetContact(id);

                if (contact == null)
                {
                    return NotFound();
                }

                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Contacts fetch for Contact Id {id} failed");
                _logger.LogError(ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

        // GET: api/Contacts/?nameLike=name
        [HttpGet("GetContacts")]
        public IEnumerable<Contact> GetContactsByName(string nameLike)
        {
            try
            {
                var contacts = _contactsBAL.GetContactsByName(nameLike);

                _logger.LogInformation($"{contacts.Count()} filtered contacts fetched.");

                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError("Filtered contacts fetch failed");
                _logger.LogError(ex.StackTrace);
            }

            return null;
        }
    }
}