using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class AdminModel
    {
        public string LoginName { set; get; }
        public string Password { set; get; }
    }
    
    public class NewsViewsModel
    {
        public int Id { set; get; }
        [Required]
        [Display(Name = "标题")]
        public string Title { set; get; }
        [Required]
        [Display(Name = "新闻内容")]
        public string NewsContent { set; get; }
        public int VisitCount { set; get; }
        public string WriteTime { set; get; }
        public string NewsTypeName { set; get; }
        [Required]
        [Display(Name = "新闻标题")]
        public int NewsTypeId { set; get; }
        public string NickName { set; get; }
        public int AdminId { set; get; }
    }

    public class PicViewsModel
    {
        public int Id { set; get; }
        [Required]
        [Display(Name = "标题")]
        public string Title { set; get; }
        [Required]
        [Display(Name = "链接地址")]
        public string Link { set; get; }
        [Required]
        [Display(Name = "上传图片")]
        public string Pic { set; get; }        
        public string PicTypeName { set; get; }
        [Required]
        [Display(Name = "图片类别")]
        public int PicTypeId { set; get; }
        public string NickName { set; get; }
        public int AdminId { set; get; }
        public string WriteTime { set; get; }
        [Required]
        [Display(Name = "排序序号")]
        public int OrderId { set; get; }
    }
}