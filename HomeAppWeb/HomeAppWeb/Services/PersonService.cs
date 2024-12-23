using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _unitOfWork.Persons.GetAllAsync();
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Persons.GetByIdAsync(id);
        }

        public async Task AddAsync(Person person)
        {
            await _unitOfWork.Persons.AddAsync(person);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _unitOfWork.Persons.Update(person);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person != null)
            {
                _unitOfWork.Persons.Delete(person);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
