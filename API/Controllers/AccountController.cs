using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.Utilities.Handlers;
using System.Net;
using API.Repositories;
using API.DataTransferObjects.Accounts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using API.Data;
using API.Utilities.Responses;
using API.Utilities;

namespace API.Controllers;

// ACCOUNT CONTROLLER IS A CLASS FOR SETTING UP ACCOUNT ENDPOINT.
[ApiController]
[Route("api/[controller]"), Authorize]
public class AccountController : ControllerBase
{
    private readonly AccountRepository _accountRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly EducationRepository _educationRepository;
    private readonly UniversityRepository _universityRepository;
    private readonly RoleRepository _roleRepository;
    private readonly AccountRoleRepository _accountRoleRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly EmployeeAccountRepository _employeeAccountRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly BookingManagementDbContext _context;

    public AccountController(AccountRepository accountRepository, EmployeeRepository employeeRepository, EducationRepository educationRepository, UniversityRepository universityRepository, RoleRepository roleRepository, AccountRoleRepository accountRoleRepository, IEmailHandler emailHandler, EmployeeAccountRepository employeeAccountRepository, ITokenHandler tokenHandler, BookingManagementDbContext context)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _emailHandler = emailHandler;
        _employeeAccountRepository = employeeAccountRepository;
        _tokenHandler = tokenHandler;
        _context = context;
    }

    [HttpGet, Authorize(Roles = "manager")]
    public IActionResult GetAll()
    {
        // Retrieve all accounts from the repository
        var accounts = _accountRepository.GetAll();

        // Handle empty accounts data
        if (!accounts.Any()) 
        { 
            return NotFound(ErrorResponses.DataNotFound()); 
        }

        // Return a success response
        var accountsDto = accounts.Select(item => (AccountDTO)item);

        return Ok(OkResponses.Success(accountsDto));
    }

    [HttpGet("{guid}"), Authorize(Roles = "manager")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check account availability with Guid
        Account? account = _accountRepository.GetByGuid(guid);

        // Handle unregistered guid accounts
        if (account is null)
        {
            return NotFound(ErrorResponses.DataNotFound());
        }

        // Return a success response
        return Ok(OkResponses.Success((AccountDTO) account));
    }

    [HttpPost, AllowAnonymous]
    public IActionResult Create(CreateAccountDTO accountDTO)
    {
        try
        {
            // Create an account object from the request payload
            Account toCreate = accountDTO;
            toCreate.Password = HashingHandler.HashPassword(accountDTO.Password);

            // Create a new account via the repository
            Account? result = _accountRepository.Create(toCreate);

            // Return a success response and a DTO object
            return Ok(OkResponses.Success((AccountDTO) result));
        }
        catch (ExceptionHandler ex)
        {           
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponses.InternalServerError(ex.Message));
        }
    }

    [HttpPut, Authorize(Roles = "user")]
    public IActionResult Update(AccountDTO accountDTO)
    {
        try
        {
            // Check account availability with guid
            Account? account = _accountRepository.GetByGuid(accountDTO.Guid);

            // Handle unregistered guid accounts
            if (account is null)
            {
                return NotFound(ErrorResponses.DataNotFound());
            }

            // Perbarui data akun melalui repositori
            Account toUpdate = accountDTO;
            toUpdate.CreatedDate = account.CreatedDate;

            bool updateAccount = _accountRepository.Update(toUpdate);

            // Update account data via repository
            if (!updateAccount)
            {
                throw new ExceptionHandler(Messages.ErrorOnUpdatingData);
            }

            // Return a success response
            return Ok(OkResponses.SuccessUpdate());
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponses.InternalServerError(ex.Message));
        }
    }

    [HttpDelete("{guid}"), Authorize(Roles = "manager")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Check account availability with guid
            Account? account = _accountRepository.GetByGuid(guid);

            // Handle unregistered account guid
            if (account is null)
            {
                return NotFound(ErrorResponses.DataNotFound());
            }

            // Delete account via repository
            bool deleteAccount = _accountRepository.Delete(account);

            // Handle failure to delete account in repository
            if (!deleteAccount)
            {
                throw new ExceptionHandler(Messages.ErrorOnDeletingData);
            }

            // Return a success response
            return Ok(OkResponses.SuccessDelete());
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponses.InternalServerError(ex.Message));
        }
    }

    
    [HttpPost, AllowAnonymous]
    [Route("forgot-password")]
    public IActionResult ForgotPassword(AccountEmailRequestDTO accountEmailRequestDTO)
    {
        try
        {
            // Check employee data availability based on Email
            Employee? employeeData = _employeeRepository.GetByEmail(accountEmailRequestDTO.Email);

            // Handle unregistered employee data
            if (employeeData is null)
            {
                return NotFound(ErrorResponses.DataNotFound());
            }

            // Update OTP information for the selected account
            Account accountData = _accountRepository.GetByGuid(employeeData.Guid);
            accountData.OTP = GenerateHandler.OTP();
            accountData.IsUsed = false;
            accountData.ExpiredTime = DateTime.Now.AddMinutes(5);

            bool updateEntity = _accountRepository.Update(accountData);

            // Handle update failures in the repository
            if (!updateEntity)
            {
                throw new ExceptionHandler("Failed to Update OTP");
            }

            // Send the OTP code to email and return success response
            string otpMessage = $"Your OTP Code : {accountData.OTP}, Valid Until : {accountData.ExpiredTime}";
            _emailHandler.Send("RESET PASSWORD", otpMessage, accountEmailRequestDTO.Email);

            return Ok(OkResponses.Success(Messages.OTPCodeHasSent));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponses.InternalServerError(ex.Message));
        }
    }


    [HttpPost]
    [Route("change-password"), AllowAnonymous]
    public IActionResult ChangePassword(AccountNewPasswordRequestDTO changeAccountPasswordDTO)
    {
        try
        {
            // Check employee data availability based on Email
            Employee? employeeData = _employeeRepository.GetByEmail(changeAccountPasswordDTO.Email);

            if (employeeData is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Messages.DataNotFound
                });
            }

            // Retrieve account data based on the GUID obtained from the previous process
            Account? accountData = _accountRepository.GetByGuid(employeeData.Guid);

            // Validate the similarity of the OTP code in the request with that in the database
            if (changeAccountPasswordDTO.OTP != accountData.OTP)
            {
                return Unauthorized(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = Messages.InvalidOTPCode
                });
            }

            // Validate whether the OTP code has been used or not
            if (accountData.IsUsed)
            {
                return StatusCode(StatusCodes.Status410Gone, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status410Gone,
                    Status = HttpStatusCode.Gone.ToString(),
                    Message = Messages.OTPCodeAlreadyUsed
                });
            }

            // Validate whether the OTP code has expired
            if (accountData.ExpiredTime <= DateTime.Now)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status403Forbidden,
                    Status = HttpStatusCode.Forbidden.ToString(),
                    Message = Messages.OTPCodeHasExpired
                });
            }

            // Update password data after passing validation
            Account toUpdate = accountData;
            toUpdate.Password = HashingHandler.HashPassword(changeAccountPasswordDTO.NewPassword);
            toUpdate.IsUsed = true;

            bool result = _accountRepository.Update(toUpdate);

            // Handle update failures in the repository
            if (!result)
            {
                throw new ExceptionHandler("Failed to Update Password");
            }

            // Return Ok if the entire process is successful
            return Ok(new ResponseOKHandler<string>("Password changed"));
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Messages.FailedToUpdateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }


    [HttpPost]
    [Route("login"), AllowAnonymous]
    public IActionResult Login(AccountLoginRequestDTO accountLoginDTO)
    {
        try
        {
            // Check employee data availability based on Email
            Employee? employee = _employeeRepository.GetByEmail(accountLoginDTO.Email);

            if (employee is null) 
            {
                return Unauthorized(ErrorResponses.AccountPasswordInvalid());
            }

            // Validate account data
            Account? account = _accountRepository.GetByGuid(employee.Guid);

            if (account is null)
            {
                return Unauthorized(ErrorResponses.AccountPasswordInvalid());
            }

            // Validate the password from the request with the one in the database
            bool isVerified = HashingHandler.VerifyPassword(accountLoginDTO.Password, account.Password);

            if (!isVerified)
            {
                return Unauthorized(ErrorResponses.AccountPasswordInvalid());
                ;
            }

            // Get Account Roles
            var accountRoles = _accountRoleRepository.GetByAccountGuid(account.Guid);


            // Create JWT Token
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Email", employee.Email));
            claims.Add(new Claim("FullName", string.Concat(employee.FirstName," ", employee.LastName)));

            // Add roles in claims
            foreach (var item in accountRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, _roleRepository.GetByGuid(item.RoleGuid).Name));
            }

            string generateToken = _tokenHandler.Generate(claims);

            // Return a success response
            return Ok(OkResponses.Success("Login success", new { Token = generateToken}));
        }
        catch (ExceptionHandler ex)
        { 
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponses.InternalServerError(ex.Message));
        }
    }


    [HttpPost, AllowAnonymous]
    [Route("registration")]
    public IActionResult Registration(CreateEmployeeAccountDTO accountRegistration)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            // Check employee data availability based on Emailk
            Employee? employee = _employeeRepository.GetByEmail(accountRegistration.Email);

            // Return a conflict response if the email is already registered
            if (employee is not null)
            {
                return Conflict(ErrorResponses.DataConflict(Messages.EmailAlreadyRegistered));
            }

            // Create new employee data
            Employee newEmployee = accountRegistration; //implicit operator 
            newEmployee.NIK = GenerateHandler.NIK(_employeeRepository.GetLastNIK());

            Employee? createEmployee = _employeeRepository.Create(newEmployee);

            if (createEmployee is null)
            {
                throw new ExceptionHandler(Messages.FailedToCreateData);
            }

            // Create new account data
            Account newAccount = accountRegistration;
            newAccount.Password = HashingHandler.HashPassword(newAccount.Password);

            Account? createAccount = _accountRepository.Create(newAccount);

            if (createAccount is null)
            {
                throw new ExceptionHandler(Messages.FailedToCreateData);
            }

            // Validate university data in the request with code
            University? university = _universityRepository.GetByCode(accountRegistration.UniversityCode);

            // Handling university data availability
            if (university is null)
            {
                University newUniversity = accountRegistration;
                University? createUniversity = _universityRepository.Create(newUniversity);

                if(createUniversity is null)
                {
                    throw new ExceptionHandler(Messages.FailedToCreateUniversity);
                }
            }
            else
            {
                // Gunakan data yang sudah terdaftar 
                accountRegistration.UniversityCode = university.Code;
                accountRegistration.UniversityName = university.Name;
            }

            // Create new education data
            Education newEducation = accountRegistration; // implicit operator
            newEducation.UniversityGuid = _universityRepository.GetByCode(accountRegistration.UniversityCode).Guid;
            Education? createEducation = _educationRepository.Create(newEducation);
            
            if(createEducation is null)
            {
                throw new ExceptionHandler(Messages.FailedToCreateEducation);
            }

            // Create a new role account (default user)
            AccountRole newAccountRole = accountRegistration;
            AccountRole? createAccountRole = _accountRoleRepository.Create(newAccountRole);

            if(createAccountRole is null)
            {
                throw new ExceptionHandler(Messages.FailedToCreateAccountRole);

            }

            // Commit
            transaction.Commit();

            // Return response
            return Ok(OkResponses.Success());
        }
        catch (ExceptionHandler ex)
        {
            transaction.Rollback();

            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponses.InternalServerError(ex.Message));
        }
    }
}