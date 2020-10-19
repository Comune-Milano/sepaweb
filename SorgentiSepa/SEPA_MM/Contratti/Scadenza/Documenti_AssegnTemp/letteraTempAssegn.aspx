<%@ Page Language="VB" AutoEventWireup="false" CodeFile="letteraTempAssegn.aspx.vb" Inherits="Contratti_Scadenza_Documenti_AssegnTemp_letteraTempAssegn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 <form id="form1" runat="server">

   <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('../../../NuoveImm/sfondo2.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../../NuoveImm/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
    <% Response.Flush()%>



    <div>
     <asp:HiddenField ID="dataRif" runat="server" />
         <asp:HiddenField ID="idComune" runat="server" />
          <asp:HiddenField ID="idContr" runat="server" />
    </div>


     <script language="javascript" type="text/javascript">
         document.getElementById('caric').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
