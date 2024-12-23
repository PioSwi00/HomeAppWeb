using HomeAppWeb.Interface.Services;
using HomeAppWeb.Interfaces;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _unitOfWork.Events.GetAllAsync();
        }

        public async Task<Event> GetByIdAsync(string id)
        {
            return await _unitOfWork.Events.GetByIdAsync(id);
        }

        public async Task AddAsync(Event eventEntity)
        {
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Event eventEntity)
        {
            _unitOfWork.Events.Update(eventEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
            if (eventEntity != null)
            {
                _unitOfWork.Events.Delete(eventEntity);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

