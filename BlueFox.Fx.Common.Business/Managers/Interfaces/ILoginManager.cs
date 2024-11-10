//-----------------------------------------------------------------------
// <copyright file="ILoginManager.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx.Common.Managers.Interfaces
{
    /// <summary>
    /// Handles all types of login types.
    /// Encrypts all nessarcy logins.
    /// </summary>
    public interface ILoginHandler
    {
        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The Dencoded string.</returns>
        string DecodeBase64(string value);

        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The encoded string.</returns>
        string EncodeBase64(string value);

        /// <summary>
        /// Encrypts the password or any other login type of a user.
        /// </summary>
        /// <param name="password">The password given by the user.</param>
        /// <returns>An encrypted key to be stored in the database.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Checks the password.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="password">The password.</param>
        /// <returns>If the password is valid.</returns>
        (bool Verified, bool NeedsUpgrade) CheckPassword(string hash, string password);
    }
}