﻿using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UsersRepository { get;  }
        IGenericRepository<FileResource> FileResourcesRepository { get; }

        void Save();
    }
}