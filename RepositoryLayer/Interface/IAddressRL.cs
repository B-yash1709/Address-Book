using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        IEnumerable<AddressBookEntity> GetAllContacts();
        ResponseUserModel GetContactById(int id);
        ResponseUserModel AddContact(ResponseUserModel user);
        ResponseUserModel UpdateContact(int id, ResponseUserModel user);
        bool DeleteContact(int id);
    }
}
