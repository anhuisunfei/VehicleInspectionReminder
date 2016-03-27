using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleInspectionReminder.Web.App_Start
{
    /// <summary>
    /// 用户角色页面赋值
    /// </summary>
    public class UserProfileActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            bool isAdmin = false; // 是否管理员
            bool isCarOwner = false; // 车主
            bool isCarCheck = false; // 车检负责人
            bool isPolice = false; // 交警
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userIdentity = (System.Security.Claims.ClaimsIdentity)filterContext.HttpContext.User.Identity;
                var claims = userIdentity.Claims;
                var roleClaimType = userIdentity.RoleClaimType;
                var roles = claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).ToList();
                isAdmin = roles.Any(p => p.Value == "Admin");
                isCarOwner = roles.Any(p => p.Value == "CarOwner");
                isCarCheck = roles.Any(p => p.Value == "CarCheck");
                isPolice = roles.Any(p => p.Value == "TrafficPolice");
            }
            filterContext.Controller.ViewBag.isAdmin = isAdmin;
            filterContext.Controller.ViewBag.isCarOwner = isCarOwner;
            filterContext.Controller.ViewBag.isCarCheck = isCarCheck;
            filterContext.Controller.ViewBag.isPolice = isPolice;
        }
    }
}