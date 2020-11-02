using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Web.Models
{
    public class ErrorViewModel 
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
