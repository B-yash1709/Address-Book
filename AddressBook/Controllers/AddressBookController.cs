using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.UserEntity;

namespace AddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        // Service Layer Logic Already Implemented in UC2
        private readonly IAddressBL _addressBL;

        public AddressBookController(IAddressBL addressBL)
        {
            _addressBL = addressBL;
        }

        // GET: Fetch all contacts
        [HttpGet]
        public ActionResult<ResponseModelSMT<IEnumerable<AddressBookEntity>>> GetAllContacts()
        {
            var result = _addressBL.GetAllContacts();
            return Ok(result);
        }

        // GET: Fetch contact by ID
        [HttpGet("{id}")]
        public ActionResult<ResponseModelSMT<ResponseUserModel>> GetContactById(int id)
        {
            var result = _addressBL.GetContactById(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // POST: Add new contact
        [HttpPost]
        public ActionResult<ResponseModelSMT<ResponseUserModel>> AddContact([FromBody] ResponseUserModel request)
        {
            var result = _addressBL.AddContact(request);
            return result.Success ? CreatedAtAction(nameof(GetContactById), new { id = result.Data.Id }, result)
                                  : BadRequest(result);
        }

        // PUT: Update contact by ID
        [HttpPut("{id}")]
        public ActionResult<ResponseModelSMT<ResponseUserModel>> UpdateContact(int id, [FromBody] ResponseUserModel request)
        {
            var result = _addressBL.UpdateContact(id, request);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // DELETE: Delete contact by ID
        [HttpDelete("{id}")]
        public ActionResult<ResponseModelSMT<string>> DeleteContact(int id)
        {
            var result = _addressBL.DeleteContact(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

    }
}
