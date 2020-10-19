<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManutenzioniRRS.aspx.vb"
    Inherits="RRS_ManutenzioniRRS" %>

<%@ Register Src="Tab_Manu_Riepilogo.ascx" TagName="Tab_Manu_Riepilogo" TagPrefix="uc1" %>
<%@ Register Src="Tab_Manu_Dettagli.ascx" TagName="Tab_Manu_Dettagli" TagPrefix="uc2" %>
<%@ Register Src="Tab_Manu_Note.ascx" TagName="Tab_Manu_Note" TagPrefix="uc3" %>
<%@ Register Src="TabMenuAllegati.ascx" TagName="Tab_Manu_Allegati" TagPrefix="uc4" %>
<%@ Register Src="TabMenuIrregolarita.ascx" TagName="Tab_Manu_Irregolarita" TagPrefix="uc5" %>
<%@ Register Src="TabMenuEventi.ascx" TagName="Tab_Manu_Eventi" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Selezionato;
    var OldColor;
    var SelColo;

    function InviaEmail() {
        document.location.href = 'mailto:' + document.getElementById('indirizzoEmail').value + '?subject=' + document.getElementById('oggettoEmail').value + '&body=' + document.getElementById('bodyEmail').value;
    };

    function Integrato() {
        document.getElementById('USCITA').value = '1';
        ConfermaEsci()
        document.getElementById('btnINFOintegrato').click();
    }
    function Integrazione() {
        document.getElementById('USCITA').value = '1';
        ConfermaEsci()
        document.getElementById('btnINFOintegrazione').click();
    }


    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    function SostPuntVirg(e, obj) {
        var keyPressed;
        keypressed = (window.event) ? event.keyCode : e.which;
        if (keypressed == 46) {
            if (navigator.appName == 'Microsoft Internet Explorer') {
                event.keyCode = 0;
            }
            else {
                e.preventDefault();
            };
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        };
    };

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }


    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        //        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';
    }


    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - 2);
                var dascrivere = a.substring(a.length - 3, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {

                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                }
                risultato = dascrivere + risultato + ',' + decimali
                //document.getElementById(obj.id).value = a.replace('.', ',')
                document.getElementById(obj.id).value = risultato
            }
            else {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
    }

    function DelPointer(obj) {
        obj.value = obj.value.replace('.', '');
        document.getElementById(obj.id).value = obj.value;

    }

    function CalcolaPrezzoTotale(obj, quantita, prezzo) {
        var risultato;

        //alert("pippo");
        quantita = quantita.replace('.', '');
        quantita = quantita.replace(',', '.');

        prezzo = prezzo.replace('.', '');
        prezzo = prezzo.replace(',', '.');

        risultato = quantita * prezzo;
        risultato = risultato.toFixed(2);
        //alert(risultato);
        document.getElementById('Tab_Manu_Consuntivo_txtTotale').value = risultato.replace('.', ',');
        document.getElementById('Tab_Manu_Consuntivo_txtTotale2').value = risultato.replace('.', ',');
    }



    function AttesaDIV() {
        document.getElementById('ATTESA').style.visibility = 'visible';
    }


    //IMPORTI DEL PREVENTIVO
    function CalcolaImporto(inserimento) {
        var perc_iva;
        var importo;
        var perc_oneri;
        var perc_sconto;
        var fl_rit_legge;
        var ritenuta;
        var ritenutaIVATA;

        // var penale;
        // var penaleOLD;

        var oneri;
        var asta;
        var iva;
        var risultato1;
        var risultato2;
        var risultato3;
        var risultato4;
        var risultato5;
        var risultato6;

        if (inserimento == 1) {
            document.getElementById('Tab_Manu_Riepilogo_txtOneriP_MANO').value = document.getElementById('Tab_Manu_Riepilogo_txtOneri').value
        }


        document.getElementById('txtPercIVA_P').value = document.getElementById('Tab_Manu_Riepilogo_cmbIVA_P').value

        perc_iva = document.getElementById('txtPercIVA_P').value.replace(/\./g, '');
        perc_iva = perc_iva.replace(/\,/g, '.');

        if (parseFloat(perc_iva) < 0) {
            //document.getElementById('Tab_Manu_Riepilogo_lblIVAC').innerText = 'IVA (  %)';
            apriAlert('Attenzione...Selezionare la percentuale dell\'iva del preventivo!', 300, 150, 'Attenzione', null, null);
            return;
        }

        //document.getElementById('Tab_Manu_Riepilogo_lblIVAC').innerText = 'IVA (' + perc_iva + '%)';

        //        penale = obj.value.replace(/\./g, '');
        //        penale = penale.replace(/\,/g, '.');


        importo = document.getElementById('Tab_Manu_Riepilogo_txtImporto').value.replace(/\./g, '');
        importo = importo.replace(/\,/g, '.');

        //A) Importo
        //V) perc_oneri
        //Y) perc_sconto
        //Z) perc_iva

        if (parseFloat(importo) > 0) {
            perc_oneri = document.getElementById('txtPercOneri').value.replace(/\./g, '');
            perc_oneri = perc_oneri.replace(/\,/g, '.');

            perc_sconto = document.getElementById('txtScontoConsumo').value.replace(/\./g, '');
            perc_sconto = perc_sconto.replace(/\,/g, '.');

            fl_rit_legge = document.getElementById('txtFL_RIT_LEGGE').value.replace(/\./g, '');
            fl_rit_legge = fl_rit_legge.replace(/\,/g, '.');



            //B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
            oneri = document.getElementById('Tab_Manu_Riepilogo_txtOneriP_MANO').value.replace(/\./g, '');
            oneri = oneri.replace(/\,/g, '.');

            if (parseFloat(oneri) == -1) {
            oneri = parseFloat(importo) - ((parseFloat(importo) * 100) / (100 + parseFloat(perc_oneri)));
            oneri = oneri.toFixed(2);
            }
            else {
                if (oneri.length == 0) {
                    oneri = 0;
                }
            }

            //C) A-B LORDO senza ONERI= IMPORTO-oneri
            risultato1 = parseFloat(importo) - parseFloat(oneri);
            risultato1 = risultato1.toFixed(2);

            //D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
            asta = (parseFloat(risultato1) * parseFloat(perc_sconto)) / 100;
            asta = asta.toFixed(2);

            //E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
            risultato2 = parseFloat(risultato1) - parseFloat(asta);
            risultato2 = risultato2.toFixed(2);

            //G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
            risultato3 = parseFloat(risultato2) + parseFloat(oneri)
            risultato3 = risultato3.toFixed(2);

            //F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
            if (parseFloat(fl_rit_legge) == 1) {
                ritenuta = ((parseFloat(risultato3) * 0.5)) / 100;
                // ritenuta = ((parseFloat(risultato2) * 0.5)) / 100;} 
                ritenutaIVATA = parseFloat(ritenuta) + (parseFloat(ritenuta) * parseFloat(perc_iva)) / 100;
            }
            else {
                ritenuta = 0;
                ritenutaIVATA = 0;
            }

            //G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
            //risultato3 = parseFloat(risultato2) - parseFloat(ritenuta) + parseFloat(oneri)
            risultato5 = parseFloat(risultato3) - parseFloat(ritenuta)
            risultato5 = risultato5.toFixed(2);


            //H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
            iva = (parseFloat(risultato5) * parseFloat(perc_iva)) / 100;
            iva = iva.toFixed(2);

            //I) NETTO+ONERI+IVA
            risultato4 = parseFloat(risultato5) + parseFloat(iva);
            risultato4 = risultato4.toFixed(2);

            //I) NETTO+ONERI+IVA + RITENUTA IVATA (da inserire in PRENOTAZIONI)
            risultato6 = parseFloat(risultato4) + parseFloat(ritenutaIVATA);
            risultato6 = risultato6.toFixed(2);


            oneri = oneri + '';
            iva = iva + '';
            asta = asta + '';
            ritenuta = ritenuta + '';
            risultato1 = risultato1 + '';
            risultato2 = risultato2 + '';
            risultato3 = risultato3 + '';
            risultato4 = risultato4 + '';
            risultato5 = risultato5 + '';
            risultato6 = risultato6 + '';

            document.getElementById('Tab_Manu_Riepilogo_txtOneri').value = oneri.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtOneri'));

            document.getElementById('Tab_Manu_Riepilogo_txtOneriImporto').value = risultato1.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtOneriImporto'));

            document.getElementById('Tab_Manu_Riepilogo_txtRibassoAsta').value = asta.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtRibassoAsta'));

            document.getElementById('Tab_Manu_Riepilogo_txtNetto').value = risultato2.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtNetto'));

            document.getElementById('Tab_Manu_Riepilogo_txtRitenuta').value = ritenuta.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtRitenuta'));

            document.getElementById('Tab_Manu_Riepilogo_txtNettoOneri').value = risultato3.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtNettoOneri'));

            document.getElementById('Tab_Manu_Riepilogo_txtIVA').value = iva.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtIVA'));

            document.getElementById('Tab_Manu_Riepilogo_txtNettoOneriIVA').value = risultato4.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtNettoOneriIVA'));

            document.getElementById('txtNettoOneriIVA_TMP').value = risultato6.replace('.', ',');
            AutoDecimal2(document.getElementById('txtNettoOneriIVA_TMP'));


            //        document.getElementById('txtPenaleOLD').value = penale.replace('.', ',');
            //        AutoDecimal2(document.getElementById('txtPenaleOLD'));            

            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneri, "", "##,##0.00")
            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1, "", "##,##0.00")
            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(asta, "", "##,##0.00")
            // CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2, "", "##,##0.00")

            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenuta, "", "##,##0.00") '6 campo

            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3, "", "##,##0.00")
            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtIVA"), TextBox).Text = IsNumFormat(iva, "", "##,##0.00")
            //CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4, "", "##,##0.00")
            //CType(Me.Page.FindControl("txtNettoOneriIVA_TMP"), HiddenField).Value = IsNumFormat(risultato4, "", "##,##0.00")



            //NETTO senza ONERI= (LORDO senza oneri-asta)
            //risultato2 = parseFloat(risultato1) - parseFloat(asta);
            //risultato2 = risultato2.toFixed(2);

            // if (parseFloat(penale) > parseFloat(risultato2)) {
            //      alert('Attenzione...La penale non deve superare l\'importo netto escluso oneri!');
            //      penale=0;

            //      document.getElementById('Tab_Manu_Riepilogo_txtPenale').value = 0;
            // }

            //NETTO senza ONERI= (LORDO senza oneri-asta) - PENALE
            //risultato2 = parseFloat(risultato1) - parseFloat(asta) - parseFloat(penale);
            //risultato2 = risultato2.toFixed(2);

            //NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI + ONERI risultato2+oneri
            //risultato3 = parseFloat(risultato2) + parseFloat(oneri)
            //risultato3 =risultato3.toFixed(2);              

            //IVA= (NETTO con oneri*perc_iva)/100
            //iva = (parseFloat(risultato3) * parseFloat(perc_iva)) / 100;
            //iva= iva.toFixed(2);

            //        if (parseFloat(penale) > parseFloat(risultato4)) {
            //            alert('Attenzione...La penale non deve superare l\'importo netto compresi oneri e iva!');
            //            penale=0;

            //            document.getElementById('Tab_Manu_Riepilogo_txtPenale').value = 0;
            //        }

            //NETTO+ONERI+IVA
            //risultato4 = parseFloat(risultato3) + parseFloat(iva); //- parseFloat(penale);
            //risultato4 = risultato4.toFixed(2);
        }
    }


    //IMPORTI DEL CONSUNTIVO
    function CalcolaImportoC(inserimento) {
        var perc_iva;
        var importo;
        var impRimborsi;
        var perc_oneri;
        var perc_sconto;
        var fl_rit_legge;
        var ritenuta;
        var ritenutaIVATA;

        //        var penale;
        //        var penaleOLD;

        var oneri;
        var asta;
        var iva;
        var risultato1;
        var risultato2;
        var risultato3;
        var risultato4;
        var risultato5;
        var risultato6;

        if (inserimento == 1) {
            document.getElementById('Tab_Manu_Riepilogo_txtOneriC_MANO').value = document.getElementById('Tab_Manu_Riepilogo_txtOneriC').value
        }

        document.getElementById('txtPercIVA_C').value = document.getElementById('Tab_Manu_Riepilogo_cmbIVA_C').value

        perc_iva = document.getElementById('txtPercIVA_C').value.replace(/\./g, '');
        perc_iva = perc_iva.replace(/\,/g, '.');

        if (parseFloat(perc_iva) < 0) {
            //document.getElementById('Tab_Manu_Riepilogo_lblIVAC').innerText = 'IVA (  %)';
            apriAlert('Attenzione...Selezionare la percentuale dell\'iva del consuntivo!', 300, 150, 'Attenzione', null, null);
            return;
        }

        //document.getElementById('Tab_Manu_Riepilogo_lblIVAC').innerText = 'IVA (' + perc_iva + '%)';

        //        penale = obj.value.replace(/\./g, '');
        //        penale = penale.replace(/\,/g, '.');


        importo = document.getElementById('Tab_Manu_Riepilogo_txtImportoC').value.replace(/\./g, '');
        importo = importo.replace(/\,/g, '.');
        impRimborsi = document.getElementById('Tab_Manu_Riepilogo_txtRimborsi').value.replace(/\./g, '');
        impRimborsi = impRimborsi.replace(/\,/g, '.');

        //A) Importo
        //V) perc_oneri
        //Y) perc_sconto
        //Z) perc_iva

        if (parseFloat(importo) > 0) {
            perc_oneri = document.getElementById('txtPercOneri').value.replace(/\./g, '');
            perc_oneri = perc_oneri.replace(/\,/g, '.');

            perc_sconto = document.getElementById('txtScontoConsumo').value.replace(/\./g, '');
            perc_sconto = perc_sconto.replace(/\,/g, '.');

            fl_rit_legge = document.getElementById('txtFL_RIT_LEGGE').value.replace(/\./g, '');
            fl_rit_legge = fl_rit_legge.replace(/\,/g, '.');



            //B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
            oneri = document.getElementById('Tab_Manu_Riepilogo_txtOneriC_MANO').value.replace(/\./g, '');
            oneri = oneri.replace(/\,/g, '.');

            if (parseFloat(oneri) == -1) {
            oneri = parseFloat(importo) - ((parseFloat(importo) * 100) / (100 + parseFloat(perc_oneri)));
            oneri = oneri.toFixed(2);
            }

            //C) A-B LORDO senza ONERI= IMPORTO-oneri
            risultato1 = parseFloat(importo) - parseFloat(oneri);
            risultato1 = risultato1.toFixed(2);

            //D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
            asta = (parseFloat(risultato1) * parseFloat(perc_sconto)) / 100;
            asta = asta.toFixed(2);

            //E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
            risultato2 = parseFloat(risultato1) - parseFloat(asta);
            risultato2 = risultato2.toFixed(2);

            //G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
            risultato3 = parseFloat(risultato2) + parseFloat(oneri)
            risultato3 = risultato3.toFixed(2);

            //F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
            if (parseFloat(fl_rit_legge) == 1) {
                ritenuta = ((parseFloat(risultato3) * 0.5)) / 100;
                // ritenuta = ((parseFloat(risultato2) * 0.5)) / 100;} 
                ritenutaIVATA = parseFloat(ritenuta) + (parseFloat(ritenuta) * parseFloat(perc_iva)) / 100;
            }
            else {
                ritenuta = 0;
                ritenutaIVATA = 0;
            }

            //G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
            //risultato3 = parseFloat(risultato2) - parseFloat(ritenuta) + parseFloat(oneri)
            risultato5 = parseFloat(risultato3) - parseFloat(ritenuta)
            risultato5 = risultato5.toFixed(2);


            //H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
            iva = (parseFloat(risultato5) * parseFloat(perc_iva)) / 100;
            iva = iva.toFixed(2);

            //I) NETTO+ONERI+IVA
            risultato4 = parseFloat(risultato5) + parseFloat(iva);
            risultato4 = parseFloat(risultato4) + parseFloat(impRimborsi);
            risultato4 = risultato4.toFixed(2);

            //I) NETTO+ONERI+IVA + RITENUTA IVATA (da inserire in PRENOTAZIONI)
            risultato6 = parseFloat(risultato4) + parseFloat(ritenutaIVATA);
            risultato6 = risultato6.toFixed(2);


            oneri = oneri + '';
            iva = iva + '';
            asta = asta + '';
            ritenuta = ritenuta + '';
            risultato1 = risultato1 + '';
            risultato2 = risultato2 + '';
            risultato3 = risultato3 + '';
            risultato4 = risultato4 + '';
            risultato5 = risultato5 + '';
            risultato6 = risultato6 + '';

            document.getElementById('Tab_Manu_Riepilogo_txtOneriC').value = oneri.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtOneriC'));

            document.getElementById('Tab_Manu_Riepilogo_txtOneriImportoC').value = risultato1.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtOneriImportoC'));

            document.getElementById('Tab_Manu_Riepilogo_txtRibassoAstaC').value = asta.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtRibassoAstaC'));

            document.getElementById('Tab_Manu_Riepilogo_txtNettoC').value = risultato2.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtNettoC'));

            document.getElementById('Tab_Manu_Riepilogo_txtRitenutaC').value = ritenuta.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtRitenutaC'));

            document.getElementById('Tab_Manu_Riepilogo_txtNettoOneriC').value = risultato3.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtNettoOneriC'));

            document.getElementById('Tab_Manu_Riepilogo_txtIVAC').value = iva.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtIVAC'));

            document.getElementById('Tab_Manu_Riepilogo_txtNettoOneriIVAC').value = risultato4.replace('.', ',');
            AutoDecimal2(document.getElementById('Tab_Manu_Riepilogo_txtNettoOneriIVAC'));

            document.getElementById('txtNettoOneriIVAC_TMP').value = risultato6.replace('.', ',');
            AutoDecimal2(document.getElementById('txtNettoOneriIVAC_TMP'));

        }
    }



    function ApriConsuntivo() {
        //document.getElementById('copri').style.visibility = 'visible';

        if (document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value > 0) {
            window.showModalDialog('Tab_Manu_Consuntivo.aspx?ID_MANUTENZIONE=' + document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value + '&IDCON=' + document.getElementById('Tab_Manu_Dettagli_txtIdConnessione').value + '&IDPADRE=' + document.getElementById('Tab_Manu_Dettagli_txtIdManuPadre').value + '&RESIDUO=' + document.getElementById('Tab_Manu_Dettagli_txtResiduoConsumo').value + '&IDVISUAL=' + document.getElementById('SOLO_LETTURA').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            //document.getElementById('txtIdManutenzione').value = 1
        }
        else {
            apriAlert('Selezionare un elemento dalla lista!', 300, 150, 'Attenzione', null, null);

        }
    };

    function getDropDownListvalue() {
        var e = document.getElementById("cmbNoteChiusura");
        var strUser = e.options[e.selectedIndex].value;
        document.getElementById("txtDescNoteChiusura").value = strUser

    }
    function Apri(Page) {
        var oWnd = $find('RadWindow1');
        oWnd.setUrl(Page);
        oWnd.show();
    };
    function AllegaFile() {
        if ((document.getElementById('HiddenFieldIdManutenzione').value == '') || (document.getElementById('HiddenFieldIdManutenzione').value == '0')) {
            alert('E\' necessario salvare la manutenzione prima di allegare documenti!');
        } else {
            CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('HiddenFieldIdManutenzione').value, 'Allegati', 1000, 800);
        };
    };
    function CenterPage(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    };


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>MODULO GESTIONE NON PATRIMONIALE</title>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <link href="../../../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };

    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit; 

        function EliminaManutenzione() {
            var sicuro = confirm('Sei sicuro di voler eliminare questa manutenzione? Tutti i dati andranno persi.');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }

        function AnnullaManutenzione() {
            var sicuro = confirm('Sei sicuro di voler annullare questa manutenzione? L\'ordine visualizzato non sarà più modificabile!');
            if (sicuro == true) {
                document.getElementById('txtAppare1').value = '1';
                document.getElementById('DIV_C').style.visibility = 'visible';
                //document.getElementById('txtElimina').value='1';
            }
            else {
                // document.getElementById('txtAppare1').value = '0';
                //document.getElementById('DIV_C').style.visibility = 'hidden';
                //document.getElementById('txtElimina').value='0'; 
            }
        }

        function CreaOrdineIntegrativo() {

            if (document.getElementById('txtModificato').value == '1') {
                apriAlert('Attenzione...Sono state apportate delle modifiche. Salvare prima di emettere un ordine integrativo!', 300, 150, 'Attenzione', null, null);

            }
            else {
                var sicuro = confirm('Sei sicuro di voler creare un ordine integrativo? L\'ordine visualizzato non sarà più modificabile!');
                if (sicuro == true) {
                    document.getElementById('txtOrdine').value = '1';
                }
                else {
                    document.getElementById('txtOrdine').value = '0';
                }
            }
        }


        function ConfermaAnnulloIntervento() {
            var sicuro = confirm('Sei sicuro di voler cancellare questo intervento?');
            if (sicuro == true) {
                document.getElementById('Tab_Manu_Dettagli_txtannullo').value = '1';
            }
            else {
                document.getElementById('Tab_Manu_Dettagli_txtannullo').value = '0';
            }
        }

        function ConfermaAnnulloConsuntivo() {
            var sicuro = confirm('Sei sicuro di voler cancellare questo consuntivo?');
            if (sicuro == true) {
                document.getElementById('Tab_Manu_Consuntivo_txtannullo').value = '1';
            }
            else {
                document.getElementById('Tab_Manu_Consuntivo_txtannullo').value = '0';
            }
        }




        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                if (document.getElementById('SOLO_LETTURA').value == '0') {
                    if (document.getElementById("BLOCCATO").value == '0') {
                        if (document.getElementById('txtSTATO').value < 2) {
                            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                            if (chiediConferma == false) {
                                document.getElementById('txtModificato').value = '111';
                                //document.getElementById('USCITA').value='0';
                            }
                        }
                    }
                }
            }
        }


        function ConfermaEsciConsuntivo() {
            if (document.getElementById('TabM_Manu_Consuntivo_txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('TabM_Manu_Consuntivo_txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }

        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda manutenzione premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda manutenzione premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }














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
</head>
<body class="sfondo">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <telerik:RadWindow ID="RadWindowAnnullo" runat="server" CenterIfModal="true" Modal="True"
            Title="Annulla Manutenzione" VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize"
            Skin="Web20" Height="450px" Width="700px">
            <ContentTemplate>
                <table class="sfondo" style="width: 100%" class="FontTelerik">
                    <tr>
                        <td class="TitoloModulo">Motivazione dell'annullamento
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 24px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="btnAnnullaManu" runat="server" Text="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                                        ToolTip="Salva le modifiche apportate" CausesValidation="False">
                                                    </telerik:RadButton>
                                                </td>
                                                <td>
                                                    <telerik:RadButton ID="btnChiudiAnnullaManu" runat="server" Text="Esci" OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowAnnullo');document.getElementById('USCITA').value='0';}"
                                                        ToolTip="Esci senza inserire o modificare" CausesValidation="False">
                                                    </telerik:RadButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24px"></td>
                                    <td>
                                        <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="250px"
                                            MaxLength="400" TextMode="MultiLine" Width="620px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24px"></td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24px"></td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="500" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
        <telerik:RadWindow ID="RadWindow1" runat="server" VisibleStatusbar="false" VisibleTitlebar="true"
            CenterIfModal="true" InitialBehaviors="Maximize" ShowOnTopWhenMaximized="false">
        </telerik:RadWindow>
        <table style="width: 100%" class="FontTelerik">
            <tr>
                <td class="TitoloModulo">Manutenzione non patrimoniale
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnINDIETRO" runat="server" Text="Indietro" OnClientClicking="function(sender, args){Blocca_SbloccaMenu(0);document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                    ToolTip="Indietro" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                    ToolTip="Salva" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnElimina" runat="server" Text="Elimina" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';  EliminaManutenzione();}"
                                    ToolTip="Elimina la manutezione visualizzata" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Annulla" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; openWindow(null, null, 'RadWindowAnnullo');}"
                                    ToolTip="Annulla la manutezione visualizzata" CausesValidation="False" AutoPostBack="false" Visible="TRUE">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="ImgStampa" runat="server" Text="Stampa" OnClientClicking="function(sender, args){StampaOrdine();}"
                                    ToolTip="Stampa Ordine" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="ImgEmail" runat="server" Text="Invia e-mail" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';InviaEmail();}"
                                    ToolTip="Invia e-mail" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="ImgEventi" runat="server" Text="Eventi" OnClientClicking="function(sender, args){Eventi();}" AutoPostBack="false"
                                    ToolTip="Eventi Scheda Manutenzione" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="ImgAllegati" runat="server" Text="Allegati" OnClientClicking="function(sender, args){AllegaFile();}"
                                    ToolTip="Allegati" CausesValidation="False" AutoPostBack="false">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnOrdineIntegrativo" runat="server" Text="Ordine Integrativo"
                                    OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';  CreaOrdineIntegrativo();}"
                                    ToolTip="Emette un ordine integrativo" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" OnClientClicking="function(sender, args){Blocca_SbloccaMenu(0);document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                    ToolTip="Esci" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" class="TitoloH1" Style="z-index: 100; left: 8px; top: 88px; font-size: 8pt; text-align: left">UBICAZIONE</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle" width="15%">
                                <asp:RadioButtonList ID="RBL1" runat="server"
                                    ForeColor="Black" RepeatDirection="Horizontal">
                                    <asp:ListItem>Complesso</asp:ListItem>
                                    <asp:ListItem Value="Edificio"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="vertical-align: middle" width="5%">
                                <asp:Label ID="lbl_INDIRIZZO" runat="server" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="60px">Indirizzo *</asp:Label>
                            </td>
                            <td style="vertical-align: middle;" width="30%">
                                <telerik:RadComboBox ID="cmbIndirizzo" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                            <td style="vertical-align: middle" width="5%">
                                <asp:Label ID="lbl_SCALA" runat="server" Style="z-index: 100; left: -46px; top: -302px">Scala</asp:Label>
                            </td>
                            <td style="vertical-align: middle" width="10%">
                                <telerik:RadComboBox ID="cmbScala" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                            <td width="10%">
                                <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: -46px; top: -302px">Intestaz. ODL</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbIntestazione" Width="90%" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    &nbsp;<asp:Label ID="lblODL" runat="server" CssClass="TitoloH1" Style="z-index: 100; font-size: 8pt; left: 8px; top: 88px; text-align: left"
                        Width="50px">ODS N°</asp:Label><asp:Label
                            ID="lblODL1" runat="server" Font-Bold="True" CssClass="TitoloH1" Style="z-index: 100; text-align: left; font-size: 8pt; left: 8px; top: 88px"
                            Width="60px"></asp:Label><asp:Label
                                ID="lblDataDel" runat="server" CssClass="TitoloH1" Style="z-index: 100; left: 8px; font-size: 8pt; top: 88px; text-align: left"
                                Width="30px">del</asp:Label><asp:Label
                                    ID="lblData" runat="server" CssClass="TitoloH1" Style="z-index: 100; left: 8px; font-size: 8pt; top: 88px; text-align: left"
                                    Width="70px"></asp:Label><asp:Label
                                        ID="lblODL_INTEGRAZIONE" runat="server" CssClass="TitoloH1" Style="z-index: 100; left: 8px; text-align: left; font-size: 8pt; top: 88px"
                                        Visible="False"
                                        Width="120px"></asp:Label>
                    &nbsp;
                <asp:Label ID="lblOLDINTEGRAZIONE" runat="server" CssClass="TitoloH1" Style="z-index: 100; left: 8px; top: 88px; font-size: 8pt; text-align: left"
                    Visible="False"
                    Width="70px"></asp:Label>
                    &nbsp;
                <asp:CheckBox Checked="false" runat="server" ID="ChkAutorizzazioneEmissione" Text="Autorizzazione emissione"
                    CssClass="TitoloH1" Font-Size="8pt" />
                    &nbsp;
                <asp:Label ID="lblODL_INTEGRATO" runat="server" CssClass="TitoloH1"
                    Style="z-index: 100; left: 8px; top: 88px; text-align: left; font-size: 8pt"
                    Visible="False" Width="100px"></asp:Label>
                    <asp:Label ID="lblOLDINTEGRATO" runat="server" CssClass="TitoloH1" Style="z-index: 100; font-size: 8pt; left: 8px; top: 88px; text-align: left"
                        Visible="False"
                        Width="70px"></asp:Label>
                    <asp:Button ID="btnINFOintegrazione" runat="server" Text="Button" Style="visibility: hidden" />
                    <asp:Button ID="btnINFOintegrato" runat="server" Text="Button" Style="visibility: hidden" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblSAL" runat="server" CssClass="TitoloH1" Style="z-index: 100; left: 8px; top: 88px; text-align: left"
                    Width="50px" Font-Size="8pt"></asp:Label>
                    <asp:Label ID="lblNsal" runat="server" CssClass="TitoloH1" Style="z-index: 100; left: 8px; top: 88px; text-align: left"
                        Width="60px" Font-Size="8pt"></asp:Label>&nbsp;
                <table style="width: 100%">
                    <tr>
                        <td style="width: 73px">
                            <asp:Label ID="lblVocePF" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="70px">Voce PF *</asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbVocePF" Width="70%" AppendDataBoundItems="true" Filter="Contains"
                                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 8%">
                                <asp:Label ID="lblStato" runat="server" Style="z-index: 100; left: 8px; top: 88px">STATO NON PATRIMONIALE:</asp:Label>
                            </td>
                            <td style="width: 10%">
                                <telerik:RadComboBox ID="cmbStato" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 30px;"></td>
                            <td style="width: 11%">
                                <asp:Label ID="lblDataInizio" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                    Width="135px">Data Presunta Inizio Lavori:</asp:Label>
                            </td>
                            <td style="width: 12%">
                                <telerik:RadDatePicker ID="txtDataInizio" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                    ToolTip="DATA PRESUNTA INIZIO LAVORI gg/mm/aaaa" DatePopupButton-Visible="true"
                                    DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                    <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                        Width="70px">
                                        <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                    </DateInput>
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today">
                                                <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td style="width: 12%">
                                <asp:Label ID="lblDataFine" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                    Width="130px">Data Presunta Fine Lavori</asp:Label>
                            </td>
                            <td style="width: 12%">
                                <telerik:RadDatePicker ID="txtDataFine" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                    ToolTip="DATA PRESUNTA INIZIO LAVORI gg/mm/aaaa" DatePopupButton-Visible="true"
                                    DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                    <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                        Width="70px">
                                        <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                    </DateInput>
                                    <Calendar ID="Calendar2" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today">
                                                <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Label ID="lblEsercizioFinanziario" Text="" runat="server" CssClass="TitoloH1"
                                    Font-Size="8pt" Font-Bold="True" />
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop"
                        Width="100%" MultiPageID="RadMultiPage1" ShowBaseLine="true" ScrollChildren="true"
                        OnClientTabSelected="tabSelezionato">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Riepilogo" Value="Riepilogo"
                                Selected="true">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Dettagli" Value="Dettagli">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView3" Text="Motivazioni Annullamento"
                                Value="MotivazioniAnnullamento">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView4" Text="Allegati" Value="Allegati">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView5" Text="Non conformità" Value="Irregolarità">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView6" Text="Eventi" Value="Eventi">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
                        ScrollChildren="true">
                        <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip" Selected="true">
                            <asp:Panel runat="server" ID="tab1">
                                <uc1:Tab_Manu_Riepilogo ID="Tab_Manu_Riepilogo" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab2">
                                <uc2:Tab_Manu_Dettagli ID="Tab_Manu_Dettagli" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab3">
                                <uc3:Tab_Manu_Note ID="Tab_Manu_Note" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView4" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab4">
                                <uc4:Tab_Manu_Allegati ID="Tab_Manu_Allegati" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView5" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab5">
                                <uc5:Tab_Manu_Irregolarita ID="Tab_Manu_Irregolarita1" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView6" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab6">
                                <uc6:Tab_Manu_Eventi ID="Tab_Manu_Eventi1" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
        </table>
        <br />
        <br />
        <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px; visibility: hidden; z-index: -1;">0</asp:TextBox>
        <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
            ForeColor="White" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;">0</asp:TextBox>
        <asp:TextBox ID="txtElimina" runat="server" BackColor="White" BorderStyle="None"
            ForeColor="White" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden; top: 200px">0</asp:TextBox>
        <asp:HiddenField ID="txtOrdine" runat="server"></asp:HiddenField>
        <asp:TextBox ID="txttab" runat="server" ForeColor="White" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;">1</asp:TextBox>
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden; top: 200px"
            Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"></asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden; top: 415px"
            Width="24px">0</asp:TextBox>
        <asp:HiddenField ID="txtVisualizza" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtConfermaPagamento" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="BLOCCATO" runat="server"></asp:HiddenField>
        <asp:TextBox ID="txtIdManutenzione" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"
            Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtIdComplesso" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"
            Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtIdEdificio" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"
            Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtIdScala" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"
            Visible="False"></asp:TextBox>
        <asp:HiddenField ID="txtIdAppalto" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdLotto" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIntegrazione" runat="server" Value="0"></asp:HiddenField>
        <asp:HiddenField ID="txtIntegrato" runat="server" Value="0"></asp:HiddenField>
        <asp:HiddenField ID="txtPercOneri" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtScontoConsumo" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtOneriSicurezza" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtPercIVA_P" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtPercIVA_C" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtSTATO" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtID_Segnalazioni" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtID_Fornitore" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtID_Prenotazione" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtID_Pagamento" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtID_Unita" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdPianoFinanziario" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtAnnoEsercizioF" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdPenale" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtPenaleOLD" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtNettoOneriIVA_TMP" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtNettoOneriIVAC_TMP" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtFL_RIT_LEGGE" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtAppare1" runat="server" Value="0" />
        <asp:HiddenField ID="txtTIPO" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtID_STRUTTURA" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtStatoPF" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtFlagVOCI" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtAnnoManutenzione" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="ANNULLAVISIBILE" runat="server" Value="0"></asp:HiddenField>
        <asp:HiddenField ID="autorizzazione" runat="server" Value="0"></asp:HiddenField>
        <asp:HiddenField ID="operatoreIniziale" runat="server" Value="0"></asp:HiddenField>
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField ID="HFTAB" runat="server" />
        <asp:HiddenField ID="HFAltezzaTab" runat="server" />
        <asp:HiddenField ID="TipoAllegato" runat="server" Value="" />
        <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
        <asp:HiddenField ID="numTab" runat="server" Value="6" />
        <asp:HiddenField ID="HiddenMostraPulsantiDett" runat="server" Value="1" />
        <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" Value="1" />
        </div>
    <asp:HiddenField runat="server" ID="indirizzoEmail" />
        <asp:HiddenField runat="server" ID="oggettoEmail" />
        <asp:HiddenField runat="server" ID="bodyEmail" />


        <asp:HiddenField ID="HiddenFieldIdManutenzione" runat="server" Value=""></asp:HiddenField>
    </form>
    <script language="javascript" type="text/javascript">
        window.focus();
        self.focus();



        //        if (document.getElementById('txtVisualizza').value == '0') {
        //            //BOZZA
        //            document.getElementById('ImgAllegaFile').style.visibility = 'hidden';
        //            document.getElementById('ImgAllegati').style.visibility = 'hidden';
        //        }

        //        if (document.getElementById('txtVisualizza').value == '1') {
        //            //SEMPRE
        //            document.getElementById('ImgAllegaFile').style.visibility = 'visible';
        //            document.getElementById('ImgAllegati').style.visibility = 'visible';
        //        }

        //        if (document.getElementById('txtVisualizza').value == '2') {
        //            //SOLO LETTURA, CONSUNTIVATO, LIQUIDATO, ANNULLATO
        //            document.getElementById('ImgAllegaFile').style.visibility = 'hidden';
        //            document.getElementById('ImgAllegati').style.visibility = 'visible';
        //        }

        //        if (document.getElementById('txtSTATO').value == '0') {
        //            //BOZZA
        //            document.getElementById('ImgStampa').style.visibility = 'hidden';
        //            document.getElementById('ImgEmail').style.visibility = 'hidden';
        //        }

        //        if (document.getElementById('txtSTATO').value == '1') {
        //            //EMESSO
        //            document.getElementById('ImgStampa').style.visibility = 'visible';
        //            document.getElementById('ImgEmail').style.visibility = 'visible';

        //        }

        //        if (document.getElementById('txtSTATO').value == '2') {
        //            //CONSUNTIVO
        //            document.getElementById('ImgStampa').style.visibility = 'visible';
        //            document.getElementById('ImgEmail').style.visibility = 'visible';

        //        }

        //        if (document.getElementById('txtSTATO').value == '3') {
        //            //INTEGRATO
        //            document.getElementById('ImgStampa').style.visibility = 'visible';
        //            document.getElementById('ImgEmail').style.visibility = 'visible';

        //        }

        //        if (document.getElementById('txtSTATO').value == '4') {
        //            //EMESSO PAGAMENTO (ex LIQUIDATO)
        //            document.getElementById('ImgStampa').style.visibility = 'visible';
        //            document.getElementById('ImgEmail').style.visibility = 'visible';

        //        }

        //        if (document.getElementById('txtSTATO').value == '5') {
        //            //ANNULLATO
        //            document.getElementById('ImgStampa').style.visibility = 'hidden';
        //            document.getElementById('ImgEmail').style.visibility = 'hidden';
        //        }

        //        if (document.getElementById("BLOCCATO").value == '1') {
        //            document.getElementById('ImgStampa').style.visibility = 'hidden';
        //            document.getElementById('ImgEmail').style.visibility = 'hidden';
        //        }

        //        if (document.getElementById("txtTIPO").value == '0') {
        //            document.getElementById('Tab_Manu_Riepilogo_btnAggAppalto').style.visibility = 'hidden';
        //            document.getElementById('btn_VisUnita').style.visibility = 'hidden';
        //        }

        //        if (document.getElementById("txtTIPO").value == '1') {
        //            document.getElementById('Tab_Manu_Riepilogo_btnAggAppalto').style.visibility = 'hidden';
        //            document.getElementById('btn_VisUnita').style.visibility = 'visible';
        //        }


        //        if (document.getElementById('txtAppare1').value != '1') {
        //            document.getElementById('DIV_C').style.visibility = 'hidden';
        //        }

        //        if (document.getElementById('ANNULLAVISIBILE').value == '1') {
        //            if (document.getElementById('btnAnnulla') != null) {
        //                document.getElementById('btnAnnulla').style.visibility = 'hidden';
        //            }
        //        }


        //        if (document.getElementById('autorizzazione').value == '0') {
        //            if (document.getElementById('btnElimina') != null) {
        //                document.getElementById('btnElimina').style.visibility = 'hidden';
        //            }
        //            if (document.getElementById('ImgAllegaFile') != null) {
        //                document.getElementById('ImgAllegaFile').style.visibility = 'hidden';
        //            }
        //            if (document.getElementById('ImgAllegati') != null) {
        //                document.getElementById('ImgAllegati').style.visibility = 'hidden';
        //            }
        //            if (document.getElementById('ImgStampa') != null) {
        //                document.getElementById('ImgStampa').style.visibility = 'hidden';
        //            }
        //            if (document.getElementById('ImgEmail') != null) {
        //                document.getElementById('ImgEmail').style.visibility = 'hidden';
        //            }
        //        }

        //        function ApriAllegati() {
        //            // if ( document.getElementById('imgCambiaAmm').style.visibility != 'hidden'){
        //            window.open('ElencoAllegati.aspx?LT=0&COD=<%=vIdManutenzione %>', 'Allegati', '');
        //            //                    }
        //            //                    else{
        //            //                        window.open('ElencoAllegati.aspx?LT=1&COD=19', 'Allegati', '');

        //            //                    }

        //        }

        function AllegaFile() {
            if ('<%=vIdManutenzione %>' == '' || '<%=vIdManutenzione %>' == '0') {
                apriAlert('E\' necessario salvare l\' ordine prima di allegare documenti!', 300, 150, 'Attenzione', null, null);
                //window.open('ElencoAllegati.aspx?T=3&COD=' + document.getElementById('txtIdAppalto').value, 'AllegatiContratto', 'scrollbars=1, width=800px, height=600px, resizable');
                return false;
            }
            else {
                CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=<%=vIdManutenzione %>', 'Allegati', 1000, 800);
            };
        };

        function Eventi() {
            if ('<%=vIdManutenzione %>' == '' || '<%=vIdManutenzione %>' == '0') {
                alert('E\' necessario salvare la manutenzione prima di visualizzare gli eventi!');
            } else {
                CenterPage('Report/Eventi.aspx?ID_MANUTENZIONE=' +<%=vIdManutenzione %>, 'Eventi', 1000, 800);
            };
        };

        function StampaOrdine() {
            window.open('StampaOrdine.aspx?COD=<%=vIdManutenzione %>', 'Ordine', '');
            //document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '1';
        };
        function ApriAllegati() {
            // if ( document.getElementById('imgCambiaAmm').style.visibility != 'hidden'){
            window.open('ElencoAllegati.aspx?LT=0&COD=<%=vIdManutenzione %>', 'Allegati', '');
            //                    }
            //                    else{
            //                        window.open('ElencoAllegati.aspx?LT=1&COD=19', 'Allegati', '');

            //                    }

        }


        function StampaOrdine() {
            window.open('StampaOrdine.aspx?COD=<%=vIdManutenzione %>', 'Ordine', '');
            //document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '1';
        }




    </script>
    <script type="text/javascript">
        $(function () {
            $("#txtDataInizio").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
            $("#txtDataFine").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        });
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
