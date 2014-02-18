using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatTheNancy.Models
{
	public class Quip
	{
		public string Message { get; private set; }
		
		public Quip(string message)
		{
			Message = message;
		}
	}
}