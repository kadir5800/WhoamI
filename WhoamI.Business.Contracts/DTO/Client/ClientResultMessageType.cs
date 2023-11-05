﻿namespace WhoamI.Business.Contracts.DTO.Client
{
    /// <summary>
    /// Mesaj Tipi
    /// </summary>
    public enum ClientResultMessageType : byte
    {
        /// <summary>
        /// Bilgilendirme
        /// </summary>
        Information = 0,
        /// <summary>
        /// Hata
        /// </summary>
        Error = 1,
        /// <summary>
        /// Basarili
        /// </summary>
        Success = 2,
        /// <summary>
        /// Uyari
        /// </summary>
        Warning = 3
    }
}
