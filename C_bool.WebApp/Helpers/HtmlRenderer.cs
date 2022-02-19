using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;

namespace C_bool.WebApp.Helpers
{
    public static class HtmlRenderer
    {
        public static string CheckTaskPhoto(UserGameTask gameTask)
        {
            return $@"
<p><span style=""color: #17224f;""><span style=""font-size: 32px;"">🥳Hi!<br />User {gameTask.User.UserName} just did your {gameTask.GameTask.Name} quest and they want you to approve their photo.</span></span></p>
<p><span style=""text-decoration: underline; font-size: 14pt;""><strong><span style=""color: #17224f;"">User submission:</span></strong></span></p>
<p><span style=""color: #17224f;""><span style=""font-size: 32px;""><img src=""data:image;base64,{gameTask.Photo}"" /></span></span></p>
<p><span style=""color: #17224f;""><span style=""font-size: 32px;"">Everything seems good? </span></span><span style=""color: #17224f;""><span style=""font-size: 32px;"">Click the link below to award him some points!</span></span></p>
<p style=""line-height: 1.5; font-size: 18px;""> 
<a href=""https://localhost:5001/GameTasks/ApproveUserSubmission?userToApproveId={gameTask.UserId}?gameTaskId={gameTask.GameTaskId}"" class=""btn btn-info"" role=""button"">Approve submission!</a>";
        }
    }
}
