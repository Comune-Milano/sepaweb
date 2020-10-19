<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchedaEustorgio.aspx.vb"
    Inherits="ARCHIVIO_SchedaEustorgio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scheda Archivio</title>
    <style type="text/css">
        .style1
        {
            width: 180px;
        }
        .style2
        {
            width: 292px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="cmbGestore">
    <div>
        <table style="width: 100%;">
            <tr bgcolor="#990000">
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        ForeColor="White" Text="SCHEDA ARCHIVIO"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <br />
                    <asp:Label ID="lblEusto" runat="server" BackColor="#990000" Font-Bold="True" ForeColor="White"
                        Text="CODICE EUSTORGIO: "></asp:Label>
                    <asp:Label ID="lblEustorgio" runat="server" BackColor="#990000" Font-Bold="True"
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style1">
                                &nbsp; &nbsp;
                            </td>
                            <td class="style2">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblDatiContratto4" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">GESTORE</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="cmbGestore" runat="server" Width="300px" BorderStyle="Solid"
                                    BorderWidth="1px" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblDatiContratto5" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">CATEGORIA</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="cmbCategoria" runat="server" Width="300px" BorderStyle="Solid"
                                    BorderWidth="1px" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblDatiContratto6" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">SCATOLA</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtScatola" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="9" TabIndex="3" Width="96px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblErrore0" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                                    ForeColor="#CC3300">(max 9 caratteri)</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblDatiContratto7" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">CONTENITORE/FALDONE</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtFaldone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="30" Width="300px" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblDatiContratto8" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">NOTE</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtNote" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="500" Width="300px" TabIndex="5" Height="38px" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImgElimina" runat="server" 
                        ImageUrl="~/NuoveImm/Img_EliminaModello.png" 
                        onclientclick="ChiediConferma();" TabIndex="6" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImgSalva" runat="server" 
                        ImageUrl="~/NuoveImm/img_SalvaModelli.png" TabIndex="7" />
                    &nbsp;
                    <asp:ImageButton ID="ImgAnnulla" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Home.png" TabIndex="8" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        ForeColor="#CC3300" Visible="False"></asp:Label>
                    <asp:HiddenField ID="cancella" runat="server" />
                    <asp:HiddenField ID="esiste" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function RicavaEustorgio() {
            var e = document.getElementById("cmbGestore");
            var a = document.getElementById("cmbCategoria");
            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.getElementById('lblEustorgio').innerText = e.options[e.selectedIndex].value + '-' + a.options[a.selectedIndex].value + '-' + document.getElementById("txtScatola").value.toUpperCase();
            }
            else {
                document.getElementById('lblEustorgio').textContent = e.options[e.selectedIndex].value + '-' + a.options[a.selectedIndex].value + '-' + document.getElementById("txtScatola").value.toUpperCase();
            }
        }

        function ChiediConferma() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sei sicuro di volere eliminare questa scheda archivio?");
            if (chiediConferma == true) {
                document.getElementById('cancella').value = '1';
            }
            else {
                document.getElementById('cancella').value = '0';
            }
        }
    </script>
    </form>
</body>
</html>
