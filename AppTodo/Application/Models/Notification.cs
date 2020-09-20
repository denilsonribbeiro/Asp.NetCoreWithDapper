using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public enum TipoNotificacao
    {
        danger, info, light, success, warning
    }

    public class Notificacao
    {
        public string Mensagem { get; set; }
        public TipoNotificacao Tipo { get; set; }
    }

    public static class Notification
    {
        public static void Set(ITempDataDictionary temp, Notificacao notificacao)
        {
            temp["notify"] = Newtonsoft.Json.JsonConvert.SerializeObject(notificacao);
        }

        public static IHtmlContent Get(ITempDataDictionary temp)
        {
            if (temp["notify"] != null)
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Notificacao>((string)temp["notify"]);
                string html = $"<div class=\"alert alert-{obj.Tipo}\">{obj.Mensagem}</div>";
                return new HtmlContentBuilder().AppendHtml(html);
            }
            else
            {
                return new HtmlContentBuilder().AppendHtml("");
            }
        }

    }
}
