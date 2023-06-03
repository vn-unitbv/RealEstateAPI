using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
	public class CommentDto
	{
		public Guid CommenterId { get; set; }
		public string CommenterName { get; set;}
		public string CommentMessage { get; set;}
		public string CommentDate { get; set;}
	}
}
