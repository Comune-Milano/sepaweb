<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SpeseUnita.aspx.vb" Inherits="Contratti_SpeseUnita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Spese Unità</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
        <tr>
            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12pt; font-weight: bold">
                Calcolo Spese
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
&nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr style="font-family: ARIAL; font-size: 10pt; font-weight: bold">
                        <td>
                            &nbsp;</td>
                        <td>
                            VOCE</td>
                        <td>
                            DETTAGLI</td>
                        <td>
                            IMPORTO in Euro</td>
                    </tr>
                    <tr style="font-family: ARIAL; font-size: 10pt; font-weight: bold">
                        <td>
                            <asp:CheckBox ID="Ch300" runat="server" />
                        </td>
                        <td>
                            Spese Ascensore</td>
                        <td>
        
        
            <asp:Label ID="lblDett300" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                        <td>
        
        
            <asp:Label ID="lblImp300" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                    </tr>
                    <tr style="font-family: ARIAL; font-size: 10pt; font-weight: bold">
                        <td>
                            <asp:CheckBox ID="Ch301" runat="server" />
                        </td>
                        <td>
                            Spese Portierato</td>
                        <td>
        
        
            <asp:Label ID="lblDett301" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                        <td>
        
        
            <asp:Label ID="lblImp301" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                    </tr>
                    <tr style="font-family: ARIAL; font-size: 10pt; font-weight: bold">
                        <td>
                            <asp:CheckBox ID="Ch302" runat="server" />
                        </td>
                        <td>
                            Spese Riscaldamento</td>
                        <td>
        
        
            <asp:Label ID="lblDett302" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                        <td>
        
        
            <asp:Label ID="lblImp302" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                    </tr>
                    <tr style="font-family: ARIAL; font-size: 10pt; font-weight: bold">
                        <td>
                            <asp:CheckBox ID="Ch303" runat="server" />
                        </td>
                        <td>
                            Spese Generali</td>
                        <td>
        
        
            <asp:Label ID="lblDett303" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                        <td>
        
        
            <asp:Label ID="lblImp303" runat="server" Font-Bold="True" Font-Names="arial" 
                Font-Size="10pt"></asp:Label>
        
        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td style="font-family: arial; font-size: 8pt">
        
        
            <b>
            <br />
            Seleziona le voci di spesa da includere nel contratto. Queste voci non potranno 
            essere modificate dopo la stampa del contratto!</b></td>
        </tr>
        <tr>
        <td style="font-family: arial; font-size: 8pt">
        
        
            &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" 
                ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
        <td style="font-family: arial; font-size: 8pt; text-align: right;">
        
        

        
        
            <asp:ImageButton ID="imgSalva" runat="server" 
                ImageUrl="~/NuoveImm/Img_Applica.png" ToolTip="Salva" 
                style="cursor:pointer; height: 16px;" />
&nbsp;&nbsp;&nbsp;
        
        

        
        
            <img alt="Esci" src="../NuoveImm/Img_AnnullaVal.png" onclick="self.close();" style="cursor:pointer;"/></td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="idunita" runat="server" />
    <asp:HiddenField ID="T" runat="server" />
    <asp:HiddenField ID="CONTRATTO" runat="server" />
    </form>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
