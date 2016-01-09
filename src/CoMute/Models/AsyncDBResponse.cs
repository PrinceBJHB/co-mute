using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
	public class AsyncDBResponse<T>
	{
		public bool IsSuccess { get; set; }

		public string Error { get; set; }

		public T Result { get; set; }

	}

	public class AsyncDBResponse
	{
		public bool IsSuccess { get; set; }

		public string Error { get; set; }


	}
}
