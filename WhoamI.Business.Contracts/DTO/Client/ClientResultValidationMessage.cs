﻿namespace WhoamI.Business.Contracts.DTO.Client
{
    /// <summary>
    /// Validasyon Mesaji
    /// </summary>
    public class ClientResultValidationMessage
    {
        /// <summary>
        /// Alan Adi
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Uyari Mesaji
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Validasyon Mesaji
        /// </summary>
        public ClientResultValidationMessage(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
