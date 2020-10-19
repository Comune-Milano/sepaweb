<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TrasformaContratto.aspx.vb"
    Inherits="Contratti_TrasformaContratto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trasforma Contratto</title>
    <style type="text/css">
        .font_caption
        {
            font-size: 13pt;
            font-weight: bold;
            color: #721C1F;
            font-family: Arial;
            text-align: center;
        }
        
        .colonna_domanda
        {
            width: 650px;
        }
        
        .colonna_cbx1
        {
            width: 50px;
        }
        
        .colonna_cbx2
        {
            width: 30px;
        }
        
        .stile_tabella
        {
            width: 100%;
            margin-left: 10px;
            font-family: Arial;
            font-size: 10pt;
        }
        
        .pulsante
        {
            margin-left: 60%;
            margin-top: 12%;
        }
        .bottone
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-family: Arial;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/jscript">
        window.name = "modal";
    </script>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server" target="modal">
    <table style="width: 520px;">
        <tr>
            <td class="font_caption">
                TRASFORMA CONTRATTO
            </td>
        </tr>
    </table>
    <div style="width: 520px; margin-top: 20px;">
        <table width="100%" class="stile_tabella">
            <tr>
                <td width="250px">
                    <asp:Label ID="lblTitoloPg" runat="server" Text="Num. dichiarazione"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDataDisdetta" runat="server" Text="Data di diritto pregresso (di ricalcolo)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataDisdetta" runat="server" BorderStyle="None" ReadOnly ="True" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="imgVisto" runat="server" ImageUrl="~/Contratti/Immagini/Img_Visto.png"
                        Visible="False" />
                    <asp:Label ID="lblRicalcolo" runat="server" Text="Ricalcolo dei canoni pregressi"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="imgRight" runat="server" ImageUrl="~/Contratti/Immagini/Img_Right.png"
                        Visible="False" />
                    <asp:Image ID="imgVisto2" runat="server" ImageUrl="~/Contratti/Immagini/Img_Visto.png"
                        Visible="False" />
                    <asp:Label ID="lblCrea" runat="server" Text="Creazione bolletta fine contratto" Font-Bold="True"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <%-- <tr>
                <td>
                    <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" />&nbsp
                    <asp:Label ID="lblMsgInfo" runat="server" Text="Verificare i conteggi nel tab Partite Contabili"
                        Visible="False" Font-Italic="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>--%>
        </table>
        <table style="margin-left: 10px; width: 520px;">
            <tr>
                <td>
                    <asp:TextBox ID="lblMsgData" runat="server" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Black" Width="520px" ReadOnly="true" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <asp:Label ID="lblMsgAttesa" runat="server" Text=">> Cliccare su CHIUDI e attendere l'aggiornamento della maschera del contratto <<"
                        Visible="False" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="pulsante">
        <asp:Button ID="btnStampaCanone" runat="server" CssClass="bottone" CausesValidation="False"
            ToolTip="Stampa Canone" Text="Stampa Canone" Width="110px" />
        <asp:Button ID="btnProcedi" runat="server" CssClass="bottone" CausesValidation="False"
            ToolTip="Procedi" Text="Procedi" OnClientClick="ConfermaStorno();" Width="70px" />
        <asp:Button ID="btnChiudiContr" runat="server" CssClass="bottone" CausesValidation="False"
            ToolTip="Chiudi Contratto" Text="Procedi" Width="70px" Visible="False" OnClientClick="document.getElementById('btnChiudiContr').style.display = 'none';" />
        <asp:Button ID="btnChiudiMaschera" runat="server" CssClass="bottone" CausesValidation="False"
            ToolTip="Chiudi" Text="Chiudi" Width="70px" Visible="False" OnClientClick="CloseModal(11);return false;" />
    </div>
    <asp:Panel ID="hiddenpanel" runat="server">
        <asp:HiddenField ID="idDich" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idAggregazione" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idSelezionato" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="tipo" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="dataFine" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idContratto" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="dataDisdetta" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="dataRicons" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="conferma" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idAreaEconomica" runat="server" Value="0" ClientIDMode="Static" />
    </asp:Panel>
    </form>
</body>
<script language="javascript" type="text/javascript">

    function CloseModal(returnParameter) {
        window.returnValue = returnParameter;
        window.close();
    };

    function ConfermaStorno() {
        var mesi = new Array();
        mesi[0] = "01";
        mesi[1] = "02";
        mesi[2] = "03";
        mesi[3] = "04";
        mesi[4] = "05";
        mesi[5] = "06";
        mesi[6] = "07";
        mesi[7] = "08";
        mesi[8] = "09";
        mesi[9] = "10";
        mesi[10] = "11";
        mesi[11] = "12";

        var giorni = new Array();
      
        giorni[1] = "01";
        giorni[2] = "02";
        giorni[3] = "03";
        giorni[4] = "04";
        giorni[5] = "05";
        giorni[6] = "06";
        giorni[7] = "07";
        giorni[8] = "08";
        giorni[9] = "09";
        giorni[10] = "10";
        giorni[11] = "11";
        giorni[12] = "12";
        giorni[13] = "13";
        giorni[14] = "14";
        giorni[15] = "15";
        giorni[16] = "16";
       
        var chiediConferma;
        var selezionato = false;
        var msg1 = "Attenzione, per questo Rapporto di Utenza si procederà al ricalcolo dei canoni pregressi. Continuare?";
        var msg2 = "Attenzione, in assenza di data disdetta si assume che essa coincida con la data odierna. Procedere con la trasformazione contratto?";
        var msg3 = "Attenzione, procedere con la trasformazione contratto in 431?";

        var currentdate = new Date();
        var dataodierna = new String("" + currentdate.getFullYear() + (mesi[currentdate.getMonth()]) + (giorni[currentdate.getDate()]) + "");
        if (document.getElementById('dataDisdetta').value < dataodierna && document.getElementById('dataDisdetta').value != '') {
//            document.getElementById('lblMsgData').style.display = 'none';
//            document.getElementById('lblMsgData').value = '';
            if (document.getElementById('idAreaEconomica').value == '4') {
                chiediConferma = window.confirm(msg3);
            } else {
                chiediConferma = window.confirm(msg1);
            }
            if (chiediConferma == true) {
                document.getElementById('conferma').value = '1';
                document.getElementById('btnProcedi').style.display = 'none';
            }
            else {
                document.getElementById('conferma').value = '0';
            }
        } else {

            if (document.getElementById('idAreaEconomica').value != '4') {

//            document.getElementById('lblMsgData').style.display = 'block';
//            document.getElementById('lblMsgData').value = "La data disdetta è l\'odierna perchè l\'Utente non ha prodotto documenti per il diritto pregresso";
            }

            if (document.getElementById('idAreaEconomica').value == '4') {
                chiediConferma = window.confirm(msg3);
            } else {
            chiediConferma = window.confirm(msg2);
            }
            if (chiediConferma == true) {
                document.getElementById('conferma').value = '1';
                document.getElementById('btnProcedi').style.display = 'none';
            }
            else {
                document.getElementById('conferma').value = '0';
            }
        }
    }
    if (document.getElementById('divLoading')) {
        document.getElementById('divLoading').style.visibility = 'hidden';
    }
</script>
</html>
