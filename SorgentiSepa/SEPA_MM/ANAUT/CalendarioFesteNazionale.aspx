<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalendarioFesteNazionale.aspx.vb" Inherits="ANAUT_CalendarioFesteNazionale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Selezionato;

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<base target="_self"/>
    <title>Calendario Nazionale delle festività</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
        #contenitore
        {
            top: 541px;
            left: 235px;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            height: 26px;
        }
        .style4
        {
            height: 26px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table style="width:100%;">
            <tr bgcolor="Maroon">
                <td class="style1" style="text-align: center; font-weight: 700">
                    CALENDARIO NAZIONALE FESTIVITA&#39;</td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="15" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" Width="100%" AllowPaging="True" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White" Height="1px"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:BoundColumn DataField="GIORNO1" HeaderText="GIORNO/MESE"></asp:BoundColumn>
								<asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                            </td>
                            <td>
                                <img id="imgMostra" alt="" src="../NuoveImm/Img_Aggiungi.png" 
                                    style="cursor:pointer" onclick="Mostra();"/><br />
                                <br />
                                <asp:ImageButton ID="imgElimina" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Elimina.png" 
                                    onclientclick="VerificaSelezionato();" />
                                    <asp:ImageButton ID="btnEliminaGiorno" runat="server" 
                ImageUrl="~/NuoveImm/Img_Elimina.png" 
                style="position:absolute; top: -100px; left: -100px;"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                                <img id="imgChiudi" alt="" src="../NuoveImm/Img_EsciCorto.png" 
                                    style="cursor:pointer;position:absolute; top: 442px; left: 490px;" 
                                    onclick="self.close();"/></td>
            </tr>
        </table>
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="H1" runat="server" Value="" />
    </div>
    <div id="contenitore" 
        
        
        style="position: absolute; width: 600px; height: 480px; visibility: hidden; background-color: #CCCCCC; top: 0px; left: 0px;">
    
        <table style="width: 100%;">
            <tr>
                <td height="100%" style="text-align: center" valign="middle" width="100%">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <table align="center" 
                        style="border: 2px solid #0000FF; width: 50%; background-color: #FFFFFF;">
                        <tr>
                            <td class="style2" style="text-align: left">
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" style="text-align: left">
                                Giorno</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbGiorno" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3" style="text-align: left">
                                Mese</td>
                            <td class="style4">
                                <asp:DropDownList ID="cmbMese" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                </td>
                        </tr>
                        <tr>
                            <td class="style3" style="text-align: left">
                                Note</td>
                            <td class="style4">
                                <asp:TextBox ID="txtNote" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Width="307px"></asp:TextBox>
                            </td>
                            <td class="style4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" style="text-align: left">
                                &nbsp;</td>
                            <td>
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="imgSalva" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Salva.png" onclientclick="Sparisci();" />
                            </td>
                            <td>
                                <img id="imgAnnulla" alt="" src="../NuoveImm/Img_Esci.png" style="cursor:pointer" onclick="Sparisci();"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    <script type="text/javascript" language="javascript">
        function Sparisci() {
            if (document.getElementById('contenitore')) {
                document.getElementById('contenitore').style.visibility = 'hidden';
            }
        }

        function Mostra() {
            if (document.getElementById('contenitore')) {
                document.getElementById('contenitore').style.visibility = 'visible';
            }
        }

        function VerificaSelezionato() {

            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
            }
            else {
                document.getElementById('H1').value = '0';

            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
        </asp:UpdatePanel>
                        <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
       </form>
</body>
</html>
