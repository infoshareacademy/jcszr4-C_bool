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
<p><span style=""color: #17224f;""><span style=""font-size: 32px;"">🥳Witaj!<br />Użytkownik <b>{gameTask.User.UserName}</b> właśnie zakończył stworzone przez Ciebie zadanie: <b>{gameTask.GameTask.Name}</b> i prosi o sprawdzenie przesłanego zdjęcia.</span></span></p>
<p><span style=""text-decoration: underline; font-size: 14pt;""><strong><span style=""color: #17224f;"">Zdjęcie przesłane przez użytkownika:</span></strong></span></p>
<p><span style=""color: #17224f;""><span style=""font-size: 32px;""><img src=""data:image;base64,{gameTask.Photo}"" /></span></span></p>
<p><span style=""color: #17224f;""><span style=""font-size: 32px;"">Wygląda dobrze? </span></span><span style=""color: #17224f;""><span style=""font-size: 32px;"">Kliknij na przycisk poniżej by zatwierdzić zadanie i obdarować użytkownika cebulami!</span></span></p>
<p style=""line-height: 1.5; font-size: 18px;""> 
<a href=""https://localhost:5001/GameTasks/ApproveUserSubmission?userToApproveId={gameTask.UserId}&gameTaskId={gameTask.GameTaskId}"" class=""btn btn-info"" role=""button"">Zatwierdź próbę!</a>";
        }

        public static string CheckTaskPhotoEmail(UserGameTask gameTask, int messageId)
        {
            return $@"
<p><span style=""color: #17224f;""><span style=""font-size: 32px;"">🥳Witaj!<br />Użytkownik <b>{gameTask.User.UserName}</b> właśnie zakończył stworzone przez Ciebie zadanie: <b>{gameTask.GameTask.Name}</b> i prosi o sprawdzenie przesłanego zdjęcia.</span></span></p>
<p><span style=""color: #17224f;""><span style=""font-size: 32px;"">Kliknij na link by porównać jego wynik i przyznać mu cebule!</span></span></p>
<p style=""line-height: 1.5; font-size: 18px;""> 
<a href=""https://localhost:5001/Messaging/Details/{messageId}"" class=""btn btn-info"" role=""button"">Porównaj i zatwierdź</a>";
        }
    }
}
