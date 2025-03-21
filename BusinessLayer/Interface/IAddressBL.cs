using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        ResponseModelSMD<IEnumerable<AddressBookEntity>> GetAllContacts();
        ResponseModelSMD<ResponseUserModel> GetContactById(int id);
        ResponseModelSMD<ResponseUserModel> AddContact(ResponseUserModel user);
        ResponseModelSMD<ResponseUserModel> UpdateContact(int id, ResponseUserModel user);
        ResponseModelSMD<string> DeleteContact(int id);
    }
}
