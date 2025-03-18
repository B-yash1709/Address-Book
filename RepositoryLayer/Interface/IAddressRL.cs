using ModelLayer.Model;
using RepositoryLayer.UserEntity;
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
        UserModel GetContactById(int id);
        UserModel AddContact(UserModel user);
        UserModel UpdateContact(int id, UserModel user);
        bool DeleteContact(int id);
    }
}
