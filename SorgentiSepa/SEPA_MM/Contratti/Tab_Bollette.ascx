<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Bollette.ascx.vb" Inherits="Contratti_Tab_Bollette" %>
<div style="left: 8px; width: 856px; position: absolute; top: 168px; height: 520px">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="BOLLETTE EMESSE" Width="628px"></asp:Label><br />
                <table style="border-top-width: 3px; border-left-width: 3px; border-left-color: lightgrey;
                    border-bottom-width: 3px; border-bottom-color: lightgrey; width: 100%; border-top-color: lightgrey;
                    border-right-width: 3px; border-right-color: lightgrey">
                    <tr>
                        <td style="width: 89%; height: 230px; font-family: 'Courier New'; font-size: 11px;">
                        <asp:Label Width="900px" ID="Label1" runat="server" Text="&nbsp;N°/Tipo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PERIODO DAL&nbsp;&nbsp;PERIODO AL&nbsp;&nbsp;&nbsp;IMP.EMESSO&nbsp;EMISSIONE&nbsp;&nbsp;&nbsp;&nbsp;SCADENZA&nbsp;&nbsp;&nbsp;&nbsp;IMP.PAGATO&nbsp;&nbsp;DATA PAGAM.&nbsp;&nbsp;NOTE&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                     Font-Bold="True"></asp:Label>
                              
                                                          <asp:ListBox ondblclick="apriMorosita(this);" ID="lstBollette" runat="server" 
                                Font-Names="Courier New" Font-Size="8pt"
                                Height="300px" Width="860px" TabIndex="70" ></asp:ListBox></td>
                    </tr>
                    <tr>
                        <td style="width: 89%; height: 21px;">
                            <table width="100%">
                                <tr>
                                    <td style="height: 18px">
                                        <img id="IMG1NuovaBolletta" alt="Crea nuova bolletta" src="../NuoveImm/Img_CreaNuovaBolletta.png" onclick="return IMG1_onclick()" style="cursor: pointer" /></td>
                                    <td style="height: 18px">
                                    <asp:ImageButton ID="btnModificaBolletta" 
                                            runat="server" ImageUrl="~/NuoveImm/Img_CreaModificaBolletta.png"
                                            ToolTip="Modifica una bolletta non ancora emessa" style="cursor: pointer; height: 16px;" 
                                            OnClientClick="document.getElementById('USCITA').value='1';myOpacity1.toggle();" 
                                            TabIndex="71" /></td>
                                    <td style="height: 18px">
                                        <asp:ImageButton ID="btnAnnullaBolletta" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaBolletta.png"
                                            ToolTip="Annulla una bolletta Emessa" 
                                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();" 
                                            style="cursor: pointer" TabIndex="72" /></td>
                                    <td style="height: 18px">
                                        <asp:Image ID="ImgAnteprima" runat="server" 
                                            onclick="javascript:ApriAnteprima();" style="cursor:pointer"
                                            ImageUrl="~/NuoveImm/Img_AnteprimaBolletta.png" /></td>
                                            <td style="height: 18px">
                                            <asp:Image ID="imgSollecitiEmessi" runat="server" 
                                            onclick="javascript:ApriSolleciti();" ImageUrl="~/NuoveImm/Img_SollecitiEmessi.png"
                                            ToolTip="Elenco solleciti emessi" style="cursor: pointer" 
                                            TabIndex="75" /></td>
                                    <td style="height: 18px">
                                        <asp:Image ID="ImgMavOnLine" runat="server" onclick="javascript:ApriMav();" style="cursor:pointer"
                                            ImageUrl="~/NuoveImm/Img_CreaMAV.png" ToolTip="Emette un m.a.v. tramite Banca Popolare di Sondrio. Valido solo per le bollette di deposito cauzionale." /></td>
                                                                                <td style="height: 18px">
                                                                                    <asp:Image ID="imgModulo" runat="server" onclick="javascript:ApriModulo();" style="cursor:pointer"
                                            ImageUrl="~/NuoveImm/Img_ModuloPagamento.png" ToolTip="Emette un m.a.v. tramite Banca INTESA S.PAOLO. Valido per tutte le tipologie di bollette tranne DEPOSITO CAUZIONALE." /></td>
                                </tr>
                            </table></td>
                    </tr>
                    <tr>
                        <td style="width: 89%; height: 43px;">
        <asp:Label ID="lblPreventivo" runat="server" 
        Font-Bold="False" 
        Font-Names="ARIAL" Font-Size="8pt"></asp:Label>
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<p>
    &nbsp;</p>
<div id="InserimentoBolletta" 
    
    
    
    
    
    
    
    
    
    
    style="display: block; left: 0px; width: 900px; position: absolute;
    top: 0px; height: 700px; text-align: left; z-index: 199; background-color: #c3c3bb; visibility: hidden;">
    <span style="font-family: Arial"></span>
    <br />
    <br />
    <table border="0" cellpadding="1" cellspacing="1" 
        
        
        style="z-index: 200;left: 190px; width: 61%;
        position: absolute; top: 60px; height: 480px; background-color: #FFFFFF; ">
        <tr>
            <td style="width: 404px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">BOLLETTA</span></strong></td>
        </tr>
        <tr>
            <td style="width: 404px">
                &nbsp;<table style="width: 100%">
                    <tr>
                        <td>
                            <span style="font-size: 10pt; font-family: Arial"><strong>Periodo</strong></span></td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="font-size: 10pt; font-family: Arial">Dal</span></td>
                        <td>
                            <asp:TextBox ID="txtPeriodoDa" runat="server" Font-Names="Arial" Font-Size="9pt"
                                ToolTip="Inizio periodo di riferimento (gg/mm/aaaa)" Width="71px" 
                                MaxLength="10" TabIndex="400"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPeriodoDa" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                        <td>
                            <span style="font-size: 10pt; font-family: Arial">Al</span></td>
                        <td>
                            <asp:TextBox ID="txtPeriodoAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                ToolTip="Fine periodo di riferimento (gg/mm/aaaa)" Width="71px" 
                                TabIndex="401"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPeriodoAl" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <span style="font-size: 10pt; font-family: Arial">Emissione</span></td>
                        <td>
                            <asp:TextBox ID="txtEmissione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                ToolTip="Data Emissione (gg/mm/aaaa)" Width="71px" TabIndex="402" 
                                ReadOnly="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmissione" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                        <td>
                            <span style="font-size: 10pt; font-family: Arial">Scadenza</span></td>
                        <td>
                            <asp:TextBox ID="txtScadenza" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Scadenza (gg/mm/aaaa)"
                                Width="71px" TabIndex="403"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtScadenza" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <br />
                <span style="font-size: 10pt; font-family: Arial"><strong>&nbsp;VOCI BOLLETTA</strong></span><br />
                <table style="width: 129%; height: 122px;">
                    <tr>
                        <td style="width: 3px; height: 64px;">
                            <asp:ListBox ID="lstVociBolletta" runat="server" Font-Names="Courier New" Font-Size="8pt"
                                Width="450px" Height="124px" TabIndex="404"></asp:ListBox></td>
                        <td style="height: 64px">
                            <img id="Img2" alt="Aggiungi Voce nello Schema" onclick="document.getElementById('Tab_Bollette1_txtImportoVoce').value='0,00';document.getElementById('InserimentoVoce').style.visibility='visible';"
                                src="../NuoveImm/img_Aggiungi.png" style="cursor: pointer" /><br />
                            <br />
                            <asp:ImageButton ID="img_EliminaVoce" runat="server" ImageUrl="~/NuoveImm/img_Elimina.png"
                                ToolTip="Elimina Voce dallo Schema" 
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='1';" 
                                TabIndex="405" />
                            <br /><br />
                            <asp:ImageButton ID="img_CopiaSchema" runat="server" ImageUrl="~/NuoveImm/img_CopiaSchema.png"
                                ToolTip="Copia tutte le voci dallo schema" 
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='1';" 
                                TabIndex="405" /></td>
                    </tr>
                </table>
                <br />
                <strong><span style="font-size: 10pt; font-family: Arial">&nbsp;NOTE<br />
                    &nbsp;<asp:TextBox ID="txtNote" runat="server" Height="69px" 
                    TextMode="MultiLine" Width="452px" TabIndex="406"></asp:TextBox></span></strong></td>
        </tr>
        <tr style="text-align: right";font-size: 12pt; font-family: Times New Roman">
            <td style="width: 404px; >
                            
                &nbsp;<table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='0';"
                                ToolTip="Salva" style="cursor: pointer" Visible="False" TabIndex="408" />
                            <asp:ImageButton ID="img_InserisciBolletta" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='0';"
                                ToolTip="Inserisci la nuova Bolletta" style="cursor: pointer; " 
                                TabIndex="409" />&nbsp;<asp:ImageButton ID="img_ChiudiBolletta"
                                    runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';myOpacity1.toggle();document.getElementById('Tab_Bollette1_txtAppare').value='0';"
                                    ToolTip="Esci senza inserire" style="cursor: pointer" TabIndex="410" /></td></tr></table>
            </td>
        </tr>
    </table>
    <div id="InserimentoVoce" style="border: 2px solid red; left: 289px; width: 334px; position: absolute; top: 233px;
        height: 202px; background-color: dimgray; z-index: 205;">
        <table width="100%">
            <tr>
                <td>
                    <span style="font-size: 10pt; color: #ffffff; font-family: Arial"><strong>Voce</strong></span></td>
            </tr>
            <tr>
                <td>
                <asp:DropDownList ID="cmbVoceSchema" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="317px" TabIndex="411">
                </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Arial">Importo in
                        Euro (2 cifre decimali)</span></strong></td>
            </tr>
            <tr>
                <td>
                <asp:TextBox ID="txtImportoVoce" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="74px" TabIndex="412"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtImportoVoce"
                    ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td><strong><span style="font-size: 10pt; color: #ffffff; font-family: Arial">Note (andranno in stampa i primi 30 caratteri)</span></strong></td>
            </tr>
                        <tr>
                <td><asp:TextBox ID="txtnotevoce" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="314px" TabIndex="412"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;<asp:ImageButton ID="btnInserisciVoce"
                                    runat="server" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                                    ToolTip="Inserisci voce" style="cursor: pointer" 
                        OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='1';" 
                        TabIndex="413" />
                    <img id="imgAnnullaVoce" alt="Annulla Inserimento" src="../NuoveImm/img_AnnullaVoce.png" onclick="return imgAnnullaVoce_onclick()" style="cursor: pointer" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="txtConnessione" runat="server" />
    <asp:HiddenField ID="V2" runat="server" />
    <asp:HiddenField ID="V3" runat="server" />
    <asp:HiddenField ID="txtAppare" runat="server" />
    <asp:HiddenField ID="V1" runat="server" />
    <asp:HiddenField ID="txtIdContratto" runat="server" />
    <asp:HiddenField ID="txtannullo" runat="server" />
    <asp:HiddenField ID="IdBolletta" runat="server" />
    
    <asp:Image ID="ImgSfondoSchema" runat="server" 
        ImageUrl="~/ImmDiv/SfondoDim2.jpg" 
        style="z-index: 190;position:absolute; top: 42px; left: 166px;" 
        BackColor="White"/>
    
</div>
<script type="text/javascript">

document.getElementById('InserimentoVoce').style.visibility='hidden';

function IMG1_onclick() {
    //if (document.getElementById('VIRTUALE').value == '0') {
    var oggi = new Date();
    var G = oggi.getDate();
    var M = (oggi.getMonth() + 1);
    if (G < 10) {
        var gg = "0" + oggi.getDate();
    }
    else {
        var gg = oggi.getDate();
    }

    if (M < 10) {
        var mm = "0" + (oggi.getMonth() + 1);
    }
    else {
        var mm = (oggi.getMonth() + 1);
    }
    var aa = oggi.getFullYear();
    var data = gg + "/" + mm + "/" + aa;
    
        document.getElementById('USCITA').value = '1';
        document.getElementById('Tab_Bollette1_txtPeriodoDa').value = '';
        document.getElementById('Tab_Bollette1_txtPeriodoAl').value = '';
        document.getElementById('Tab_Bollette1_txtEmissione').value = data;
        document.getElementById('Tab_Bollette1_txtScadenza').value = '';
        document.getElementById('Tab_Bollette1_txtNote').value = '';
        document.getElementById('Tab_Bollette1_lstVociBolletta').options.length = 0;
        //document.getElementById('Tab_Bollette1_IdBolletta').value = '';
        myOpacity1.toggle();
    //}
    //else {
    //    document.getElementById('USCITA').value = '1';
    //    alert('Non è possibile inserire una nuova bolletta!');
    //    document.getElementById('USCITA').value = '0';
    //}
}

function imgAnnullaVoce_onclick() {
document.getElementById('Tab_Bollette1_txtImportoVoce').value='';
document.getElementById('Tab_Bollette1_txtAppare').value='0';
document.getElementById('InserimentoVoce').style.visibility='hidden';
}

function ApriMav() {
    if (document.getElementById('txtModificato').value == '1') {
        alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di generare il MAV on Line!');
    }
    else {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            var fin;
            fin = window.open('Sondrio.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'MAV', '');
            fin.focus();
        }
    }
}

function ApriSolleciti() {
    if (document.getElementById('txtModificato').value == '1') {
        alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima visualizzare l\'anteprima!');
    }
    else {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            var fin;
            fin = window.open('ElencoSolleciti.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Solleciti', 'height=350,top=0,left=0,width=350');
            fin.focus();
        }
    }
}

function ApriAnteprima() {
    if (document.getElementById('txtModificato').value == '1') {
        alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima visualizzare l\'anteprima!');
    }
    else {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            var fin;
            fin = window.open('AnteprimaBolletta.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Anteprima' + document.getElementById('Tab_Bollette1_V3').value, 'top=0,left=0,resizable=yes,scrollbars=yes');
            fin.focus();
        }
    }
}

function ApriModulo() {
    if (document.getElementById('txtModificato').value == '1') {
        alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di generare il Modulo di Pagamento!');
    }
    else {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            var fin;
            fin = window.open('MavIntesa.aspx?X=' + document.getElementById('Tab_Bollette1_V3').value, 'Modulo', '');
            //fin = window.open('ModelloIntesa.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Modulo', '');
            fin.focus();
            
        }
    }
}
</script>