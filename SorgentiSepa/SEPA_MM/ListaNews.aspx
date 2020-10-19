<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaNews.aspx.vb" Inherits="ListaNews" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript">
function VisMessaggio(ID)
{
window.open('Messaggio.aspx?ID='+ID,'Messaggio','top=50,left=50,width=530,height=320');
}
</script>
<head runat="server">
    <title>Lista News</title>
</head>
<body style="font-size: 12pt">
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>
    </form>
</body>
</html>
