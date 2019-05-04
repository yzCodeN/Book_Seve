using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotText.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage ="请输入姓名")]
        public string Name { get; set; }
        [Required(ErrorMessage ="请输入邮箱")]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage ="请输入正确的邮箱格式")]
        public string Email { get; set; }
        [Required(ErrorMessage ="请输入电话号码")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="请输入true还是false")]
        public bool? WillAttend { get; set; }
    }
}