﻿using PetsDate.Data.Common.Repositories;
using PetsDate.Data.Models;
using PetsDate.Web.ViewModels.Event;
using System;
using System.Threading.Tasks;

namespace PetsDate.Services.Data
{
    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;

        public EventService(IDeletableEntityRepository<Event> eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public async Task CreateAsync(CreateEventInputModel input)
        {
            var eventItem = new Event
            {
                Name = input.Name,
                Location = input.Location,
                BeginEvent = input.BeginEvent,
                EndEvent = input.EndEvent,
            };

            await this.eventsRepository.AddAsync(eventItem);
            await this.eventsRepository.SaveChangesAsync();
        }
    }
}
