<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES10.aspx.vb" Inherits="AMMSEPA_Controllo_DownloadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language ="javascript" type ="text/javascript" >

        function downloadFile(filePath) {
            if (document.getElementById('noClose')) {
                var a = document.getElementById('noClose').value;
                document.getElementById('noClose').value = '0'
                location.replace('' + filePath + '');
                document.getElementById('noClose').value = a

            }
            else {
                location.replace('' + filePath + '');

            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPw" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnAccedi" runat="server" Text="ACCEDI" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TblFoundFile" runat ="server" >
                        <tr>
                            <td>
                                Percorso(senza carattere &quot;\&quot; iniziale e finale)</td>
                            <td>
                                Nome del File</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPercorso" runat="server" Width="500px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileName" runat="server" Width="250px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnCerca" runat="server" Text="Cerca" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
