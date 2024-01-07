using Product.Messages.Events.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Repository.Entity
{
    [Table("Comments", Schema = "Product")]
    public class CommentEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
        
        public string UserName { get; set; }
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
            UserName = comment.UserName;
            CommentText = comment.CommentText;
            RegDate = comment.RegDate;
        }
    }
}
