using ModelLayer.Model;
using RepositoryLayer.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        ResponseModel<IEnumerable<AddressBookEntity>> GetAllContacts();
        ResponseModel<UserModel> GetContactById(int id);
        ResponseModel<UserModel> AddContact(UserModel user);
        ResponseModel<UserModel> UpdateContact(int id, UserModel user);
        ResponseModel<string> DeleteContact(int id);
    }
}
