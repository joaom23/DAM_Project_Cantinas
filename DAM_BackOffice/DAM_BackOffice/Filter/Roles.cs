using DAM_BackOffice.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Filters
{
    public class Roles : ActionFilterAttribute
    {
        public string Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (UtilizadorsController.Autenticado(context.HttpContext))
            //{
            if (context.HttpContext.Session.GetString("Role") == Role)
            {
                base.OnActionExecuting(context);
            }
            else
            {
               
                context.Result = new ViewResult { StatusCode = 401, ViewName = "SemPermissoes"};
            }
        }
    }
}
