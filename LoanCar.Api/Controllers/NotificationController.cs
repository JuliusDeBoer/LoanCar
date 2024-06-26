﻿using LoanCar.Api.Models;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("notifications")]
    [Authorize]
    public class NotificationController(LoanCarContext db) : ControllerBase
    {
        private readonly LoanCarContext db = db;

        [HttpGet]
        public IActionResult Get()
        {
            var notifications = db.Notifications.Select(n => new PublicNotificationDTO()
            {
                Id = n.Id,
                Message = n.Message,
            });

            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var notification = db.Notifications.FirstOrDefault(n => n.Id == id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(new PublicNotificationDTO()
            {
                Id = notification.Id,
                Message = notification.Message
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public IActionResult Post([FromBody] NewNotificationDTO dto)
        {
            var notification = new Notification()
            {
                Message = dto.Message
            };

            db.Notifications.Add(notification);

            db.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public IActionResult Put(Guid id, NewNotificationDTO dto)
        {
            var notification = db.Notifications.FirstOrDefault(n => n.Id == id);

            if (notification == null)
            {
                return NotFound();
            }

            notification.Message = dto.Message;

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public IActionResult Delete(Guid id)
        {
            var notification = db.Notifications.Where(n => n.Id == id);

            if (!notification.Any())
            {
                return NotFound();
            }

            notification.ExecuteDelete();

            return Ok();
        }
    }
}
