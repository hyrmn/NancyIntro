namespace WhatTheNancy.Models
{
	public class Quip
	{
		private Quip()
		{
		}

		public Quip(string message)
		{
			Message = message;
		}

		public string Message { get; set; }
	}
}