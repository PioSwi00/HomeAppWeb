using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserEvent>> GetAllAsync()
        {
            return await _unitOfWork.UserEvents.GetAllAsync();
        }

        public async Task<UserEvent> GetByIdAsync(string userId, string eventId)
        {
            return await _unitOfWork.UserEvents.GetByIdAsync(userId, eventId);
        }

        public async Task AddAsync(UserEvent userEvent)
        {
            await _unitOfWork.UserEvents.AddAsync(userEvent);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(UserEvent userEvent)
        {
            _unitOfWork.UserEvents.Update(userEvent);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(string userId, string eventId)
        {
            var userEvent = await _unitOfWork.UserEvents.GetByIdAsync(userId, eventId);
            if (userEvent != null)
            {
                _unitOfWork.UserEvents.Delete(userEvent);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
