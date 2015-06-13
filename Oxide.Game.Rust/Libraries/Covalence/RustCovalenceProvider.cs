﻿using System;

using Oxide.Core.Libraries.Covalence;

namespace Oxide.Game.Rust.Libraries.Covalence
{
    /// <summary>
    /// Provides Covalence functionality for the game "Rust"
    /// </summary>
    public class RustCovalenceProvider : ICovalenceProvider
    {
        /// <summary>
        /// Gets the name of the game for which this provider provides
        /// </summary>
        public string GameName { get { return "Rust"; } }

        /// <summary>
        /// Gets the singleton instance of this provider
        /// </summary>
        internal static RustCovalenceProvider Instance { get; private set; }

        /// <summary>
        /// Gets the player manager
        /// </summary>
        public RustPlayerManager PlayerManager { get; private set; }

        /// <summary>
        /// Gets the command system provider
        /// </summary>
        public RustCommandSystem CommandSystem { get; private set; }

        public RustCovalenceProvider()
        {
            Instance = this;
        }

        /// <summary>
        /// Creates the game-specific server object
        /// </summary>
        /// <returns></returns>
        public IServer CreateServer()
        {
            return new RustServer();
        }

        /// <summary>
        /// Creates the game-specific player manager object
        /// </summary>
        /// <returns></returns>
        public IPlayerManager CreatePlayerManager()
        {
            PlayerManager = new RustPlayerManager();
            PlayerManager.Initialise();
            return PlayerManager;
        }

        /// <summary>
        /// Creates the game-specific command system provider object
        /// </summary>
        /// <returns></returns>
        public ICommandSystem CreateCommandSystemProvider()
        {
            return CommandSystem = new RustCommandSystem();
        }
    }
}
