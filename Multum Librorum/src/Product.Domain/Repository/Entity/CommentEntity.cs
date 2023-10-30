using Product.Messages.Events.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repository.Entity
{
    [Table("Comments", Schema = "Product")]
    public class CommentEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime RegDate { get; set; }

        [Column("BookId")]
        public Guid BookEntityId { get; set; }

        [ForeignKey("BookEntityId")]
        public virtual BookEntity BookEntity { get; set; }

        public CommentEntity()
        {
            
        }

        public CommentEntity(Comment comment)
        {
            Id = comment.Id;
            UserId = comment.UserId;
            CommentText = comment.CommentText;
            RegDate = comment.RegDate;
        }
    }
}
