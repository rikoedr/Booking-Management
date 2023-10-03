﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * Account Role Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class AccountRoleRepository : AbstractRepository<AccountRole>, IGeneralRepository<AccountRole>
{
    public AccountRoleRepository(BookingManagementDbContext context) : base(context)
    {
    }
}
