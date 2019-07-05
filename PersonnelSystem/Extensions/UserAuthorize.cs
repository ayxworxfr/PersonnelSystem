using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Extensions
{
    public class UserAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            string currentrole = GetRole(filterContext.HttpContext.User.Identity.Name);
            //if (Roles.Contains(currentrole))
            if (currentrole == "Admin")
            {

            }
            else if (currentrole == "User")
            {

            }
            else
            {
                filterContext.RequestContext.HttpContext.Response.Write("请先登录");
                filterContext.RequestContext.HttpContext.Response.End();
            }
            //string currentuser = filterContext.HttpContext.User.Identity.Name;
            //if (currentuser == null || currentuser == "")
            //{
            //    filterContext.RequestContext.HttpContext.Response.Write("请先登录");
            //    filterContext.RequestContext.HttpContext.Response.End();
            //}
            //else
            //{
            //    base.OnAuthorization(filterContext); 
            //}
        }

        public string GetRole(string name)
        {
            switch (name)
            {
                case "学生": return "User";
                case "老师": return "User";
                case "校长": return "Admin";
                default: return "error";
            }
        }
    }
}