using System;
using System.Collections.Generic;
using System.Text;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Services
{
    public interface IUserPostService
    {
        
    }

    public class UserPostService : IUserPostService
    {
        private readonly IRepository repository;

        public UserPostService(IRepository repository)
        {
            this.repository = repository;
        }
    }
}
