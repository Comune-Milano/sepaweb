<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DivisioneManuale1.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_Plan_DivisioneManuale1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"/>
    <title>Modifica Manuale</title>
    <script type="text/javascript" language="javascript" >
        window.name="modal";
        </script>
</head>
<body>
    <form id="form1" runat="server" target ="modal">
    <div style="text-align: center">
    <div id="Contenitore" 
            style="filter:alpha(opacity=90);position: absolute; width: 100%; height: 100%; top: 0px; left: 0px; background-color: #FFFFFF; visibility: hidden;">
        <table width="100%;">
        <tr>

            <td valign="middle" align="center">
            
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            
            <asp:ImageButton ID="imgAggiorna" runat="server" 
            ImageUrl="Immagini/Aggiorna.png" 
                    onclientclick="document.getElementById('Contenitore').style.visibility = 'hidden';" />
            
        
            </td>

        </tr>
            </table>
    </div>
    </div>
    <script type="text/javascript">

        document.getElementById('Contenitore').style.visibility = 'hidden';
    </script>
    
    </form>
</body>
</html>
