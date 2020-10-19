<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdempimentiSucc.aspx.vb"
    Inherits="Contratti_AdempimentiSucc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">

        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }


    </script>
    <form id="form1" runat="server" defaultbutton="imgProcedi" defaultfocus="cmbMese">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="lblTitolo" runat="server" Text="Adempimenti Successivi"></asp:Label>
                        <br />
                    </strong></span>
                    <br />
                    <br />
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Width="700px">Generazion file XML da inviare all'agenzia delle entrate</asp:Label>&nbsp;<br />
                    <br />
                    &nbsp;
                    <table width="90%">
                        <tr>
                            <td style="width: 5px">
                            </td>
                            <td style="width: 54px">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 50px; position: static; top: 188px" Width="51px">Periodo</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbMese" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 116px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 185px" TabIndex="1" Width="125px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td class="style2">
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 50px; position: static; top: 188px" Width="119px">Data Addebito*</asp:Label>
                            </td>
                            <td class="style3">
                                <asp:TextBox ID="txtValuta" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="10" Style="z-index: 113; left: 152px; top: 172px; right: 580px;" TabIndex="2"
                                    ToolTip="gg/mm/aaaa" Width="75px"></asp:TextBox>
                                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txtValuta" Display="Dynamic" ErrorMessage="Formato gg/mm/aaaa"
                                    Font-Bold="True" Font-Names="arial" Font-Size="8pt" Style="left: 172px; top: 91px"
                                    TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;
                            </td>
                            <td style="width: 54px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                    Text="* Attenzione, la data nella quale si richiede venga contabilizzato l’addebito delle somme dovute viene calcolato automaticamente dal sistema (data decorrenza+ 29gg). Indicare comunque la data di addebito che verrà utilizzata per le risoluzioni e  per calcolare gli interessi e le sanzioni (in base a ultimo giorno utile per il pagamento) per i pagamenti effettuati in ritardo."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;
                            </td>
                            <td style="width: 54px">
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                    Text="Si ricorda che i rapporti in cui non è specificato l'Ufficio del registro non saranno presi in considerazione!"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;
                            </td>
                            <td style="width: 54px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;
                            </td>
                            <td style="width: 54px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Visible="False"
                        Width="700px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 712px; position: absolute; top: 501px" ToolTip="Home"
            TabIndex="4" />
        <asp:ImageButton ID="imgProcedi" runat="server" OnClientClick="ConfermaProcedi();"
            ImageUrl="~/NuoveImm/Img_Procedi.png" Style="z-index: 101; left: 617px; position: absolute;
            top: 501px; right: 433px; height: 20px;" ToolTip="Procedi" TabIndex="3" />
    </div>
    <asp:HiddenField ID="confermaProcedi" runat="server" Value="0" />
    <asp:HiddenField ID="dataOdierna" runat="server" Value="0" />
    <asp:HiddenField ID="provenienza" runat="server" Value="0" />
    <div align='center' id='dvvvPre' 
        style='position:absolute; background-color:#ffffff; text-align:center; width:100%; height:100%; top:0px; left:0px; z-index:500; border:1px dashed #660000;font:verdana; font-size:10px; visibility: hidden;'><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><img src='Immagini/load.gif' alt='Elaborazione in corso'><br />Elaborazione in corso...</div>
    </form>
</body>
<script type="text/javascript">
    function ConfermaProcedi() {

        var data1 = document.getElementById('txtValuta').value;

        var chiediConferma;
        var errore1;

        data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
        data2str = document.getElementById('dataOdierna').value;

        if (data1str - data2str < 0) {
            alert('Errore intervallo date stipula!');
            errore1 = '1';
        }
        else {
            errore1 = '0';
        }

        if (errore1 == '0') {
            if (document.getElementById('dataOdierna').value=='1') {
            chiediConferma = window.confirm("Attenzione...procedendo verrà creato un file XML valido per il pagamento delle Cessioni. Continuare?");
            }
            else {
            chiediConferma = window.confirm("Attenzione...procedendo verrà creato un file XML valido per il pagamento delle annualità successive. Continuare?");
            }
            if (chiediConferma == false) {
                document.getElementById('confermaProcedi').value = '0';
            }
            else {
                document.getElementById('confermaProcedi').value = '1';
                document.getElementById('dvvvPre').style.visibility = 'visible';
            }
        }
    };
    document.getElementById('dvvvPre').style.visibility = 'hidden';
       
</script>
</html>
