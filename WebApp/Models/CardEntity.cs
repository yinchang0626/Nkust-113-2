using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Card
    {
        public Card()
        {
            DisplayName = string.Empty;
            CardNumber = string.Empty;
            MemberName = ""; 
            Remark = "";
            AccessGrants = new List<CardAccessGrant>();
        }

        public Card(Guid id, string displayName, string cardNumber, Guid memberId)
        {
            Id = id;
            DisplayName = displayName;
            CardNumber = cardNumber;
            MemberId = memberId;

            AccessGrants = new List<CardAccessGrant>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(64)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(64)]
        public string MemberName { get; set; }

        public Guid MemberId { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime? EnabledFrom { get; set; }

        public DateTime? EnabledTo { get; set; }

        [StringLength(256)]
        public string Remark { get; set; }

        public void SetEnabledDateTime(DateTime? enabledFrom, DateTime? enabledTo)
        {
            if (enabledFrom.HasValue && enabledTo.HasValue)
            {
                if (enabledFrom.Value > enabledTo.Value)
                {
                    throw new ArgumentException("EnabledFrom cannot be later than EnabledTo");
                }
            }

            if (!enabledFrom.HasValue && enabledTo.HasValue)
            {
                throw new ArgumentException("EnabledFrom is required when EnabledTo is set");
            }

            EnabledFrom = enabledFrom;
            EnabledTo = enabledTo;
        }

        public void SetMemberDisplayName(string memberName)
        {
            MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
        }

        // Navigation property
        public virtual ICollection<CardAccessGrant> AccessGrants { get; set; }
    }
}
