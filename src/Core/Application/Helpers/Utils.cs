using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Application.Helpers;

public static class Utils
{
    /// <summary>
    /// Generate Json String From Objet | Datani stringe cevirme
    /// </summary>
    /// <param name="obj">Parsing Object</param>
    /// <returns>object string</returns>
    public static string JsonString(this object obj)
    {
        return Regex.Unescape(JsonConvert.SerializeObject(obj));
    }
    /// <summary>
    /// Application Start Date 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime Zero()
    {
        return new DateTime(2203, 12, 14);
    } 
    
    
}