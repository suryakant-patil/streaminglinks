using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Configuration;

/// <summary>
/// Summary description for Commonfunction.
/// </summary>
namespace BLL
{
public class Commonfunction 
{
    #region ::  Private Data Members  ::
    private string strSiteURL;
    public string newssplitdesc = "";
    public string _countryname = string.Empty;

    #endregion

    #region ::  Constructors  ::
    public Commonfunction()
    {
        try
        {
            strSiteURL = Constants.siteurl;            
        }
        catch { }
        finally { }
    }
    #endregion    

    #region ::  Public Member Functions ::

    
    public static String toTitle(object input)
    {
        string strtype = ConfigurationManager.AppSettings["MachineType"].ToString();
        string output = input.ToString().Trim().Replace("\"", @"");
        output = output.ToString().Trim().Replace("´", " ");
        output = output.ToString().Trim().Replace("®", " ");
        output = output.ToString().Trim().Replace("–", "");
        output = output.ToString().Trim().Replace("’", "'");
        output = output.ToString().Trim().Replace("!", "-");
        output = output.ToString().Trim().Replace("‘", "'");
        output = output.ToString().Trim().Replace("“", "");
        output = output.ToString().Trim().Replace("”", "");
        output = output.ToString().Trim().Replace("‘", "'");
        output = output.ToString().Trim().Replace("¼", "1/4");
        output = output.ToString().Trim().Replace("€", "");
        output = output.ToString().Trim().Replace("¥", "");
        output = output.ToString().Trim().Replace("…", "...");
        output = output.ToString().Trim().Replace("'", @"");
        output = output.ToString().Trim().Replace(".", " ");
        output = output.ToString().Trim().Replace(":", " ");
        output = output.ToString().Trim().Replace(";", " ");
        output = output.ToString().Trim().Replace("?", " ");
        output = output.ToString().Trim().Replace("? ", " ");
        output = output.ToString().Trim().Replace(",", " ");
        output = output.ToString().Trim().Replace("|", " ");
        output = output.ToString().Trim().Replace("£", " ");
        output = output.ToString().Trim().Replace("$", " ");
        output = output.ToString().Trim().Replace("&", " ");
        output = output.ToString().Trim().Replace("<", " ");
        output = output.ToString().Trim().Replace(">", " ");
        output = output.ToString().Trim().Replace(", ", " ");
        output = output.ToString().Trim().Replace("/", " ");
        output = output.ToString().Trim().Replace("%", " ");
        output = output.ToString().Trim().Replace("#", " ");
        output = output.ToString().Trim().Replace(" ", "-");
        output = output.ToString().Trim().Replace("--", "-");
        output = output.ToString().Trim().Replace("+", "-");
        output = output.ToString().Trim().Replace("[", "-");
        output = output.ToString().Trim().Replace("]", "-");
        output = output.ToString().Trim().Replace("»", "-");
        output = output.ToString().Trim().Replace("@", "");
        output = output.ToString().Trim().Replace("--", "-");
        if (strtype != "local")
            output = HTMLEncodeSpecialChars(output);
        return output;

    }


    public string RemoveHTML(string strText)
    {
        string s = "";
        Regex RegEx = new Regex("<[^>]*>");
        strText = strText.Replace("<br>", Convert.ToChar(10).ToString());
        s = RegEx.Replace(strText, "");
        return s;
    }
    public static String RemoveHTMLForDesc(string strText)
    {
        string s = "";
        Regex RegEx = new Regex("<[^>]*>");
        strText = strText.Replace("\r", " ");
        strText = strText.Replace("<br>", " ");
        strText = strText.Replace("  ", " ");
        strText = strText.Replace("\n", "");
        strText = strText.Replace(System.Environment.NewLine, " ");
        s = RegEx.Replace(strText, "");
        return s;
    }
    public string splitString(string inp, string splitter, int width)
    {
        if (inp.Length <= width) return inp;
        string part1 = inp.Substring(0, width);
        string part2 = splitString(inp.Substring(width), splitter, width);
        return part1 + splitter + part2;
    }
    public string splitNewsString(string desc)
    {
        string newsvalue = "";
        string newsdesc = desc.ToString();
        string[] splitdesc = newsdesc.Split(' ');
        int i = 0;
        for (int t = 0; t < splitdesc.Length; t++)
        {
            if (splitdesc[t].Length > 50)
            {
                splitdesc[t] = splitdesc[t].Replace(splitdesc[t].Substring(0, 50), splitdesc[t].Substring(0, 50) + " <br>");
                i++;
            }
            newsvalue += splitdesc[t] + " ";
        }
        newssplitdesc = newsvalue;
        if (i > 0)
        {
            splitNewsString(newsvalue);
        }
        return newssplitdesc;
    }
    public string getdatestring(string newsdate)
    {
        string tmp = "";
        int tmp1 = 0;

        tmp1 = DateTime.Parse(newsdate).Month;
        switch (tmp1)
        {
            case 1:
                tmp += "Jan";
                break;
            case 2:
                tmp += "Feb";
                break;
            case 3:
                tmp += "Mar";
                break;
            case 4:
                tmp += "Apr";
                break;
            case 5:
                tmp += "May";
                break;
            case 6:
                tmp += "Jun";
                break;
            case 7:
                tmp += "Jul";
                break;
            case 8:
                tmp += "Aug";
                break;
            case 9:
                tmp += "Sept";
                break;
            case 10:
                tmp += "Oct";
                break;
            case 11:
                tmp += "Nov";
                break;
            case 12:
                tmp += "Dec";
                break;
        }
        tmp += " " + DateTime.Parse(newsdate).Day.ToString();
        tmp1 = DateTime.Parse(newsdate).Day;
        switch (tmp1)
        {
            case 1:
            case 21:
            case 31:
                tmp += "st ";
                break;
            case 2:
            case 22:
                tmp += "nd ";
                break;
            case 3:
            case 23:
                tmp += "rd ";
                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
                tmp += "th ";
                break;
        }
        tmp += DateTime.Parse(newsdate).Year.ToString();
        return tmp;

    }
    public string ToFileName(object input)
    {
        string output = input.ToString().Trim().Replace("'", "");
        output = output.ToString().Trim().Replace(" ", "-");
        return output;
    }
    public static string HTMLEncodeSpecialChars(string text)
    {
        string t = System.Web.HttpUtility.UrlEncode(Encoding.GetEncoding("iso-8859-8").GetBytes(text));
        return t.ToString();
    }
    public void setSession(string sessionName, string sessionValue)
    {
        System.Web.HttpContext.Current.Session[sessionName] = sessionValue;
    }
    public string getSession(string sessionName)
    {
        if (System.Web.HttpContext.Current.Session[sessionName] != null)
            return System.Web.HttpContext.Current.Session[sessionName].ToString();
        else
            return "";
    }
    public bool Write_Template(string strstring, string filename)
    {
        System.IO.StreamWriter objStreamWriter;
        try
        {
            objStreamWriter = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            objStreamWriter.Write(strstring);
            objStreamWriter.Close();

        }
        catch 
        {
            return false;
        }
        return true;

    }
    public string Read_Template(string filename)
    {
        System.IO.StreamReader objStreamReader;
        string strHTML = "";
        try
        {
            objStreamReader = new StreamReader(filename);
            strHTML = objStreamReader.ReadToEnd();
            objStreamReader.Close();
        }
        catch
        {

        }
        return strHTML;

    }
    public static String MetaSqlTitle(object input)
    {
        string output = input.ToString().Trim().Replace("'", @"''");

        output = output.ToString().Trim().Replace("\"", "\\\"");

        return output;
    }
    public static String MetaSql(object input)
    {
        string output = input.ToString().Trim().Replace("'", @"''");
        output = output.ToString().Trim().Replace("“", "");
        output = output.ToString().Trim().Replace("”", "");
        output = output.ToString().Trim().Replace("\"", "");
        return output;
    }
    public static String toSql(object input)
    {
        string output = input.ToString().Trim().Replace("'", @"");
        return output;
    }
    public static int PageSize
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"].ToString()); }
    }
    public static string RootPath
    {
        get { return System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath); }
    }
    public static string GetUniqueFileName(string fname, string dirAbsPath)
    {
        string retstr = fname;
        int i = 0;
        while (File.Exists(dirAbsPath + "/" + retstr))
        {
            i++;
            retstr = i.ToString() + "_" + fname;
        }
        return retstr;
    }
    public static string RemoveSplCharacters(string strTitle)
    {
        string replString = strTitle;
        try
        {
            replString = replString.Replace(" ", "-");
            replString = replString.Replace("/", "");
            replString = replString.Replace("'", "");
            replString = replString.Replace("+", "");
            replString = replString.Replace("&", "");
            replString = replString.Replace(".", "");
            replString = replString.Replace("!", "");
            replString = replString.Replace("%", "");
            replString = replString.Replace("(", "");
            replString = replString.Replace(")", "");
            replString = replString.Replace(":", "");
            replString = replString.Replace("[", "");
            replString = replString.Replace("]", "");
            replString = replString.Replace("]", "");
            replString = replString.Replace("Ã", "");
            replString = replString.Replace("â", "");
            replString = replString.Replace("ë", "");
            replString = replString.Replace("š", "");
            replString = replString.Replace("€", "");
            replString = replString.Replace("®", "");
            replString = replString.Replace("ƒ", "");
            replString = replString.Replace(",", "");
            replString = replString.Replace("$", "");
            replString = replString.Replace("@", "");
            replString = replString.Replace("#", "");
            replString = replString.Replace("„", "");
            replString = replString.Replace("¢", "");
            replString = replString.Replace(";", "");
            replString = replString.Replace("?", "");
            replString = replString.Replace("“", "");
            replString = replString.Replace("”", "");
            replString = replString.Replace("‘", "");
            replString = replString.Replace("’", "");
            replString = replString.Replace("™", "");
            replString = replString.Replace("--", "-");
        }
        catch
        {

        }
        return replString;
    }
    //draw smooth image thumbnail

    //draw smothimage thumbnail 
    public bool SmothImage(int iwidth, int iheight, int jwidth, int jheight, string imgpath, string albumPath)
    {
        //drawimage
        int cnt = 0;
        if (iwidth < jwidth)
            jwidth = iwidth;
        if (iheight < jheight)
            jheight = iheight;

        if (iheight > iwidth)
        {
            double xx = (iwidth * jheight) / iheight;
            jwidth = int.Parse(xx.ToString());
        }
        else
        {
            double xx = (iheight * jwidth) / iwidth;
            jheight = int.Parse(xx.ToString());
            if (jheight >= 250)
                jheight = 250;
        }
        //string ImgFilePath = img;
        string uriName = Path.GetFileName(imgpath);
        //change urname
        Bitmap SourceBitmap = null;
        System.Drawing.Image thumbnail = null;
        string fnameHRW = "TN_" + uriName;
        Bitmap SourceBitmap1 = null;
        System.Drawing.Image thumbnail1 = null;
        System.Drawing.Image thmimage = System.Drawing.Image.FromFile(imgpath);
        /*******************************************************************/
        // create image object 
        try
        {

            SourceBitmap = new Bitmap(jwidth, jheight);

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(SourceBitmap);

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, jwidth, jheight);

            gr.DrawImage(thmimage, rectDestination, 0, 0, iwidth, iheight, GraphicsUnit.Pixel);

            string filename = albumPath + fnameHRW;

            SourceBitmap.Save(filename);
            cnt++;
            SourceBitmap.Dispose();
            thmimage.Dispose();
        }
        catch
        {
            //exp.ToString(); 
        }
        finally
        {
            SourceBitmap.Dispose();
            thmimage.Dispose();

        }
        if (cnt > 0)
            return true;
        else
            return false;

    }
    /// <summary>
    /// Image file formate 04 nov 2008
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static String toImage(object input)
    {

        string output = input.ToString().Trim().Replace("\"", @"");
        output = output.ToString().Trim().Replace("’", "'");
        output = output.ToString().Trim().Replace("–", "-");
        output = output.ToString().Trim().Replace("“", "");
        output = output.ToString().Trim().Replace("”", "");
        output = output.ToString().Trim().Replace("‘", "'");
        output = output.ToString().Trim().Replace("¼", "1/4");
        output = output.ToString().Trim().Replace("€", "euro");
        output = output.ToString().Trim().Replace("…", "...");
        output = output.ToString().Trim().Replace("'", @"");
        output = output.ToString().Trim().Replace("æ", "");
        output = output.ToString().Trim().Replace("Æ", "");
        output = output.ToString().Trim().Replace("%", " ");
        output = output.ToString().Trim().Replace(":", " ");
        output = output.ToString().Trim().Replace("?", " ");
        output = output.ToString().Trim().Replace("? ", " ");
        output = output.ToString().Trim().Replace(",", " ");
        output = output.ToString().Trim().Replace("|", " ");
        output = output.ToString().Trim().Replace("£", " ");
        output = output.ToString().Trim().Replace("&", " ");
        output = output.ToString().Trim().Replace("$", " ");
        output = output.ToString().Trim().Replace("<", " ");
        output = output.ToString().Trim().Replace("#", " ");
        output = output.ToString().Trim().Replace(">", " ");
        output = output.ToString().Trim().Replace("+", " ");
        output = output.ToString().Trim().Replace(",", " ");
        output = output.ToString().Trim().Replace(" ", "-");
        output = output.ToString().Trim().Replace("--", "-");
        output = output.ToString().Trim().Replace("__", "-");
        return output;
    }
      

   
    public static string GetDateForImageToSave()
    {

        return string.Format("{0}/{1}/", DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("MMMdd"));
    }

    public static void RefreshCache(string siteurl)
    {

        System.Uri objURI;
        System.Net.WebRequest objWebRequest;
        System.Net.WebResponse objWebResponse;
        System.IO.Stream objStream;
        System.IO.StreamReader objStreamReader;

        string strHTML = "";
        try
        {
            objURI = new Uri(siteurl);
            objWebRequest = HttpWebRequest.Create(objURI);
            objWebResponse = objWebRequest.GetResponse();
            objStream = objWebResponse.GetResponseStream();
            objStreamReader = new StreamReader(objStream);
            strHTML = objStreamReader.ReadToEnd();

            objURI = null;

        }
        catch (Exception ex)
        {
            ex.ToString();
            //CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Client, ":GetWebPageAsString:", ex);
        }

    }

    #region:For Calendar:    
    public static void IncludeCalendarCSS(CommonLib.ProjectSection curSection)
    {
        try
        {
            string strVirtualRootPath = "";
            if (curSection == CommonLib.ProjectSection.Admin) { strVirtualRootPath = CommonLib.Constants.AdminURL; }
            else { strVirtualRootPath = CommonLib.Constants.SiteURL; }

            CommonLib.CurrentPage.LinkCSS(strVirtualRootPath + "Css/Calendar.css");


        }
        catch (System.Exception ex) { CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "", ex); }
        finally { }
    }
    public static void IncludeGoodiesCalendarCSSAndJS(CommonLib.ProjectSection curSection)
    {
        try
        {
            string strVirtualRootPath = "";
            if (curSection == CommonLib.ProjectSection.Admin) { strVirtualRootPath = CommonLib.Constants.AdminURL; }
            else { strVirtualRootPath = CommonLib.Constants.SiteURL; }

            CommonLib.CurrentPage.IncludeScript(strVirtualRootPath + "dhtmlgoodies_calendar/dhtmlgoodies_calendar.js?random=20060118");
            CommonLib.CurrentPage.LinkCSS(strVirtualRootPath + "dhtmlgoodies_calendar/dhtmlgoodies_calendar.css?random=20060118");

        }
        catch (System.Exception ex) { CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "", ex); }
        finally { }
    }
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public static string GetImageServerUrl(string imagename, string sitefolder, string imagefolder)
    {
        /*if (imagename.StartsWith("TN"))
            imagename = imagename.Substring(2);*/

        return string.Format("<img src='{0}{1}/{2}{3}' border='0'/>", ConfigurationManager.AppSettings["ImageServerUrl"].ToString(), sitefolder, imagefolder, imagename);
    }
    public static string GetMainBackendImageServerUrl(string imagename, string sitefolder, string imagefolder)
    {
        /*if (imagename.StartsWith("TN"))
            imagename = imagename.Substring(2);*/

        return string.Format("<img src='{0}{1}/{2}{3}' border='0'/>", ConfigurationManager.AppSettings["MainAdminURL"].ToString(), "NewImages", imagefolder, imagename);
    }
    public static string GetImageAdminUrl(string imagename, string sitefolder, string imagefolder)
    {
        /*if (imagename.StartsWith("TN"))
            imagename = imagename.Substring(2);*/

        return string.Format("<img src='{0}/{1}/{2}{3}' border='0'/>", ConfigurationManager.AppSettings["SiteURL"].ToString(), "NewImages/" + sitefolder, imagefolder, imagename);
    }
    #endregion

    #endregion


     
}
}
