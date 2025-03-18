using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.UserEntity;
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

        public ResponseModelSMT<IEnumerable<AddressBookEntity>> GetAllContacts()
        {
            var contacts = _addressRL.GetAllContacts();

            return new ResponseModelSMT<IEnumerable<AddressBookEntity>>
            {
                Success = true,
                Message = "Contacts retrieved successfully",
                Data = contacts
            };
        }

        public ResponseModelSMT<ResponseUserModel> GetContactById(int id)
        {
            var contact = _addressRL.GetContactById(id);

            if (contact == null)
            {
                return new ResponseModelSMT<ResponseUserModel>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModelSMT<ResponseUserModel>
            {
                Success = true,
                Message = "Contact retrieved successfully",
                Data = contact
            };
        }

        public ResponseModelSMT<ResponseUserModel> AddContact(ResponseUserModel user)
        {
            var addedContact = _addressRL.AddContact(user);

            return new ResponseModelSMT<ResponseUserModel>
            {
                Success = true,
                Message = "Contact added successfully",
                Data = addedContact
            };
        }

        public ResponseModelSMT<ResponseUserModel> UpdateContact(int id, ResponseUserModel user)
        {
            var updatedContact = _addressRL.UpdateContact(id, user);

            if (updatedContact == null)
            {
                return new ResponseModelSMT<ResponseUserModel>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModelSMT<ResponseUserModel>
            {
                Success = true,
                Message = "Contact updated successfully",
                Data = updatedContact
            };
        }

        public ResponseModelSMT<string> DeleteContact(int id)
        {
            var isDeleted = _addressRL.DeleteContact(id);

            if (!isDeleted)
            {
                return new ResponseModelSMT<string>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModelSMT<string>
            {
                Success = true,
                Message = "Contact deleted successfully",
                Data = $"Contact with ID {id} deleted"
            };


        }
    }
}
