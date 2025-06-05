using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dtos
{
    // DTO for reading/returning Card information
    public class CardDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public Guid MemberId { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime? EnabledFrom { get; set; }
        public DateTime? EnabledTo { get; set; }
        public string Remark { get; set; } = string.Empty;
        public List<CardAccessGrantDto> AccessGrants { get; set; } = new List<CardAccessGrantDto>();
    }

    // DTO for creating a new Card
    public class CreateCardDto
    {
        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string MemberName { get; set; } = string.Empty;

        public Guid MemberId { get; set; }
        public bool IsDisabled { get; set; } = false;
        public DateTime? EnabledFrom { get; set; }
        public DateTime? EnabledTo { get; set; }

        [StringLength(256)]
        public string Remark { get; set; } = string.Empty;
    }

    // DTO for updating an existing Card
    public class UpdateCardDto
    {
        public Guid Id { get; set; } // Added Id property

        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string MemberName { get; set; } = string.Empty;

        public Guid MemberId { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime? EnabledFrom { get; set; }
        public DateTime? EnabledTo { get; set; }

        [StringLength(256)]
        public string Remark { get; set; } = string.Empty;
    }
}
