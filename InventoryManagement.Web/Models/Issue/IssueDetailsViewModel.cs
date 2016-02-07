using System;
using InventoryManagement.Web.Domain;
using InventoryManagement.Web.Infrastructure.Mapping;

namespace InventoryManagement.Web.Models.Issue
{
	public class IssueDetailsViewModel : IMapFrom<Domain.Issue>
	{
		public int IssueID { get; set; }
		public string Subject { get; set; }
		public string CreatorUserName { get; set; }
		public string AssignedToUserName { get; set; }
		public DateTime CreatedAt { get; set; }
		public IssueType IssueType { get; set; }
		public string Body { get; set; }
	}
}