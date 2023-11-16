


using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Album.Models {

    [Table("Article")]
    public class Article {
        [Key]
        public int ID {set;get;}


        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [DisplayName("Tên bài viết")]
        [StringLength (50,MinimumLength=3, ErrorMessage="Phải dài 3 đến 50 ký tự")]
        public string Title {set;get;} = "";

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [DataType(DataType.Date)]
        [DisplayName("Ngày tạo bài viết")]
        public DateTime CreateTime {set;get;} 


        [DisplayName("Nội dung của bài viết")]
        public string? Content {set;get;}
    }
}