using BCrypt.Net;
using AutoMapper;
using BusinessLayer.Service;
using RepositoryLayer.Service;
using RepositoryLayer.Entity;
using ModelLayer.Model;

namespace BusinessLayer.Service
{
    public class AuthService
    {
        private readonly AuthRepository _authRepo;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;  //  Injecting AutoMapper

        public AuthService(AuthRepository authRepo, JwtService jwtService, IMapper mapper)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        //  Register Method
        public ResponseModelSMD<UserResponse> Register(UserRequest dto)
        {
            var existingUser = _authRepo.GetUserByEmail(dto.Email);
            if (existingUser != null)
            {
                return new ResponseModelSMD<UserResponse>
                {
                    Success = false,
                    Message = "Email already in use"
                };
            }

            var userEntity = _mapper.Map<UserEntity>(dto);  //  Map DTO to Entity
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var registeredUser = _authRepo.Register(userEntity);

            //  Map to UserResponse with Name and Email only
            var userResponse = _mapper.Map<UserResponse>(registeredUser);

            return new ResponseModelSMD<UserResponse>
            {
                Success = true,
                Message = "Registration successful",
                Data = userResponse
            };
        }

        //  Login Method
        public ResponseModelSMD<UserResponse> Login(LoginDTO dto)
        {
            var user = _authRepo.GetUserByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return new ResponseModelSMD<UserResponse>
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            var token = _jwtService.GenerateToken(user.Email);

            //  Map to UserResponse with Name and Email only
            var userResponse = _mapper.Map<UserResponse>(user);

            return new ResponseModelSMD<UserResponse>
            {
                Success = true,
                Message = "Login successful",
                Data = userResponse
            };
        }
    }
}
