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
        ResponseModelSMT<IEnumerable<AddressBookEntity>> GetAllContacts();
        ResponseModelSMT<ResponseUserModel> GetContactById(int id);
        ResponseModelSMT<ResponseUserModel> AddContact(ResponseUserModel user);
        ResponseModelSMT<ResponseUserModel> UpdateContact(int id, ResponseUserModel user);
        ResponseModelSMT<string> DeleteContact(int id);
    }
}
