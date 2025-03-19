using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL _addressRL;


        public AddressBL(IAddressRL addressRL)
        {
            _addressRL = addressRL;
        }

        public ResponseModelSMD<IEnumerable<AddressBookEntity>> GetAllContacts()
        {
            var contacts = _addressRL.GetAllContacts();

            return new ResponseModelSMD<IEnumerable<AddressBookEntity>>
            {
                Success = true,
                Message = "Contacts retrieved successfully",
                Data = contacts
            };
        }

        public ResponseModelSMD<ResponseUserModel> GetContactById(int id)
        {
            var contact = _addressRL.GetContactById(id);

            if (contact == null)
            {
                return new ResponseModelSMD<ResponseUserModel>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModelSMD<ResponseUserModel>
            {
                Success = true,
                Message = "Contact retrieved successfully",
                Data = contact
            };
        }

        public ResponseModelSMD<ResponseUserModel> AddContact(ResponseUserModel user)
        {
            var addedContact = _addressRL.AddContact(user);

            return new ResponseModelSMD<ResponseUserModel>
            {
                Success = true,
                Message = "Contact added successfully",
                Data = addedContact
            };
        }

        public ResponseModelSMD<ResponseUserModel> UpdateContact(int id, ResponseUserModel user)
        {
            var updatedContact = _addressRL.UpdateContact(id, user);

            if (updatedContact == null)
            {
                return new ResponseModelSMD<ResponseUserModel>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModelSMD<ResponseUserModel>
            {
                Success = true,
                Message = "Contact updated successfully",
                Data = updatedContact
            };
        }

        public ResponseModelSMD<string> DeleteContact(int id)
        {
            var isDeleted = _addressRL.DeleteContact(id);

            if (!isDeleted)
            {
                return new ResponseModelSMD<string>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModelSMD<string>
            {
                Success = true,
                Message = "Contact deleted successfully",
                Data = $"Contact with ID {id} deleted"
            };


        }
    }
}
