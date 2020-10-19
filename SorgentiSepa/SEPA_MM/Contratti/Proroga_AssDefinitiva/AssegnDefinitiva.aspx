<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssegnDefinitiva.aspx.vb"
    Inherits="Contratti_Proroga_AssDefinitiva_AssegnDefinitiva" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assegnazione Definitiva</title>
    <script type="text/javascript" language="javascript">
        window.name = "modal";

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
        function confronta_date(data1, data2, d1, d2, CAMPO) {	// controllo validità formato data    

            //trasformo le date nel formato aaaammgg (es. 20081103)        
            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
            if (data2str != '') {

                //controllo se la seconda data è successiva alla prima
                if (data2str - data1str < 0) {
                    alert('La data ' + d1 + ' deve essere precedente o uguale alla data ' + d2 + '!');
                    CAMPO.value = '';
                } else {
                    //alert("ok");
                }
            }
        }

        function confronta_dataDelibera(data1, data2) {	// controllo validità formato data    

            //trasformo le date nel formato aaaammgg (es. 20081103)        
            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
            //controllo se la seconda data è successiva alla prima
            if (data2str - data1str < 0) {
                alert("La data deve essere precedente alla data odierna!");
                document.getElementById('txtDataProvv').value = '';
            } else {
                //alert("ok");
            }
        }
        function ConfermaAssDefinit() {

            var numProvv;
            var data1str;
            var data2str;
            var data3str;
            var data4str;

            numProvv = document.getElementById('txtNumProvv').value
            data1str = document.getElementById('txtDataProvv').value
            data2str = document.getElementById('txtDataStipula').value
            data3str = document.getElementById('txtDataDecorr').value
            data4str = document.getElementById('txtDataCons').value

            var Conferma;
            if (numProvv != '' && data1str != '' && data2str != '' && data3str != '' && data4str != '') {
                Conferma = window.confirm('Attenzione...procedendo verrà creato un nuovo contratto di tipo ERP. Continuare?');
                if (Conferma == false) {
                    document.getElementById('confermaAssegnDef').value = '0';
                }
                else {
                    document.getElementById('confermaAssegnDef').value = '1';
                }
            }
            else {
                alert('Campi obbligatori!');
            }
        }
        var data;
        function dateAdd(date, tipo, valore) {
            (typeof (date) == "number") ? 1 == 1 : data = new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), 0);

            switch (tipo) {
                case "s":
                    si = div(data.getSeconds() + valore, 60);
                    s = (data.getSeconds() + valore) % 60;
                    si ? addInterval(data.setSeconds(s), "m", si) : data.setSeconds(s);
                    break;
                case "m":
                    mi = div(data.getMinutes() + valore, 60);
                    m = (data.getMinutes() + valore) % 60;
                    mi ? addInterval(data.setMinutes(m), "h", mi) : data.setMinutes(m);
                    break;
                case "h":
                    hi = div(data.getHours() + valore, 24);
                    h = (data.getHours() + valore) % 24;
                    hi ? addInterval(data.setHours(h), "dd", hi) : data.setHours(h);
                    break;
                case "dd":
                    mod = getDaysInMonth(data);
                    ddi = div(data.getDate() + valore, mod);
                    dd = (data.getDate() + valore) % mod;
                    ddi ? addInterval(data.setDate(dd), "mm", ddi) : data.setDate(dd);
                    break;
                case "mm":
                    mmi = div(data.getMonth() + valore, 12);
                    mm = (data.getMonth() + valore) % 12;
                    mmi ? addInterval(data.setMonth(mm), "yy", mmi) : data.setMonth(mm);
                    break;
                case "yy":
                    yy = (data.getFullYear() + valore);
                    data.setFullYear(yy);
                    break;
                default:
            }
            return data;
        }
        function getDaysInMonth(aDate) {
            var m = new Number(aDate.getMonth());
            var y = new Number(aDate.getYear());

            var tmpDate = new Date(y, m, 28);
            var checkMonth = tmpDate.getMonth();
            var lastDay = 27;

            while (lastDay <= 31) {
                temp = tmpDate.setDate(lastDay + 1);
                if (checkMonth != tmpDate.getMonth())
                    break;
                lastDay++
            }
            return lastDay;
        }

        function div(op1, op2) {
            return (op1 / op2 - op1 % op2 / op2)
        }


        function ScriviDate() {

            a = document.getElementById('txtDataDecorr').value;
            b = 4; //durata contratto

            if (a.length == 10) {
                anno1 = parseInt(a.substr(6), 10);
                mese1 = parseInt(a.substr(3, 2), 10);
                giorno1 = parseInt(a.substr(0, 2), 10);
                var dataok1 = new Date(anno1, mese1 - 1, giorno1);
                dateAdd(dataok1, 'dd', -1);
                a = data;
                var G = a.getDate();
                var M = (a.getMonth() + 1);
                if (G < 10) {
                    var gg = "0" + a.getDate();
                }
                else {
                    var gg = a.getDate();
                }
                if (M < 10) {
                    var mm = "0" + (a.getMonth() + 1);
                }
                else {
                    var mm = (a.getMonth() + 1);
                }
                var aa = a.getFullYear();
                miaData = gg + "/" + mm + "/" + aa;
                //miaData = a;

                Scadenza = eval(miaData.substr(6, 4)) + eval(b);
                miaData = miaData.substr(0, 6) + Scadenza;
            }
            else {
                miaData = '';
            }

            document.getElementById('dataScadenza').value = miaData;

            a = document.getElementById('dataScadenza').value;
            b = 4;

            if (a.length == 10) {
                Scadenza = eval(a.substr(6, 4)) + eval(b);
                miaData = a.substr(0, 6) + Scadenza;
            }
            else {
                miaData = '';
            }



            document.getElementById('dataScadenza2').value = miaData;
        }

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <div>
        <table style="padding: 20px; width: 100%;">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; font-weight: bold;">
                        Assegnazione definitiva </span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt">Compilare i seguenti campi:</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Numero Provvedimento</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumProvv" runat="server" TabIndex="1"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Data Provvedimento</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataProvv" runat="server" TabIndex="2"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataProvv"
                                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Data Stipula</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataStipula" runat="server" TabIndex="3"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataStipula"
                                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Data Decorrenza</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataDecorr" runat="server" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataDecorr"
                                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Data Consegna</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataCons" runat="server" TabIndex="5"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataCons"
                                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ForeColor="#CC3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table align="right" width="350px">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                                    OnClientClick="ConfermaAssDefinit();" TabIndex="5" />
                                <img id="exit" alt="Esci" src="../../NuoveImm/Img_EsciCorto.png" title="Esci" style="cursor: pointer"
                                    onclick="CloseModal(document.getElementById('confermaAssegnDef').value)" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="confermaAssegnDef" runat="server" Value="0" />
    <asp:HiddenField ID="dataScadenza" runat="server" Value="0" />
    <asp:HiddenField ID="dataScadenza2" runat="server" Value="0" />
    </form>
</body>
</html>
