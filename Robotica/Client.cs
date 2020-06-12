﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_side
{
	class Client
	{
		private static Client _instance;

		public TcpClient _tcpClient;
		public NetworkStream _networkStream;

		public static Client GetInstance()
		{
			if (_instance == null)
				_instance = new Client();
			return _instance;
		}

		private Client()
		{ 
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Connect(string _ip, int _port)
		{
			// Start connection on ip_port
			try
			{
				_tcpClient = new TcpClient(_ip, _port);
				Console.WriteLine("Starting client...");
				_networkStream = _tcpClient.GetStream();
			}
			catch
			{
				MessageBox.Show("No connection to the robot could be made, please try again later.", "No robot connection");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Send command to server
		/// </summary>
		/// <param name="_command"></param>
		/// <param name="_value"></param>
		public void Send(string _command, string _value)
		{
			string command_data = _command + ";" + _value;
			string command_data_lenght = command_data.Length.ToString();
			string command_header = command_data_lenght.PadRight(10);

			// Convert input from console to byte array
			byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(command_header + command_data); 

			try
			{
				// Try to send data to server (if failed exit program)
				_networkStream.Write(bytesToSend, 0, bytesToSend.Length); 
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// Close client
		/// </summary>
		public void Close()
		{
			_tcpClient.Close();
		}
	}
}