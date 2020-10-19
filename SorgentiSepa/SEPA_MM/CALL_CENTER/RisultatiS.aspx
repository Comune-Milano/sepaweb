<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiS.aspx.vb" Inherits="CALL_CENTER_RisultatiS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>Segnalazioni</title>
    <style type="text/css">
        #contenitore
        {
            top: 68px;
            left: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="left: 0px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
        width: 798px; position: absolute; top: 0px">
        <tr>
            <td style="width: 706px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Segnalazioni.&nbsp;
                    <asp:Label ID="lblrisu" runat="server" Text="Label"></asp:Label>
                </strong></span>
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
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Image ID="imgApri" runat="server" Style="position: absolute; top: 507px; left: 409px;
                    cursor: pointer" ImageUrl="~/CALL_CENTER/Immagini/Img_Visualizza.png" onclick="Apri();" />
                <asp:Image ID="imgEsci" runat="server" Style="position: absolute; top: 507px; left: 669px;
                    cursor: pointer; height: 20px;" ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclick="document.location.href='pagina_home.aspx';" />
                <br />
                <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                    Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px" BackColor="#FCFCFC">Nessuna Selezione</asp:TextBox>
                <br />
                <asp:Label ID="lblErrore" runat="server" Visible="False" Style="position: absolute;
                    top: 485px; left: 10px;" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    ForeColor="Red"></asp:Label>
                <br />
            </td>
        </tr>
        <asp:HiddenField ID="tipo" runat="server" />
        <tr>
            <td>
                <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                    Style="z-index: 106; left: 526px; position: absolute; top: 507px" ToolTip="Nuova Ricerca" />
            </td>
        </tr>
        <asp:HiddenField ID="TipoSegnalazione" runat="server" />
        <asp:HiddenField ID="tipo1" runat="server" />
        <asp:HiddenField ID="dal" runat="server" />
        <asp:HiddenField ID="oreda" runat="server" />
        <asp:HiddenField ID="minda" runat="server" />
        <asp:HiddenField ID="al" runat="server" />
        <asp:HiddenField ID="orea" runat="server" />
        <asp:HiddenField ID="mina" runat="server" />
        <asp:HiddenField ID="filiale" runat="server" />
        <asp:HiddenField ID="edificio" runat="server" />
        <asp:HiddenField ID="complesso" runat="server" />
        <asp:HiddenField ID="segnalante" runat="server" />
        <asp:HiddenField ID="LBLID" runat="server" />
        <asp:HiddenField ID="identificativo" runat="server" />
        <asp:HiddenField ID="origine" runat="server" />
        <asp:HiddenField ID="stato" runat="server" />
        <asp:HiddenField ID="urgenza" runat="server" Value="-1" />
        <asp:HiddenField ID="numero" runat="server" Value="-1" />
    </table>
    <div>
        <div id="contenitore" style="position: absolute; width: 770px; height: 370px; overflow: auto;
            top: 66px; left: 13px;">
            <asp:DataGrid ID="DataGridSegnalaz" runat="server" AutoGenerateColumns="False" BackColor="White"
                CellPadding="1" CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                GridLines="None" PageSize="21" Style="z-index: 105; left: 193px; top: 54px" Width="176%"
                AllowPaging="True">
                <PagerStyle Mode="NumericPages" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle ForeColor="Black" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM" HeaderText="N°" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_INT" HeaderText="TIPO INT." ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO_RICHIEDENTE" HeaderText="TELEFONO RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="OGGETTO" HeaderText="COD RU" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FILIALE" HeaderText="STRUTTURA ASSOCIATA" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOTE_C" HeaderText="NOTE DI CHIUSURA" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_ORDINE" HeaderText="DATA EMISSIONE ORDINE" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_PERICOLO_sEGNALAZIONE" HeaderText="ID_PERICOLO_sEGNALAZIONE"
                        Visible="false"></asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" />
            </asp:DataGrid>
        </div>
        <asp:HiddenField runat="server" ID="prov" Value="" />
    </div>
    <script type="text/javascript">
        var selezionato;
        function ConfermaEsci() {


            var chiediConferma
            chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            if (chiediConferma == true) {
                document.location.href = 'pagina_home.aspx';
            }

        }

        function Apri() {


            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                today = new Date();
                var Titolo = 'Segnalazione' + today.getMinutes() + today.getSeconds();

                //                    popupWindow = window.open('Segnalazione.aspx?T=' + document.getElementById('tipo').value + '&IDE=' + document.getElementById('identificativo').value + '&ID=' + document.getElementById('LBLID').value, Titolo, 'height=700,width=900');
                //                    popupWindow.focus();

                if (document.getElementById('prov').value == 'S') {
                    window.open('SegnalazioneP.aspx?PROV=S&ID=' + document.getElementById('LBLID').value + '', 'Segnalazione', 'height=700px,width=900px,resizable=yes');
                } else {
                    window.open('SegnalazioneP.aspx?ID=' + document.getElementById('LBLID').value + '', 'Segnalazione', 'height=700px,width=900px,resizable=yes');
                };



            }
            else {
                alert('Nessuna Segnalazione Selezionata!');
            }

        }

        function NuovaSegnalazione() {

            today = new Date();
            var Titolo = 'Segnalazione' + today.getMinutes() + today.getSeconds();

            popupWindow = window.open('Segnalazione.aspx?T=' + document.getElementById('tipo').value + '&IDE=' + document.getElementById('identificativo').value + '&ID=-1', Titolo, 'height=700,width=900');
            popupWindow.focus();
        }

        document.getElementById('dvvvPre').style.visibility = 'hidden';

    </script>
    <p>
        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_Grande.png"
            Style="z-index: 111; left: 15px; position: absolute; top: 507px" ToolTip="Esporta in formato XLS i risultati"
            TabIndex="4" />
    </p>
    </form>
</body>
</html>
