using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class ActionLogService : IActionLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ActionLog>> GetAllAsync()
        {
            return await _unitOfWork.ActionLogs.GetAllAsync();
        }

        public async Task<ActionLog> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.ActionLogs.GetByIdAsync(id);
        }

        public async Task AddAsync(ActionLog actionLog)
        {
            await _unitOfWork.ActionLogs.AddAsync(actionLog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(ActionLog actionLog)
        {
            _unitOfWork.ActionLogs.Update(actionLog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var actionLog = await _unitOfWork.ActionLogs.GetByIdAsync(id);
            if (actionLog != null)
            {
                _unitOfWork.ActionLogs.Delete(actionLog);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
