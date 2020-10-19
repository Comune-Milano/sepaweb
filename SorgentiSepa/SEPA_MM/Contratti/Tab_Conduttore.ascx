<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Conduttore.ascx.vb"
    Inherits="Contratti_Tab_Conduttore" %>
<style type="text/css">
    #ImgChiudiOspiti
    {
        width: 80px;
        height: 16px;
    }
</style>
&nbsp;
<div style="left: 6px; width: 1130px; position: absolute; top: 168px; height: 520px">
    <table style="border-color: lightgrey; border-width: 3px; width: 850px;" id="aaaa">
        <tr>
            <td width="700px">
                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="ELENCO INTESTATARI" Width="151px"></asp:Label>
                -
                <asp:Label ID="lblSIPO" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#000000" Text=">>VERIFICA CON ANAGRAFE POPOLAZIONE<<" Width="250px"
                    Style="cursor: pointer"></asp:Label>
            </td>
            <td width="150px">
            </td>
        </tr>
        <tr>
            <td width="900px">
                &nbsp;<asp:Label 
                    ID="Label6" runat="server" Font-Bold="False" Font-Names="Courier New"
                    Font-Size="8pt" ForeColor="Black" Text="Nominativo    &nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CF/P.IVA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TIPO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DATA INIZIO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    Width="700px"></asp:Label><br />
                <asp:ListBox ID="lstIntestatari" runat="server" Font-Names="Courier New" Font-Size="8pt"
                    Width="1010px" Height="95px" TabIndex="26"></asp:ListBox>
            </td>
            <td width="150" style="height: 86px">
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnAggiungiCond" runat="server" ImageUrl="~/NuoveImm/img_Aggiungi.png"
                                ToolTip="Aggiungi Intestatario" OnClientClick="document.getElementById('USCITA').value='1';ApriAccessoAnagrafica();"
                                TabIndex="27" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgEliminaCond" runat="server" ImageUrl="~/NuoveImm/img_Elimina.png"
                                ToolTip="Elimina Conduttore" OnClientClick="document.getElementById('USCITA').value='1';"
                                TabIndex="28" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="Img_DiventaComp" runat="server" ImageUrl="~/NuoveImm/Img_Componente.png"
                                ToolTip="Diventa Componente" CausesValidation="False" OnClientClick="document.getElementById('USCITA').value='1';"
                                TabIndex="29" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="700px">
                &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="ELENCO COMPONENTI" Width="229px" Height="16px"></asp:Label>
            </td>
            <td width="150px" style="height: 21px">
            </td>
        </tr>
        <tr>
            <td width="700px">
                &nbsp;<asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Courier New"
                    Font-Size="8pt" ForeColor="Black" Text="Nominativo  &nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CF/P.IVA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TIPO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DATA INIZIO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    Width="700px"></asp:Label><br />
                <asp:ListBox ID="lstComponenti" runat="server" Font-Names="Courier New" Font-Size="8pt"
                    Width="1010px" Height="105px" TabIndex="30"></asp:ListBox>
            </td>
            <td width="150px" style="height: 78px">
                <table width="100%">
                    <tr>
                        <td style="height: 14px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnAggiungiComp" runat="server" ImageUrl="~/NuoveImm/img_Aggiungi.png"
                                ToolTip="Aggiungi Componente" CausesValidation="False" OnClientClick="document.getElementById('USCITA').value='1';InserisciComponente();"
                                TabIndex="31" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="img_EliminaComp" runat="server" ImageUrl="~/NuoveImm/img_Elimina.png"
                                ToolTip="Elimina Componente" OnClientClick="document.getElementById('USCITA').value='1';"
                                TabIndex="32" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgDiventaINT" runat="server" ImageUrl="~/NuoveImm/Img_Intestatario.png"
                                ToolTip="Diventa Intestatario" CausesValidation="False" OnClientClick="document.getElementById('USCITA').value='1';"
                                TabIndex="33" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="700px">
                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="ELENCO OSPITI / COABITANTI" Width="200px"></asp:Label>
            </td>
            <td width="150px">
            </td>
        </tr>
        <tr>
            <td width="700px">
                &nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Courier New"
                    Font-Size="8pt" ForeColor="Black" Text="Nominativo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;C.Fiscale&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Data Inizio&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Data Fine&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tipologia"
                    Width="800px"></asp:Label>
                <asp:ListBox ID="lstOspiti" runat="server" Font-Names="Courier New" Font-Size="8pt"
                    Width="1010px" Height="66px" TabIndex="34"></asp:ListBox>
            </td>
            <td width="150px" style="height: 91px">
                <table width="100%">
                    <tr>
                        <td style="height: 14px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="700px">
            </td>
            <td width="150px">
            </td>
        </tr>
        <tr>
            <td>
                <div id="elencoDomande" style="overflow: auto; height: 109px; position: relative;
                    width: 1010px; left: 0px; top: 0px;">
                    <asp:Label ID="lblElencoComponenti" runat="server" Font-Names="arial" Font-Size="8pt"
                        Width="100%"></asp:Label></div>
            </td>
        </tr>
    </table>
</div>
<div id="InserimentoOspiti" style="left: 0px; width: 900px; position: absolute; top: 0px;
    height: 700px; text-align: left; display: block; background-color: #c3c3bb; visibility: hidden;">
    <br />
    <br />
    <table cellpadding="1" cellspacing="1" style="width: 48%; left: 243px; position: absolute;
        top: 214px; background-color: #FFFFFF; z-index: 200;" border="0">
        <tr>
            <td style="width: 80px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">Gestione</span></strong>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">Ospiti</span></strong>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; height: 19px; text-align: left">
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 80px; height: 19px; text-align: left">
                <span style="font-size: 10pt; font-family: Arial">Nominativo</span>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:TextBox ID="txtNominativo" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="252px" MaxLength="50" ToolTip="Cognome e Nome dell'ospite" TabIndex="600"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; height: 19px; text-align: left; font-family: Arial; font-size: 10pt;">
                Cod. Fiscale
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:TextBox ID="txtCodiceFiscale" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="168px" MaxLength="16" ToolTip="Codice Fiscale dell'ospite" TabIndex="601"></asp:TextBox>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td style="width: 80px; height: 19px; text-align: left">
                <span style="font-size: 10pt; font-family: Arial">Dal</span>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:TextBox ID="txtDal" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Inizio periodo ospitalità"
                    Width="75px" Style="margin-right: 4px" MaxLength="10" TabIndex="602"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDal"
                    Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial" Font-Size="8pt"
                    TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px; font-family: Arial; font-size: 10pt;">
                Al
            </td>
            <td style="width: 300px; height: 19px">
                <asp:TextBox ID="txtAl" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Fine periodo ospitalità"
                    Width="75px" MaxLength="10" TabIndex="603"></asp:TextBox>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                    ControlToValidate="txtAl" Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px">
                &nbsp;
            </td>
            <td style="width: 300px; height: 19px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px">
            </td>
            <td style="width: 300px; height: 19px; text-align: right;" align="right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="img_InserisciOspite" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                OnClientClick="confronta_data(document.getElementById('Tab_Conduttore1_txtDal').value,document.getElementById('Tab_Conduttore1_txtAl').value);document.getElementById('USCITA').value='1';"
                                ToolTip="Inserisci l'ospite nello schema" TabIndex="604" />&nbsp;<img id="ImgChiudiOspiti"
                                    alt="Esci senza inserire" src="../NuoveImm/Img_AnnullaVal.png" onclick="document.getElementById('USCITA').value='0';document.getElementById('Tab_Conduttore1_txtCodiceFiscale').value='';document.getElementById('Tab_Conduttore1_txtDal').value='';document.getElementById('Tab_Conduttore1_txtAl').value='';document.getElementById('Tab_Conduttore1_txtNominativo').value='';myOpacity.toggle();"
                                    style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="V2" runat="server" />
    <asp:HiddenField ID="V3" runat="server" />
    <asp:Image ID="ImgSfondoSchema" runat="server" ImageUrl="~/ImmDiv/SfondoDim1.jpg"
        Style="z-index: 190; position: absolute; top: 159px; left: 211px;" BackColor="White" />
</div>
<asp:HiddenField ID="V1" runat="server" />
<script type="text/javascript">
    document.getElementById('InserimentoOspiti').style.visibility = 'hidden';


    function NuovoOspite() {

        if (document.getElementById('VIRTUALE').value == '0' && document.getElementById('HStatoContratto').value != 'CHIUSO') {
            document.getElementById('USCITA').value = '1';
            myOpacity.toggle();
        }
        else {
            alert('Operazione non possibile al momento!');
        }
    }

    function InserisciComponente() {
        window.showModalDialog('Anagrafica/Inserimento.aspx?ABS=1&INS=1', window, 'status:no;dialogTop=0;dialogLeft=0;dialogWidth:500px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
    }
</script>
