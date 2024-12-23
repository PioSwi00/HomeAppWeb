using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext _context;

        public PersonRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
        }

        public void Delete(Person person)
        {
            _context.Persons.Remove(person);
        }
    }
}

