using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dtos
{
    // DTO for reading/returning Device information
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string DeviceCode { get; set; } = string.Empty;
        public string RemoteId { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public List<CardAccessGrantBaseDto> AccessGrants { get; set; } = new List<CardAccessGrantBaseDto>();
    }

    // DTO for creating a new Device
    public class CreateDeviceDto
    {
        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string DeviceCode { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string RemoteId { get; set; } = string.Empty;

        [Required]
        [StringLength(15)] // Max length for IPv4
        // [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "Invalid IP Address")]
        public string IpAddress { get; set; } = string.Empty;
    }

    // DTO for updating an existing Device
    public class UpdateDeviceDto
    {
        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string DeviceCode { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string RemoteId { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        // [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "Invalid IP Address")]
        public string IpAddress { get; set; } = string.Empty;
    }
}
