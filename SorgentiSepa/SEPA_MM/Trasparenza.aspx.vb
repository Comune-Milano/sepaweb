
Partial Class Trasparenza
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            btnChiudi.Attributes.Add("onClick", "javascript:window.close();")

            Dim lsiFrutto As New ListItem("0,00", "0")
            cmbMobiliare.Items.Add(lsiFrutto)
            For i = 1 To 100
                lsiFrutto = New ListItem(par.Converti(Format(i * 5165, "##,##0.00")), Val(i * 5165))
                cmbMobiliare.Items.Add(lsiFrutto)
            Next

            lsiFrutto = New ListItem("2012", 565 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2011", 525 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2010", 401 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2009", 432 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2008", 475 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2007", 441 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2006", 395 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2005", 354 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2004", 429 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2003", 420 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2002", 504 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2001", 513 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("2000", 557 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("1999", 557 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("1998", 557 / 100)
            txtAnno.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("1997", 557 / 100)
            txtAnno.Items.Add(lsiFrutto)

        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        cmbComp.Text = "1"
        cmbMinorenni.Text = "0"
        cmbInvAcc.Text = "0"
        cmbInvNoAcc.Text = "0"
        cmbMaggiorenni.Text = "1"
        txtSpese.Text = "0"
        cmbInv66_100.Text = "0"
        txtReddito.Text = "0"
        txtCanone.Text = "0"
        txtImmobiliare.Text = "0"
        txtDetrazioni.Text = "0"
        txtAccessorie.Text = "0"
        txtMutuo.Text = "0"
        cmbMobiliare.SelectedIndex = -1
        cmbMobiliare.Items(0).Selected = True
        txtAnno.SelectedIndex = -1
        txtAnno.Items(0).Selected = True
    End Sub

    Protected Sub btnElabora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElabora.Click
        CalcolaStampa()
    End Sub

    Private Function CalcolaStampa()

        Dim DETRAZIONI As Long


        Dim INV_100_CON As Integer
        Dim INV_100_NO As Integer
        Dim INV_66_99 As Integer
        Dim TOT_COMPONENTI As Integer
        Dim REDDITO_COMPLESSIVO As Double
        Dim TOT_SPESE As Long
        Dim DETRAZIONI_FRAGILE As Long
        Dim DETRAZIONI_FR As Long

        Dim MOBILI As Double
        Dim TASSO_RENDIMENTO As Double
        Dim FIGURATIVO_MOBILI As Double
        Dim TOTALE_ISEE_ERP As Double
        Dim IMMOBILI As Long
        Dim MUTUI As Long
        Dim IMMOBILI_RESIDENZA As Long
        Dim MUTUI_RESIDENZA As Long
        Dim TOTALE_PATRIMONIO_ISEE_ERP As Double
        Dim TOTALE_IMMOBILI As Long
        Dim LIMITE_PATRIMONIO As Double

        Dim ISR_ERP As Double
        Dim ISP_ERP As Double
        Dim ISE_ERP As Double
        Dim VSE As Double
        Dim ISEE_ERP As Double
        Dim ESCLUSIONE As String


        Dim PARAMETRO_MINORI As Double

        Dim MINORI As Integer
        Dim adulti As Integer

        Dim STRINGA_STAMPA As String
        Dim STRINGA_STAMPA_1 As String
        Dim STRINGA_STAMPA_2 As String
        Dim STRINGA_STAMPA_3 As String
        Dim STRINGA_STAMPA_4 As String
        Dim STRINGA_STAMPA_5 As String
        Dim STRINGA_STAMPA_6 As String
        Dim STRINGA_STAMPA_66 As String
        Dim STRINGA_STAMPA_7 As String
        Dim TIPO_ALLOGGIO As Integer

        Dim Testo_Da_Scrivere As String



        Try

            MINORI = 0
            adulti = 0

            ISR_ERP = 0
            ISP_ERP = 0
            ISE_ERP = 0

            VSE = 0

            TOT_COMPONENTI = 0

            DETRAZIONI = 0
            REDDITO_COMPLESSIVO = 0
            TOT_SPESE = 0
            DETRAZIONI_FRAGILE = 0
            DETRAZIONI_FR = 0
            ISEE_ERP = 0
            MOBILI = 0
            FIGURATIVO_MOBILI = 0

            IMMOBILI = 0
            MUTUI = 0
            IMMOBILI_RESIDENZA = 0
            MUTUI_RESIDENZA = 0
            TOTALE_IMMOBILI = 0

            TOTALE_ISEE_ERP = 0
            TOTALE_PATRIMONIO_ISEE_ERP = 0
            LIMITE_PATRIMONIO = 0

            STRINGA_STAMPA = ""
            STRINGA_STAMPA_1 = ""
            STRINGA_STAMPA_2 = ""
            STRINGA_STAMPA_3 = ""
            STRINGA_STAMPA_4 = ""
            STRINGA_STAMPA_5 = ""
            STRINGA_STAMPA_6 = ""
            STRINGA_STAMPA_66 = ""
            STRINGA_STAMPA_7 = ""
            TIPO_ALLOGGIO = -1

            ESCLUSIONE = ""


            TASSO_RENDIMENTO = txtAnno.SelectedItem.Value

            TOT_COMPONENTI = Val(cmbComp.Text)
            MINORI = cmbMinorenni.Text
            DETRAZIONI = Val(txtDetrazioni.Text)
            REDDITO_COMPLESSIVO = Val(txtReddito.Text)

            DETRAZIONI_FR = Val(txtSpese.Text)

            MOBILI = Val(cmbMobiliare.SelectedItem.Value)

            IMMOBILI = Val(txtImmobiliare.Text)
            MUTUI = Val(txtMutuo.Text)
            INV_100_CON = Val(cmbInvAcc.Text)
            INV_100_NO = Val(cmbInvNoAcc.Text)
            INV_66_99 = Val(cmbInv66_100.Text)
            adulti = Val(cmbMaggiorenni.Text)


            DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)




            FIGURATIVO_MOBILI = MOBILI



            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP
            ISEE_ERP = 0


            TOTALE_IMMOBILI = (IMMOBILI - MUTUI)


            TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * (2 / 10)

            ISP_ERP = TOTALE_ISEE_ERP


            TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)
            'TOTALE_PATRIMONIO_ISEE_ERP = (ISP_ERP + FIGURATIVO_MOBILI)


            Dim PARAMETRO As Double

            Select Case TOT_COMPONENTI
                Case 1
                    PARAMETRO = 1
                Case 2
                    PARAMETRO = (138 / 100)
                Case 3
                    PARAMETRO = (167 / 100)
                Case 4
                    PARAMETRO = (190 / 100)
                Case 5
                    PARAMETRO = (211 / 100)
                Case Else
                    PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
            End Select

            PARAMETRO_MINORI = 0
            VSE = PARAMETRO
            If adulti >= 2 Then
                VSE = VSE - (MINORI * (1 / 10))
                PARAMETRO_MINORI = (MINORI * (1 / 10))
            End If

            LIMITE_PATRIMONIO = 16000 + (6000 * VSE)

            If REDDITO_COMPLESSIVO <> 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> REDDITO COMPLESSIVO DEL NUCLEO FAMILIARE" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            Else
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> Entrate nessun reddito indicato" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If


            If FIGURATIVO_MOBILI > 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> FIGURATIVO REDDITO MOBILIARE" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            If DETRAZIONI <> 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> DETRAZIONI DAL REDDITO LORDO" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>- " & par.Converti(Format(DETRAZIONI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            Else
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> Non ci sono detrazioni" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>0 " _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            End If

            If INV_100_CON > 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> INVALIDI 100% CON INDENNITA'" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & INV_100_CON _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            End If

            If TOT_SPESE > 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> SPESE SOSTENUTE PER ASSISTENZA" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(TOT_SPESE, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            If INV_100_NO > 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> INVALIDI 100% SENZA INDENNITA'" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & INV_100_NO _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            End If

            If INV_66_99 > 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> INVALIDI 66" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & INV_66_99 _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            End If

            If DETRAZIONI_FR > 0 Then
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> Detrazioni per nucleo familiare affetto da fragilità (" & INV_100_CON + INV_100_NO + INV_66_99 & " invalidi)" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>-" & par.Converti(Format(DETRAZIONI_FR, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                         & "<td width='75%' height='23'> TOTALE DEL REDDITO DA CONSIDERARE AI FINI ISEE-erp" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(ISR_ERP, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"



            If FIGURATIVO_MOBILI > 0 Then
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>CONSISTENZA DEL PATRIMONIO MOBILIARE" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(FIGURATIVO_MOBILI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If


            If IMMOBILI > 0 Then
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>CONSISTENZA DEL PATRIMONIO IMMOBILIARE" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            If MUTUI > 0 Then
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>DETRAZIONI PER MUTUI CONTRATTI" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>-" & par.Converti(Format(MUTUI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                         & "<td width='75%' height='23'>TOTALE CONSISTENZA DEL PATRIMONIO IMMOBILIARE" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI - MUTUI, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            If IMMOBILI_RESIDENZA > 0 Then
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>VALORE DELLA RESIDENZA DI PROPRIETA'" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI_RESIDENZA, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            If MUTUI_RESIDENZA > 0 Then
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>MUTUO RESIDUO PER LA RESIDENZA" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(MUTUI_RESIDENZA, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If

            If IMMOBILI_RESIDENZA > 0 Then
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>VALORE NETTO DELLA CASA DI RESIDENZA" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI_RESIDENZA - MUTUI_RESIDENZA, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

            End If



            STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                         & "<td width='75%' height='23'>TOTALE COMPLESSIVO DEL PATRIMONIO DA CONSIDERARE AI FINI ISEE-erp" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(TOTALE_IMMOBILI, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                         & "<td width='75%' height='23'>Coefficiente della valutazione del patrimonio immobiliare" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>0,20" _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            'TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.2

            'ISP_ERP = TOTALE_ISEE_ERP

            STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                         & "<td width='75%' height='23'>VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(TOTALE_ISEE_ERP, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"




            STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                         & "<td width='75%' height='23'>PATRIMONIO COMPLESSIVO AI FINI ERP" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(TOTALE_PATRIMONIO_ISEE_ERP, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
             & "<td width='75%' height='23'>Limite Patrimonio Familiare" _
             & "</td>" _
             & "<td width='13%' height='23'>" _
             & "</td>" _
             & "<td width='13%' height='23'>" _
             & "<table border='1' width='100%'>" _
             & "<tr>" _
             & "<td width='100%' align='right'>" & par.Converti(Format(LIMITE_PATRIMONIO, "##,##0.00")) _
             & "</td>" _
             & "</tr>" _
             & "</table>      </td>   </tr>"



            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>Numero di componenti del nucleo familiare" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & TOT_COMPONENTI _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"



            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>Parametro corrispondente alla composizione del nucleo familiare" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(PARAMETRO) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"

            'If MINORI > 0 Then
            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>Numero di minori di 15 anni nel nucleo familiare" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & MINORI _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            If PARAMETRO_MINORI > 0 Then
                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>- Parametro per minori" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>-" & par.Converti(Format(PARAMETRO_MINORI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            End If


            If adulti >= 1 Then
                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>Numero di adulti nel nucleo familiare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & adulti _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            Else
                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>Non ci sono adulti nel nucleo familiare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>0" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
            End If

            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>REDDITO DA CONSIDERARE AI FINI ISEE-erp" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(ISR_ERP, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(ISP_ERP, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            ISE_ERP = ISR_ERP + ISP_ERP

            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>ISE-erp: indicatore della situazione economica (erp)" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(Format(ISE_ERP, "##,##0.00")) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"



            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'>VSE: Valore della scala di equivalenza" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Converti(VSE) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            ISEE_ERP = ISE_ERP / VSE

            If ISEE_ERP <= 16000 Then
                TIPO_ALLOGGIO = 0
            Else
                If ISEE_ERP > 16000 And ISE_ERP <= 17000 Then
                    TIPO_ALLOGGIO = 0
                Else
                    If ISEE_ERP <= 40000 And ISE_ERP > 17000 Then
                        TIPO_ALLOGGIO = 1
                    Else
                        ESCLUSIONE = "<li><p align='left'>ISEE superiore al limite ERP</li>"
                    End If
                End If
            End If

            'If ISP_ERP > LIMITE_PATRIMONIO Then
            If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO Then
                ESCLUSIONE = ESCLUSIONE & "<li><p align='left'>Limite Patrimoniale superato</li>"
            End If


            If ESCLUSIONE <> "" Then
                Testo_Da_Scrivere = "<b>DOMANDA ESCLUSA</b>"
            End If

            STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                         & "<td width='75%' height='23'><b>ISEE-erp:</b> Indicatore della Situazione Economica Equivalente (erp)" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'><b>" & par.Converti(Format(ISEE_ERP, "##,##0.00")) _
                         & "</b></td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"
          

            If TIPO_ALLOGGIO = 0 Then
                STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                         & "<td width='75%' height='23'>ISEE intermedio per canone sociale" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>16.000,00" _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"

            End If

            If TIPO_ALLOGGIO = 1 Then
                STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                         & "<td width='75%' height='23'>ISEE intermedio per canone sociale" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>17.000,00" _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"
            End If

            Dim Totale_Risultati As Long

            Totale_Risultati = 0

            Dim F1 As Double
            Dim F2 As Double
            Dim F3 As Double

            Dim F10 As Double
            Dim F20 As Double
            Dim F30 As Double

            F1 = 0
            F2 = 0
            F3 = 0

            F10 = 0
            F20 = 0
            F30 = 0












            Dim a1 As Double
            Dim A2 As Double
            Dim A3 As Double
            Dim A4 As Double

            Dim A10 As Double
            Dim A20 As Double
            Dim A30 As Double
            Dim A40 As Double

            a1 = 0
            A2 = 0
            A3 = 0
            A4 = 0

            A10 = 0
            A20 = 0
            A30 = 0
            A40 = 0




            Dim canone_int As Double



            canone_int = 0
            If Val(txtCanone.Text) <> 0 Then
                canone_int = CDbl(Val(txtCanone.Text))
            End If
            If Val(txtAccessorie.Text) <> 0 Then
                If Int(txtAccessorie.Text) > 516 Then
                    canone_int = canone_int + 516
                Else
                    canone_int = canone_int + Int(txtAccessorie.Text)
                End If
            End If


            'If ESCLUSIONE = "" Then
            'If canone_int <> 0 Then
            STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                     & "<td width='75%' height='23'>Canone Integrato" _
                     & "</td>" _
                     & "<td width='13%' height='23'>" _
                     & "</td>" _
                     & "<td width='13%' height='23'>" _
                     & "<table border='1' width='100%'>" _
                     & "<tr>" _
                     & "<td width='100%' align='right'>" & par.Converti(Format(canone_int, "##,##0.00")) _
                     & "</td>" _
                     & "</tr>" _
                     & "</table>      </td>   </tr>"


            Dim C1 As Double
            Dim C2 As Double
            Dim LIMITE_CANONE As Double


            C1 = canone_int + ((canone_int / 100) * 5)



            C2 = ISE_ERP / VSE ' PARAMETRO

            Select Case C2
                Case 0 To 4000
                    LIMITE_CANONE = (ISE_ERP / 100) * 8
                Case (400001 / 100) To 4500
                    LIMITE_CANONE = (ISE_ERP / 100) * 9
                Case (450001 / 100) To 5000
                    LIMITE_CANONE = (ISE_ERP / 100) * 10
                Case (500001 / 100) To 5500
                    LIMITE_CANONE = (ISE_ERP / 100) * 11
                Case (550001 / 100) To 6000
                    LIMITE_CANONE = (ISE_ERP / 100) * 12
                Case (600001 / 100) To 6500
                    LIMITE_CANONE = (ISE_ERP / 100) * 13
                Case (650001 / 100) To 7000
                    LIMITE_CANONE = (ISE_ERP / 100) * 14
                Case (700001 / 100) To 7500
                    LIMITE_CANONE = (ISE_ERP / 100) * 15
                Case (750001 / 100) To 8000
                    LIMITE_CANONE = (ISE_ERP / 100) * 16
                Case (800001 / 100) To 8500
                    LIMITE_CANONE = (ISE_ERP / 100) * 17
                Case (850001 / 100) To 9000
                    LIMITE_CANONE = (ISE_ERP / 100) * 18
                Case (900001 / 100) To 9500
                    LIMITE_CANONE = (ISE_ERP / 100) * 19
                Case (950001 / 100) To 10000
                    LIMITE_CANONE = (ISE_ERP / 100) * 20
                Case (1000001 / 100) To 10500
                    LIMITE_CANONE = (ISE_ERP / 100) * 21
                Case (1050001 / 100) To 11000
                    LIMITE_CANONE = (ISE_ERP / 100) * 22
                Case (1100001 / 100) To 11500
                    LIMITE_CANONE = (ISE_ERP / 100) * 23
                Case (1150001 / 100) To 12000
                    LIMITE_CANONE = (ISE_ERP / 100) * 24
                Case (1200001 / 100) To 12500
                    LIMITE_CANONE = (ISE_ERP / 100) * (245 / 10)
                Case (1250001 / 100) To 13000
                    LIMITE_CANONE = (ISE_ERP / 100) * 25
                Case (1300001 / 100) To 13500
                    LIMITE_CANONE = (ISE_ERP / 100) * (255 / 10)
                Case (1350001 / 100) To 14000
                    LIMITE_CANONE = (ISE_ERP / 100) * 26
                Case (1400001 / 100) To 14500
                    LIMITE_CANONE = (ISE_ERP / 100) * (265 / 10)
                Case (1450001 / 100) To 15000
                    LIMITE_CANONE = (ISE_ERP / 100) * 27
                Case 15000.01 To 15500
                    LIMITE_CANONE = (ISE_ERP / 100) * (275 / 10)
                Case (1550001 / 100) To 16000
                    LIMITE_CANONE = (ISE_ERP / 100) * 28
                Case (1600001 / 100) To 16500
                    LIMITE_CANONE = (ISE_ERP / 100) * (285 / 10)
                Case Is >= (1650001 / 100)
                    LIMITE_CANONE = (ISE_ERP / 100) * 29
            End Select

            STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                     & "<td width='75%' height='23'>Limite Affitto oneroso (con rivalutazione del 5%)" _
                     & "</td>" _
                     & "<td width='13%' height='23'>" _
                     & "</td>" _
                     & "<td width='13%' height='23'>" _
                     & "<table border='1' width='100%'>" _
                     & "<tr>" _
                     & "<td width='100%' align='right'>" & par.Converti(Format(LIMITE_CANONE + ((LIMITE_CANONE * 5) / 100), "##,##0.00")) _
                     & "</td>" _
                     & "</tr>" _
                     & "</table>      </td>   </tr>"



            If canone_int > LIMITE_CANONE + ((LIMITE_CANONE * 5) / 100) Then
                STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                         & "<td width='75%' height='23'><b>SUSSISTE</b> la condizione di Affitto oneroso" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>SI" _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"


            Else
                STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                         & "<td width='75%' height='23'>SUSSISTE la condizione di Affitto oneroso" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>NO" _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"

            End If
            'End If
            'End If


            Dim DISAGIO_F As Double
            Dim DISAGIO_A As Double
            Dim DISAGIO_E As Double

            Dim DISAGIO_F0 As Double
            Dim DISAGIO_A0 As Double
            Dim DISAGIO_E0 As Double


            DISAGIO_F = 0
            DISAGIO_A = 0
            DISAGIO_E = 0

            DISAGIO_F0 = 0
            DISAGIO_A0 = 0
            DISAGIO_E0 = 0

            If F1 > F2 Then
                If F1 > F3 Then
                    DISAGIO_F = F1
                Else
                    DISAGIO_F = F3
                End If
            Else
                If F2 > F3 Then
                    DISAGIO_F = F2
                Else
                    DISAGIO_F = F3
                End If
            End If


            If F10 > F20 Then
                If F10 > F30 Then
                    DISAGIO_F0 = F10
                Else
                    DISAGIO_F0 = F30
                End If
            Else
                If F20 > F30 Then
                    DISAGIO_F0 = F20
                Else
                    DISAGIO_F0 = F30
                End If
            End If


            If A10 > A20 Then
                If A10 > A30 Then
                    If A10 > A40 Then
                        DISAGIO_A0 = A10
                    Else
                        DISAGIO_A0 = A40
                    End If
                Else
                    If A30 > A40 Then
                        DISAGIO_A0 = A30
                    Else
                        DISAGIO_A0 = A40
                    End If
                End If
            Else
                If A20 > A30 Then
                    If A20 > A40 Then
                        DISAGIO_A0 = A20
                    Else
                        DISAGIO_A0 = A40
                    End If
                Else
                    If A30 > A40 Then
                        DISAGIO_A0 = A30
                    Else
                        DISAGIO_A0 = A40
                    End If
                End If
            End If

            If a1 > A2 Then
                If a1 > A3 Then
                    If a1 > A4 Then
                        DISAGIO_A = a1
                    Else
                        DISAGIO_A = A4
                    End If
                Else
                    If A3 > A4 Then
                        DISAGIO_A = A3
                    Else
                        DISAGIO_A = A4
                    End If
                End If
            Else
                If A2 > A3 Then
                    If A2 > A4 Then
                        DISAGIO_A = A2
                    Else
                        DISAGIO_A = A4
                    End If
                Else
                    If A3 > A4 Then
                        DISAGIO_A = A3
                    Else
                        DISAGIO_A = A4
                    End If
                End If
            End If

            'If DISAGIO_A0 > 0 Then
            '    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            '             & "<td width='75%' height='23'>Valutazione Intermedia a)" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "<table border='1' width='100%'>" _
            '             & "<tr>" _
            '             & "<td width='100%' align='right'>" & DISAGIO_A0 _
            '             & "</td>" _
            '             & "</tr>" _
            '             & "</table>      </td>   </tr>"
            'End If

            'If DISAGIO_F0 > 0 Then
            '    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            '             & "<td width='75%' height='23'>Valutazione Intermedia f)" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "<table border='1' width='100%'>" _
            '             & "<tr>" _
            '             & "<td width='100%' align='right'>" & DISAGIO_F0 _
            '             & "</td>" _
            '             & "</tr>" _
            '             & "</table>      </td>   </tr>"
            'End If


            'DISAGIO_A = (DISAGIO_A / 100) * 0.8
            'DISAGIO_F = (DISAGIO_F / 100) * 0.5
            'DISAGIO_E = ((17000 - ISEE_ERP) / 17000) * 0.3

            'DISAGIO_A0 = (DISAGIO_A0 / 100) * 0.8
            'DISAGIO_F0 = (DISAGIO_F0 / 100) * 0.5
            'DISAGIO_E0 = ((17000 - ISEE_ERP) / 17000) * 0.3


            ''If DISAGIO_A0 > 0 Then
            ''    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            ''             & "<td width='75%' height='23'>a) Indicatore disagio Abitativo" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "<table border='1' width='100%'>" _
            ''             & "<tr>" _
            ''             & "<td width='100%' align='right'>" & par.Tronca(DISAGIO_A0) _
            ''             & "</td>" _
            ''             & "</tr>" _
            ''             & "</table>      </td>   </tr>"
            ''Else
            ''    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            ''             & "<td width='75%' height='23'>a) Indicatore disagio Abitativo Nessun disagio riportato" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "<table border='1' width='100%'>" _
            ''             & "<tr>" _
            ''             & "<td width='100%' align='right'>0,000" _
            ''             & "</td>" _
            ''             & "</tr>" _
            ''             & "</table>      </td>   </tr>"
            ''    DISAGIO_A = 0
            ''End If

            'If DISAGIO_E0 > 0 Then
            '    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            '             & "<td width='75%' height='23'>e) Indicatore disagio economico" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "<table border='1' width='100%'>" _
            '             & "<tr>" _
            '             & "<td width='100%' align='right'>" & par.Tronca(DISAGIO_E0) _
            '             & "</td>" _
            '             & "</tr>" _
            '             & "</table>      </td>   </tr>"
            'Else
            '    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            '             & "<td width='75%' height='23'>e) Indicatore disagio economico Nessun disagio Riportato" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "</td>" _
            '             & "<td width='13%' height='23'>" _
            '             & "<table border='1' width='100%'>" _
            '             & "<tr>" _
            '             & "<td width='100%' align='right'>0,000" _
            '             & "</td>" _
            '             & "</tr>" _
            '             & "</table>      </td>   </tr>"
            '    DISAGIO_E = 0

            'End If

            ''If DISAGIO_F0 > 0 Then
            ''    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            ''             & "<td width='75%' height='23'>a) Indicatore disagio familiare" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "<table border='1' width='100%'>" _
            ''             & "<tr>" _
            ''             & "<td width='100%' align='right'>" & par.Tronca(DISAGIO_F0) _
            ''             & "</td>" _
            ''             & "</tr>" _
            ''             & "</table>      </td>   </tr>"
            ''Else
            ''    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
            ''             & "<td width='75%' height='23'>f) Indicatore disagio familiare Nessun disagio riportato" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "</td>" _
            ''             & "<td width='13%' height='23'>" _
            ''             & "<table border='1' width='100%'>" _
            ''             & "<tr>" _
            ''             & "<td width='100%' align='right'>0,000" _
            ''             & "</td>" _
            ''             & "</tr>" _
            ''             & "</table>      </td>   </tr>"
            ''    DISAGIO_F = 0
            ''End If

            Dim ISBARC As Double
            Dim ISBARC_R As Double
            Dim ISBAR As Double
            Dim VALORE_R As Double
            Dim T As Double
            Dim T1 As Double
            Dim r As Double

            If ESCLUSIONE = "" Then

                T = (1 - ((1 - DISAGIO_A0) * (1 - DISAGIO_F0) * (1 - DISAGIO_E0)))
                ISBARC = T * 10000
                T1 = (1 - ((1 - DISAGIO_A) * (1 - DISAGIO_F) * (1 - DISAGIO_E)))
                ISBAR = T1 * 10000



                'Select Case CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Value
                '    Case 0
                VALORE_R = 0
                '    Case 1
                'VALORE_R = 0
                '    Case 2
                'VALORE_R = 40
                '    Case 3
                'VALORE_R = 85
                '    Case 4
                'VALORE_R = 0
                'End Select
                r = (VALORE_R / 100) * 0.3

                ISBARC_R = (1 - ((1 - T) * (1 - r))) * 10000

                STRINGA_STAMPA_6 = "" 'par.Tronca(ISBARC)
                STRINGA_STAMPA_5 = "" 'par.Tronca(ISBAR)
                STRINGA_STAMPA_66 = "" 'par.Tronca(ISBARC_R)

            Else
                ISBARC = 0
                ISBAR = 0
                ISBARC_R = 0

                STRINGA_STAMPA_6 = "" '"0,000"
                STRINGA_STAMPA_5 = "" '"0,000"
                STRINGA_STAMPA_66 = "" '"0,000"

            End If

            If ESCLUSIONE <> "" Then
                STRINGA_STAMPA_7 = "" '"<p align='left'><b>Motivo:</b></p><ul>" & ESCLUSIONE & "</ul>"
            End If


            '***INIZIO STAMPA

            'Dim DATI_ANAGRAFICI As String
            'Dim sa1 As String
            'Dim sA2 As String
            'Dim sA3 As String
            'Dim sA4 As String
            'Dim k1 As String
            'Dim i1 As String
            'Dim i2 As String
            'Dim m1 As String
            'Dim m2 As String
            'Dim sc1 As String
            'Dim sc2 As String
            'Dim sc3 As String
            'Dim sc4 As String
            'Dim sc5 As String
            'Dim sc6 As String
            'Dim i1a As String
            'Dim i1b As String
            'Dim i1c As String
            'Dim i2a As String
            'Dim i2b As String
            'Dim i2c As String
            'Dim i2d As String
            'Dim i3a As String
            'Dim i3b As String
            'Dim i3c As String
            'Dim i3d As String
            'Dim i3e As String
            'Dim i4a As String
            'Dim i4b As String
            'Dim i4c As String
            'Dim i5a As String
            'Dim i5b As String
            'Dim i5c As String
            'Dim i5d As String
            'Dim i6a As String
            'Dim i6b As String
            'Dim i6c As String
            'Dim i7a As String
            'Dim i7b As String
            'Dim i7c As String
            'Dim i8a1 As String
            'Dim i8a2 As String
            'Dim i8b As String
            'Dim i8c As String
            'Dim i8d As String

            Dim LOCAZIONE As String
            Dim ACCESSORIE As String




            Dim SITUAZIONE_REDDITUALE As String
            Dim SITUAZIONE_PATRIMONIALE As String

            Dim sISEE_ERP As String
            Dim RISULTATI_INTERMEDI As String
            Dim sISBARC_R As String
            Dim sISBARC As String
            Dim testo As String
            Dim Motivo As String


            Dim PUNTEGGI_INTERMEDI As String
            Dim sISBAR As String




            SITUAZIONE_REDDITUALE = STRINGA_STAMPA
            SITUAZIONE_PATRIMONIALE = STRINGA_STAMPA_1
            sISEE_ERP = STRINGA_STAMPA_2
            RISULTATI_INTERMEDI = STRINGA_STAMPA_3
            PUNTEGGI_INTERMEDI = STRINGA_STAMPA_4
            sISBAR = STRINGA_STAMPA_5
            sISBARC = STRINGA_STAMPA_6
            sISBARC_R = STRINGA_STAMPA_66
            Motivo = STRINGA_STAMPA_7

            If TIPO_ALLOGGIO = 0 Then
                If STRINGA_STAMPA_7 = "" Then
                    testo = "" '"<b>DOMANDA IDONEA per assegnazione di alloggi di cui all'articolo 1, comma 3, lettera a).</b>"
                Else
                    testo = "" '"<b>DOMANDA ESCLUSA</b>"
                End If
            Else
                If STRINGA_STAMPA_7 = "" Then
                    testo = "" '"<b>DOMANDA IDONEA per assegnazione di alloggi di cui all'articolo 1, comma 3, lettera b).</b>"
                Else
                    testo = "" '"<b>DOMANDA ESCLUSA</b>"
                End If
            End If


            LOCAZIONE = Val(txtCanone.Text)
            ACCESSORIE = Val(txtAccessorie.Text)



            Dim CANONE_LOCAZIONE As String

            'If Val(i2) > 0 Then
            '    CANONE_LOCAZIONE = " < TR ><td><font face='Arial' size='2'>3)</font></td>" _
            '                     & "<TD colspan='3'><font face='Arial' size='2'>che per l'abitazione occupata in locazione" _
            '                     & " come residenza principale al momento di presentazione" _
            '                     & " della domanda il canone di locazione per" _
            '                     & " l'anno &lt;" & i1 & "&gt; è di euro " & i2 & ";</font></td>" _
            '                     & "</tr>" _
            '                     & "<tr>" _
            '                     & "<td><font face='Arial' size='2'>4)</font></td>" _
            '                     & "<TD colspan='3'><font face='Arial' size='2'>che per l'abitazione di cui al comma precedente" _
            '                     & " le spese accessorie di competenza per l'anno" _
            '                     & "&lt;" & m1 & "&gt; sono di euro " & m2 & ";</font></td>" _
            '                     & "</tr>"
            'Else
            CANONE_LOCAZIONE = ""
            'End If


            Dim SstringaSql As String


            SstringaSql = ""
            SstringaSql = SstringaSql & "<html xmlns='http://www.w3.org/1999/xhtml'>"
            SstringaSql = SstringaSql & "<head>"
            SstringaSql = SstringaSql & "<meta http-equiv='Content-Style-Type' content='text/css'>"
            SstringaSql = SstringaSql & "<title>Stampa ERP Comune di Milano</title>"
            SstringaSql = SstringaSql & "<title>SEPA@Web</title></head>"
            SstringaSql = SstringaSql & "<BODY>"
            SstringaSql = SstringaSql & ""
            SstringaSql = SstringaSql & ""
            SstringaSql = SstringaSql & "<div align='center'>"
            SstringaSql = SstringaSql & "<center>"
            SstringaSql = SstringaSql & ""
            SstringaSql = SstringaSql & "<p class='mini'><font face='Arial' size='2'>&nbsp;</font></p>"
            SstringaSql = SstringaSql & "<CENTER><H2><b><font face='Arial' size='5'>COMUNE DI MILANO ACCESSO ERP</font></b></H2></CENTER>"
            SstringaSql = SstringaSql & "<p align=left>ANNO:" & txtAnno.SelectedItem.Text & "</p><table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'>"
            SstringaSql = SstringaSql & "<tr>"
            SstringaSql = SstringaSql & "<td width='75%' height='23'><b><font face='Arial' size='2'>SITUAZIONE REDDITUALE DEL NUCLEO FAMILIARE</font></b>"
            SstringaSql = SstringaSql & ""
            SstringaSql = SstringaSql & SITUAZIONE_REDDITUALE
            SstringaSql = SstringaSql & ""
            SstringaSql = SstringaSql & "</table>"
            SstringaSql = SstringaSql & ""
            SstringaSql = SstringaSql & "<table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'>"
            SstringaSql = SstringaSql & "<tr><td width='75%' height='23'><b><font face='Arial' size='2'>SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</font></b>"
            SstringaSql = SstringaSql & SITUAZIONE_PATRIMONIALE
            SstringaSql = SstringaSql & "</table>"
            SstringaSql = SstringaSql & "<table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='2'>DETERMINAZIONE"
            SstringaSql = SstringaSql & "DELL'ISEE-erp </font></b>" & sISEE_ERP & "</td>   </table><table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='2'>Risultati Intermedi"
            SstringaSql = SstringaSql & "</font></b>" & RISULTATI_INTERMEDI & "</td>   </table><table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='2'>"
            SstringaSql = SstringaSql & "</font></b>" & PUNTEGGI_INTERMEDI & "</td>   </table>" ' <table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='2'></font></b></td><tr><td width='75%' height='23'><font face='Arial' size='2'></font>  </td>      <td width='13%' height='23'></td>      <td width='13%' height='23'>         <table border='1' width='100%'>            <tr></td>   </tr><tr><td width='75%' height='23'><b><font face='Arial' size='2'></font></b>  </td>      <td width='13%' height='23'></td>      <td width='13%' height='23'>"
            'SstringaSql = SstringaSql & "<table border='1' width='100%'>"
            'SstringaSql = SstringaSql & "<tr>"
            'SstringaSql = SstringaSql & "<td width='100%'>"
            'SstringaSql = SstringaSql & "<p align='right'> <b><font face='Arial' size='2'>" & sISBARC & "</font></b></p>"
            'SstringaSql = SstringaSql & "</td>"
            'SstringaSql = SstringaSql & "</tr>"
            'SstringaSql = SstringaSql & "</table>"
            SstringaSql = SstringaSql & "</td>   </tr>     </table>   <P align='left'>" & testo & "   </P><P align='left'>" & Motivo & "</p>"  '<font face='Arial' size='2'>La presente domanda è stata valutata sulla base della dichiarazione sostitutiva unica con numero protocollo "
            SstringaSql = SstringaSql & "<b></b>"
            SstringaSql = SstringaSql & "<P align='left'>&nbsp;</p><P align='left'>&nbsp;</p><P align='left'>&nbsp;</p><p align='left'>Data:" & Format(Now, "dd/MM/yyyy") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            SstringaSql = SstringaSql & " Firma</p> "
            SstringaSql = SstringaSql & "<p align='left'>&nbsp;&nbsp;"
            SstringaSql = SstringaSql & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            SstringaSql = SstringaSql & "------------------------------</p>"
            SstringaSql = SstringaSql & "<P><HR><font face='Arial' size='2'><br> </font><HR>"
            SstringaSql = SstringaSql & "<p align='center'><font face='Arial' size='2'></font></BODY>"


            HttpContext.Current.Session.Add("TRASPARENZA", SstringaSql)
            'Response.Redirect("StampaTrasparenza.aspx")
            Response.Write("<script>window.open('StampaTrasparenza.aspx','Trasparenza','scrollbars=yes,resizable=yes,width=600,height=450,status=no,location=no,toolbar=no,top=0,left=0');</script>")




            'par.myTrans.Commit()
            'par.myTrans = par.OracleConn.BeginTransaction()
            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        Catch ex As Exception
            Label1.Text = ex.ToString
            'par.myTrans.Rollback()
            'par.myTrans = par.OracleConn.BeginTransaction()
            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        Finally

        End Try

    End Function
End Class
