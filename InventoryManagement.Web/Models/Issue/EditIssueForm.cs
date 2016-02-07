using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using InventoryManagement.Web.Domain;
using InventoryManagement.Web.Filters;
using InventoryManagement.Web.Infrastructure.Mapping;

namespace InventoryManagement.Web.Models.Issue
{
	public class EditIssueForm : IMapFrom<Domain.Issue>
	{
		[HiddenInput]
		public int IssueID { get; set; }

		[ReadOnly(true)]
		public string CreatorUserName { get; set; }

		[Required]
		public string Subject { get; set; }

		public IssueType IssueType { get; set; }
	
		[Display(Name = "Assigned To")]
		public string AssignedToID { get; set; }
		
		[Required]
		public string Body { get; set; }
	}
}