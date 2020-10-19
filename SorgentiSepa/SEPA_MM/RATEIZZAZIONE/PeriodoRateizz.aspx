<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PeriodoRateizz.aspx.vb" Inherits="RATEIZZAZIONE_PeriodoRateizz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>


<head id="Head1" runat="server">
    <title>Emissione bollette Rateizzazione</title>
    <style type="text/css">
        #form1
        {
            width: 780px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 8pt;
            color: #0000FF;
        }
                .style2
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
        }
        
    </style>
</head>
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
                    <table style="width:100%; position:absolute; top: 20px; left: 8px;">
                        <tr>
                            <td>
                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                Periodo per il quale verranno emesse le bollette delle Rateizzazioni</span></strong></td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial; font-size: 6pt">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Periodo di bollettazione per il quale verranno emesse le bollette di 
                                rateizzazione</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;<asp:DropDownList 
                                                ID="cmbPeriodoBol" runat="server" Width="280px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style2">
                                            anno</td>
                                        <td>
                                            <asp:Label ID="lblAnno" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                Text="Label" Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="border-bottom-style: ridge; border-bottom-width: 1px; border-bottom-color: #000066">
                                            <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Font-Bold="False" style="text-align: left">Numero bollette/M.A.V. che verranno generati:</asp:Label>
                                        </td>
                                        <td style="text-align: right; border-bottom-style: ridge; border-bottom-width: 1px; border-bottom-color: #000066">
                                            <asp:Label ID="lblNumBollette" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Font-Bold="True" style="text-align: left"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom-style: ridge; border-bottom-width: 1px; border-bottom-color: #000066">
                                            <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Font-Bold="False" style="text-align: left">Rateizzazioni riferite al suddetto periodo di bollettazion:</asp:Label>
                                        </td>
                                        <td style="text-align: right; border-bottom-style: ridge; border-bottom-width: 1px; border-bottom-color: #000066">
                                            <asp:Label ID="lblNumRateizz" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Font-Bold="True" style="text-align: left"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom-style: ridge; border-bottom-width: 1px; border-bottom-color: #000066">
                                            <asp:Label ID="lbl3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Font-Bold="False" style="text-align: left">Importo TOTALE che verrà bollettato pari a</asp:Label>
                                        </td>
                                        <td style="text-align: right; border-bottom-style: ridge; border-bottom-width: 1px; border-bottom-color: #000066">
                                            <asp:Label ID="lblImpBollett" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Font-Bold="True" style="text-align: left"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align = "right" >
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;</td>
                                        <td>
                                            &nbsp;&nbsp;</td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="btnConfirm" runat="server" 
                                            ImageUrl="~/NuoveImm/Img_ConfermaGrande.png" ToolTip="Conferma"/>
                                        </td>
                                        <td style="text-align: left; vertical-align: top;">
                                            <img onclick="document.location.href='../CONTRATTI/pagina_home.aspx';" 
                                            alt="Home" src="../NuoveImm/Img_Home.png" 
                                             style="cursor:pointer;" title="Home"/>
                                         </td>
                                        <td style="text-align: right">
                                             &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
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
                    <br />
                    <br />
                    &nbsp;<br />
                    &nbsp;<br />
                    <asp:HiddenField ID="DivVisible" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 12px; position: absolute; top: 323px; height: 13px; width: 758px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
    
    </form> 
        <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
 
</body>
</html>
