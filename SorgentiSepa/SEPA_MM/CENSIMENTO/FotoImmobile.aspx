<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FotoImmobile.aspx.vb" Inherits="CENSIMENTO_FotoImmobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Dettaglio Unità Immobiliare</title>
        <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" type="text/css" media="screen"/>
    <script type="text/javascript">
    document.write('<style type="text/css">.tabber{display:none;}<\/style>');
  

    </script>

    <link href="example.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/immagini/googleMaps.gif" Style="cursor: pointer" /><br />
            <div class="tabber" style="width:80%">
        <% TabFoto()%><% TabPlanimetrie()%>
        &nbsp;</div>
        <br />
    
    </div>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 73px; position: absolute;
            top: 867px" Visible="False" Width="525px"></asp:Label>
        <span style="font-size: 10pt; font-family: Arial; text-align:left "><strong>Attenzione: </strong>Sono
            vietate la riproduzione anche parziale, la diffusione e l'utilizzo del materiale
            fotografico e planimetrico presente nella sezione diverso dalle finalità contrattuali,
            salvo autorizzazione del Comune di Milano</span><strong>&nbsp;</strong>
    </form>
</body>
</html>

