using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForReact.Data.Dto
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid UserIdOwner { get; set; }
        public Guid UserIdCompanion { get; set; }

        public Guid DialogId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

        [ForeignKey("UserIdCompanion")]
        public virtual User UserCompanion { get; set; }
        [ForeignKey("UserIdOwner")]
        public virtual User UserOwner { get; set; }
        [ForeignKey("DialogId")]
        public virtual Dialog Dialog { get; set; }
    }
}
