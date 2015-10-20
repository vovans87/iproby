<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[MvcSiteMapProvider.Web.Html.Models.SiteMapPathHelperModel,MvcSiteMapProvider]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="MvcSiteMapProvider.Web.Html.Models" %>

<% foreach (var node in Model) { %>
   
    <% if (node != Model.Last()) { %>
<div class="sitemapitem" style="color:#446cb3;font-family: Helvetica Bold; font-size: 14px;float:left;border-bottom:1px solid #446cb3;height:19px;"> <%=Html.DisplayFor(m => node)%></div>
       <div style="color:#b3adad;margin-left:15px;margin-right:15px;float:left;"> &gt; </div>
    <% } else { %>
<div class="sitemapitem" style="color:black;font-family: Helvetica Bold; font-size: 14px;float:left;"> <%=Html.DisplayFor(m => node)%></div>    

    <% } %>
<% } %>