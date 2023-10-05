using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.Utilities.Handlers;
using API.Utilities;
using System.Net;
using API.Repositories;
using API.DataTransferObjects.Accounts;

namespace API.Controllers;

/*
 * Account Controller adalah class untuk yang mengatur penerimaan request dan pengembalian response API.
 * Class ini terhubung dengan class Account Repository yang berfungsi untuk melakukan ORM.
 * Success dan Error Response di dalam controller ini di handle oleh ControllerBase dan Utility Class
 * terkait format Response API.
 */

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountRepository _accountRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly UniversityRepository _universityRepository;
    private readonly IEmailHandler _emailHandler;

    public AccountController(AccountRepository accountRepository, EmployeeRepository employeeRepository,
        UniversityRepository universityRepository, IEmailHandler emailHandler)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _universityRepository = universityRepository;
        _emailHandler = emailHandler;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get data collection from repository
        IEnumerable<Account> dataCollection = _accountRepository.GetAll();

        // Handling null data
        if (!dataCollection.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Message.DataNotFound
            });
        }

        // Return success response
        IEnumerable<AccountDTO> data = dataCollection.Select(item => (AccountDTO)item);
        ResponseOKHandler<IEnumerable<AccountDTO>> response = new ResponseOKHandler<IEnumerable<AccountDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check if data available
        Account? data = _accountRepository.GetByGuid(guid);

        // Handling request if data is not found
        if (data is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Message.DataNotFound
            });
        }

        // Return success response 
        AccountDTO response = (AccountDTO)data;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateAccountDTO accountDTO)
    {
        try
        {
            // Create data from request paylod
            Account toCreate = accountDTO;
            toCreate.Password = HashingHandler.HashPassword(accountDTO.Password);

            // Return success response
            Account? result = _accountRepository.Create(toCreate);
            ResponseOKHandler<AccountDTO> response = new ResponseOKHandler<AccountDTO>((AccountDTO)result);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler()
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPut]
    public IActionResult Update(AccountDTO accountDTO)
    {
        try
        {
            // Check if data available
            Account? entity = _accountRepository.GetByGuid(accountDTO.Guid);

            // Handling null data
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            // Update data
            Account toUpdate = accountDTO;
            toUpdate.CreatedDate = entity.CreatedDate;

            bool result = _accountRepository.Update(toUpdate);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Message.ErrorOnUpdatingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Message.DataUpdated);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Check if data available
            Account? data = _accountRepository.GetByGuid(guid);

            // Handling null data
            if (data is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            // Delete data
            bool result = _accountRepository.Delete(data);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Message.ErrorOnDeletingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Message.DataDeleted);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /*
     * Forgot Password adalah endpoint yang digunakan untuk request kode OTP yang akan
     * dimanfaatkan ketika mengganti password.
     */

    [HttpPost]
    [Route("forgot-password")]
    public IActionResult ForgotPassword(AccountEmailRequestDTO accountEmailRequestDTO)
    {
        try
        {
            // Cek apakah email terdaftar dalam database
            Employee? employeeData = _employeeRepository.GetByEmail(accountEmailRequestDTO.Email);

            // Handling jika data employee berdasarkan input email tidak ditemukan
            if (employeeData is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            // Update informasi OTP untuk akun yang dipilih
            Account accountData = _accountRepository.GetByGuid(employeeData.Guid);
            accountData.OTP = GenerateHandler.CreateOTP();
            accountData.IsUsed = false;
            accountData.ExpiredTime = DateTime.Now.AddMinutes(5);

            bool updateEntity = _accountRepository.Update(accountData);

            // Throw exception jika terjadi kegagalan dalam proses update di repository
            if (!updateEntity)
            {
                throw new ExceptionHandler("Failed to Update OTP");
            }

            // Kirim kode OTP ke Email dan kirim return berhasil
            string otpMessage = $"Your OTP Code : {accountData.OTP}, Valid Until : {accountData.ExpiredTime}";
            _emailHandler.Send("RESET PASSWORD", otpMessage, accountEmailRequestDTO.Email);

            return Ok(new ResponseOKHandler<string>(Message.OTPCodeHasSent));
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToUpdateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /*
     * Change Password adalah endpoint yang berfungsi untuk melakukan perubahan password.
     * Endpoint ini membutuhkan format request yang sudah dibuat dalam Account New Password DTO.
     */

    [HttpPost]
    [Route("change-password")]
    public IActionResult ChangePassword(AccountNewPasswordRequestDTO changeAccountPasswordDTO)
    {
        try
        {
            // Validasi apakah email yang dikirim terdaftar dalam database
            Employee? employeeData = _employeeRepository.GetByEmail(changeAccountPasswordDTO.Email);

            if (employeeData is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            // Ambil data akun berdasarkan GUID yang didapatkan dari proses sebelumnya
            Account? accountData = _accountRepository.GetByGuid(employeeData.Guid);

            // Validasi kesamaan kode OTP dalam request dengan di database
            if (changeAccountPasswordDTO.OTP != accountData.OTP)
            {
                return Unauthorized(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = Message.InvalidOTPCode
                });
            }

            // Validasi apakah kode OTP sudah pernah digunakan atau tidak
            if (accountData.IsUsed)
            {
                return StatusCode(StatusCodes.Status410Gone, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status410Gone,
                    Status = HttpStatusCode.Gone.ToString(),
                    Message = Message.OTPCodeAlreadyUsed
                });
            }

            // Validasi apakah kode OTP sudah kadaluarsa
            if (accountData.ExpiredTime <= DateTime.Now)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status403Forbidden,
                    Status = HttpStatusCode.Forbidden.ToString(),
                    Message = Message.OTPCodeHasExpired
                });
            }

            // Perbarui data password jika validasi OTP telah berhasil
            Account toUpdate = accountData;
            toUpdate.Password = HashingHandler.HashPassword(changeAccountPasswordDTO.NewPassword);
            toUpdate.IsUsed = true;

            bool result = _accountRepository.Update(toUpdate);

            // Throw exception jika terjadi kegagalan dalam update data di Repository
            if (!result)
            {
                throw new ExceptionHandler("Failed to Update Password");
            }

            // Return Ok jika seluruh proses berhasil
            return Ok(new ResponseOKHandler<string>("Password changed"));
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToUpdateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /*
     * Login adalah endpoint yang digunakan untuk melakukan validasi login berdasarkan
     * data yang dikirim dalam HttpRequest.
     * Format data request untuk endpoint ini diatur oleh AccountLoginRequestDTO, dimana
     * class ini juga diterapkan validasi input.
     */

    [HttpPost]
    [Route("login")]
    public IActionResult Login(AccountLoginRequestDTO accountLoginDTO)
    {
        try
        {
            // Validasi apakah email yang dikirim terdaftar atau tidak
            Employee? employee = _employeeRepository.GetByEmail(accountLoginDTO.Email);

            // Deklarasi format error response karena akan digunakan dalam beberapa skenario
            ResponseErrorHandler errorResponse = new ResponseErrorHandler
            {
                Code = StatusCodes.Status401Unauthorized,
                Status = HttpStatusCode.Unauthorized.ToString(),
                Message = Message.AccountPasswordInvalid
            };

            // Response error karena data employee dengan email yang dikirim tidak ada
            if (employee is null) return Unauthorized(errorResponse);

            // Ambil data akun berdasarkan GUID dari objek employee sebelumnya
            Account? account = _accountRepository.GetByGuid(employee.Guid);

            // Response error karena objek employee belum memiliki akun
            if (account is null) return Unauthorized(errorResponse);

            // Validasi password dari request dengan yang ada di database
            bool isVerified = HashingHandler.VerifyPassword(accountLoginDTO.Password, account.Password);

            // Response error karena password tidak sesuai 
            if (!isVerified)
            {
                return Unauthorized(errorResponse);
                ;
            }

            // Response berhasil setelah seluruh proses validasi sukses
            return Ok(new ResponseOKHandler<string>("Login success"));
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.LoginFailed,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }


    [HttpPost]
    [Route("Registration")]
    public IActionResult Registration(AccountRegistrationRequestDTO accountRegistration)
    {
        try
        {
            // Check if email has registered
            Employee? employee = _employeeRepository.GetByEmail(accountRegistration.Email);

            if (employee is not null)
            {
                return Conflict(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status409Conflict,
                    Status = HttpStatusCode.Conflict.ToString(),
                    Message = Message.EmailAlreadyRegistered
                });
            }

            // Check university data
            University? university = _universityRepository.GetByCode(accountRegistration.UniversityCode);

            if (university is null)
            {
                _universityRepository.Create(new University
                {
                    Guid = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Code = accountRegistration.UniversityCode,
                    Name = accountRegistration.UniversityName
                });
            }
            else
            {
                accountRegistration.UniversityCode = university.Code;
                accountRegistration.UniversityName = university.Name;
            }

            // Create Employee Data
            Employee newEmployee = accountRegistration;
            newEmployee.NIK = GenerateHandler.CreateNIK(_employeeRepository.GetLastNIK());

            Employee? createEmployee = _employeeRepository.Create(newEmployee);
            if (createEmployee is null) throw new ExceptionHandler(Message.FailedToCreateData);

            // Create account data
            Account? createAccount = _accountRepository.Create(new Account
            {
                Guid = createEmployee.Guid,
                CreatedDate = createEmployee.CreatedDate,
                ModifiedDate = createEmployee.ModifiedDate,
                Password = HashingHandler.HashPassword(accountRegistration.Password),
                IsDeleted = false,
                OTP = 0,
                IsUsed = true,
                ExpiredTime = DateTime.Now
            });

            if (createAccount is null) throw new ExceptionHandler(Message.FailedToCreateData);

            // Return response
            ResponseOKHandler<EmployeeDTO> response = new ResponseOKHandler<EmployeeDTO>((EmployeeDTO)createEmployee);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.LoginFailed,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}