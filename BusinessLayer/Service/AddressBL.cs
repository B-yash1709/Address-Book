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

        public ResponseModel<IEnumerable<AddressBookEntity>> GetAllContacts()
        {
            var contacts = _addressRL.GetAllContacts();

            return new ResponseModel<IEnumerable<AddressBookEntity>>
            {
                Success = true,
                Message = "Contacts retrieved successfully",
                Data = contacts
            };
        }

        public ResponseModel<UserModel> GetContactById(int id)
        {
            var contact = _addressRL.GetContactById(id);

            if (contact == null)
            {
                return new ResponseModel<UserModel>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModel<UserModel>
            {
                Success = true,
                Message = "Contact retrieved successfully",
                Data = contact
            };
        }

        public ResponseModel<UserModel> AddContact(UserModel user)
        {
            var addedContact = _addressRL.AddContact(user);

            return new ResponseModel<UserModel>
            {
                Success = true,
                Message = "Contact added successfully",
                Data = addedContact
            };
        }

        public ResponseModel<UserModel> UpdateContact(int id, UserModel user)
        {
            var updatedContact = _addressRL.UpdateContact(id, user);

            if (updatedContact == null)
            {
                return new ResponseModel<UserModel>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModel<UserModel>
            {
                Success = true,
                Message = "Contact updated successfully",
                Data = updatedContact
            };
        }

        public ResponseModel<string> DeleteContact(int id)
        {
            var isDeleted = _addressRL.DeleteContact(id);

            if (!isDeleted)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Contact with ID {id} not found"
                };
            }

            return new ResponseModel<string>
            {
                Success = true,
                Message = "Contact deleted successfully",
                Data = $"Contact with ID {id} deleted"
            };


        }
    }
}
