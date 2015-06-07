﻿using System;
using System.Net;

using Oxide.Core.Libraries.Covalence;

namespace Oxide.Game.Rust.Libraries.Covalence
{
    /// <summary>
    /// Represents the Rust server hosting the game instance
    /// </summary>
    public class RustServer : IServer
    {
        /// <summary>
        /// Gets the public-facing name of the server
        /// </summary>
        public string Name
        {
            get
            {
                return server.identity;
            }
        }

        /// <summary>
        /// Gets the public-facing IP address of the server, if known
        /// </summary>
        public IPAddress Address
        {
            get
            {
                /*local ip = Steamworks.SteamGameServer.GetPublicIP()
                ("%s.%s.%s.%s"):format(bit32.rshift(ip, 24), bit32.band(bit32.rshift(ip, 16), 0xff), bit32.band(bit32.rshift(ip, 8), 0xff), bit32.band(ip, 0xff))*/
                uint ip = Steamworks.SteamGameServer.GetPublicIP();
                if (ip == 0)
                    return null;
                else
                    return new IPAddress(ip >> 24 | ((ip & 0xff0000) >> 8) | ((ip & 0xff00) << 8) | ((ip & 0xff) << 24));
            }
        }

        /// <summary>
        /// Gets the public-facing network port of the server, if known
        /// </summary>
        public ushort Port
        {
            get
            {
                return (ushort)server.port;
            }
        }

        private ServerMgr mgr;

        /// <summary>
        /// Initialises a new instance of the RustServer class
        /// </summary>
        public RustServer()
        {
            mgr = ServerMgr.Instance;
        }

        #region Console

        /// <summary>
        /// Prints the specified message to the server console
        /// </summary>
        /// <param name="message"></param>
        public void Print(string message)
        {
            //logger.Write(LogType.Info, message != null ? message.ToString() : "null");
            UnityEngine.Debug.Log(message);
        }

        /// <summary>
        /// Runs the specified server command
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        public void RunCommand(string command, params object[] args)
        {
            ConsoleSystem.Run.Server.Normal(command, args);
        }

        #endregion
    }
}
