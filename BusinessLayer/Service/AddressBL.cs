using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL _addressRL;
        private readonly RedisCacheService _cacheService;

        public AddressBL(IAddressRL addressRL, RedisCacheService cacheService)
        {
            _addressRL = addressRL;
            _cacheService = cacheService;
        }

        // ✅ Get All Contacts with Redis Caching
        public ResponseModelSMD<IEnumerable<AddressBookEntity>> GetAllContacts()
        {
            const string cacheKey = "contacts";

            IEnumerable<AddressBookEntity> contacts;

            // ✅ Check Redis Cache First
            if (_cacheService.Exists(cacheKey))
            {
                var cachedData = _cacheService.Get(cacheKey);
                contacts = JsonConvert.DeserializeObject<IEnumerable<AddressBookEntity>>(cachedData);
                Console.WriteLine("Retrieved contacts from cache.");
            }
            else
            {
                contacts = _addressRL.GetAllContacts();

                // ✅ Cache the result in Redis
                _cacheService.Set(cacheKey, JsonConvert.SerializeObject(contacts), TimeSpan.FromMinutes(30));
                Console.WriteLine("Stored contacts in cache.");
            }

            return new ResponseModelSMD<IEnumerable<AddressBookEntity>>
            {
                Success = true,
                Message = "Contacts retrieved successfully",
                Data = contacts
            };
        }

        // ✅ Get Contact by ID with Redis Caching
        public ResponseModelSMD<ResponseUserModel> GetContactById(int id)
        {
            string cacheKey = $"contact:{id}";

            ResponseUserModel contact;

            if (_cacheService.Exists(cacheKey))
            {
                var cachedData = _cacheService.Get(cacheKey);
                contact = JsonConvert.DeserializeObject<ResponseUserModel>(cachedData);
                Console.WriteLine($"Retrieved contact ID {id} from cache.");
            }
            else
            {
                contact = _addressRL.GetContactById(id);

                if (contact == null)
                {
                    return new ResponseModelSMD<ResponseUserModel>
                    {
                        Success = false,
                        Message = $"Contact with ID {id} not found"
                    };
                }

                // ✅ Cache the contact data
                _cacheService.Set(cacheKey, JsonConvert.SerializeObject(contact), TimeSpan.FromMinutes(30));
                Console.WriteLine($"Stored contact ID {id} in cache.");
            }

            return new ResponseModelSMD<ResponseUserModel>
            {
                Success = true,
                Message = "Contact retrieved successfully",
                Data = contact
            };
        }

        // ✅ Add Contact with Cache Invalidation
        public ResponseModelSMD<ResponseUserModel> AddContact(ResponseUserModel user)
        {
            var addedContact = _addressRL.AddContact(user);

            // ✅ Invalidate cache after adding a new contact
            _cacheService.Remove("contacts");

            return new ResponseModelSMD<ResponseUserModel>
            {
                Success = true,
                Message = "Contact added successfully",
                Data = addedContact
            };
        }

        // ✅ Update Contact with Cache Invalidation
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

            // ✅ Invalidate cache after updating a contact
            string cacheKey = $"contact:{id}";
            _cacheService.Remove(cacheKey);
            _cacheService.Remove("contacts");

            return new ResponseModelSMD<ResponseUserModel>
            {
                Success = true,
                Message = "Contact updated successfully",
                Data = updatedContact
            };
        }

        // ✅ Delete Contact with Cache Invalidation
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

            // ✅ Invalidate cache after deleting a contact
            string cacheKey = $"contact:{id}";
            _cacheService.Remove(cacheKey);
            _cacheService.Remove("contacts");

            return new ResponseModelSMD<string>
            {
                Success = true,
                Message = "Contact deleted successfully",
                Data = $"Contact with ID {id} deleted"
            };
        }
    }
}
