using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Abstract;
using WebSite.Helper;
using WebSite.Models;
using System.ComponentModel;
using WebSite.Model;

namespace WebSite.Controllers
{
    [MyAuthorize]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        #region 通用函数
        public ActionResult Get_NewsType()
        {
            var lservice = UnitOfWork.NewsTypes;
            return Json(lservice.Select(e => e.NewsTypeName).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_PicType()
        {
            var lservice = UnitOfWork.PicTypes;
            return Json(lservice.Select(e => e.PicTypeName).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Admin()
        {
            var lservice = UnitOfWork.Admins;
            return Json(lservice.Select(e => e.NickName).Distinct(), JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> getNewsType()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var lnewstype = UnitOfWork.NewsTypes.ToList();
            foreach (var item in lnewstype)
            {
                SelectListItem obj = new SelectListItem();
                obj.Text = item.NewsTypeName;
                obj.Value = item.Id.ToString();
                list.Add(obj);
            }
            return list;
        }

        private List<SelectListItem> getPicType()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var lpictype = UnitOfWork.PicTypes.ToList();
            foreach (var item in lpictype)
            {
                SelectListItem obj = new SelectListItem();
                obj.Text = item.PicTypeName;
                obj.Value = item.Id.ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion        

        #region 新闻管理模块       
        public ActionResult NewsList()
        {
            return View();
        }               

        public ActionResult News_Read([DataSourceRequest]DataSourceRequest request)
        {            
            var lnews = UnitOfWork.NewsInfos;

            //filter
            if (request.Filters.Count() > 0)
            {
                foreach (var item in request.Filters)
                {
                    string type = item.GetType().ToString();
                    if (type == "Kendo.Mvc.FilterDescriptor")
                    {
                        var model = ((Kendo.Mvc.FilterDescriptor)item);
                        if (model.Member == "Title")
                        {
                            lnews = (IQueryable<NewsInfo>)lnews.Where(x => x.Title == model.Value.ToString());
                        }
                        else if (model.Member == "NewsTypeName")
                        {
                            lnews = (IQueryable<NewsInfo>)lnews.Where(x => x.NewsType.NewsTypeName == model.Value.ToString());
                        }
                        else if (model.Member == "NickName")
                        {
                            lnews = (IQueryable<NewsInfo>)lnews.Where(x => x.Admin.NickName == model.Value.ToString());
                        }
                    }
                    else
                    {
                        var model = ((Kendo.Mvc.CompositeFilterDescriptor)item);
                        foreach (var i in model.FilterDescriptors)
                        {
                            var cur = ((Kendo.Mvc.FilterDescriptor)i);
                            if (cur.Member == "Title")
                            {
                                lnews = (IQueryable<NewsInfo>)lnews.Where(x => x.Title == cur.Value.ToString());
                            }
                            else if (cur.Member == "NewsTypeName")
                            {
                                lnews = (IQueryable<NewsInfo>)lnews.Where(x => x.NewsType.NewsTypeName == Convert.ToString(cur.Value));
                            }
                            else if (cur.Member == "NickName")
                            {
                                lnews = (IQueryable<NewsInfo>)lnews.Where(x => x.Admin.NickName == Convert.ToString(cur.Value));
                            }
                        }
                    }
                }
            }

            int nTotalCount = lnews.Count();

            //Apply sorting
            if (request.Sorts.Count() == 0)
            {
                request.Sorts.Add(new SortDescriptor("Id", ListSortDirection.Descending));
            }
            foreach (SortDescriptor sortDescriptor in request.Sorts)
            {
                if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                {
                    switch (sortDescriptor.Member)
                    {
                        case "Id":
                            lnews = lnews.OrderBy(x => x.Id);
                            break;
                        case "VisitCount":
                            lnews = lnews.OrderBy(x => x.VisitCount);
                            break;
                    }
                }
                else
                {
                    switch (sortDescriptor.Member)
                    {
                        case "Id":
                            lnews = lnews.OrderByDescending(x => x.Id);
                            break;
                        case "VisitCount":
                            lnews = lnews.OrderByDescending(x => x.VisitCount);
                            break;
                    }
                }
            }

            List<NewsViewsModel> list = new List<NewsViewsModel>();

            lnews = lnews.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            foreach (var item in lnews)
            {
                NewsViewsModel model = new NewsViewsModel();
                model.Id = item.Id;
                model.AdminId = item.Admin.Id;
                model.NewsContent = item.NewsContent;
                model.NewsTypeId = item.NewsTypeId;
                model.NewsTypeName = item.NewsType.NewsTypeName;
                model.NickName = item.Admin.NickName;
                model.Title = item.Title;
                model.VisitCount = item.VisitCount;
                model.WriteTime = item.WriteTime.ToString();
                list.Add(model);
            }

            var result = new DataSourceResult()
            {
                Data = list,
                Total = nTotalCount
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewsAddOrUpdate(int? id)
        {
            NewsViewsModel model = new NewsViewsModel();
            ViewBag.NewsTypeList = getNewsType();
            if (id.HasValue)
            {                
                var obj = UnitOfWork.NewsInfos.Where(x => x.Id == id.Value).FirstOrDefault();
                if(obj != null)
                {
                    model.Title = obj.Title;
                    model.NewsTypeId = obj.NewsTypeId;
                    model.NewsContent = obj.NewsContent;
                }                
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult NewsAddOrUpdate(NewsViewsModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var obj = UnitOfWork.NewsInfos.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Title = model.Title;
                        obj.NewsTypeId = model.NewsTypeId;
                        obj.NewsContent = model.NewsContent;
                        obj.AdminId = MySession.UserID;
                        obj.WriteTime = DateTime.Now;
                        UnitOfWork.SaveChanges();
                    }
                }
                else
                {
                    NewsInfo obj = new NewsInfo();
                    obj.Title = model.Title;
                    obj.NewsTypeId = model.NewsTypeId;
                    obj.NewsContent = model.NewsContent;
                    obj.AdminId = MySession.UserID;
                    obj.WriteTime = DateTime.Now;
                    UnitOfWork.Add(obj);
                    UnitOfWork.SaveChanges();
                }
                return RedirectToAction("NewsList");
            }
            ModelState.AddModelError("Title","请将资料填写完整");
            ViewBag.NewsTypeList = getNewsType();
            return View(model);
        }

        public ActionResult NewsDelete([DataSourceRequest] DataSourceRequest request, NewsViewsModel model)
        {
            if (model != null)
            {
                var obj = UnitOfWork.NewsInfos.Where(x=>x.Id == model.Id).FirstOrDefault();
                if (obj != null)
                {
                    UnitOfWork.Remove(obj);
                    UnitOfWork.SaveChanges();
                }
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region 图片管理模块
        public ActionResult PicList()
        {
            return View();
        }

        public ActionResult Read_PicList([DataSourceRequest]DataSourceRequest request)
        {
            var lnews = UnitOfWork.PicInfos;

            //filter
            if (request.Filters.Count() > 0)
            {
                foreach (var item in request.Filters)
                {
                    string type = item.GetType().ToString();
                    if (type == "Kendo.Mvc.FilterDescriptor")
                    {
                        var model = ((Kendo.Mvc.FilterDescriptor)item);
                        if (model.Member == "Title")
                        {
                            lnews = (IQueryable<PicInfo>)lnews.Where(x => x.Title == model.Value.ToString());
                        }
                        else if (model.Member == "PicTypeName")
                        {
                            lnews = (IQueryable<PicInfo>)lnews.Where(x => x.PicType.PicTypeName == model.Value.ToString());
                        }
                        else if (model.Member == "NickName")
                        {
                            lnews = (IQueryable<PicInfo>)lnews.Where(x => x.Admin.NickName == model.Value.ToString());
                        }
                    }
                    else
                    {
                        var model = ((Kendo.Mvc.CompositeFilterDescriptor)item);
                        foreach (var i in model.FilterDescriptors)
                        {
                            var cur = ((Kendo.Mvc.FilterDescriptor)i);
                            if (cur.Member == "Title")
                            {
                                lnews = (IQueryable<PicInfo>)lnews.Where(x => x.Title == cur.Value.ToString());
                            }
                            else if (cur.Member == "PicTypeName")
                            {
                                lnews = (IQueryable<PicInfo>)lnews.Where(x => x.PicType.PicTypeName == Convert.ToString(cur.Value));
                            }
                            else if (cur.Member == "NickName")
                            {
                                lnews = (IQueryable<PicInfo>)lnews.Where(x => x.Admin.NickName == Convert.ToString(cur.Value));
                            }
                        }
                    }
                }
            }

            int nTotalCount = lnews.Count();

            //Apply sorting
            if (request.Sorts.Count() == 0)
            {
                request.Sorts.Add(new SortDescriptor("Id", ListSortDirection.Descending));
            }
            foreach (SortDescriptor sortDescriptor in request.Sorts)
            {
                if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                {
                    switch (sortDescriptor.Member)
                    {
                        case "Id":
                            lnews = lnews.OrderBy(x => x.Id);
                            break;
                        case "VisitCount":
                            lnews = lnews.OrderBy(x => x.OrderId);
                            break;
                    }
                }
                else
                {
                    switch (sortDescriptor.Member)
                    {
                        case "Id":
                            lnews = lnews.OrderByDescending(x => x.Id);
                            break;
                        case "VisitCount":
                            lnews = lnews.OrderByDescending(x => x.OrderId);
                            break;
                    }
                }
            }

            List<PicViewsModel> list = new List<PicViewsModel>();

            lnews = lnews.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            foreach (var item in lnews)
            {
                PicViewsModel model = new PicViewsModel();
                model.Id = item.Id;
                model.AdminId = item.Admin.Id;
                model.Pic = item.Pic;
                model.Link = item.Link;
                model.PicTypeId = item.PicTypeId;
                model.PicTypeName = item.PicType.PicTypeName;
                model.NickName = item.Admin.NickName;
                model.Title = item.Title;
                model.OrderId = item.OrderId;
                model.WriteTime = item.WriteTime.ToString();
                list.Add(model);
            }

            var result = new DataSourceResult()
            {
                Data = list,
                Total = nTotalCount
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PicAddOrUpdate(int? id)
        {
            PicViewsModel model = new PicViewsModel();
            if (id.HasValue)
            {
                var obj = UnitOfWork.PicInfos.Where(x=>x.Id == id.Value).FirstOrDefault();
                if (obj != null)
                {
                    model.Id = obj.Id;
                    model.Title = obj.Title;
                    model.Pic = obj.Pic;
                    model.PicTypeId = obj.PicTypeId;
                    model.Link = obj.Link;
                    model.OrderId = obj.OrderId;
                }
            }
            ViewBag.PicTypeList = getPicType();
            return View(model);
        }

        [HttpPost]
        public ActionResult PicAddOrUpdate(PicViewsModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var obj = UnitOfWork.PicInfos.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Link = model.Link;
                        obj.Pic = "/Images/Upload/" + model.Pic;
                        obj.PicTypeId = model.PicTypeId;
                        obj.WriteTime = DateTime.Now;
                        obj.AdminId = MySession.UserID;
                        obj.Title = model.Title;
                        obj.OrderId = model.OrderId;
                        UnitOfWork.SaveChanges();
                    }
                }
                else
                {
                    PicInfo obj = new PicInfo();
                    obj.Link = model.Link;
                    obj.Pic = "/Images/Upload/" + model.Pic;
                    obj.PicTypeId = model.PicTypeId;
                    obj.WriteTime = DateTime.Now;
                    obj.AdminId = MySession.UserID;
                    obj.Title = model.Title;
                    obj.OrderId = model.OrderId;
                    UnitOfWork.Add(obj);
                    UnitOfWork.SaveChanges();
                }
                return RedirectToAction("PicList");
            }
            else
            {
                ModelState.AddModelError("Title", "请将资料填写完整");
            }            
            ViewBag.PicTypeList = getPicType();
            return View(model);
        }

        public ActionResult PicDelete([DataSourceRequest] DataSourceRequest request, PicViewsModel model)
        {
            if (model != null)
            {
                var obj = UnitOfWork.PicInfos.Where(x => x.Id == model.Id).FirstOrDefault();
                if (obj != null)
                {
                    UnitOfWork.Remove(obj);
                    UnitOfWork.SaveChanges();
                }
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}
