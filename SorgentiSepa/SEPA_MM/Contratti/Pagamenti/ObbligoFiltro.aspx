<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ObbligoFiltro.aspx.vb" Inherits="Contratti_Pagamenti_ObbligoFiltro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Filtro Bollette</title>
    <link href="css/Site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="js/jsFunzioni.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 10pt;
            text-align: left;
        }
    </style>
    <script language="javascript" type="text/javascript">
        window.name = 'modal'
    </script>
</head>
<body style="background-color: #D2F0E5">
    <form id="form1" runat="server" target="modal" defaultbutton="btnFiltra">
    <table style="width: 100%;">
        <tr>
            <td style="color: #CC3300; text-align: center; font-family: Arial; font-size: 12pt">
                <strong>FILTRO BOLLETTE</strong>
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial, Helvetica, sans-serif; font-weight: 700; font-size: 9pt; text-align: center">
                ATTENZIONE!
                <br />
                Il numero di bollette eccede la memoria disponibile!<br />
                Definire un filtro per procedere.</td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td class="style1">
                            RIFERIMENTO DAL
                        </td>
                        <td class="style1">
                            <b>
                                <asp:TextBox ID="txtDataDal" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                            </b>
                        </td>
                        <td class="style1">
                            AL
                        </td>
                        <td class="style1">
                            <b>
                                <asp:TextBox ID="txtDataAl" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            EMESSIONE DAL
                        </td>
                        <td class="style1">
                            <b>
                                <asp:TextBox ID="txtEmesDal" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                            </b>
                        </td>
                        <td class="style1">
                            AL
                        </td>
                        <td class="style1">
                            <b>
                                <asp:TextBox ID="txtEmesAl" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            NUM. BOLLETTA
                        </td>
                        <td class="style1" colspan="3">
                            <b>
                                <asp:TextBox ID="txtNumBolletta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    MaxLength="10" Width="200px"></asp:TextBox>
                            </b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnFiltra" runat="server" CssClass="bottone" 
                                Text="Applica Filtro" TabIndex="-1" ToolTip="Applica il filtro" />
                        </td>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Annulla" 
                                TabIndex="-1" />
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idContratto" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    initialize();

    function initialize() {
//        $("#txtDataDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
//        $("#txtDataAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
//        $("#txtEmesDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
//        $("#txtEmesAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
    };

</script>
</html>
