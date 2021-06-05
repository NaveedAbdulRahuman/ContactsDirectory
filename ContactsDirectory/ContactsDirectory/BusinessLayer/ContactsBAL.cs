using ContactsDirectory.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsDirectory.BusinessLayer
{
    public class ContactsBAL : IContactsBAL
    {
        private readonly ContactDbContext _context;

        public ContactsBAL(ContactDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts;
        }

        public async Task<Contact> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            return contact;
        }

        public IEnumerable<Contact> GetContactsByName(string nameLike)
        {
            if (String.IsNullOrEmpty(nameLike))
                return _context.Contacts;

            return _context.Contacts.Where(x => x.Name.Contains(nameLike));
        }
    }
}
