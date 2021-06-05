using ContactsDirectory.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsDirectory.BusinessLayer
{
    public interface IContactsBAL
    {
        IEnumerable<Contact> GetContacts();
        Task<Contact> GetContact(int id);
        IEnumerable<Contact> GetContactsByName(string nameLike);
    }
}