using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class CardAccessGrant
    {
        public CardAccessGrant()
        {
            Remark = string.Empty;
        }

        public CardAccessGrant(Guid id, Guid cardId, Guid deviceId, string remark)
        {
            Id = id;
            CardId = cardId;
            DeviceId = deviceId;
            Remark = remark;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid CardId { get; set; }

        public Guid DeviceId { get; set; }

        [StringLength(256)]
        public string Remark { get; set; }

        // Navigation properties
        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }
}
