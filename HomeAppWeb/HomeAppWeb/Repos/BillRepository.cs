using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class BillRepository : IBillRepository
    {
        private readonly DatabaseContext _context;

        public BillRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<Bill> GetByIdAsync(Guid id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public async Task AddAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
        }

        public void Update(Bill bill)
        {
            _context.Bills.Update(bill);
        }

        public void Delete(Bill bill)
        {
            _context.Bills.Remove(bill);
        }
    }
}

