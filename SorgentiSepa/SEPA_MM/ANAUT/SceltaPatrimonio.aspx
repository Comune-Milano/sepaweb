<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaPatrimonio.aspx.vb" Inherits="ANAUT_SceltaPatrimonio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Selezionato;

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<base target="_self"/>
    <title>PATRIMONIO IMMOBILIARE</title>
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
                    <asp:Label ID="lblTitolo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#FFFFCC">
                <td class="style1" style="text-align: center; font-weight: 700">
                    <asp:Label ID="lblTitolo0" runat="server" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td width="80%">
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
								<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="CODICE UN."></asp:BoundColumn>
							    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                            </td>
                            <td width="20%">
                                <asp:ImageButton ID="btnAggiungi" runat="server" 
                                    ImageUrl="../NuoveImm/Img_Aggiungi.png" onclientclick="Assegna();" />
                                <br />
                                <br />
                                    <img id="img1" alt="" src="../NuoveImm/Img_Elimina.png" 
                                    style="cursor:pointer;" 
                                    
                                    onclick="VerificaSelezionato();"/>
                                <br />
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
    <asp:HiddenField ID="AU" runat="server" Value="" />
        <asp:ImageButton ID="btnEliminaGiorno" runat="server" 
            ImageUrl="~/loading.gif" 
            style="position:absolute; top: -100px; left: -100px;" />
    <asp:HiddenField ID="SP" runat="server" Value="" />
    <asp:HiddenField ID="H1" runat="server" Value="" />
    </div>
    
    <script type="text/javascript" language="javascript">

        function Assegna() {
            window.showModalDialog('AssegnaPatrimonio.aspx?SP=' + document.getElementById('SP').value + '&AU=' + document.getElementById('AU').value, window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        }

        function VerificaSelezionato() {

            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                SceltaFunzione('Eliminare il Complesso selezionato? Non sarà più possibile annullare questa operazione.');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare un Complesso dalla lista!');
            }
        }

        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }

        function SceltaFunzione(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaGiorno', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
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

