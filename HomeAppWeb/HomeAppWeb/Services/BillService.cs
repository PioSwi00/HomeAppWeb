using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            return await _unitOfWork.Bills.GetAllAsync();
        }

        public async Task<Bill> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Bills.GetByIdAsync(id);
        }

        public async Task AddAsync(Bill bill)
        {
            await _unitOfWork.Bills.AddAsync(bill);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Bill bill)
        {
            _unitOfWork.Bills.Update(bill);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bill = await _unitOfWork.Bills.GetByIdAsync(id);
            if (bill != null)
            {
                _unitOfWork.Bills.Delete(bill);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
