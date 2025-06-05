using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dtos
{
    // DTO for reading/returning CardAccessGrant information
    public class CardAccessGrantDto
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public Guid DeviceId { get; set; }
        public string Remark { get; set; } = string.Empty;
        public CardBaseDto? Card { get; set; }
        public DeviceBaseDto? Device { get; set; }
    }

    // Base DTO for Card, used in CardAccessGrantDto to avoid circular references
    public class CardBaseDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty; // Added MemberName
    }

    // Base DTO for Device, used in CardAccessGrantDto to avoid circular references
    public class DeviceBaseDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string DeviceCode { get; set; } = string.Empty;
    }
    
    // Base DTO for CardAccessGrant, used in CardDto and DeviceDto to avoid circular references
    public class CardAccessGrantBaseDto
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public Guid DeviceId { get; set; }
        public string Remark { get; set; } = string.Empty;
    }

    // DTO for creating a new CardAccessGrant
    public class CreateCardAccessGrantDto
    {
        [Required]
        public Guid CardId { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [StringLength(256)]
        public string Remark { get; set; } = string.Empty;
    }

    // DTO for updating an existing CardAccessGrant
    public class UpdateCardAccessGrantDto
    {
        [Required]
        public Guid CardId { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [StringLength(256)]
        public string Remark { get; set; } = string.Empty;
    }
}
