﻿using ModelLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.UserEntity;

namespace RepositoryLayer.Service
{
    public class AddressRL : IAddressRL
    {
        private readonly AddressBookDbContext _context;

        public AddressRL(AddressBookDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AddressBookEntity> GetAllContacts()
        {
            return _context.AddressBookEntities.ToList();
        }

        public UserModel GetContactById(int id)
        {
            var entity = _context.AddressBookEntities.Find(id);
            if (entity == null) return null;

            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Contact = entity.Contact
            };
        }

        public UserModel AddContact(UserModel user)
        {
            var entity = new AddressBookEntity
            {
                Name = user.Name,
                Email = user.Email,
                Contact = user.Contact
            };

            _context.AddressBookEntities.Add(entity);
            _context.SaveChanges();

            user.Id = entity.Id;  // Set the ID after saving
            return user;
        }

        public UserModel UpdateContact(int id, UserModel user)
        {
            var entity = _context.AddressBookEntities.Find(id);
            if (entity == null) return null;

            entity.Name = user.Name;
            entity.Email = user.Email;
            entity.Contact = user.Contact;

            _context.SaveChanges();

            return user;
        }

        public bool DeleteContact(int id)
        {
            var entity = _context.AddressBookEntities.Find(id);
            if (entity == null) return false;

            _context.AddressBookEntities.Remove(entity);
            _context.SaveChanges();
            return true;
        }

    }
}
