﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Index;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;


namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IMessageService))]
    public class MessageService : IMessageService
    {

        private readonly IMessageRepository _messageRepository;
        
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
        
        public async Task<List<TextMessage>> GetMessages(string chatId)
        {
            List<TextMessage> messages = await _messageRepository.GetTextMessagesByChat(chatId);
            return messages;
        }

        public async Task<TextMessage> PostMessage(TextMessage message)
        {
            TextMessage result = await _messageRepository.PostTextMessageAsync(message);
            return result;
        }

        public async Task<TextMessage> GetMessage(string chatId, string messageId)
        {
            TextMessage message = await _messageRepository.GetTextMessageById(chatId, messageId);
            return message;
        }
    }
}