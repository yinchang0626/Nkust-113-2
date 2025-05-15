using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApp.Models
{
    public class Device
    {
        public Device()
        {
            DisplayName = string.Empty;
            DeviceCode = string.Empty;
            RemoteId = string.Empty;
            IpAddress = string.Empty;
            AccessGrants = new List<CardAccessGrant>();
        }

        public Device(Guid id, string displayName, string deviceCode, string remoteId, string ipAddress)
        {
            Id = id;
            DisplayName = displayName;
            DeviceCode = deviceCode;
            RemoteId = remoteId;
            IpAddress = ipAddress;
            AccessGrants = new List<CardAccessGrant>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(64)]
        public string DeviceCode { get; set; }

        [Required]
        [StringLength(64)] 
        public string RemoteId { get; set; }

        [Required]
        [StringLength(15)]
        public string IpAddress { get; set; }

        public void SetRemoteId(string remoteId)
        {
            if (remoteId == this.RemoteId)
            {
                return;
            }

            this.RemoteId = remoteId;
        }

        public void SetDeviceCode(string deviceCode)
        {
            if (deviceCode.Length < 4)
            {
                throw new ArgumentException("Device code must be at least 4 characters");
            }

            if (!Regex.IsMatch(deviceCode, @"^\d+$"))
            {
                throw new ArgumentException("Device code must contain only numeric characters");
            }

            this.DeviceCode = deviceCode;
        }

        public void SetIpAddress(string ipAddress)
        {
            if (!Regex.IsMatch(ipAddress, @"^(\d{1,3}\.){3}\d{1,3}$"))
            {
                throw new ArgumentException("Invalid IP address format");
            }
            this.IpAddress = ipAddress;
        }

        // Navigation property
        public virtual ICollection<CardAccessGrant> AccessGrants { get; set; }
    }
}
