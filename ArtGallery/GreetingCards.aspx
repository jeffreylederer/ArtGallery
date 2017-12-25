<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GreetingCards.aspx.cs" Inherits="ArtGallery.GreetingCards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1 style="font-family:Palatino, serif">Greeting Cards</h1>
        <div style="font-family:Palatino, serif; font-size:larger">
    <p>
        Over the years, I have fielded many requests for greeting cards. I have now created a selection of unique designs for: </p>
        <ul>
            <li>birthdays</li>
            <li>holidays</li>
            <li>weddings</li> 
            <li>childbirths</li> 
            <li>anniversaries</li>
            <li>bar and bat mitzvot</li>
            <li>notecards</li>
            <li>condolences</li>
        </ul>

        <p>
        A small sample of these cards appears in the window below.
        Select the <i>Buy Cards</i> button
        to view the entire gallery and order your cards.
    </p>
        </div>
        <div style='width:730px; height:180p; position:center;'>
      <object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://get.adobe.com/shockwave/' width='730' height='165'>
        <param name='allowScriptAccess' value='sameDomain'>
        <param name='allowFullScreen' value='false'>
        <param name='movie' value='http://www.greetingcarduniverse.com/widget/Widget_D.swf?aid=213063&exturl=http://www.greetingcarduniverse.com/scripts/card_widget_xml2.asp'>
        <param name='loop' value='false'>
        <param name='menu' value='false'>
        <param name='quality' value='best'>
        <param name='scale' value='noborder'>
        <param name='salign' value='lt'>
        <param name='wmode' value='transparent'>
        <param name='bgcolor' value='#ffffff'>
        <embed src='http://www.greetingcarduniverse.com/widget/Widget_D.swf?aid=213063&exturl=http://www.greetingcarduniverse.com/scripts/card_widget_xml2.asp' loop='false' menu='false' quality='best'               scale='noborder' salign='lt' wmode='transparent' bgcolor='#ffffff'               width='730' height='165' name='main' align='middle'               allowScriptAccess='sameDomain' allowFullScreen='false'               type='application/x-shockwave-flash'               pluginspage='http://www.macromedia.com/go/getflashplayer'>
            </embed>
      </object>
    </div>

   <%-- If the window is not visible, you can go  directly to my <a href="http://www.greetingcarduniverse.com/community/store.asp?store_id=7625&gcu=66935659028" target="_blank">greeting card gallery</a>.--%>
     If the window is not visible, you can go  directly to my <a href="http://www.greetingcarduniverse.com/imaginarius" target="_blank">greeting card gallery</a>.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
