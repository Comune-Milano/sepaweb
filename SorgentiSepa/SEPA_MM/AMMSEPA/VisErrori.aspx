<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisErrori.aspx.vb" Inherits="AMMSEPA_VisErrori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RisultatoRicercaD</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;
        var Selezionato;
        function IMG1_onclick() {
        }
    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Elenco Errori" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                    &nbsp; <span style="font-size: 24pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="Label12" runat="server" Font-Names="ARIAL" Font-Size="12pt" Text="Label"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <div style="width: 100%; overflow: auto; height: 482px;">
                    <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                        AllowPaging="True" Font-Size="8pt" Width="100%" PageSize="28" BackColor="White"
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" GridLines="None" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="Navy" Wrap="False"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="DATA/ORA" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ORA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DESCRIZIONE" HeaderStyle-Width="90%">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" />
                    </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;</td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                            </center>
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                                    ToolTip="Home" /></center>
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                </center>
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>