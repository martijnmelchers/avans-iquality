﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Http;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<TextMessage>> GetMessages(string chatId, int skip, int take)
        {
            List<TextMessage> messages = await _messageRepository.GetTextMessagesByChat(chatId, skip, take);
            return messages;
        }

        public async Task<TextMessage> PostMessage(TextMessage message)
        {
            message.SendDate = DateTime.Now;
            TextMessage result = await _messageRepository.PostTextMessageAsync(message);
            return result;
        }

        public async Task<TextMessage> GetMessage(string chatId, string messageId)
        {
            TextMessage message = await _messageRepository.GetTextMessageById(chatId, messageId);
            return message;
        }

        public async Task<bool> RemoveMessage(string groupName, string messageId)
        {
            return await _messageRepository.DeleteMessage(groupName, messageId);
        }

        public async Task<BotMessage> CreateBotMessage(BotMessage message, string chatId)
        {
            message.SendDate = DateTime.Now;
            message.ChatId = chatId;

            await _messageRepository.SaveAsync(message);

            return message;
        }
    }
}