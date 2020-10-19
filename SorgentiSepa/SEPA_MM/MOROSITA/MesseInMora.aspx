<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MesseInMora.aspx.vb" Inherits="MOROSITA_MesseInMora" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            font-family: Arial;font-weight: bold;color: #FFFFFF; width: 100%;height: 40px;text-align: center;
                  }
        .style3
        {
            color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;       }
        .style5
        {
            font-family: Arial;font-weight: bold;color: #000000;font-size: 8pt;width: 100%;height: 40px;text-align: center;background-color: #D9D9D9;
        }
        .style6
        {
            color: #000000; font-size: 8pt;font-family: Arial; font-weight: bold; text-align: center; height: 30px; background-color: #D9D9D9;       }
        .style7
        {
            font-family: Arial;text-align: center;font-size: 8pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
            width="100%">
            <tr style="height: 17.25pt">
                <td style="height: 45px; color: #FFFFFF; font-size: 18.0pt; font-weight: 700; font-style: normal;
                    text-decoration: none; font-family: Lucida Sans, sans-serif; text-align: center;
                    vertical-align: middle; white-space: normal; border-left: 1.0pt solid windowtext;
                    border-right-style: none; border-right-color: inherit; border-right-width: medium;
                    border-top: 1.0pt solid windowtext; border-bottom-style: none; border-bottom-color: inherit;
                    border-bottom-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;
                    background: #507CD1;">
                    Report situazione messe in mora
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:50%; text-align:left">
                                <asp:ImageButton ID="btnIndietro" runat="server" 
                                    ImageUrl="../NuoveImm/Img_NuovaRicerca.png" AlternateText="Indietro" 
                                    ToolTip="Indietro" />
                                <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="../NuoveImm/Img_Stampa_Grande.png"
                                    AlternateText="Stampa" ToolTip="Stampa" />
                            </td>
                            <td style="width:50%; text-align:right">
                                <asp:ImageButton ID="btnExcel" runat="server" Height="32px" ImageUrl="Immagini/excel.jpg"
                                    Width="32px" AlternateText="Esporta in Excel" ToolTip="Esporta in Excel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td style="height: 17.25pt; width: 100%; color: #262626; font-size: 11.0pt; font-weight: 700;
                    font-style: italic; text-decoration: none; font-family: Book Antiqua , serif;
                    text-align: left; vertical-align: bottom; white-space: normal; border-left: 1.0pt solid windowtext;
                    border-right-style: none; border-right-color: inherit; border-right-width: medium;
                    border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                    padding-right: 1px; padding-top: 1px; background: #D9D9D9;">
                    Riepilogo dei filtri di estrazione inseriti per ottenere il risultato (solo filtri
                    valorizzati)
                    <br />
                    <asp:Label ID="parametriDiRicerca" Text="" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="RiepilogoProtocolli" runat="server" Text=""></asp:Label>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
    </script>
</body>
</html>
