<%@ Page Language="vb" AutoEventWireup="false" Inherits="CM.impostapw" CodeFile="impostapw.aspx.vb" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<script type="text/javascript">
//var Uscita;
//Uscita=0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Imposta Password</title>
            <script type="text/javascript" language="javascript">
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    document.onkeydown = $onkeydown;
                }
                else {
                    window.document.addEventListener("keydown", TastoInvio, true);
                };
                function TastoInvio(e) {
                    sKeyPressed1 = e.which;
                    if (sKeyPressed1 == 112 || sKeyPressed1 == 115 || sKeyPressed1 == 116 || sKeyPressed1 == 117 || sKeyPressed1 == 118 || sKeyPressed1 == 122 || sKeyPressed1 == 123 || sKeyPressed1 == 60 || sKeyPressed1 == 91 || sKeyPressed1 == 92) {
                        sKeyPressed1 = 0;
                        e.preventDefault();
                        e.stopPropagation();
                    };
                    if (document.activeElement.type != 'text' && document.activeElement.type != 'textarea' && document.activeElement.type != 'password') {
                        if (sKeyPressed1 == 8) {
                            sKeyPressed1 = 0;
                            e.preventDefault();
                            e.stopPropagation();
                        };
                        if (sKeyPressed1 == 13) {
                            if (document.getElementById('HFbtnClickGo') != null) {
                                if (document.getElementById('HFbtnClickGo').value != '') {
                                    var idoggetto = document.getElementById('HFbtnClickGo').value;
                                    if (document.getElementById('' + idoggetto + '')) {
                                        document.getElementById('' + idoggetto + '').click();
                                    };
                                } else {
                                    sKeyPressed1 = 0;
                                    e.preventDefault();
                                    e.stopPropagation();
                                };
                            } else {
                                sKeyPressed1 = 0;
                                e.preventDefault();
                                e.stopPropagation();
                            };
                        };
                    }
                    else {
                        if (sKeyPressed1 == 226) {
                            sKeyPressed1 = 0;
                            e.preventDefault();
                            e.stopPropagation();
                        };
                        var el = document.activeElement;
                        var multiline = el.tagName;
                        if (multiline != 'TEXTAREA') {
                            if (document.activeElement.readOnly == true) {
                                if (sKeyPressed1 == 8) {
                                    sKeyPressed1 = 0;
                                    e.preventDefault();
                                    e.stopPropagation();
                                };
                                if (sKeyPressed1 == 13) {
                                    if (document.getElementById('HFbtnClickGo') != null) {
                                        if (document.getElementById('HFbtnClickGo').value != '') {
                                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                                            if (document.getElementById('' + idoggetto + '')) {
                                                document.getElementById('' + idoggetto + '').click();
                                            };
                                        } else {
                                            sKeyPressed1 = 0;
                                            e.preventDefault();
                                            e.stopPropagation();
                                        };
                                    } else {
                                        sKeyPressed1 = 0;
                                        e.preventDefault();
                                        e.stopPropagation();
                                        return false;
                                    };
                                };
                            } else {
                                if (sKeyPressed1 == 13) {
                                    if (document.getElementById('HFbtnClickGo') != null) {
                                        if (document.getElementById('HFbtnClickGo').value != '') {
                                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                                            if (document.getElementById('' + idoggetto + '')) {
                                                document.getElementById('' + idoggetto + '').click();
                                            };
                                        } else {
                                            sKeyPressed1 = 0;
                                            e.preventDefault();
                                            e.stopPropagation();
                                        };
                                    } else {
                                        sKeyPressed1 = 0;
                                        e.preventDefault();
                                        e.stopPropagation();
                                        return false;
                                    };
                                };
                            };
                        };
                    };
                    var alt = window.event.altKey;
                    if (alt && sKeyPressed1 == 115) {
                        if (document.getElementById('noClose').value == 1) {
                            if (document.getElementById('btnEsci') != null) {
                                exitClick = 1;
                                document.getElementById('btnEsci').click();
                            }
                            else if (document.getElementById('MainContent_btnEsci') != null) {
                                exitClick = 1;
                                document.getElementById('MainContent_btnEsci').click();
                            };
                            alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
                        };
                    };
                };
                function $onkeydown() {
                    var alt = window.event.altKey;
                    if (alt && event.keyCode == 115) {
                        if (document.getElementById('noClose').value == 1) {
                            if (document.getElementById('btnEsci') != null) {
                                exitClick = 1;
                                document.getElementById('btnEsci').click();
                            }
                            else if (document.getElementById('MainContent_btnEsci') != null) {
                                exitClick = 1;
                                document.getElementById('MainContent_btnEsci').click();
                            };
                            alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
                        };
                    };
                    if (event.keyCode == 112 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 122 || event.keyCode == 123 || event.keyCode == 60 || event.keyCode == 91 || event.keyCode == 92) {
                        event.keyCode = 0;
                        return false;
                    };
                    if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
                        if (event.keyCode == 8) {
                            event.keyCode = 0;
                            return false;
                        };
                        if (event.keyCode == 13) {
                            if (document.getElementById('HFbtnClickGo') != null) {
                                if (document.getElementById('HFbtnClickGo').value != '') {
                                    var idoggetto = document.getElementById('HFbtnClickGo').value;
                                    if (document.getElementById('' + idoggetto + '')) {
                                        document.getElementById('' + idoggetto + '').click();
                                    };
                                } else {
                                    event.keyCode = 0;
                                    return false;
                                };
                            } else {
                                event.keyCode = 0;
                                return false;
                            };
                        };
                    }
                    else {
                        if (event.keyCode == 226) {
                            event.keyCode = 0;
                            return false;
                        };
                        if (document.activeElement.isMultiLine == false) {
                            if (document.activeElement.readOnly == true) {
                                if (event.keyCode == 8) {
                                    event.keyCode = 0;
                                    return false;
                                };
                                if (event.keyCode == 13) {
                                    if (document.getElementById('HFbtnClickGo') != null) {
                                        if (document.getElementById('HFbtnClickGo').value != '') {
                                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                                            if (document.getElementById('' + idoggetto + '')) {
                                                document.getElementById('' + idoggetto + '').click();
                                            };
                                        } else {
                                            event.keyCode = 0;
                                            return false;
                                        };
                                    } else {
                                        event.keyCode = 0;
                                        return false;
                                    };
                                };
                            } else {
                                if (event.keyCode == 13) {
                                    if (document.getElementById('HFbtnClickGo') != null) {
                                        if (document.getElementById('HFbtnClickGo').value != '') {
                                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                                            if (document.getElementById('' + idoggetto + '')) {
                                                document.getElementById('' + idoggetto + '').click();
                                            };
                                        } else {
                                            event.keyCode = 0;
                                            return false;
                                        };
                                    } else {
                                        event.keyCode = 0;
                                        return false;
                                    };
                                };
                            };
                        };
                    };
                };
    </script>

	</head>
	<body bgcolor="#c8c8c8" style="text-align: center">
        <form id="Form1" method="post" runat="server" defaultbutton="btnSalva" 
        defaultfocus="txtPw">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
            &nbsp; &nbsp;<div align="center">
            <table style="background-color: white" width="750">
                <tr>
                    <td style="height: 44px">
                        <img alt="" src="Images/sepa_header_CambioPassword.jpg" /></td>
                </tr>
                <tr>
                    <td style="height: 44px">
                        <br />
                        <asp:Label ID="Label5" runat="server" Enabled="False" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Height="26px" Style="z-index: 109; left: 20px; color: black;
                            position: static; top: 106px; background-color: white" Width="858px">In quest'area puoi modificare la password con cui ti connetti a SEPA@Web. La Password deve essere composta da almeno 8 caratteri alfanumerici e non può essere uguale alle ultime 4 password impostate. Si ricorda inoltre che la validità della password è di 30 giorni e che se non utilizzata per 180 giorni, l'utenza sarà revocata.</asp:Label><br />
                    </td>
                </tr>
                <tr align="center" style="text-align: left">
                    <td style="text-align: left">
                        <br />
                        <br />
                        <table style="text-align: left" width="300">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                        Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Password Attuale:</asp:Label></td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="txtPw" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                        Font-Size="10pt" Height="22px" Rows="1" Style="z-index: 103; left: 243px; position: static;
                                        top: 165px" TextMode="Password" Width="204px" Wrap="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                        Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Password Nuova:</asp:Label></td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="txtNPw" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                        Font-Size="10pt" Height="22px" Style="z-index: 104; left: 244px; position: static;
                                        top: 198px" TextMode="Password" Width="204px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                        Style="z-index: 102; left: 63px; position: static; top: 231px" Width="175px">Conferma Password Nuova:</asp:Label></td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="txtCNPw" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                        Font-Size="10pt" Height="22px" Style="z-index: 105; left: 244px; position: static;
                                        top: 230px" TextMode="Password" Width="204px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr >
                    <td style="text-align: left">
                        <telerik:RadCaptcha ID="RadCaptcha1" Runat="server" 
        CaptchaLinkButtonText="Genera Nuovo Codice" 
        CaptchaTextBoxLabel="Inserisci il codice" EnableRefreshImage="True" 
        ErrorMessage="Codice errato" Font-Names="arial" Font-Size="8pt" CaptchaImage-BackgroundNoise="None" CaptchaImage-LineNoise="None" CaptchaImage-TextChars="Numbers">

    </telerik:RadCaptcha></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblmessaggio" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
                            Height="17px" Style="z-index: 106; left: 12px; position: static; top: 272px"
                            Width="651px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="height: 32px; text-align: right">
                        <br />
                        <br />
                        <asp:Button ID="btnSalva" runat="server" Height="30px" Style="z-index: 107; left: 432px;
                            position: static; top: 359px" Text="Memorizza" 
                            Width="132px" />
                        <asp:Button ID="btnAnnulla" runat="server" Height="30px" Style="z-index: 108; left: 567px;
                            position: static; top: 324px" Text="Annulla" Width="75px" /><br />
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
                    </td>
                </tr>
            </table>
            </div> 
		</form>
	</body>
</html>
