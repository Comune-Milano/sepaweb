Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Math


Partial Class Contratti_CONTRATTI_LIGHT_ContrattoLight
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tab1 As String
    Public Tab2 As String
    Public Tab3 As String
    Public Tab4 As String
    Public Tab5 As String
    Public Tab6 As String
    Public Tab7 As String
    Public Tab8 As String
    Public Tab9 As String

    Public Visibile As String = ""

    Public CodContratto As String
    Public StampaContratto As String

    Private Property myReader22 As Oracle.DataAccess.Client.OracleDataReader

    Private Sub CercaAbusivi()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO =" & lIdContratto
            Dim myReaderAb As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderAb.Read Then
                au_abusivi.Value = "1"
            End If
            myReaderAb.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Response.Expires = 0
        Dim ValorePassato As String = par.DeCriptaMolto(Request.QueryString("ID"))
        If Mid(ValorePassato, 1, 10) <> Format(Now, "yyyyMMddHH") Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Response.Write("<div id='dvvvPre' style='position: absolute; width: 100%; height: 100%; top: 0px;" _
                        & "        left: 0px; background-color: #f0f0f0; visibility: visible; z-index: 500; display: block;'>" _
                        & "        <img src='../../ImmDiv/DivUscitaInCorso2.jpg' alt='caricamento in corso...' style='position: absolute;" _
                        & "            top: 125px; left: 203px' />" _
                        & "    </div>")



            cmbintestazione.Value = "0"



            lIdConnessione = Format(Now, "yyyyMMddHHmmss")
            LetteraProvenienza = Request.QueryString("Lett")

            Session.Add("CONTRATTOAPERTO", "1")
            Session.Add("OPERATORE_AU_LIGHT", "1")

            Dim CRU As String = Mid(ValorePassato, InStr(ValorePassato, "#") + 1, 19)
            Dim SessioneLavoro As String = Trim(Mid(ValorePassato, InStr(ValorePassato, "@") + 1, 100))
            If Len(CRU) <> 19 Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Redirect("~/AccessoNegato.htm", True)
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT * FROM AU_LIGHT_CONCESSIONI WHERE CODICE='" & SessioneLavoro & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = False Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Redirect("~/AccessoNegato.htm", True)
                    Exit Sub
                Else
                    par.cmd.CommandText = "DELETE FROM AU_LIGHT_CONCESSIONI WHERE CODICE='" & SessioneLavoro & "'"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT  ID FROM SISCOM_MI.rapporti_utenza WHERE COD_CONTRATTO='" & CRU & "'"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    lIdContratto = myReaderA("id")

                End If
                myReaderA.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Response.Flush()



            lIdDichiarazione = Request.QueryString("IdDichiarazione")


            'VerificaRUModifica()


            SolaLettura = "1"
            lettura.Value = "1"
            Rateizza.Value = "0"
            spostaAnnulla.Value = "0"
            Session.Remove("CONT_LETTURA_RU")
            controllaEsistBozza.Value = "0"


            If TipoContratto = "7" Then
                LBLABUSIVO.Visible = True
            End If


            CType(Tab_Bollette1.FindControl("txtConnessione"), HiddenField).Value = CStr(lIdConnessione)
            CType(Tab_Bollette1.FindControl("txtIdContratto"), HiddenField).Value = CStr(lIdContratto)

            'visualizza contratto
            lblContratto.Text = "Rapporto Cod. " & CRU
            CodContratto1 = CRU

            FL_NuovoContratto = "0"
            Session.Item("LAVORAZIONE") = "1"
            VisualizzaContratto()
            Tab1 = "tabbertabdefault"
            Tab2 = ""
            Tab3 = ""
            Tab4 = ""
            Tab5 = ""
            Tab6 = ""
            Tab7 = ""
            Tab8 = ""
            Tab9 = ""
            Visibile = "style=" & Chr(34) & "visibility:visible" & Chr(34)

            If CType(Tab_Contratto1.FindControl("chkTemporanea"), CheckBox).Checked = True Then
                LBLErpModerato.Visible = True
                LBLErpModerato.Text = "Assegn. Temporanea"
            Else
                If LetteraERP <> "B" Then
                    Select Case TipoContratto
                        Case "8"
                            LBLErpModerato.Visible = True
                            LBLErpModerato.Text = "(ART.22 C.10 RR 1/2004)"
                        Case "10"
                            LBLErpModerato.Visible = True
                            LBLErpModerato.Text = "(Forze dell'Ordine)"
                        Case "12"
                            LBLErpModerato.Visible = True
                            LBLErpModerato.Text = "(Canone Convenzionato)"
                        Case "1"
                            LBLErpModerato.Visible = True
                            LBLErpModerato.Text = "(E.R.P. Sociale)"
                        Case "6"
                            If LetteraProvenienza = "V" Then
                                LBLABUSIVO.Visible = True
                                LBLABUSIVO.Text = "(ART.15 C.2 RR 1/2004)"
                            End If
                        Case Else
                            LBLErpModerato.Visible = False
                    End Select
                Else
                    LBLErpModerato.Visible = False
                End If
            End If
        End If

        If cmbintestazione.Value = "OK" Then
            CaricaDati()
            cmbintestazione.Value = ""
        End If

        Tab1 = "tabbertabdefault"
        Tab2 = ""
        Tab3 = ""
        Tab4 = ""
        Tab5 = ""
        Tab6 = ""
        Tab7 = ""
        Tab8 = ""
        Tab9 = ""
        Visibile = "style=" & Chr(34) & "visibility:visible" & Chr(34)

        RinnovoUSD.Value = "0"
        CambioBox.Value = "0"
        GLLETTURA.Value = "1"
        AULETTURA.Value = "1"
        Dim I As Integer


        HStatoContratto.Value = CType(Generale1.FindControl("lblStato"), Label).Text

        CType(Generale1.FindControl("btnArchivio"), System.Web.UI.WebControls.Image).Attributes.Add("onClick", "javascript:ApriSchedaArchivio(" & par.CriptaMolto("LETTURA") & ");")

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key1", "<script>MakeStaticHeader('" + CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).ClientID + "', 180, 1130 , 20 ,true,1); </script>", False)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key2", "<script>MakeStaticHeader('" + CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).ClientID + "', 220, 1130 , 25 ,true,0); </script>", False)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key3", "<script>MakeStaticHeader('" + CType(Tab_SchemaBollette1.FindControl("DataGridSchema"), DataGrid).ClientID + "', 250, 1000 , 25 ,true,2); </script>", False)
    End Sub

    Private Sub VerificaRUModifica()
        par.OracleConn.Open()
        par.SettaCommand(par)

        par.cmd.CommandText = "select valore from PARAMETER where ID=123"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderA.Read Then
            If par.IfNull(myReaderA("VALORE"), "0") = "0" Then
                Session.Add("CONT_LETTURA_RU", "1")
                Response.Write("<script>alert('Attenzione...Il contratto sarà visualizzato in sola lettura.');</script>")
            Else
                Session.Add("CONT_LETTURA_RU", "0")
            End If
        End If
        myReaderA.Close()

        par.OracleConn.Close()

    End Sub

    Public Sub CaricaTabBollette()
        Try



            Dim ConnAperta As Boolean = False
            Dim num_bolletta As String = ""
            Dim I As Integer = 0
            Dim importobolletta As Decimal = 0
            Dim importopagato As Decimal = 0
            Dim residuo As Decimal = 0
            Dim morosita As Integer = 0
            Dim riclass As Integer = 0
            Dim indiceMorosita As Integer = 0
            Dim indiceBolletta As Integer = 0
            Dim storno As Integer = 0
            Dim da1 As Oracle.DataAccess.Client.OracleDataAdapter
            Dim NoteBolletta As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                ConnAperta = True
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            SaldoContratto = par.CalcolaSaldoAttuale(lIdContratto)
            CType(Tab_Bollette1.FindControl("lblSaldoCont"), Label).Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:document.getElementById('USCITA').value='1';window.open('DatiUtenza.aspx?C=RisUtenza&IDANA=" & txtCodAffittuario.Value & "&IDCONT=" & lIdContratto & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(par.CalcolaSaldoAttuale(lIdContratto), "##,##0.00") & " €</a>"
            'CType(Tab_Bollette1.FindControl("lblImpRateizzato"), Label).Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:document.getElementById('USCITA').value='1';window.open('../../RATEIZZAZIONE/RateizzEmesse.aspx?idcont=" & lIdContratto & "','Rateizzazioni','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(par.CalcolaImportoRateizzato(lIdContratto), "##,##0.00") & " €</a>"

            par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.* from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) and bol_bollette.id_contratto=" & lIdContratto & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.riferimento_da desc,BOL_BOLLETTE.riferimento_a desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"

            da1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable
            Dim dt1 As New Data.DataTable
            Dim rowDT As System.Data.DataRow

            dt1.Columns.Add("id")
            dt1.Columns.Add("num_tipo")
            dt1.Columns.Add("riferimento_da")
            dt1.Columns.Add("riferimento_a")
            dt1.Columns.Add("data_emissione")
            dt1.Columns.Add("data_scadenza")
            dt1.Columns.Add("importo_totale")
            dt1.Columns.Add("importobolletta")
            dt1.Columns.Add("imp_pagato")
            dt1.Columns.Add("imp_residuo")
            dt1.Columns.Add("data_pagamento")
            dt1.Columns.Add("note")
            dt1.Columns.Add("fl_mora")
            dt1.Columns.Add("fl_rateizz")
            dt1.Columns.Add("id_tipo")
            dt1.Columns.Add("dettagli")
            dt1.Columns.Add("anteprima")
            dt1.Columns.Add("importo_ruolo")

            da1.Fill(dtQuery)
            da1.Dispose()

            Dim TOTimportobolletta As Decimal = 0
            Dim TOTimportopagato As Decimal = 0
            Dim TOTimportoresiduo As Decimal = 0
            Dim TOTimportoEmesso As Decimal = 0
            Dim TOTImpRuolo As Decimal = 0
            For Each row As Data.DataRow In dtQuery.Rows
                indiceMorosita = 0
                indiceBolletta = 0
                rowDT = dt1.NewRow()

                Select Case par.IfNull(row.Item("n_rata"), "")
                    Case "99" 'bolletta manuale
                        num_bolletta = "MA"
                    Case "999" 'bolletta automatica
                        num_bolletta = "AU"
                    Case "99999" 'bolletta di conguaglio
                        num_bolletta = "CO"
                    Case Else
                        num_bolletta = Format(par.IfNull(row.Item("n_rata"), "??"), "00")
                End Select

                importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_B"), 0) '
                If par.IfNull(row.Item("FL_ANNULLATA"), 0) = 0 Or (par.IfNull(row.Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(row.Item("data_pagamento"), "") = "") Then
                    TOTimportobolletta = TOTimportobolletta + importobolletta
                End If

                importopagato = (par.IfNull(row.Item("IMPORTO_PAGATO"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), 0))
                If par.IfNull(row.Item("FL_ANNULLATA"), 0) = 0 Or (par.IfNull(row.Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(row.Item("data_pagamento"), "") = "") Then
                    TOTimportopagato = TOTimportopagato + importopagato
                End If

                Dim STATO As String = ""
                If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> "0" Then
                    STATO = "ANNUL."
                Else
                    STATO = "VALIDA"
                End If
                If par.IfNull(row.Item("id_bolletta_ric"), "0") <> "0" Or par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
                    STATO = "RICLA."
                    riclass = 1
                End If

                If par.IfNull(row.Item("id_bolletta_storno"), "0") <> "0" Then
                    STATO = "STORN."
                End If

                'residuo = importobolletta - importopagato
                If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> "0" Then
                    residuo = 0
                Else
                    residuo = importobolletta - importopagato
                End If

                TOTimportoresiduo = TOTimportoresiduo + residuo

                'Select Case par.IfNull(row.Item("ID_TIPO"), "0")
                '    Case "3"
                '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                '    Case "4"
                '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                'End Select

                rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "---")
                rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
                rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
                rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))
                rowDT.Item("data_scadenza") = par.FormattaData(par.IfNull(row.Item("data_scadenza"), ""))
                rowDT.Item("importobolletta") = Format(importobolletta, "##,##0.00")
                rowDT.Item("imp_pagato") = Format(importopagato, "##,##0.00")
                rowDT.Item("imp_residuo") = Format(residuo, "##,##0.00")
                rowDT.Item("data_pagamento") = par.FormattaData(par.IfNull(row.Item("data_pagamento"), ""))
                NoteBolletta = par.IfNull(row.Item("NOTE"), "")
                rowDT.Item("importo_ruolo") = Format(par.IfNull(row.Item("IMPORTO_RUOLO"), 0), "##,##0.00")
                rowDT.Item("importo_totale") = Format(par.IfNull(row.Item("IMPORTO_TOTALE"), 0), "##,##0.00")



                TOTImpRuolo = TOTImpRuolo + rowDT.Item("importo_ruolo")
                TOTimportoEmesso = TOTimportoEmesso + rowDT.Item("importo_totale")

                If par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
                    rowDT.Item("fl_rateizz") = "SI"
                Else
                    rowDT.Item("fl_rateizz") = "NO"
                End If

                indiceMorosita = par.IfNull(row.Item("id_morosita"), 0)

                If indiceMorosita <> 0 And par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
                    rowDT.Item("fl_mora") = "SI"
                Else
                    rowDT.Item("fl_mora") = "NO"
                End If

                rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)

                rowDT.Item("id") = par.IfNull(row.Item("id"), 0)

                If rowDT.Item("id_tipo") = "3" Or rowDT.Item("id_tipo") = "4" Then
                    morosita = 1
                End If

                Select Case par.IfNull(row.Item("id_tipo"), 0)
                    Case "3"
                        indiceBolletta = par.IfNull(row.Item("id"), 0)
                    Case "4"
                        indiceMorosita = par.IfNull(row.Item("id_morosita"), -1)
                        indiceBolletta = 0
                    Case "5"
                        indiceBolletta = par.IfNull(row.Item("id"), 0)
                        indiceBolletta = 0
                    Case "22"
                        storno = 1
                End Select

                If par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
                    indiceBolletta = par.IfNull(row.Item("id"), 0)
                End If

                If par.IfNull(row.Item("id_rateizzazione"), 0) <> 0 Then
                    indiceBolletta = par.IfNull(row.Item("id"), 0)
                End If

                If indiceBolletta = 0 Then
                    rowDT.Item("dettagli") = ""
                Else
                    If indiceMorosita = 0 And par.IfNull(row.Item("importo_ric_b"), 0) <> 0 Then
                        CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Columns(2).Visible = True

                        'CType(Tab_Bollette1.FindControl("divContabCon"), HiddenField).Value = "1"
                        rowDT.Item("dettagli") = "" ' "<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    End If
                End If

                rowDT.Item("anteprima") = "<a href=""javascript:ApriAnteprima();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

                If par.IfNull(row.Item("ID_TIPO"), 0) = 22 Then
                    CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Columns(2).Visible = True
                    'CType(Tab_Bollette1.FindControl("divContabCon"), HiddenField).Value = "1"
                    rowDT.Item("dettagli") = "" '"<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                End If

                'par.cmd.CommandText = "SELECT ELENCO_BOLL_CDP.ID_CDP,RAPPORTI_UTENZA_DEP_CAUZ.* FROM SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ,SISCOM_MI.ELENCO_BOLL_CDP WHERE ELENCO_BOLL_CDP.ID_BOLLETTA=RAPPORTI_UTENZA_DEP_CAUZ.ID_BOLLETTA AND RAPPORTI_UTENZA_DEP_CAUZ.ID_BOLLETTA=" & rowDT.Item("id")
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader1.HasRows = True Then
                '    If myReader1.Read Then
                '        rowDT.Item("dettagli") = "" ' rowDT.Item("dettagli") & "&nbsp;" & "<a href=""javascript:StampaCdp(" & rowDT.Item("id") & ");void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Stampa CdP" & Chr(34) & " title=" & Chr(34) & "Stampa CdP" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/Img_CDP.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                '        If par.IfNull(myReader1("NUM_MANDATO"), "") <> "" Then
                '            NoteBolletta = NoteBolletta & " - Rimborsato - mandato pag. n. " & par.IfNull(myReader1("num_mandato"), "") & "/" & par.IfNull(myReader1("anno_mandato"), "") & " del " & par.FormattaData(par.IfNull(myReader1("data_mandato"), ""))
                '        End If
                '    End If
                'End If
                'myReader1.Close()

                'par.cmd.CommandText = "SELECT ELENCO_BOLL_CDP.id_cdp,PAGAMENTI.DATA_MANDATO,PAGAMENTI.NUMERO_MANDATO FROM siscom_mi.ELENCO_BOLL_CDP,SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID=ELENCO_BOLL_CDP.ID_CDP AND ELENCO_BOLL_CDP.id_boll_gest IS NOT NULL AND ELENCO_BOLL_CDP.id_bolletta=" & rowDT.Item("id")
                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.HasRows = True Then
                '    If myReader1.Read Then
                '        rowDT.Item("dettagli") = rowDT.Item("dettagli") & "&nbsp;" & "<a href=""javascript:StampaCdp(" & rowDT.Item("id") & ");void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Stampa CdP" & Chr(34) & " title=" & Chr(34) & "Stampa CdP" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/Img_CDP.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                '        If par.IfNull(myReader1("NUMERO_MANDATO"), "") <> "" Then
                '            NoteBolletta = NoteBolletta & " - Rimborsato - mandato pag. n. " & par.IfNull(myReader1("numERO_mandato"), "") & "/" & Mid(par.IfNull(myReader1("DATA_MANDATO"), ""), 1, 4) & " del " & par.FormattaData(par.IfNull(myReader1("data_mandato"), ""))
                '        End If
                '    End If
                'End If
                'myReader1.Close()


                'par.cmd.CommandText = "select ODL.ID_PAGAMENTO,ODL.ID from siscom_mi.odl,siscom_mi.ELENCO_BOLL_CDP where ODL.ID=ELENCO_BOLL_ODL.ID_ODL AND ELENCO_BOLL_ODL.ID_BOLLETTA=" & rowDT.Item("id")
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    If par.IfNull(myReader1("ID_PAGAMENTO"), -1) <> -1 Then
                '        rowDT.Item("dettagli") = rowDT.Item("dettagli") & "&nbsp;" & "<a href=""javascript:StampaCdp(" & rowDT.Item("id") & ");void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Stampa CdP" & Chr(34) & " title=" & Chr(34) & "Stampa CdP" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/Img_CDP.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

                '        par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.NUM_MANDATO,PAGAMENTI_LIQUIDATI.ANNO_MANDATO,PAGAMENTI_LIQUIDATI.DATA_MANDATO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI,SISCOM_MI.ODL WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=ODL.ID_PAGAMENTO AND ODL.ID=" & myReader1("ID") & " ORDER BY DATA_MANDATO DESC"
                '        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '        If myReader2.HasRows = True Then
                '            If myReader2.Read Then
                '                NoteBolletta = NoteBolletta & " - Rimborsato - mandato pag. n. " & par.IfNull(myReader2("num_mandato"), "") & "/" & par.IfNull(myReader2("anno_mandato"), "") & " del " & par.FormattaData(par.IfNull(myReader2("data_mandato"), ""))
                '            End If
                '        End If
                '        myReader2.Close()
                '    End If
                'End If
                'myReader1.Close()



                rowDT.Item("note") = NoteBolletta


                dt1.Rows.Add(rowDT)
            Next

            'CType(Tab_Bollette1.FindControl("lblImpRuolo"), Label).Text = Format(TOTImpRuolo, "##,##0.00") & " €"

            If dt1.Rows.Count = 0 Then
                CType(Tab_Bollette1.FindControl("bolletteAssenti"), HiddenField).Value = "1"
            End If

            rowDT = dt1.NewRow()
            rowDT.Item("id") = -1
            rowDT.Item("num_tipo") = ""
            rowDT.Item("riferimento_da") = ""
            rowDT.Item("riferimento_a") = "TOTALE"
            rowDT.Item("data_emissione") = ""
            rowDT.Item("data_scadenza") = ""
            rowDT.Item("importo_totale") = Format(TOTimportoEmesso, "##,##0.00")
            rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
            rowDT.Item("imp_pagato") = Format(TOTimportopagato, "##,##0.00")
            rowDT.Item("imp_residuo") = Format(TOTimportoresiduo, "##,##0.00")
            rowDT.Item("data_pagamento") = ""
            rowDT.Item("note") = ""
            rowDT.Item("fl_mora") = ""
            rowDT.Item("fl_rateizz") = ""
            rowDT.Item("importo_ruolo") = Format(TOTImpRuolo, "##,##0.00")
            rowDT.Item("id_tipo") = ""
            rowDT.Item("dettagli") = ""
            rowDT.Item("anteprima") = ""
            dt1.Rows.Add(rowDT)

            CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).DataSource = dt1
            CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).DataBind()
            contaBollette.Value = dt1.Rows.Count
            ''If morosita = 1 Then
            ''    For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Items
            ''        If di.Cells(15).Text.Contains("3") Or di.Cells(15).Text.Contains("4") Then
            ''            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            ''        End If
            ''    Next
            ''End If
            'If contaBollette.Value <= 8 Then
            '    CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).HeaderStyle.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
            'End If


            'If riclass = 1 Then
            '    For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Items
            '        If di.Cells(3).Text.Contains("RICLA.") Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("blue")
            '        End If
            '    Next
            'End If
            'If storno = 1 Then
            '    For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Items
            '        If di.Cells(17).Text = "22" Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            '        End If
            '        If di.Cells(3).Text.Contains("STORN.") Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            '        End If
            '        If di.Cells(3).Text.Contains("ANNUL.") Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            '        End If
            '    Next
            'End If
            If contaBollette.Value <= 8 Then
                CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).HeaderStyle.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
            End If

            For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Items
                If riclass = 1 Then
                    If di.Cells(3).Text.Contains("RICLA.") Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("blue")
                    End If
                End If

                If storno = 1 Then
                    If di.Cells(17).Text = "22" Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    End If
                    If di.Cells(3).Text.Contains("STORN.") Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    End If
                End If
                If di.Cells(3).Text.Contains("ANNUL.") Then
                    di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                End If
            Next


            For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridContab"), DataGrid).Items
                If di.Cells(5).Text.Contains("TOTALE") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).Font.Underline = True
                        End If
                    Next
                    'di.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
                    'di.ForeColor = Drawing.ColorTranslator.FromHtml("white")
                    'di.Attributes.Add("onmouseover", "this.style.backgroundColor='#006699'")
                    'di.Attributes.Add("onmouseout", "this.style.backgroundColor='#006699'")
                End If
            Next

            CaricaGestionale()


            If ConnAperta = True Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaTabBoll) - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaGestionale()
        Try
            Dim num_bolletta As String = ""
            Dim I As Integer = 0
            Dim importobolletta As Decimal = 0
            Dim importobolletta2 As Decimal = 0
            Dim importopagato As Decimal = 0
            Dim residuo As Decimal = 0
            Dim morosita As Integer = 0
            Dim riclass As Integer = 0
            Dim indiceMorosita As Integer = 0
            Dim indiceBolletta As Integer = 0

            par.cmd.CommandText = "select TIPO_BOLLETTE_GEST.ACRONIMO,TIPO_BOLLETTE_GEST.UTILIZZABILE,bol_bollette_gest.* from SISCOM_MI.TIPO_BOLLETTE_GEST,siscom_mi.bol_bollette_GEST where BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID (+)" _
                & " and bol_bollette_gest.id_contratto=" & lIdContratto & " order by bol_bollette_gest.data_emissione desc,bol_bollette_gest.riferimento_da desc, bol_bollette_gest.riferimento_a desc,bol_bollette_gest.id desc"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery2 As New Data.DataTable
            Dim dt1 As New Data.DataTable
            Dim rowDT As System.Data.DataRow

            dt1.Columns.Add("id")
            dt1.Columns.Add("num_tipo")
            dt1.Columns.Add("riferimento_da")
            dt1.Columns.Add("riferimento_a")
            dt1.Columns.Add("data_emissione")
            dt1.Columns.Add("importobolletta")
            dt1.Columns.Add("data_pagamento")
            dt1.Columns.Add("note")
            dt1.Columns.Add("anteprima")
            dt1.Columns.Add("sposta")
            dt1.Columns.Add("dettagli")
            dt1.Columns.Add("id_tipo")
            dt1.Columns.Add("tipo_appl")

            da1.Fill(dtQuery2)
            da1.Dispose()

            Dim TOTimportobolletta As Decimal = 0
            Dim importoVoceEmessa As Decimal = 0

            For Each row As Data.DataRow In dtQuery2.Rows
                indiceMorosita = 0
                indiceBolletta = 0
                rowDT = dt1.NewRow()

                importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
                If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "N") = "N" Then
                    importobolletta2 = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
                End If


                'CONTROLLO IN BOL_BOLLETTE_VOCI SE è stata emessa quella bolletta (in bol_bollette)
                par.cmd.CommandText = "SELECT ID AS ID_VOCE_GEST FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & par.IfNull(row.Item("ID"), 0)
                Dim daVociGest As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtVociGest As New Data.DataTable
                Dim rowDTVociGest As System.Data.DataRow
                daVociGest.Fill(dtVociGest)
                daVociGest.Dispose()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                If dtVociGest.Rows.Count > 0 Then
                    For Each rowDTVociGest In dtVociGest.Rows
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE_BOLLETTA_GEST=" & par.IfNull(rowDTVociGest.Item("ID_VOCE_GEST"), 0)
                        Dim daVociNew As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtVociNew As New Data.DataTable
                        Dim rowDTVociNew As System.Data.DataRow
                        daVociNew.Fill(dtVociNew)
                        daVociNew.Dispose()
                        par.cmd.Dispose()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        If dtVociNew.Rows.Count > 0 Then
                            For Each rowDTVociNew In dtVociNew.Rows
                                importoVoceEmessa += par.IfNull(rowDTVociNew.Item("IMPORTO"), 0)
                            Next
                        End If
                    Next
                End If

                Dim STATO As String = ""

                residuo = importobolletta - importoVoceEmessa

                TOTimportobolletta = TOTimportobolletta + (importobolletta2 - importoVoceEmessa)

                rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "---")
                rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
                rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
                rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))

                rowDT.Item("importobolletta") = Format(residuo, "##,##0.00")
                rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
                rowDT.Item("anteprima") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettaglioVociGest.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Dettagli" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Details-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

                If rowDT.Item("importobolletta") > 0 Then
                    If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "P" Then
                        rowDT.Item("sposta") = "<a href=""javascript:alert('Attenzione questa scrittura risulta già distribuita in rate come voce nello schema bollette!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    Else
                        rowDT.Item("sposta") = "<a href=""javascript:ScegliElaborazione();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    End If
                Else
                    If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "P" Then
                        rowDT.Item("sposta") = "<a href=""javascript:alert('Attenzione il credito è stato gestito parzialmente!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    Else
                        rowDT.Item("sposta") = "<a href=""javascript:ScegliElaborazione();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    End If
                End If
                If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "T" Then
                    rowDT.Item("sposta") = "<img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & "/>"
                End If

                rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)
                rowDT.Item("id") = par.IfNull(row.Item("id"), 0)


                'rowDT.Item("anteprima") = "<a href=javascript:ApriAnteprima();void(0); onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                'rowDT.Item("anteprima") = "<a href=""javascript:alert('Non disponibile!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                importobolletta = 0
                importobolletta2 = 0
                rowDT.Item("tipo_appl") = par.IfNull(row.Item("TIPO_APPLICAZIONE"), "")
                Select Case rowDT.Item("TIPO_APPL")
                    Case "P"
                        CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).Columns(3).Visible = True
                        CType(Tab_Bollette1.FindControl("divGestCon"), HiddenField).Value = "1"
                        If par.IfNull(rowDT.Item("importobolletta"), 0) < 0 Then
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.showModalDialog('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest', 'status:no;dialogWidth:630px;dialogHeight:250px;dialogHide:true;help:no;scroll:yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        Else
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        End If
                    Case "T"
                        CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).Columns(3).Visible = True
                        CType(Tab_Bollette1.FindControl("divGestCon"), HiddenField).Value = "1"
                        If par.IfNull(rowDT.Item("importobolletta"), 0) < 0 Then
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.showModalDialog('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest', 'status:no;dialogWidth:630px;dialogHeight:250px;dialogHide:true;help:no;scroll:yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        Else
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        End If
                    Case "N"
                        rowDT.Item("dettagli") = ""
                End Select
                If par.IfNull(row.Item("UTILIZZABILE"), 0) = 0 Then
                    rowDT.Item("sposta") = ""
                End If
                dt1.Rows.Add(rowDT)
            Next

            rowDT = dt1.NewRow()
            rowDT.Item("id") = -1
            rowDT.Item("num_tipo") = ""
            rowDT.Item("riferimento_da") = ""
            rowDT.Item("riferimento_a") = "TOTALE"
            rowDT.Item("data_emissione") = ""
            rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
            rowDT.Item("data_pagamento") = ""
            rowDT.Item("note") = ""
            rowDT.Item("id_tipo") = ""
            rowDT.Item("tipo_appl") = ""
            rowDT.Item("anteprima") = ""
            rowDT.Item("dettagli") = ""
            dt1.Rows.Add(rowDT)

            If Session.Item("MOD_ELAB_SING_GEST") = 1 Then
                CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).Columns(1).Visible = True
            Else
                CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).Columns(1).Visible = False
            End If

            CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).DataSource = dt1
            CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).DataBind()


            For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).Items
                If di.Cells(6).Text.Contains("TOTALE") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).Font.Underline = True
                        End If
                    Next
                    'di.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
                    'di.ForeColor = Drawing.ColorTranslator.FromHtml("white")
                    'di.Attributes.Add("onmouseover", "this.style.backgroundColor='#006699'")
                    'di.Attributes.Add("onmouseout", "this.style.backgroundColor='#006699'")
                End If
            Next

            For Each di As DataGridItem In CType(Tab_Bollette1.FindControl("DataGridGest"), DataGrid).Items
                If di.Cells(9).Text.Contains("P") Then
                    di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    di.ToolTip = "Documento già elaborato con scrittura delle voci in schema bollette! L'importo scalerà in base alle future emissioni."
                End If
                If di.Cells(9).Text.Contains("T") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(j).Font.Strikeout = True
                    Next
                End If
            Next



        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaGestionale) - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaSchemaBoll()
        Try
            Dim LL As Integer = 0
            'CARICAMENTO schema bollette

            CType(Tab_SchemaBollette1.FindControl("Label4"), Label).Text = "SCHEMA VOCI BOLLETTA" & " " & sAnnoSchema & "  -  <a href='SchemaAltriAnni.aspx?CN=" & lIdConnessione & "&ID=" & lIdContratto & "&A=" & sAnnoSchema & "' target='_blank'>Clicca qui per visualizzare lo schema di altri anni</a>"

            'MODIFICATO ORDINE DI VISUALIZZAZIONE VOCI (PER IMPORTO ASC)
            par.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from SISCOM_MI.t_voci_bolletta, SISCOM_MI.bol_schema where t_voci_bolletta.id=bol_schema.id_voce and bol_schema.id_contratto=" & lIdContratto & " and anno=" & sAnnoSchema & " order by t_voci_bolletta.descrizione,(-importo) ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "BOL_SCHEMA")

            CType(Tab_SchemaBollette1.FindControl("DataGridSchema"), DataGrid).DataSource = ds
            CType(Tab_SchemaBollette1.FindControl("DataGridSchema"), DataGrid).DataBind()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HttpContext.Current.Session.Remove(lIdConnessione)
            Page.Dispose()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaSchemaBoll) - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Function VisualizzaContratto()
        Dim scriptblock As String
        Dim Conduttore As String = ""
        Dim Conduttore1 As String = ""
        Dim testoTabella As String = ""
        Dim ExConduttore As String = ""


        'max 15/12/2014 visualizzazione dati reg.
        Dim TipoPosizione As String = ""
        Dim ModalitaPagamento As String = ""

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add(lIdConnessione, par.OracleConn)

            Session.Add("lIdConnessione", lIdConnessione)

            Dim LimiteTassaRegistrazione As Double = 0
            par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=6"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                LimiteTassaRegistrazione = CDbl(par.PuntiInVirgole(par.IfNull(myReaderA("VALORE"), 0)))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=35"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                sAnnoSchema = par.IfNull(myReaderA("VALORE"), "2010")
                annoschema.Value = sAnnoSchema
            End If
            myReaderA.Close()

            
            par.RiempiDListConVuoto(Tab_Registrazione1, par.OracleConn, "cmbUfficioRegistro", "SELECT * FROM SISCOM_MI.TAB_UFFICIO_REGISTRO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDListConVuoto(Tab_Comunicazioni1, par.OracleConn, "cmbTipoViaCor", "select * from siscom_mi.tipologia_indirizzo order by descrizione asc", "DESCRIZIONE", "COD")
            par.RiempiDListConNULL(Tab_Comunicazioni1, par.OracleConn, "cmbCommissariato", "select * from siscom_mi.tab_commissariati order by descrizione asc", "DESCRIZIONE", "ID")

            'GESTITO DA SINDACATO
            par.RiempiDListConVuoto(Tab_SchemaBollette1, par.OracleConn, "lstSindacati", "select * from sindacati_vsa", "DESCRIZIONE", "ID")



            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.* FROM SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=" & lIdContratto
InSolaLettura:
            'If SolaLettura = "1" Then
            '    If lettura.Value = "1" Then
            '        par.OracleConn.Close()
            '    End If
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            '    HttpContext.Current.Session.Add(lIdConnessione, par.OracleConn)
            '    Session.Add("lIdConnessione", lIdConnessione)
            '    par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.* FROM SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=" & lIdContratto

            'End If

            Dim myReaderVer As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()



            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS ""TIPO_C"",TIPOLOGIA_CONTRATTO_LOCAZIONE.RIF_LEGISLATIVO ,TIPOLOGIA_CONTRATTO_LOCAZIONE.perc_tr_canone,TIPOLOGIA_CONTRATTO_LOCAZIONE.perc_conduttore,RAPPORTI_UTENZA.*,SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS ""STATO"" FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AND RAPPORTI_UTENZA.ID=" & lIdContratto
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read Then

                'max 15/12/2014 visualizzazione dati reg.
                TipoPosizione = par.IfNull(myReader("ID_TIPO_POSIZIONE"), "")
                ModalitaPagamento = par.IfNull(myReader("ID_TIPO_PAGAMENTO"), "")

                If par.IfNull(myReader("imp_canone_provv"), 0) <> 0 Then

                    'par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID,domande_bando_vsa.id_dichiarazione FROM DOMANDE_BANDO_VSA,DOMANDE_VSA_ALLOGGIO where DOMANDE_VSA_ALLOGGIO.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=3 AND DOMANDE_VSA_ALLOGGIO.num_contratto='" & par.IfNull(myReader("cod_contratto"), "") & "'"
                    'Dim myReader432 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader432.HasRows = True Then
                    '    If myReader432.Read Then
                    '        CType(Tab_Canone1.FindControl("lblProvvisiorio0"), Label).Visible = True
                    '        CType(Tab_Canone1.FindControl("lblProvvisiorio0"), Label).Text = "<a href='#' onclick=" & Chr(34) & "javascript:window.open('../VSA/domanda.aspx?L=1&X=1&ID=" & myReader432("id") & "&ID1=" & myReader432("id_dichiarazione") & "&PROGR=-1&INT=0','Domanda','height=480,top=0,left=0,width=670,scrollbars=no');" & Chr(34) & " alt='Visualizza domanda'>* Domanda Rid. Canone</a>"
                    '    End If
                    'Else
                    '    CType(Tab_Canone1.FindControl("lblProvvisiorio0"), Label).Visible = False
                    'End If
                    'myReader432.Close()

                    CType(Tab_Canone1.FindControl("lblProvvisiorio"), Label).Visible = True
                    CType(Tab_Canone1.FindControl("lblCanoneProvvisiorio"), Label).Visible = True
                    CType(Tab_Canone1.FindControl("lblCanoneProvvisiorio"), Label).Text = Format(par.IfNull(myReader("imp_canone_provv"), "0"), "##,##0.00")
                End If

                lblContratto.Text = "Rapporto Cod. " & par.IfNull(myReader("cod_contratto"), "")
                CodContratto1 = par.IfNull(myReader("cod_contratto"), "")
                If par.IfNull(myReader("ART_15"), "0") = "3" Then
                    LetteraProvenienza = "V"
                ElseIf par.IfNull(myReader("ART_15"), "0") = "2" Then
                    LetteraProvenienza = "D"
                Else
                    LetteraProvenienza = "-1"
                End If

                If par.IfNull(myReader("FL_TUTORE_STR"), "0") = "0" Then
                    CType(Tab_Contratto1.FindControl("ChTutore"), CheckBox).Checked = False
                Else
                    CType(Tab_Contratto1.FindControl("ChTutore"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader("sindacato"), "-1") <> "-1" Then
                    CType(Tab_SchemaBollette1.FindControl("lstSindacati"), DropDownList).SelectedIndex = -1
                    CType(Tab_SchemaBollette1.FindControl("lstSindacati"), DropDownList).Items.FindByValue(par.IfNull(myReader("sindacato"), "1")).Selected = True
                End If


                TipoContratto = par.IfNull(myReader("PROVENIENZA_ASS"), 1)



                If TipoContratto = "3" Then
                    par.RiempiDListConVuoto(Tab_Contratto1, par.OracleConn, "cmbNomeUfficiale", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE SUBSTR(COD,1,3)='USD' OR SUBSTR(COD,1,3)='CON' ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("STANDARD", "0"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("COOPERATIVE", "C"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431 P.O.R.", "P"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("RESIDENZIALE", "R"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("392/78 ASSOCIAZIONI", "A"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431/98 Art.15 R.R.1/2004", "D"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431/98 Art.15 C.2 R.R.1/2004", "V"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431/98 SPECIALI", "S"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("FORZE DELL'ORDINE", "Z"))


                    CType(Tab_Conduttore1.FindControl("Label1"), Label).Text = "ELENCO EX INTESTATARI"

                    'CType(Tab_Conduttore1.FindControl("btnAggiungiComp"), System.Web.UI.WebControls.Image).Visible = False
                    'CType(Tab_Conduttore1.FindControl("imgDiventaINT"), System.Web.UI.WebControls.Image).Visible = False
                    'CType(Tab_Conduttore1.FindControl("img_EliminaComp"), System.Web.UI.WebControls.Image).Visible = False

                    'CType(Tab_Conduttore1.FindControl("img_EliminaOspite"), System.Web.UI.WebControls.Image).Visible = False
                    'CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Visible = False
                    CType(Tab_Conduttore1.FindControl("Label4"), Label).Visible = False
                    CType(Tab_Conduttore1.FindControl("Label2"), Label).Visible = False


                    'CType(Tab_Conduttore1.FindControl("btnAggiungiCond"), System.Web.UI.WebControls.Image).Visible = False
                    'CType(Tab_Conduttore1.FindControl("imgEliminaCond"), System.Web.UI.WebControls.Image).Visible = False
                    'CType(Tab_Conduttore1.FindControl("Img_DiventaComp"), System.Web.UI.WebControls.Image).Visible = False


                End If


                If TipoContratto = "10" Or TipoContratto = "8" Or TipoContratto = "1" Or TipoContratto = "2" Or TipoContratto = "12" Then
                    par.RiempiDListConVuoto(Tab_Contratto1, par.OracleConn, "cmbNomeUfficiale", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE SUBSTR(COD,1,3)='ERP' ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedIndex = -1
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.FindByValue("R").Selected = True
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Enabled = False

                End If
                If TipoContratto = "6" Then
                    par.RiempiDListConVuoto(Tab_Contratto1, par.OracleConn, "cmbNomeUfficiale", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE SUBSTR(COD,1,6)='L43198' ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("POSTO AUTO COPERTO,SCOPERTO,BOX,MOTOBOX ETC.", "B"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("NEGOZIO, MAGAZZINO, LABORATORIO, UFFICIO, ETC.", "N"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("RESIDENZIALE", "R"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("392/78 ASSOCIAZIONI", "A"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("CONCESSIONE SPAZI P.", "X"))
                    'MAX 23/04/2015
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("COMODATO D'USO GRATUITO", "Y"))

                    If par.IfNull(myReader("dest_uso"), "0") = "C" Then

                        CType(Tab_Conduttore1.FindControl("Label1"), Label).Text = "ELENCO EX INTESTATARI"

                        CType(Tab_Conduttore1.FindControl("btnAggiungiComp"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("imgDiventaINT"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("img_EliminaComp"), System.Web.UI.WebControls.Image).Visible = False

                        CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Visible = False
                        CType(Tab_Conduttore1.FindControl("Label4"), Label).Visible = False
                        CType(Tab_Conduttore1.FindControl("Label2"), Label).Visible = False


                        CType(Tab_Conduttore1.FindControl("btnAggiungiCond"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("imgEliminaCond"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("Img_DiventaComp"), System.Web.UI.WebControls.Image).Visible = False
                    End If

                End If

                If TipoContratto = "5" Then
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("POSTO AUTO COPERTO,SCOPERTO,BOX,MOTOBOX ETC.", "B"))
                    'CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("NEGOZIO, MAGAZZINO, LABORATORIO, UFFICIO, ETC.", "N"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("RESIDENZIALE", "R"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("COOPERATIVE", "C"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431 P.O.R.", "P"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431/98 Art.15 R.R.1/2004", "D"))
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("431/98 Art.15 C.2 R.R.1/2004", "V"))
                    'MAX 23/04/2015
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.Remove(New ListItem("COMODATO D'USO GRATUITO", "Y"))

                    par.RiempiDListConVuoto(Tab_Contratto1, par.OracleConn, "cmbNomeUfficiale", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE SUBSTR(COD,1,6)='EQC392' ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")


                    If par.IfNull(myReader("dest_uso"), "0") = "A" Then

                        CType(Tab_Conduttore1.FindControl("Label1"), Label).Text = "ELENCO EX INTESTATARI"

                        CType(Tab_Conduttore1.FindControl("btnAggiungiComp"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("imgDiventaINT"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("img_EliminaComp"), System.Web.UI.WebControls.Image).Visible = False

                        'CType(Tab_Conduttore1.FindControl("img_EliminaOspite"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Visible = False
                        CType(Tab_Conduttore1.FindControl("Label4"), Label).Visible = False
                        CType(Tab_Conduttore1.FindControl("Label2"), Label).Visible = False


                        CType(Tab_Conduttore1.FindControl("btnAggiungiCond"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("imgEliminaCond"), System.Web.UI.WebControls.Image).Visible = False
                        CType(Tab_Conduttore1.FindControl("Img_DiventaComp"), System.Web.UI.WebControls.Image).Visible = False
                    End If

                End If

                If TipoContratto = "7" Then
                    par.RiempiDListConVuoto(Tab_Contratto1, par.OracleConn, "cmbNomeUfficiale", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE SUBSTR(COD,1,4)='NONE' ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
                    LBLABUSIVO.Visible = True

                    CType(Tab_Contratto1.FindControl("Label26"), Label).Visible = True
                    CType(Tab_Contratto1.FindControl("Image1"), System.Web.UI.WebControls.Image).Visible = True

                End If

                lIdDichiarazione = par.IfNull(myReader("ID_ISEE"), -1)
                lIdDomandaERP = par.IfNull(myReader("ID_DOMANDA"), -1)
                lIdAU = par.IfNull(myReader("ID_AU"), -1)

                CType(Tab_SchemaBollette1.FindControl("lblProssimaEmissione"), Label).Text = "Prossima Emissione: " & Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "XXXXXX"), 1, 4) & "/" & MESE(Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "XXXXXX"), 5, 2))


                CodContratto = par.IfNull(myReader("cod_contratto"), "")
                CodContratto1 = par.IfNull(myReader("cod_contratto"), "")

                If Mid(CodContratto, 1, 6) = "000000" Then
                    LBLVIRTUALE.Visible = True
                    VIRTUALE.Value = "1"

                    If par.IfNull(myReader("DURATA_MESI"), 0) <> 0 Then
                        LBLVIRTUALE.BackColor = System.Drawing.Color.LightCyan
                        LBLVIRTUALE.Text = "VIRTUALE MANUALE"
                    End If
                End If


                txtIdContratto.Value = par.IfNull(myReader("id"), "")



                CanoneCorrente = par.IfNull(myReader("imp_canone_iniziale"), "0")
                'Nlla tabella SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE ci sono tutti gli adeguamenti fatti al canone iniziale, compresi aggiornamenti ISTAT

                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_CONTRATTO=" & lIdContratto
                Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX1.Read Then
                    CanoneCorrente = CanoneCorrente + par.IfNull(myReaderX1(0), 0)
                End If
                myReaderX1.Close()


                CType(Generale1.FindControl("lblDecorrenza"), Label).Text = par.FormattaData(par.IfNull(myReader("data_decorrenza"), ""))
                CType(Generale1.FindControl("lblScadenza"), Label).Text = par.FormattaData(par.IfNull(myReader("data_scadenza"), ""))
                CType(Generale1.FindControl("lblDisdetta"), Label).Text = par.FormattaData(par.IfNull(myReader("data_Disdetta_Locatario"), ""))

                CType(Generale1.FindControl("lblStato"), Label).Text = par.IfNull(myReader("STATO"), "")
                CType(Generale1.FindControl("lblCodiceGimi"), Label).Text = par.IfNull(myReader("cod_contratto_gimi"), "")

                DataDisponibilitaAlloggio = ""
                Select Case CType(Generale1.FindControl("lblStato"), Label).Text
                    Case "BOZZA"
                        'If par.IfNull(myReader("fl_stampato"), "0") = "1" Then
                        '    Tab_Contratto1.Disabilita()
                        '    CType(Tab_Contratto1.FindControl("txtDataStipula"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDataRiconsegna"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDataAppPresloggio"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtOraAppPresloggio"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDataAppSloggio"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtOraAppSloggio"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDataDelibera"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtEntroCuiDisdettare"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDelibera"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDurata"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDurataRinnovo"), TextBox).ReadOnly = True
                        '    CType(Tab_Contratto1.FindControl("txtDataSecScadenza"), TextBox).ReadOnly = True

                        '    Tab_Registrazione1.Disabilita()
                        '    Tab_Canone1.disattiva()
                        '    If TipoContratto = "7" And SolaLettura = "0" Then
                        '        Tab_Conduttore1.Disabilita_Tutto_Tranne_Comp()
                        '    Else
                        '        Tab_Conduttore1.Disabilita_Tutto()
                        '    End If
                        '    speseunita.Value = "0"
                        'Else

                        '    CType(Tab_Contratto1.FindControl("txtDurata"), TextBox).ReadOnly = False
                        '    CType(Tab_Contratto1.FindControl("txtDurataRinnovo"), TextBox).ReadOnly = False

                        '    If (TipoContratto = "3" Or TipoContratto = "6") And LBLSTATOC.Text = "Stato: BOZZA" Then
                        '        CType(Tab_Contratto1.FindControl("cmbNomeUfficiale"), DropDownList).Enabled = True
                        '    End If

                        'End If
                       
                        'speseunita.Value = "1"
                        'LBLNOMEFILECANONE.Value = Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & CodContratto & ".txt"

                        'If VIRTUALE.Value <> "1" Then
                        '    If SolaLettura <> "1" Then
                        '        If TipoContratto = "1" Or TipoContratto = "2" Or TipoContratto = "8" Then
                        '            CType(Tab_Canone1.FindControl("ImgRicalcolaC"), ImageButton).Visible = True
                        '        End If
                        '    End If
                        'End If


                    Case "CHIUSO"
                        speseunita.Value = "0"
                    Case Else
                        speseunita.Value = "0"
                End Select


                LBLSTATOC.Visible = True
                LBLSTATOC.Text = "Stato: " & UCase(par.IfNull(myReader("STATO"), ""))

                'TAB CONTRATTO

                CType(Tab_Contratto1.FindControl("txtCodContratto"), TextBox).Text = par.IfNull(myReader("COD_CONTRATTO"), "")
                CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_decorrenza"), ""))
                CType(Tab_Contratto1.FindControl("txtDataStipula"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_stipula"), ""))
                CType(Tab_Contratto1.FindControl("txtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_scadenza"), ""))
                CType(Tab_Contratto1.FindControl("txtDataRiconsegna"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_riconsegna"), ""))
                CType(Tab_Contratto1.FindControl("txtDataDelibera"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_delibera"), ""))
                CType(Tab_Contratto1.FindControl("txtDelibera"), TextBox).Text = par.IfNull(myReader("delibera"), "")
                CType(Tab_Contratto1.FindControl("txtDataConsegna"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_consegna"), ""))
                CType(Tab_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_disdetta_locatario"), ""))
                CType(Tab_Contratto1.FindControl("txtNotificaDisdetta"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_notifica_disdetta"), ""))

                CType(Tab_Contratto1.FindControl("txtDataDisdetta0"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INVIO_RIC_DISDETTA"), ""))
                CType(Tab_Contratto1.FindControl("txtDataSecScadenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_scadenza_rinnovo"), ""))

                CType(Tab_Contratto1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("note"), "")
                CType(Tab_Contratto1.FindControl("txtEntroCuiDisdettare"), TextBox).Text = par.IfNull(myReader("mesi_disdetta"), "")
                CType(Tab_Contratto1.FindControl("txtDurata"), TextBox).Text = par.IfNull(myReader("durata_anni"), "")
                CType(Tab_Contratto1.FindControl("txtDurataRinnovo"), TextBox).Text = par.IfNull(myReader("durata_rinnovo"), "")

                CType(Tab_Contratto1.FindControl("txtRifLegislativo"), TextBox).Text = par.IfNull(myReader("RIF_LEGISLATIVO"), "")

                CType(Tab_Contratto1.FindControl("txtDescrcontratto"), TextBox).Text = par.IfNull(myReader("TIPO_C"), "")

                CType(Tab_Contratto1.FindControl("cmbNomeUfficiale"), DropDownList).SelectedIndex = -1
                CType(Tab_Contratto1.FindControl("cmbNomeUfficiale"), DropDownList).Items.FindByValue(par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "")).Selected = True

                CType(Tab_Contratto1.FindControl("cmbForzoso"), DropDownList).SelectedIndex = -1
                CType(Tab_Contratto1.FindControl("cmbForzoso"), DropDownList).Items.FindByValue(par.IfNull(myReader("motivo_rec_forzoso"), "4")).Selected = True

                CType(Tab_Contratto1.FindControl("cmbMittenteDisdetta"), DropDownList).SelectedIndex = -1
                CType(Tab_Contratto1.FindControl("cmbMittenteDisdetta"), DropDownList).Items.FindByValue(par.IfNull(myReader("mittente_disdetta"), "-1")).Selected = True

                CType(Generale1.FindControl("lblTipologia"), Label).Text = CType(Tab_Contratto1.FindControl("cmbNomeUfficiale"), DropDownList).SelectedItem.Text

                CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedIndex = -1
                Select Case TipoContratto
                    Case "3"
                        CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.FindByValue(par.IfNull(myReader("dest_uso"), "B")).Selected = True
                    Case "6"
                        CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.FindByValue(par.IfNull(myReader("dest_uso"), "0")).Selected = True
                    Case Else
                        CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Items.FindByValue(par.IfNull(myReader("dest_uso"), "0")).Selected = True
                End Select

                If LetteraProvenienza = "V" Then
                    CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).Enabled = False
                End If

                CType(Tab_Contratto1.FindControl("txtDescrDestinazione"), TextBox).Text = par.IfNull(myReader("DESCR_DEST_USO"), "")

                If par.IfNull(myReader("FL_BOLLO_ASSOLTO"), "0") = "0" Then
                    CType(Tab_Contratto1.FindControl("ChBolloEsente"), CheckBox).Checked = False
                Else
                    CType(Tab_Contratto1.FindControl("ChBolloEsente"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_ASSEGN_TEMP"), "0") = "0" Then
                    CType(Tab_Contratto1.FindControl("chkTemporanea"), CheckBox).Checked = False
                Else
                    CType(Tab_Contratto1.FindControl("chkTemporanea"), CheckBox).Checked = True
                End If

                ' modifiche nuove
                '::::::::::::::::::::::::::::::::::::::::::::

                par.cmd.CommandText = "Select DATA_APP_PRE_SLOGGIO, DATA_APP_RAPPORTO_SLOGGIO FROM SISCOM_MI.SL_SLOGGIO WHERE ID_CONTRATTO = " & lIdContratto
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettore.Read Then
                    If par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "").length >= 8 Then
                        CType(Tab_Contratto1.FindControl("txtDataAppPresloggio"), TextBox).Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "                 "), 1, 8))
                        CType(Tab_Contratto1.FindControl("txtOraAppPresloggio"), TextBox).Text = Mid(par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "          "), 9, 2) & ":" & Mid(par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "          "), 11, 2)

                    Else
                        CType(Tab_Contratto1.FindControl("txtDataAppPresloggio"), TextBox).Text = ""
                        CType(Tab_Contratto1.FindControl("txtOraAppPresloggio"), TextBox).Text = ""
                    End If

                    If par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "").length >= 8 Then
                        CType(Tab_Contratto1.FindControl("txtDataAppSloggio"), TextBox).Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "                 "), 1, 8))
                        CType(Tab_Contratto1.FindControl("txtOraAppSloggio"), TextBox).Text = Mid(par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "          "), 9, 2) & ":" & Mid(par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "          "), 11, 2)
                    Else
                        CType(Tab_Contratto1.FindControl("txtDataAppSloggio"), TextBox).Text = ""
                        CType(Tab_Contratto1.FindControl("txtOraAppSloggio"), TextBox).Text = ""
                    End If
                End If
                lettore.Close()

                '::::::::::::::::::::::::::::::::::::::::::::

                par.cmd.CommandText = "SELECT anagrafica.id,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & lIdContratto & " AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE DESC"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    If par.IfNull(myReader1("COD_TIPOLOGIA_OCCUPANTE"), "-1") = "INTE" Then
                        Conduttore1 = Conduttore1 & par.IfNull(myReader1("INTESTATARIO"), "") & " *, "
                        txtCodAffittuario.Value = par.IfNull(myReader1("id"), "-1")
                    Else
                        Conduttore1 = Conduttore1 & par.IfNull(myReader1("INTESTATARIO"), "") & ", "
                    End If
                    Conduttore = Conduttore & par.IfNull(myReader1("INTESTATARIO"), "") & ", "
                Loop
                myReader1.Close()

                '09/05/2012 CERCO L'INTESTATARIO PRECEDENTE
                par.cmd.CommandText = "SELECT anagrafica.id,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & lIdContratto & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='EXINTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY SOGGETTI_CONTRATTUALI.DATA_INIZIO DESC"
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    ExConduttore = par.IfNull(myReader1("INTESTATARIO"), "")
                Else
                    ExConduttore = "---"
                End If
                myReader1.Close()

                If Len(ExConduttore) >= 30 Then
                    CType(Generale1.FindControl("lblExcondutt"), Label).Font.Size = 7
                End If
                CType(Generale1.FindControl("lblExcondutt"), Label).Text = ExConduttore
                '09/05/2012 fine CERCO L'INTESTATARIO PRECEDENTE

                If Len(Conduttore) > 40 Then
                    CType(Generale1.FindControl("lblConduttore"), Label).Font.Size = 8
                End If

                CType(Generale1.FindControl("lblConduttore"), Label).Text = Conduttore1
                txtconduttore.Value = Replace(Conduttore, "<br />", ", ")


                CType(Generale1.FindControl("Label20"), Label).Text = "Saldo al " & Format(Now, "dd/MM/yyyy")
                Dim SaldoCalcolato As Double = par.CalcolaSaldoAttuale(lIdContratto)

                CType(Generale1.FindControl("lblSaldoAttuale"), Label).Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "javascript:document.getElementById('USCITA').value='1';window.open('DatiUtenza.aspx?C=RisUtenza&IDANA=" & txtCodAffittuario.Value & "&IDCONT=" & lIdContratto & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(SaldoCalcolato, "##,##0.00") & " Euro</a>"

                '13/03/2015 RESTITUZIONE CREDITI
                SaldoContratto = SaldoCalcolato
                If SaldoCalcolato > 0 Then
                    'DISABILITO COMANDO SE HA UN SALDO SUPERIORE A 0
                    CType(Tab_Bollette1.FindControl("HRimborso"), HiddenField).Value = "0"
                Else
                    CType(Tab_Bollette1.FindControl("HRimborso"), HiddenField).Value = "1"
                End If

                par.cmd.CommandText = "select COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME AS ""COMUNE_DI"",unita_contrattuale.*,tipologia_unita_immobiliari.descrizione as ""tipo"" from SEPA.COMUNI_NAZIONI,siscom_mi.tipologia_unita_immobiliari,siscom_mi.unita_contrattuale where COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND tipologia_unita_immobiliari.cod=unita_contrattuale.tipologia and id_contratto=" & lIdContratto & "  and id_unita_principale is null order by tipologia asc"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    CType(Generale1.FindControl("lblCodUnita"), Label).Text = par.IfNull(myReader1("cod_unita_immobiliare"), "")
                    CType(Generale1.FindControl("lblIndirizzo"), Label).Text = par.IfNull(myReader1("COMUNE_DI"), "") & " (" & par.IfNull(myReader1("SIGLA"), "") & ") " & "CAP " & par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("indirizzo"), "") & ", " & par.IfNull(myReader1("civico"), "") '
                    CType(Generale1.FindControl("lblTipoImmobile"), Label).Text = par.IfNull(myReader1("Tipo"), "")
                    CType(Generale1.FindControl("lblComplessoEdificio"), Label).Attributes.Add("onclick", "javascript:window.open('DatiComplessoEdificio.aspx?COD=" & par.IfNull(myReader1("cod_unita_immobiliare"), "") & "','Dati','');")
                    VAL_LOCATIVO_UNITA = par.IfNull(myReader1("VAL_LOCATIVO_UNITA"), "0,00")
                End If
                myReader1.Close()

                If Mid(CType(Generale1.FindControl("lblCodUnita"), Label).Text, 1, 6) = "000000" Then
                    LBLVIRTUALE.Visible = True
                    VIRTUALE.Value = "1"
                End If

                'CARICO UNITA PRINCIPALI IN TABELLA

                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                & "<tr>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.UNITA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>FOGLIO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>MAPPALE</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>SUB</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>PIANO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>SCALA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>INTERNO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "</tr>"


                par.cmd.CommandText = "select TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",unita_contrattuale.*,tipologia_unita_immobiliari.descrizione as ""tipo"" from SISCOM_MI.TIPO_LIVELLO_PIANO,siscom_mi.tipologia_unita_immobiliari,siscom_mi.unita_contrattuale where TIPO_LIVELLO_PIANO.COD (+)=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO  AND tipologia_unita_immobiliari.cod=unita_contrattuale.tipologia and id_contratto=" & lIdContratto & " AND (ID_UNITA_PRINCIPALE IS NULL) order by tipologia asc"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("cod_unita_immobiliare"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("FOGLIO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("NUMERO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("SUB"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("TIPO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("INDIRIZZO"), "") & "," & par.IfNull(myReader1("CIVICO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("PIANO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("SCALA"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("INTERNO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"

                    sPiano = par.IfNull(myReader1("PIANO"), "")
                    sInterno = par.IfNull(myReader1("INTERNO"), "")
                    sSCALA = par.IfNull(myReader1("SCALA"), "")
                    sFoglio = par.IfNull(myReader1("FOGLIO"), "")
                    sParticella = par.IfNull(myReader1("NUMERO"), "")
                    sSub = par.IfNull(myReader1("sub"), "")
                    UnitaContratto = par.IfNull(myReader1("ID_UNITA"), "")
                    txtIdUnita.Value = CStr(UnitaContratto)
                    sCatastale = par.IfNull(myReader1("cod_categoria_catastale"), "")
                    sRendita = par.IfNull(myReader1("rendita"), "")
                    sClasse = par.IfNull(myReader1("cod_classe_catastale"), "")
                    sIdEdificio = par.IfNull(myReader1("id_edificio"), "")
                    'CType(Tab_SchemaBollette1.FindControl("txtIdUnita"), HiddenField).Value = CStr(UnitaContratto)
                Loop
                myReader1.Close()
                CType(Tab_UnitaImmLocate1.FindControl("TBL_UNITA_ALLOCATE"), Label).Text = testoTabella & "</table>"
                LetteraERP = "A"
                If TipoContratto = "1" Then
                    LetteraERP = "A"
                    par.cmd.CommandText = "SELECT ID_DESTINAZIONE_USO FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id=" & UnitaContratto
                    Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAA.Read Then
                        If par.IfNull(myReaderAA("ID_DESTINAZIONE_USO"), "") = "2" Then
                            LetteraERP = "B"
                            LBLErpModerato.Visible = True
                        End If
                    End If
                    myReaderAA.Close()
                End If

                'CARICO PERTINENZE

                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                & "<tr>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.UNITA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>FOGLIO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>MAPPALE</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>SUB</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>PIANO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>SCALA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>INTERNO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "</tr>"

                par.cmd.CommandText = "select TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",unita_contrattuale.*,tipologia_unita_immobiliari.descrizione as ""tipo"" from SISCOM_MI.TIPO_LIVELLO_PIANO,siscom_mi.tipologia_unita_immobiliari,siscom_mi.unita_contrattuale where TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND tipologia_unita_immobiliari.cod=unita_contrattuale.tipologia and id_contratto=" & lIdContratto & " AND (ID_UNITA_PRINCIPALE IS NOT NULL) order by tipologia asc"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("cod_unita_immobiliare"), "") & "</a></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("FOGLIO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("NUMERO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("SUB"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("TIPO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("INDIRIZZO"), "") & "," & par.IfNull(myReader1("CIVICO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("PIANO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("SCALA"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("INTERNO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"
                Loop
                myReader1.Close()
                CType(Tab_UnitaImmLocate1.FindControl("TBL_PERTINENZE_ALLOCATE"), Label).Text = testoTabella & "</table>"

                par.cmd.CommandText = "select VALORE from SISCOM_MI.DIMENSIONI_unita_contrattuale where ID_CONTRATTO=" & lIdContratto & " AND ID_UNITA=" & UnitaContratto & " AND COD_TIPOLOGIA='SUP_CONV'"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    CType(Generale1.FindControl("lblConvenzionale"), Label).Text = par.IfNull(myReader1("VALORE"), "")
                End If
                myReader1.Close()

                DataDisponibilitaAlloggio = ""
                Select Case CType(Generale1.FindControl("lblStato"), Label).Text
                    Case "BOZZA"
                        If TipoContratto <> "10" Then
                            par.cmd.CommandText = "SELECT * FROM alloggi where cod_alloggio='" & Mid(CodContratto1, 1, 17) & "'"
                            Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAA.Read Then
                                DataDisponibilitaAlloggio = par.IfNull(myReaderAA("data_disponibilita"), "")
                            End If
                            myReaderAA.Close()

                            If DataDisponibilitaAlloggio = "" Then
                                par.cmd.CommandText = "SELECT * FROM siscom_mi.ui_usi_diversi where cod_alloggio='" & Mid(CodContratto1, 1, 17) & "'"
                                myReaderAA = par.cmd.ExecuteReader()
                                If myReaderAA.Read Then
                                    DataDisponibilitaAlloggio = par.IfNull(myReaderAA("data_disponibilita"), "")
                                End If
                                myReaderAA.Close()
                            End If
                        Else
                            DataDisponibilitaAlloggio = ""
                        End If
                End Select


            End If

            CaricaCompContratto(lIdContratto)

            CType(Tab_Registrazione1.FindControl("cmbUfficioRegistro"), DropDownList).SelectedIndex = -1
            CType(Tab_Registrazione1.FindControl("cmbUfficioRegistro"), DropDownList).Items.FindByValue(par.IfNull(myReader("cod_ufficio_reg"), -1)).Selected = True

            CType(Tab_Registrazione1.FindControl("txtSerie"), TextBox).Text = par.IfNull(myReader("serie_registrazione"), "")
            CType(Tab_Registrazione1.FindControl("txtNumRegistrazione"), TextBox).Text = par.IfNull(myReader("num_registrazione"), "")
            CType(Tab_Registrazione1.FindControl("txtDataRegistrazione"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_reg"), ""))
            CType(Tab_Registrazione1.FindControl("txtNumRepertorio"), TextBox).Text = par.IfNull(myReader("nro_repertorio"), "")
            CType(Tab_Registrazione1.FindControl("txtDataRepertorio"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_repertorio"), ""))
            CType(Tab_Registrazione1.FindControl("txtNumAssegnPg"), TextBox).Text = par.IfNull(myReader("nro_assegnazione_pg"), "")
            CType(Tab_Registrazione1.FindControl("txtDataAssegnPG"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_assegnazione_pg"), ""))

            'MAX 17/07/2015
            'CType(Tab_Registrazione1.FindControl("txtNotereg"), TextBox).Text = par.IfNull(myReader("note_registrazione"), "")
            ' par.CaricaStoricoNote(lIdContratto, CType(Tab_Contratto1.FindControl("DataGridNote"), DataGrid), "1")

            If TipoContratto <> "7" Then
                CType(Tab_Registrazione1.FindControl("txtPercTotSu"), Label).Text = par.IfNull(myReader("perc_tr_canone"), "")
                CType(Tab_Registrazione1.FindControl("txtPercConduttore"), Label).Text = par.IfNull(myReader("perc_conduttore"), "")
                CType(Tab_Registrazione1.FindControl("txtPercLocatore"), Label).Text = 100 - par.IfNull(myReader("perc_conduttore"), "0")
            Else
                CType(Tab_Registrazione1.FindControl("txtPercTotSu"), Label).Text = "0"
                CType(Tab_Registrazione1.FindControl("txtPercConduttore"), Label).Text = "0"
                CType(Tab_Registrazione1.FindControl("txtPercLocatore"), Label).Text = "0"

            End If
            CType(Tab_Registrazione1.FindControl("cmbModVersamento"), DropDownList).SelectedIndex = -1
            CType(Tab_Registrazione1.FindControl("cmbModVersamento"), DropDownList).Items.FindByValue(par.IfNull(myReader("VERSAMENTO_TR"), "A")).Selected = True

            Dim TASSA_REGISTRAZIONE As Double = 0
            If TipoContratto <> "7" Then
                If CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text <> "" Then

                    If DateDiff(DateInterval.Month, CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text), CDate(Format(Now, "dd/MM/yyyy"))) <= 12 Or CType(Tab_Registrazione1.FindControl("cmbModVersamento"), DropDownList).SelectedItem.Value = "A" Then

                        TASSA_REGISTRAZIONE = Format((CType(Tab_Registrazione1.FindControl("txtPercTotSu"), Label).Text * CanoneCorrente) / 100, "0")

                        If TipoContratto = "6" And (CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedItem.Text = "STANDARD" Or CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedItem.Text = "431 P.O.R.") Then
                            TASSA_REGISTRAZIONE = Format((CType(Tab_Registrazione1.FindControl("txtPercTotSu"), Label).Text * (CanoneCorrente - ((30 / 100) * CanoneCorrente))) / 100, "0")
                            CType(Tab_Registrazione1.FindControl("Label18"), Label).Text = "RIPARTIZIONE IMPOSTA DI REGISTRAZIONE (contratto agevolato ai sensi dell'art. 2, comma 3)"
                        End If

                        If TASSA_REGISTRAZIONE > LimiteTassaRegistrazione Then
                            CType(Tab_Registrazione1.FindControl("lblRegTot"), Label).Text = Format(TASSA_REGISTRAZIONE, "0.00")
                            CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercConduttore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                            CType(Tab_Registrazione1.FindControl("lblRegLoc"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercLocatore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                        Else
                            TASSA_REGISTRAZIONE = LimiteTassaRegistrazione
                            CType(Tab_Registrazione1.FindControl("lblRegTot"), Label).Text = Format(TASSA_REGISTRAZIONE, "0.00")
                            CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercConduttore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                            CType(Tab_Registrazione1.FindControl("lblRegLoc"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercLocatore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                        End If
                    Else
                        TASSA_REGISTRAZIONE = Format((CType(Tab_Registrazione1.FindControl("txtPercTotSu"), Label).Text * CanoneCorrente) / 100, "0")

                        If TipoContratto = "6" And (CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedItem.Text = "STANDARD" Or CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedItem.Text = "431 P.O.R.") Then
                            TASSA_REGISTRAZIONE = Format((CType(Tab_Registrazione1.FindControl("txtPercTotSu"), Label).Text * (CanoneCorrente - ((30 / 100) * CanoneCorrente))) / 100, "0")
                            CType(Tab_Registrazione1.FindControl("Label18"), Label).Text = "RIPARTIZIONE IMPOSTA DI REGISTRAZIONE (contratto agevolato ai sensi dell'art. 2, comma 3)"
                        End If

                        CType(Tab_Registrazione1.FindControl("lblRegTot"), Label).Text = Format(TASSA_REGISTRAZIONE, "0.00")
                        CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercConduttore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                        CType(Tab_Registrazione1.FindControl("lblRegLoc"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercLocatore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                    End If
                Else
                    TASSA_REGISTRAZIONE = LimiteTassaRegistrazione
                    CType(Tab_Registrazione1.FindControl("lblRegTot"), Label).Text = Format(TASSA_REGISTRAZIONE, "0.00")
                    CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercConduttore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")
                    CType(Tab_Registrazione1.FindControl("lblRegLoc"), Label).Text = Format((CType(Tab_Registrazione1.FindControl("txtPercLocatore"), Label).Text * TASSA_REGISTRAZIONE) / 100, "0.00")

                End If
            Else
                TASSA_REGISTRAZIONE = 0
                CType(Tab_Registrazione1.FindControl("lblRegTot"), Label).Text = Format(TASSA_REGISTRAZIONE, "0.00")
                CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text = "0,00"
                CType(Tab_Registrazione1.FindControl("lblRegLoc"), Label).Text = "0,00"

            End If
            CType(Tab_Registrazione1.FindControl("cmbModVersamento"), DropDownList).Enabled = False

            '31/03/2015 Controllo il flag del rimborso cauzionale SE è STATO fatto un cambio tipologia contrattuale
            par.cmd.CommandText = "SELECT * from siscom_mi.GESTIONE_CAMBIO_TIPO_CONTR where id_contratto_origine=" & lIdContratto
            Dim myReaderDP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderDP.Read Then
                If par.IfNull(myReaderDP("FL_CAUZ_DA_RESTITUIRE"), 0) = "1" And par.IfNull(myReaderDP("FL_CAUZ_RESTITUITA"), 0) = "0" Then
                    CType(Generale1.FindControl("lblAlertCauz"), Label).Visible = True
                Else
                    CType(Generale1.FindControl("lblAlertCauz"), Label).Visible = False
                End If
            End If
            myReaderDP.Close()
            'Fine 31/03/2015

            '31/03/2015 Controllo il flag dei dati di registrazione fiscale
            par.cmd.CommandText = "SELECT * from siscom_mi.GESTIONE_CAMBIO_TIPO_CONTR where id_contratto_nuovo=" & lIdContratto
            myReaderDP = par.cmd.ExecuteReader()
            If myReaderDP.Read Then
                If par.IfNull(myReaderDP("FL_IMPORT_DATI_REGISTR"), 0) = "1" Then
                    CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text = "0,00"
                    CType(Tab_Registrazione1.FindControl("lblRegLoc"), Label).Text = "0,00"
                End If
            End If
            myReaderDP.Close()
            'Fine 31/03/2015

            'CARICAMENTO TAB CANONE
            CType(Tab_Canone1.FindControl("txtCanoneIniziale"), TextBox).Text = Format(par.IfNull(myReader("imp_canone_iniziale"), 0), "0.00")

            CType(Tab_Canone1.FindControl("txtCanoneCorrente"), TextBox).Text = Format(par.IfNull(CanoneCorrente, 0), "0.00")
            'CType(Tab_Canone1.FindControl("txtCanoneCorrente"), TextBox).Text = Format(par.IfNull(myReader("imp_canone_iniziale"), 0), "0.00")

            CType(Tab_Canone1.FindControl("txtImportoCauzione"), TextBox).Text = Format(par.IfNull(myReader("imp_DEPOSITO_CAUZ"), 0), "0.00")
            CType(Tab_Canone1.FindControl("txtImportoAnticipo"), TextBox).Text = Format(par.IfNull(myReader("IMPORTO_ANTICIPO"), 0), "0.00")
            CType(Tab_Canone1.FindControl("txtLibrettoDeposito"), TextBox).Text = par.IfNull(myReader("LIBRETTO_DEPOSITO"), "")

            CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedIndex = -1
            CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).Items.FindByValue(par.IfNull(myReader("NRO_RATE"), "4")).Selected = True

            CType(Tab_Canone1.FindControl("cmbDestRate"), DropDownList).SelectedIndex = -1
            CType(Tab_Canone1.FindControl("cmbDestRate"), DropDownList).Items.FindByValue(par.IfNull(myReader("id_dest_rate"), "1")).Selected = True


            CType(Tab_Canone1.FindControl("lblistat2"), DropDownList).SelectedIndex = -1
            CType(Tab_Canone1.FindControl("lblistat2"), DropDownList).Items.FindByValue(par.IfNull(myReader("freq_var_istat"), "A")).Selected = True

            CType(Tab_Canone1.FindControl("lblistat4"), DropDownList).SelectedIndex = -1
            CType(Tab_Canone1.FindControl("lblistat4"), DropDownList).Items.FindByValue(par.IfNull(myReader("perc_istat"), "0")).Selected = True


            If par.IfNull(myReader("interessi_rit_pag"), "0") = "0" Then
                CType(Tab_Canone1.FindControl("chRitardoPagamenti"), CheckBox).Checked = False
            Else
                CType(Tab_Canone1.FindControl("chRitardoPagamenti"), CheckBox).Checked = True
            End If

            If par.IfNull(myReader("interessi_cauzione"), "0") = "0" Then
                CType(Tab_Canone1.FindControl("checkInteressiCauzione"), CheckBox).Checked = False
            Else
                CType(Tab_Canone1.FindControl("checkInteressiCauzione"), CheckBox).Checked = True
            End If

            '13/03/2015
            If par.IfNull(myReader("FL_INTERESSI_C"), "0") = "0" Then
                CType(Tab_Canone1.FindControl("checkInteressiDopo"), CheckBox).Checked = False
            Else
                CType(Tab_Canone1.FindControl("checkInteressiDopo"), CheckBox).Checked = True
            End If

            If par.IfNull(myReader("fl_conguaglio"), "0") = "0" Then
                CType(Tab_Canone1.FindControl("checkConguaglioBoll"), CheckBox).Checked = False
            Else
                CType(Tab_Canone1.FindControl("checkConguaglioBoll"), CheckBox).Checked = True
            End If

            If par.IfNull(myReader("invio_bolletta"), "0") = "0" Then
                CType(Tab_Canone1.FindControl("checkInvioBoll"), CheckBox).Checked = False
            Else
                CType(Tab_Canone1.FindControl("checkInvioBoll"), CheckBox).Checked = True
            End If

            If TipoContratto <> "12" And TipoContratto <> "3" And TipoContratto <> "6" And TipoContratto <> "7" And LetteraERP <> "B" And TipoContratto <> "10" Then
                If CType(Generale1.FindControl("lblStato"), Label).Text <> "BOZZA" Then
                    CaricaDettCanoni(lIdContratto)
                Else
                    testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                                    & "<tr>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA CALCOLO</strong></span></td>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>AREA ECONOMICA</strong></span></td>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>CLASSE</strong></span></td>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA VALIDITA'</strong></span></td>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>CANONE</strong></span></td>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>ORIGINE</strong></span></td>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "</tr>"
                    testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(Now, "dd/MM/yyyy") & "</span></td>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                            & "</tr>"

                    'RicavaTestoAreaEconomica

                    CType(Tab_Canone1.FindControl("lblDettaglioCanoni"), Label).Text = testoTabella & "</table>"

                End If
            Else
                If TipoContratto = "10" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.unita_assegnate where PROVENIENZA='Z' and id_DICHIARAZIONE=" & lIdDichiarazione
                    Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAA.Read Then

                        TIPO_CANONE_APPLICATO = par.IfNull(myReaderAA("TIPO_APPLICATO"), "")
                        CANONE_ALTERNATIVO = par.IfNull(myReaderAA("CANONE_ALT"), "0")

                        CType(Tab_Canone1.FindControl("label4"), Label).Text = "E' STATO APPLICATO IL CANONE " & TIPO_CANONE_APPLICATO & ", IL CANONE ALTERNATIVO SAREBBE STATO PARI A EURO " & CANONE_ALTERNATIVO
                        CType(Tab_Canone1.FindControl("ImgRicalcolaC"), ImageButton).Visible = False

                    End If
                    myReaderAA.Close()

                    CaricaDettCanoni(lIdContratto)
                End If

                If TipoContratto = "6" And (par.IfNull(myReader("dest_uso"), "0") = "D" Or par.IfNull(myReader("dest_uso"), "0") = "V") Then
                    CaricaDettCanoni(lIdContratto)
                End If
            End If
            If TipoContratto = "1" Or TipoContratto = "8" Or TipoContratto = "12" Or TipoContratto = "10" Or TipoContratto = "5" Then
                CType(Tab_Conduttore1.FindControl("lblElencoComponenti"), Label).Text = DettaglioComponenti()
            Else
                CType(Tab_Conduttore1.FindControl("lblElencoComponenti"), Label).Text = ""
            End If

            'CARICO TAB COMUNICAZIONI
            CType(Tab_Comunicazioni1.FindControl("cmbTipoViaCor"), DropDownList).SelectedIndex = -1
            CType(Tab_Comunicazioni1.FindControl("cmbTipoViaCor"), DropDownList).Items.FindByText(par.IfNull(myReader("TIPO_COR"), "VIA")).Selected = True

            CType(Tab_Comunicazioni1.FindControl("cmbCommissariato"), DropDownList).SelectedIndex = -1
            CType(Tab_Comunicazioni1.FindControl("cmbCommissariato"), DropDownList).Items.FindByValue(par.IfNull(myReader("ID_COMMISSARIATO"), "NULL")).Selected = True


            CType(Tab_Comunicazioni1.FindControl("txtPresso"), TextBox).Text = par.IfNull(myReader("PRESSO_COR"), "")
            CType(Tab_Comunicazioni1.FindControl("txtViaCor"), TextBox).Text = par.IfNull(myReader("VIA_COR"), "")
            CType(Tab_Comunicazioni1.FindControl("txtCivicoCor"), TextBox).Text = par.IfNull(myReader("CIVICO_COR"), "")
            CType(Tab_Comunicazioni1.FindControl("txtCAPCOR"), TextBox).Text = par.IfNull(myReader("CAP_COR"), "")
            CType(Tab_Comunicazioni1.FindControl("txtLuogoCor"), TextBox).Text = par.IfNull(myReader("LUOGO_COR"), "")
            CType(Tab_Comunicazioni1.FindControl("txtSiglaCor"), TextBox).Text = par.IfNull(myReader("SIGLA_COR"), "")


            CaricaSchemaBoll()

            myReader.Close()
            myReaderVer.Close()


            If TipoContratto = "3" Then
                CType(Tab_Canone1.FindControl("lblistat"), Label).Visible = True
                CType(Tab_Canone1.FindControl("lblistat1"), Label).Visible = True
                CType(Tab_Canone1.FindControl("lblistat3"), Label).Visible = True

                CType(Tab_Canone1.FindControl("ImgIstat"), WebControls.Image).Visible = True

                CType(Tab_Canone1.FindControl("ImgIstat"), WebControls.Image).Attributes.Add("onclick", "javascript:var fin;fin=window.open('SceltaIstat2.aspx');fin.focus();")
                CType(Tab_Canone1.FindControl("lblistat2"), DropDownList).Visible = True
                CType(Tab_Canone1.FindControl("lblistat4"), DropDownList).Visible = True
            End If

            If TipoContratto = "6" Then
                CType(Tab_Canone1.FindControl("lblistat"), Label).Visible = True
                CType(Tab_Canone1.FindControl("lblistat1"), Label).Visible = True
                CType(Tab_Canone1.FindControl("lblistat3"), Label).Visible = True

                CType(Tab_Canone1.FindControl("ImgIstat"), WebControls.Image).Visible = True
                HttpContext.Current.Session.Add("BB", "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as ""INIZIO"",to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as ""FINE"", RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS ""IMP_CANONE_INIZIALE"" ,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM SISCOM_MI.ADEGUAMENTO_ISTAT,SISCOM_MI.RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.ID=" & lIdContratto & " ORDER BY ID DESC")

                CType(Tab_Canone1.FindControl("ImgIstat"), WebControls.Image).Attributes.Add("onclick", "javascript:var fin;fin=window.open('SceltaIstat2.aspx');fin.focus();")
                CType(Tab_Canone1.FindControl("lblistat2"), DropDownList).Visible = True
                CType(Tab_Canone1.FindControl("lblistat4"), DropDownList).Visible = True

            End If

            Dim FATTA_FINE_CONTRATTO As Boolean = True

            If SolaLettura = "1" Then
                speseunita.Value = "0"
                'CType(Tab_Bollette1.FindControl("btnModificaBolletta"), ImageButton).Visible = False
                'CType(Tab_Bollette1.FindControl("btnAnnullaBolletta"), ImageButton).Visible = False
                'CType(Tab_Bollette1.FindControl("imgModulo"), System.Web.UI.WebControls.Image).Visible = False
            End If

            VisualizzaPreventivo()

            Dim StringaTabella As String = ""
            Dim adISTAT As Double = 0
            Dim BOLLO_BOLLETTA As Double = 0
            Dim SPESEPOSTALI As Double = 0
            Dim AI As Double = 0
            Dim SPMAV As Double = 0
            Dim AltriAD As Double = 0

            Dim Dett1 As String = "0,00"
            Dim Dett2 As String = "0,00"
            Dim Dett3 As String = "0,00"
            Dim Dett4 As Double = 0
            Dim dettagli As String = "<table cellpadding='0' cellspacing='0' style='width:100%;font-family: arial; font-size: 8pt;'>"


            AI = (CDbl(par.PuntiInVirgole(CType(Tab_Canone1.FindControl("txtCanoneIniziale"), TextBox).Text)) / CDbl(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedItem.Value))

            Dett3 = "<tr><td>CANONE/INDENNITA</td><td align='right'>" & CType(Tab_Canone1.FindControl("txtCanoneIniziale"), TextBox).Text & "</td></tr>"

            Dett4 = Dett4 + CDbl(par.PuntiInVirgole(CType(Tab_Canone1.FindControl("txtCanoneIniziale"), TextBox).Text))
            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo=2 and ID_CONTRATTO=" & lIdContratto
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                adISTAT = (CDbl(par.PuntiInVirgole(par.IfNull(myReaderA(0), 0))) / CDbl(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedItem.Value))
                Dett4 = Dett4 + CDbl(par.PuntiInVirgole(par.IfNull(myReaderA(0), 0)))
                Dett1 = "<tr><td>MONTANTE/ADEG. ISTAT</td><td align='right'>" & Format(par.IfNull(myReaderA(0), 0), "0.00") & "</td></tr>"
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo<>2 and ID_CONTRATTO=" & lIdContratto
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                AltriAD = (CDbl(par.PuntiInVirgole(par.IfNull(myReaderA(0), 0))) / CDbl(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedItem.Value))
                Dett4 = Dett4 + CDbl(par.PuntiInVirgole(par.IfNull(myReaderA(0), 0)))
                Dett2 = "<tr><td>ADEGUAMENTO CANONE/IND.</td><td align='right'>" & Format(par.IfNull(myReaderA(0), 0), "0.00") & "</td></tr>"

            End If
            myReaderA.Close()

            StringaTabella = "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">CANONE/INDENNITA</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(AI, "0.00") & "</td></tr>"

            If adISTAT <> 0 Then
                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">MONTANTE/ADEG. ISTAT</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(adISTAT, "0.00") & "</td></tr>"
            End If

            If AltriAD <> 0 Then
                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">ADEG. CANONE/IND.</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(AltriAD, "0.00") & "</td></tr>"
            End If

            CType(Tab_Canone1.FindControl("Label9"), Label).Text = dettagli & Dett3 & Dett1 & Dett2 & "<tr><td>TOTALE</td><td align='right'>" & Format(Dett4, "0.00") & "</td></tr></table>"
            CType(Tab_Canone1.FindControl("txtCanoneCorrente"), TextBox).Attributes.Add("onmouseover", "javascript:VisualizzaDettagliCanone();")
            CType(Tab_Canone1.FindControl("txtCanoneCorrente"), TextBox).Attributes.Add("onmouseout", "javascript:NascondiDettagliCanone();")


            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO_BOLLETTA = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            If BOLLO_BOLLETTA > 0 Then
                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">BOLLO</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(BOLLO_BOLLETTA, "0.00") & "</td></tr>"
            End If

            If SPESEPOSTALI > 0 Then
                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">SPESE POSTALI</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESEPOSTALI, "0.00") & "</td></tr>"
            End If

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPMAV = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">SPESE MAV</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPMAV, "0.00") & "</td></tr>"
            End If
            myReaderA.Close()

            CType(Tab_SchemaBollette1.FindControl("lblVociAutomatiche"), Label).Text = "<table><tr><td style=" & Chr(34) & "font-family: Arial; font-size: 9px; font-weight: bold" & Chr(34) & ">OLTRE LE VOCI SOPRA INDICATE, SARANNO AUTOMATICAMENTE INSERITE IN BOLLETTA</td><td></td></tr>" & StringaTabella & "</table>"

            Dim SSS As String = ""
            Dim anagra As Long = 0
            Dim CONTA As Integer = 0

            par.cmd.CommandText = "select soggetti_contrattuali.id_anagrafica from siscom_mi.soggetti_contrattuali where (cod_tipologia_occupante='INTE' OR COD_TIPOLOGIA_OCCUPANTE='EXINTE') and id_contratto=" & lIdContratto

            myReaderA = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                anagra = par.IfNull(myReaderA("id_anagrafica"), -1)

                par.cmd.CommandText = "select COD_FISCALE FROM SISCOM_MI.ANAGRAFICA where id=" & anagra
                Dim myReaderAX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderAX.Read Then
                    par.cmd.CommandText = "select rapporti_utenza.id,rapporti_utenza.cod_contratto,rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC,rapporti_utenza.DATA_DECORRENZA,rapporti_utenza.DATA_SCADENZA,SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS ""STATO"" from siscom_mi.rapporti_utenza,siscom_mi.soggetti_contrattuali,SISCOM_MI.ANAGRAFICA where rapporti_utenza.id=soggetti_contrattuali.id_contratto and Soggetti_contrattuali.cod_tipologia_occupante='INTE' AND ANAGRAFICA.COD_FISCALE='" & par.IfNull(myReaderAX("COD_FISCALE"), "") & "' AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO<>" & lIdContratto
                    Dim myReaderAX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReaderAX1.Read
                        CONTA = CONTA + 1
                        If CONTA < 8 Then
                            SSS = SSS & par.IfNull(myReaderAX1("COD_CONTRATTO"), "SENZA CODICE") & "-" & par.IfNull(myReaderAX1("COD_TIPOLOGIA_CONTR_LOC"), "") & "-" & par.IfNull(myReaderAX1("STATO"), "") & "</br>"
                        End If
                    Loop
                    myReaderAX1.Close()
                End If
                myReaderAX.Close()

            Loop
            myReaderA.Close()

            If SSS <> "" Then
                If CONTA < 8 Then
                    CType(Generale1.FindControl("lblAltriAttivi"), Label).Text = SSS
                Else
                    SSS = SSS & "Altri contratti..."
                    CType(Generale1.FindControl("lblAltriAttivi"), Label).Text = SSS
                End If
            End If

            Select Case TipoContratto
                Case "1", "2", "7", "8", "10", "12"
                    CType(Tab_Contratto1.FindControl("ChBolloEsente"), CheckBox).Visible = False
                Case Else
                    CType(Tab_Contratto1.FindControl("ChBolloEsente"), CheckBox).Visible = True
            End Select

            If LBLVIRTUALE.Visible = True Then
                If CType(Generale1.FindControl("lblStato"), Label).Text = "CHIUSO" Then
                    Tab_Bollette1.DisattivaTuttoVirtuale()
                End If
                speseunita.Value = "0"
                CType(Tab_Contratto1.FindControl("ChBolloEsente"), CheckBox).Visible = False

                If SolaLettura <> "1" Then
                    CType(Tab_Contratto1.FindControl("txtDataRiconsegna"), TextBox).Enabled = True
                    CType(Tab_Contratto1.FindControl("txtDataRiconsegna"), TextBox).ReadOnly = False

                    CType(Tab_Contratto1.FindControl("txtDataAppPresloggio"), TextBox).Enabled = True
                    CType(Tab_Contratto1.FindControl("txtDataAppPresloggio"), TextBox).ReadOnly = False

                    CType(Tab_Contratto1.FindControl("txtOraAppPresloggio"), TextBox).Enabled = True
                    CType(Tab_Contratto1.FindControl("txtOraAppPresloggio"), TextBox).ReadOnly = False

                    CType(Tab_Contratto1.FindControl("txtDataAppSloggio"), TextBox).Enabled = True
                    CType(Tab_Contratto1.FindControl("txtDataAppSloggio"), TextBox).ReadOnly = False

                    CType(Tab_Contratto1.FindControl("txtOraAppSloggio"), TextBox).Enabled = True
                    CType(Tab_Contratto1.FindControl("txtOraAppSloggio"), TextBox).ReadOnly = False
                    CType(Tab_Contratto1.FindControl("txtDataDisdetta"), TextBox).Enabled = True
                    CType(Tab_Contratto1.FindControl("txtDataDisdetta"), TextBox).ReadOnly = False

                    'CType(Tab_Bollette1.FindControl("btnAnnullaBolletta"), ImageButton).Visible = True


                End If
            Else
                
            End If

            'par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=39"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    If par.IfNull(myReaderA("VALORE"), "0") = "0" Then
            '        CType(Tab_Bollette1.FindControl("imgModulo"), System.Web.UI.WebControls.Image).Visible = False
            '        CType(Tab_Bollette1.FindControl("ImgMavOnLine"), System.Web.UI.WebControls.Image).Visible = False
            '    End If
            'End If
            'myReaderA.Close()


            par.cmd.CommandText = "select id from siscom_mi.bol_rateizzazioni where id_contratto = " & lIdContratto
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.HasRows Then
                RateizInCorso.Value = 1
            Else
                RateizInCorso.Value = 0
            End If
            myReaderA.Close()


            CaricaTabBollette()

            'MenuFunzioni()
            DataScadCont.Value = CType(Tab_Contratto1.FindControl("txtDataScadenza"), TextBox).Text
            DataScadRinn.Value = CType(Tab_Contratto1.FindControl("txtDataSecScadenza"), TextBox).Text

            'max'15/12/2014 visualizzazione dati reg
            If ModalitaPagamento <> "" Or TipoPosizione <> "" Then
                par.cmd.CommandText = "select descrizione from SISCOM_MI.TIPOLOGIA_POSIZIONE where id=" & par.IfEmpty(TipoPosizione, "-1")
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    CType(Tab_Registrazione1.FindControl("lblTipoPosizione"), Label).Text = par.IfNull(myReaderA("descrizione"), "")
                End If
                myReaderA.Close()
                par.cmd.CommandText = "select descrizione from SISCOM_MI.TIPOLOGIA_PAGAMENTO where id=" & par.IfEmpty(ModalitaPagamento, "-1")
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    CType(Tab_Registrazione1.FindControl("lblModoPagamento"), Label).Text = par.IfNull(myReaderA("descrizione"), "")
                End If
                myReaderA.Close()
            End If
            '---------------------

            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                SolaLettura = "1"
                lettura.Value = "1"
                'Session.Item("LAVORAZIONE") = "0"
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Il rapporto aperto da altro operatore, sarà visualizzato in sola lettura!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
                bloccato.Value = "1"
                GoTo InSolaLettura
            Else
                'par.myTrans.Rollback()
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If
        Catch ex As Exception
            'par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Function

    Private Function DettaglioComponenti() As String
        Dim stringaComp As String = "" ' = "<table cellpadding='3' cellspacing='0' width='120%'>"
        Dim motivo As String = ""
        Dim risultato As Boolean = False
        Try
            stringaComp = "<table cellpadding='0' cellspacing='1' width='1010px'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 15px; width: 250px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;color:#8080FF'><strong>TIPO DOMANDA</strong></span></td>" _
                        & "<td style='height: 15px; width: 150px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;color:#8080FF'><strong>NUM.PROTOCOLLO</strong></span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;color:#8080FF'><strong>INIZIO VALIDITA'</strong></span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;color:#8080FF'><strong>FINE VALIDITA'</strong></span></td>" _
                        & "<td style='height: 15px; width: 40px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;color:#8080FF'><strong></strong></span></td>" _
                        & "</tr></table>"

            stringaComp = stringaComp & "<table cellpadding='0' cellspacing='1' width='1010px' style='border: 1px solid #8080FF;'>"
            par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.ID AS IDDICH,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOM,DOMANDE_BANDO_VSA.PG AS PGDOM,DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA,DOMANDE_BANDO_VSA.ID AS IDDOM,DICHIARAZIONI_VSA.DATA_INIZIO_VAL,DICHIARAZIONI_VSA.DATA_FINE_VAL FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & CodContratto1 & "' AND FL_AUTORIZZAZIONE = 1 ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReaderA.Read
                risultato = True
                'stringaComp = stringaComp & "<tr><td><span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReaderA("MOT_DOM"), "") & " " & par.IfNull(myReaderA("PGDOM"), "") & " " & par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_VAL"), "")) & " " & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_VAL"), "")) & " " & "</td><td><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("IDDICH"), "") & "&T=1','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Clicca qui per visualizzare la situaz.anagrafica corrispondente</a></span></td></tr>"
                If par.IfNull(myReaderA("ID_CAUSALE_DOMANDA"), "") = "30" Then
                    motivo = "AMPLIAMENTO (d'ufficio)"
                Else
                    motivo = par.IfNull(myReaderA("MOT_DOM"), "")
                End If
                stringaComp = stringaComp & "<tr>" _
                        & "<td style='height: 15px; width: 250px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & motivo & "</span></td>" _
                        & "<td style='height: 15px; width: 150px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReaderA("PGDOM"), "") & "</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_VAL"), "")) & "</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_VAL"), "")) & "</span></td>" _
                        & "<td style='height: 15px; width: 40px'>" _
                        & "<img alt='Info' title='Visualizza la situazione anagrafica' src='../../NuoveImm/Img_Info.png' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("IDDICH"), "") & "&T=1','Componenti','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & " style='cursor: pointer' />" _
                        & "</tr>"

                '& "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("IDDICH"), "") & "&T=1','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Situaz.anagrafica</a></span></td>" _
            End While
            myReaderA.Close()

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE,UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL,UTENZA_DICHIARAZIONI.DATA_FINE_VAL FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
            & "AND RAPPORTO='" & CodContratto1 & "' ORDER BY ID_BANDO DESC"
            myReaderA = par.cmd.ExecuteReader
            While myReaderA.Read
                risultato = True
                'stringaComp = stringaComp & "<tr><td><span style='font-size: 8pt; font-family: Arial'><b>>></b> Presentata domanda di <b>" & par.IfNull(myReaderA("NOME_BANDO"), "").ToString.ToLower & "</b> con inizio validità " & par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_VAL"), "")) & " e fine validità " & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_VAL"), "")) & ".  " & "</td><td><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID"), "") & "&T=2','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Clicca qui per visualizzare la situaz.anagrafica corrispondente</a></span></td></tr>"
                stringaComp = stringaComp & "<tr>" _
                        & "<td style='height: 15px; width: 250px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReaderA("NOME_BANDO"), "") & "</span></td>" _
                        & "<td style='height: 15px; width: 150px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReaderA("PG"), "") & "</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_VAL"), "")) & "</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_VAL"), "")) & "</span></td>" _
                        & "<td style='height: 15px; width: 40px'>" _
                        & "<img alt='Info' title='Visualizza la situazione anagrafica' src='../../NuoveImm/Img_Info.png' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID"), "") & "&T=2','Componenti2','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & " style='cursor: pointer' />" _
                        & "</tr>"

                '& "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID"), "") & "&T=2','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Situaz.anagrafica</a></span></td>" _

            End While
            myReaderA.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO.*,bandi.descrizione FROM BANDI,DOMANDE_BANDO WHERE BANDI.ID=DOMANDE_BANDO.ID_BANDO AND CONTRATTO_NUM='" & CodContratto1 & "'"
            myReaderA = par.cmd.ExecuteReader
            While myReaderA.Read
                risultato = True
                'stringaComp = stringaComp & "<tr><td><span style='font-size: 8pt; font-family: Arial'><b>>></b> Presentata domanda di <b>bando ERP</b> (" & par.IfNull(myReaderA("DESCRIZIONE"), "").ToString.ToLower & ") in data " & par.FormattaData(par.IfNull(myReaderA("DATA_PG"), "")) & ".  " & "</td><td><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID_DICHIARAZIONE"), "") & "&T=3','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Clicca qui per visualizzare la situaz.anagrafica corrispondente</a></span></td></tr>"
                stringaComp = stringaComp & "<tr>" _
                        & "<td style='height: 15px; width: 250px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReaderA("DESCRIZIONE"), "") & "</span></td>" _
                        & "<td style='height: 15px; width: 150px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReaderA("PG"), "") & "</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>&nbsp</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>&nbsp</span></td>" _
                        & "<td style='height: 15px; width: 40px'>" _
                        & "<img alt='Info' title='Visualizza la situazione anagrafica' src='../../NuoveImm/Img_Info.png' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID_DICHIARAZIONE"), "") & "&T=3','Componenti3','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & " style='cursor: pointer' />" _
                        & "</tr>"
                '& "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID_DICHIARAZIONE"), "") & "&T=3','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Situaz.anagrafica</a></span></td>" _
            End While
            myReaderA.Close()


            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI_INIZIO.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI_INIZIO WHERE SOGGETTI_CONTRATTUALI_INIZIO.ID_CONTRATTO = " & lIdContratto
            myReaderA = par.cmd.ExecuteReader
            If myReaderA.Read Then
                risultato = True
                'stringaComp = stringaComp & "<tr><td><span style='font-size: 8pt; font-family: Arial'><b>>></b> Presentata domanda di <b>bando ERP</b> (" & par.IfNull(myReaderA("DESCRIZIONE"), "").ToString.ToLower & ") in data " & par.FormattaData(par.IfNull(myReaderA("DATA_PG"), "")) & ".  " & "</td><td><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID_DICHIARAZIONE"), "") & "&T=3','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Clicca qui per visualizzare la situaz.anagrafica corrispondente</a></span></td></tr>"
                stringaComp = stringaComp & "<tr>" _
                        & "<td style='height: 15px; width: 250px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>SITUAZIONE INIZIALE</span></td>" _
                        & "<td style='height: 15px; width: 150px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>&nbsp</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>&nbsp</span></td>" _
                        & "<td style='height: 15px; width: 130px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>&nbsp</span></td>" _
                        & "<td style='height: 15px; width: 40px'>" _
                        & "<img alt='Info' title='Visualizza la situazione anagrafica' src='../../NuoveImm/Img_Info.png' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & lIdContratto & "&T=4','Componenti4','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & " style='cursor: pointer' />" _
                        & "</tr>"
                '& "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzazioneCompNucleo.aspx?ID=" & par.IfNull(myReaderA("ID_DICHIARAZIONE"), "") & "&T=3','Componenti','height=400,top=200,left=410,width=580,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Situaz.anagrafica</a></span></td>" _
            End If
            myReaderA.Close()

            If risultato = True Then
                stringaComp = stringaComp & "</table>"
            Else
                stringaComp = ""
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione DettaglioComponenti) - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

        Return stringaComp

    End Function


    Function CaricaDettCanoni(ByVal idContratto As Long)
        Dim testoTabella1 As String = ""
        Dim IValidita As String = ""
        Dim FValidita As String = ""

        testoTabella1 = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA CALCOLO</strong></span></td>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>AREA ECONOMICA</strong></span></td>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>CLASSE</strong></span></td>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA VALIDITA'</strong></span></td>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>CANONE</strong></span></td>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>ORIGINE</strong></span></td>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "</tr>"
        par.cmd.CommandText = "SELECT CANONI_EC.*,T_TIPO_PROVENIENZA.DESCRIZIONE AS PROVENIENZA FROM SISCOM_MI.CANONI_EC,T_TIPO_PROVENIENZA WHERE CANONI_EC.TIPO_PROVENIENZA=T_TIPO_PROVENIENZA.ID (+) AND ID_CONTRATTO=" & lIdContratto & " ORDER BY nvl(fine_validita_can,'19000101') desc,data_calcolo desc"
        Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader22.Read
            If Len(par.IfNull(myReader22("INIZIO_VALIDITA_CAN"), "")) <> 4 Then
                IValidita = par.FormattaData(par.IfNull(myReader22("INIZIO_VALIDITA_CAN"), ""))
                FValidita = par.FormattaData(par.IfNull(myReader22("FINE_VALIDITA_CAN"), ""))
            Else
                IValidita = par.IfNull(myReader22("INIZIO_VALIDITA_CAN"), "")
                FValidita = par.IfNull(myReader22("FINE_VALIDITA_CAN"), "")
            End If
            testoTabella1 = testoTabella1 _
                & "<tr>" _
                & "<td style='height: 15px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(Mid(myReader22("DATA_CALCOLO"), 1, 8)) & " " & Mid(myReader22("DATA_CALCOLO"), 9, 2) & ":" & Mid(myReader22("DATA_CALCOLO"), 11, 2) & "</span></td>" _
                & "<td style='height: 15px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.RicavaAreaEconomica(par.IfNull(myReader22("ID_AREA_ECONOMICA"), "0")) & "</span></td>" _
                & "<td style='height: 15px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader22("SOTTO_AREA"), "") & "</span></td>" _
                & "<td style='height: 15px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & IValidita & "-" & FValidita & "</span></td>" _
                & "<td style='height: 15px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader22("CANONE"), "") & "</span></td>" _
                & "<td style='height: 15px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader22("PROVENIENZA"), "IMPORTATO") & "</span></td>" _
                & "<td style='height: 15px'>"

            If par.IfNull(myReader22("DATA_CALCOLO"), "200901010000") <> "200901010000" Then
                testoTabella1 = testoTabella1 & "<span style='font-size: 8pt; font-family: Arial'>" & "<a href='DettagliCalcolo.aspx?COD=" & CodContratto1 & "&DATA=" & myReader22("DATA_CALCOLO") & "&IDC=" & lIdContratto & "' target='_blank'>Visualizza Dettagli</span></td></tr>"
            Else
                testoTabella1 = testoTabella1 & "<span style='font-size: 8pt; font-family: Arial'></span></td></tr>"
            End If
        Loop
        myReader22.Close()
        If TipoContratto = "1" Or TipoContratto = "2" Or TipoContratto = "7" Or TipoContratto = "8" Then
            CType(Tab_Canone1.FindControl("lblDettaglioCanoni"), Label).Text = testoTabella1 & "</table>"
        Else
            CType(Tab_Canone1.FindControl("ImgRicalcolaC"), ImageButton).Visible = False
            CType(Tab_Canone1.FindControl("lblDettaglioCanoni"), Label).Text = ""
            CType(Tab_Canone1.FindControl("label4"), Label).Text = ""
        End If


    End Function

    Function CaricaCompContratto(ByRef IdContratto As Long)

        CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Clear()
        CType(Tab_Conduttore1.FindControl("lstComponenti"), ListBox).Items.Clear()
        'CARICO INTESTATARI
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & IdContratto
        Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader22.Read
            If par.IfNull(myReader22("RAGIONE_SOCIALE"), "") <> "" And par.IfNull(myReader22("PARTITA_IVA"), "") <> "" Then
                CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("RAGIONE_SOCIALE"), ""), 37) & " " & par.MiaFormat(par.IfNull(myReader22("PARTITA_IVA"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader22("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio"), "")), 10), myReader22("ID")))
            Else
                CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("COGNOME"), ""), 20) & " " & par.MiaFormat(par.IfNull(myReader22("NOME"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader22("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader22("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio"), "")), 10), myReader22("ID")))
            End If
        Loop
        myReader22.Close()


        'CARICO COINTESTATARI
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE='COINT' AND ID_CONTRATTO=" & IdContratto
        myReader22 = par.cmd.ExecuteReader()
        Do While myReader22.Read
            If par.IfNull(myReader22("RAGIONE_SOCIALE"), "") <> "" Then
                CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("RAGIONE_SOCIALE"), ""), 37) & " " & par.MiaFormat(par.IfNull(myReader22("PARTITA_IVA"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader22("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio"), "")), 10), myReader22("ID")))
            Else
                CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("COGNOME"), ""), 20) & " " & par.MiaFormat(par.IfNull(myReader22("NOME"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader22("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader22("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio"), "")), 10), myReader22("ID")))
            End If
        Loop
        myReader22.Close()


        'CARICAMENTO OCCUPANTI
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE<>'INTE' AND COD_TIPOLOGIA_OCCUPANTE<>'COINT' AND ID_CONTRATTO=" & IdContratto & " and NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "' order by anagrafica.cognome asc,anagrafica.nome asc,SOGGETTI_CONTRATTUALI.data_inizio"
        myReader22 = par.cmd.ExecuteReader()
        Do While myReader22.Read
            If par.IfNull(myReader22("RAGIONE_SOCIALE"), "") <> "" Then
                CType(Tab_Conduttore1.FindControl("lstComponenti"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("RAGIONE_SOCIALE"), ""), 37) & " " & par.MiaFormat(par.IfNull(myReader22("PARTITA_IVA"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader22("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio"), "")), 10), myReader22("ID")))
            Else
                CType(Tab_Conduttore1.FindControl("lstComponenti"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("COGNOME"), ""), 20) & " " & par.MiaFormat(par.IfNull(myReader22("NOME"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader22("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader22("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio"), "")), 10), myReader22("ID")))
            End If
        Loop
        myReader22.Close()

        'CARICAMENTO OSPITI
        CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Items.Clear()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.OSPITI WHERE ID_CONTRATTO=" & IdContratto & " ORDER BY DATA_AGG DESC"
        myReader22 = par.cmd.ExecuteReader()
        Do While myReader22.Read
            CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader22("nominativo"), ""), 30) & " " & par.MiaFormat(par.IfNull(myReader22("cod_fiscale"), ""), 16) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_inizio_ospite"), "")), 15) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader22("data_fine_ospite"), "")), 15), myReader22("ID")))
        Loop
        myReader22.Close()
    End Function

    Private Sub CaricaBollette(ByVal IdContratto As Long)
        Dim num_bolletta As String = ""
        Dim importobolletta As Double = 0
        Dim importopagato As Double = 0
        Dim ImportoTotBolletta As String = "0,00"

        Dim I As Integer = 0

        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items.Clear()
        par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.* from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND bol_bollette.id_contratto=" & IdContratto & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader2.Read
            Select Case par.IfNull(myReader2("n_rata"), "")
                Case "99" 'bolletta manuale
                    num_bolletta = "MA"
                Case "999" 'bolletta automatica
                    num_bolletta = "AU"
                Case "99999" 'bolletta di conguaglio
                    num_bolletta = "CO"
                Case Else
                    num_bolletta = Format(par.IfNull(myReader2("n_rata"), "??"), "00")
            End Select

            importobolletta = par.IfNull(myReader2("IMPORTO_TOTALE"), "0,00")
            importopagato = par.IfNull(myReader2("IMPORTO_PAGATO"), "0,00")

            Dim STATO As String = ""
            If par.IfNull(myReader2("FL_ANNULLATA"), "0") <> "0" Then
                STATO = "ANNUL."
            Else
                STATO = "VALIDA"
            End If

            If par.IfNull(myReader2("id_bolletta_ric"), "0") <> "0" Or par.IfNull(myReader2("ID_RATEIZZAZIONE"), "0") <> "0" Then
                STATO = "RICLA."
            End If

            CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items.Add(New ListItem(par.MiaFormat(num_bolletta, 2) & " " & par.MiaFormat(STATO, 7) & " " & par.IfNull(myReader2("ACRONIMO"), "---") & "   " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_da"), "")), 12) & "   " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_a"), "")), 12) & " " & par.MiaFormat(Format(importobolletta, "##,##0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_emissione"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_scadenza"), "")), 12) & " " & par.MiaFormat(Format(importopagato, "##,##0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_pagamento"), "")), 12) & " " & par.MiaFormat(par.IfNull(myReader2("note"), ""), 35) & " ", myReader2("ID")))

            If I Mod 2 <> 0 Then
                CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "#dcdada")
            Else
                CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "white")
            End If

            Select Case par.IfNull(myReader2("ID_TIPO"), "0")
                Case "3"
                    CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                Case "4"
                    CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
            End Select
            I = I + 1
        Loop
        myReader2.Close()
    End Sub

    Function CaricaDomandeInCorso() As String
        Dim trovati As String = ""

        CType(Generale1.FindControl("lblDomandeInCorso"), Label).Text = "---"

        par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & lIdContratto
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader2.Read
            If par.IfNull(myReader2("RAGIONE_SOCIALE"), "") = "" Then
                par.cmd.CommandText = "select domande_bando_fsa.pg from domande_bando_fsa,comp_nucleo_fsa where comp_nucleo_fsa.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_fsa.id_dichiarazione=comp_nucleo_fsa.id_dichiarazione"
                Dim myReader257 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -FSA</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_cambi.pg from domande_bando_cambi,comp_nucleo_cambi where comp_nucleo_cambi.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_cambi.id_dichiarazione=comp_nucleo_cambi.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -CAMBI</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_VSA.pg from domande_bando_VSA,comp_nucleo_VSA where DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=0 AND comp_nucleo_VSA.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_VSA.id_dichiarazione=comp_nucleo_VSA.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -VOLTURE</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_VSA.pg from domande_bando_VSA,comp_nucleo_VSA where DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=1 AND comp_nucleo_VSA.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_VSA.id_dichiarazione=comp_nucleo_VSA.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -SUBENTRO</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_VSA.pg from domande_bando_VSA,comp_nucleo_VSA where DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=2 AND comp_nucleo_VSA.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_VSA.id_dichiarazione=comp_nucleo_VSA.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -AMPLIAM.</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_VSA.pg from domande_bando_VSA,comp_nucleo_VSA where DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=3 AND comp_nucleo_VSA.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_VSA.id_dichiarazione=comp_nucleo_VSA.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -RID.CANONE</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_VSA.pg from domande_bando_VSA,comp_nucleo_VSA where DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4 AND comp_nucleo_VSA.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_VSA.id_dichiarazione=comp_nucleo_VSA.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -ART.22C.10</br>"
                End If
                myReader257.Close()

                If trovati <> "" Then
                    CType(Generale1.FindControl("lblDomandeInCorso"), Label).Text = trovati
                End If

            Else
            End If

        Loop
        myReader2.Close()

        'CARICAMENTO OCCUPANTI
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE<>'INTE' AND ID_CONTRATTO=" & lIdContratto & " and NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "'"
        myReader2 = par.cmd.ExecuteReader()
        Do While myReader2.Read
            If par.IfNull(myReader2("RAGIONE_SOCIALE"), "") = "" Then
                par.cmd.CommandText = "select domande_bando_fsa.pg from domande_bando_fsa,comp_nucleo_fsa where comp_nucleo_fsa.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_fsa.id_dichiarazione=comp_nucleo_fsa.id_dichiarazione"
                Dim myReader257 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -FSA</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_cambi.pg from domande_bando_cambi,comp_nucleo_cambi where comp_nucleo_cambi.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_cambi.id_dichiarazione=comp_nucleo_cambi.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -CAMBI</br>"
                End If
                myReader257.Close()

                par.cmd.CommandText = "select domande_bando_VSA.pg from domande_bando_VSA,comp_nucleo_VSA where (DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA=0 OR DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA=2) AND comp_nucleo_VSA.cod_fiscale='" & par.IfNull(myReader2("cod_fiscale"), "") & "' and domande_bando_VSA.id_dichiarazione=comp_nucleo_VSA.id_dichiarazione"
                myReader257 = par.cmd.ExecuteReader()
                If myReader257.Read Then
                    trovati = trovati & par.IfNull(myReader257("pg"), "") & " -VOLTURE</br>"
                End If
                myReader257.Close()

                If trovati <> "" Then
                    CType(Generale1.FindControl("lblDomandeInCorso"), Label).Text = trovati
                End If
            Else
            End If

        Loop
        myReader2.Close()
        CaricaDomandeInCorso = ""
    End Function

    Private Function MESE(ByVal TESTO As String) As String
        Select Case TESTO
            Case "01"
                MESE = "Gennaio"
            Case "02"
                MESE = "Febbraio"
            Case "03"
                MESE = "Marzo"
            Case "04"
                MESE = "Aprile"
            Case "05"
                MESE = "Maggio"
            Case "06"
                MESE = "Giugno"
            Case "07"
                MESE = "Luglio"
            Case "08"
                MESE = "Agosto"
            Case "09"
                MESE = "Settembre"
            Case "10"
                MESE = "Ottobre"
            Case "11"
                MESE = "Novembre"
            Case "12"
                MESE = "Dicembre"
        End Select
    End Function


    Public Property LetteraERP() As String
        Get
            If Not (ViewState("par_LetteraERP") Is Nothing) Then
                Return CStr(ViewState("par_LetteraERP"))
            Else
                Return "A"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_LetteraERP") = value
        End Set

    End Property

    Public Property CodContratto1() As String
        Get
            If Not (ViewState("par_CodContratto") Is Nothing) Then
                Return CStr(ViewState("par_CodContratto"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CodContratto") = value
        End Set

    End Property

    Public Property FL_NuovoContratto() As String
        Get
            If Not (ViewState("par_FL_NuovoContratto") Is Nothing) Then
                Return CStr(ViewState("par_FL_NuovoContratto"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FL_NuovoContratto") = value
        End Set

    End Property


    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property sSCALA() As String
        Get
            If Not (ViewState("par_sSCALA") Is Nothing) Then
                Return CStr(ViewState("par_sSCALA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSCALA") = value
        End Set

    End Property

    Public Property sPiano() As String
        Get
            If Not (ViewState("par_sPiano") Is Nothing) Then
                Return CStr(ViewState("par_sPiano"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPiano") = value
        End Set

    End Property

    Public Property sInterno() As String
        Get
            If Not (ViewState("par_sInterno") Is Nothing) Then
                Return CStr(ViewState("par_sInterno"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sInterno") = value
        End Set

    End Property

    Public Property sSub() As String
        Get
            If Not (ViewState("par_sSub") Is Nothing) Then
                Return CStr(ViewState("par_sSub"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSub") = value
        End Set

    End Property

    Public Property sFoglio() As String
        Get
            If Not (ViewState("par_sFoglio") Is Nothing) Then
                Return CStr(ViewState("par_sFoglio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sFoglio") = value
        End Set

    End Property

    Public Property sCatastale() As String
        Get
            If Not (ViewState("par_sCatastale") Is Nothing) Then
                Return CStr(ViewState("par_sCatastale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCatastale") = value
        End Set

    End Property

    Public Property sClasse() As String
        Get
            If Not (ViewState("par_sClasse") Is Nothing) Then
                Return CStr(ViewState("par_sClasse"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sClasse") = value
        End Set

    End Property

    Public Property sIdEdificio() As Long
        Get
            If Not (ViewState("par_sIdEdificio") Is Nothing) Then
                Return CLng(ViewState("par_sIdEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_sIdEdificio") = value
        End Set
    End Property

    Public Property sIdComplesso() As Long
        Get
            If Not (ViewState("par_sIdComplesso") Is Nothing) Then
                Return CLng(ViewState("par_sIdComplesso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_sIdComplesso") = value
        End Set
    End Property

    Public Property sRendita() As String
        Get
            If Not (ViewState("par_sRendita") Is Nothing) Then
                Return CStr(ViewState("par_sRendita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sRendita") = value
        End Set

    End Property


    Public Property DataDisponibilitaAlloggio() As String
        Get
            If Not (ViewState("par_DataDisponibilita") Is Nothing) Then
                Return CStr(ViewState("par_DataDisponibilita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DataDisponibilita") = value
        End Set

    End Property


    '

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property

    Public Property TIPO_CANONE_APPLICATO() As String
        Get
            If Not (ViewState("par_TIPO_CANONE_APPLICATO") Is Nothing) Then
                Return CStr(ViewState("par_TIPO_CANONE_APPLICATO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TIPO_CANONE_APPLICATO") = value
        End Set

    End Property

    Public Property CANONE_ALTERNATIVO() As String
        Get
            If Not (ViewState("par_CANONE_ALTERNATIVO") Is Nothing) Then
                Return CStr(ViewState("par_CANONE_ALTERNATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CANONE_ALTERNATIVO") = value
        End Set

    End Property


    Public Property NUMERO_OFFERTA() As String
        Get
            If Not (ViewState("par_NUMERO_OFFERTA") Is Nothing) Then
                Return CStr(ViewState("par_NUMERO_OFFERTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_NUMERO_OFFERTA") = value
        End Set

    End Property

    Public Property sParticella() As String
        Get
            If Not (ViewState("par_sParticella") Is Nothing) Then
                Return CStr(ViewState("par_sParticella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sParticella") = value
        End Set

    End Property

    Public Property SolaLettura() As String
        Get
            If Not (ViewState("par_SolaLettura") Is Nothing) Then
                Return CStr(ViewState("par_SolaLettura"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_SolaLettura") = value
        End Set
    End Property

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property

    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property

    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property

    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property

    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property

    Public Property UnitaContratto() As Long
        Get
            If Not (ViewState("par_UnitaContratto") Is Nothing) Then
                Return CLng(ViewState("par_UnitaContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_UnitaContratto") = value
        End Set

    End Property

    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property


    Public Property sAnnoSchema() As String
        Get
            If Not (ViewState("par_sAnnoSchema") Is Nothing) Then
                Return CStr(ViewState("par_sAnnoSchema"))
            Else
                Return "2010"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sAnnoSchema") = value
        End Set

    End Property

    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set
    End Property

    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property





    Public Property LetteraProvenienza() As String
        Get
            If Not (ViewState("par_LetteraProvenienza") Is Nothing) Then
                Return CStr(ViewState("par_LetteraProvenienza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_LetteraProvenienza") = value
        End Set

    End Property


    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property TipoContratto() As Long
        Get
            If Not (ViewState("par_TipoContratto") Is Nothing) Then
                Return CLng(ViewState("par_TipoContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_TipoContratto") = value
        End Set

    End Property

    Public Property lIdDomandaERP() As Long
        Get
            If Not (ViewState("par_lIdDomandaERP") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomandaERP"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomandaERP") = value
        End Set

    End Property

    Public Property lIdAU() As Long
        Get
            If Not (ViewState("par_lIdAU") Is Nothing) Then
                Return CLng(ViewState("par_lIdAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdAU") = value
        End Set

    End Property

    Public Property lIdDichiarazione() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property

    Public Property lIdContratto() As Long
        Get
            If Not (ViewState("par_lIdContratto") Is Nothing) Then
                Return CLng(ViewState("par_lIdContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdContratto") = value
        End Set

    End Property

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Tab1 = ""
        Tab2 = ""
        Tab3 = ""
        Tab4 = ""
        Tab5 = ""
        Tab6 = ""
        Tab7 = ""
        Tab8 = ""
        Select Case txttab.Value
            Case "1"
                Tab1 = "tabbertabdefault"
            Case "2"
                Tab2 = "tabbertabdefault"
            Case "3"
                Tab3 = "tabbertabdefault"
            Case "4"
                Tab4 = "tabbertabdefault"
            Case "5"
                Tab5 = "tabbertabdefault"
            Case "6"
                Tab6 = "tabbertabdefault"
            Case "7"
                Tab7 = "tabbertabdefault"
            Case "8"
                Tab8 = "tabbertabdefault"
        End Select
    End Sub

    Protected Sub imgEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgEsci.Click
        If txtModificato.Value <> "111" And txtModificato.Value <> "222" Then

            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Session.Remove("IDBollCOP")

            HttpContext.Current.Session.Remove(lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>window.close();</script>")
        Else
            txtModificato.Value = "1"
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Response.Write("<script>window.close();</script>")
        End If
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Function RicavaVia1(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String


        pos = InStr(1, indirizzo, " ")
        If pos > 0 Then
            via = Mid(indirizzo, 1, pos - 1)
            Select Case via
                Case "C.SO"
                    RicavaVia1 = "CORSO"
                Case "PIAZZA", "PZ.", "P.ZZA", "PIAZZETTA"
                    RicavaVia1 = "PIAZZA"
                Case "PIAZZALE", "P.LE"
                    RicavaVia1 = "PIAZZALE"
                Case "P.T"
                    RicavaVia1 = "PORTA"
                Case "S.T.R.", "STRADA"
                    RicavaVia1 = "STRADA"
                Case "V.", "VIA"
                    RicavaVia1 = "VIA"
                Case "VIALE", "V.LE"
                    RicavaVia1 = "VIALE"
                Case "ALZAIA"
                    RicavaVia1 = "ALZAIA"
                Case Else
                    RicavaVia1 = "VIA"
            End Select

        Else
            RicavaVia1 = ""
        End If

    End Function

    Private Function RicavaInd(ByVal indirizzo As String, ByVal TipoVia As String) As String
        RicavaInd = indirizzo

        RicavaInd = Trim(Replace(indirizzo, TipoVia, ""))

    End Function

    Public Property Capoluoghi() As String
        Get
            If Not (ViewState("par_Capoluoghi") Is Nothing) Then
                Return CStr(ViewState("par_Capoluoghi"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Capoluoghi") = value
        End Set

    End Property

    Private Function CaricaCapoluoghi()
        Capoluoghi = "AGRIGENTO (SICILIA) " _
        & "ALESSANDRIA(PIEMONTE) " _
        & "ANCONA(MARCHE) " _
& "AOSTA (VALLE D'AOSTA) " _
        & "AREZZO(TOSCANA) " _
        & "ASCOLI PICENO(MARCHE)) " _
        & "ASTI(PIEMONTE) " _
        & "AVELLINO(CAMPANIA) " _
        & "BARI(PUGLIA)  " _
        & "TRANI() " _
        & "ANDRIA() " _
        & "BARLETTA() " _
        & "BELLUNO(VENETO) " _
        & "BENEVENTO(CAMPANIA) " _
        & "BERGAMO(LOMBARDIA) " _
        & "BIELLA(PIEMONTE) " _
        & "BOLOGNA(EMILIA - ROMAGNA) " _
& "BOLZANO (TRENTINO-ALTO ADIGE) " _
        & "BRESCIA(LOMBARDIA) " _
        & "BRINDISI(PUGLIA) " _
        & "CAGLIARI(SARDEGNA) " _
        & "CALTANISSETTA(SICILIA) " _
        & "CAMPOBASSO(MOLISE) " _
        & "CARBONIA-IGLESIAS(SARDEGNA)) " _
        & "CASERTA(CAMPANIA) " _
        & "CATANIA(SICILIA) " _
        & "CATANZARO(CALABRIA) " _
        & "CHIETI(ABRUZZO) " _
        & "COMO(LOMBARDIA) " _
        & "COSENZA(CALABRIA) " _
        & "CREMONA(LOMBARDIA) " _
        & "CROTONE(CALABRIA) " _
        & "CUNEO(PIEMONTE) " _
        & "ENNA(SICILIA) " _
        & "FERMO(MARCHE) " _
        & "FERRARA(EMILIA - ROMAGNA) " _
        & "FIRENZE(TOSCANA) " _
        & "FOGGIA(PUGLIA) " _
        & "FORLÌ-CESENA(EMILIA - ROMAGNA) " _
        & "FROSINONE(LAZIO) " _
        & "GENOVA(LIGURIA) " _
& "GORIZIA (FRIULI-VENEZIA GIULIA) " _
        & "GROSSETO(TOSCANA) " _
        & "IMPERIA(LIGURIA) " _
        & "ISERNIA(MOLISE) " _
        & "LA SPEZIA(LIGURIA)) " _
        & "L'AQUILA (ABRUZZO) " _
        & "LATINA(LAZIO) " _
        & "LECCE(PUGLIA) " _
        & "LECCO(LOMBARDIA) " _
        & "LIVORNO(TOSCANA) " _
        & "LODI(LOMBARDIA) " _
        & "LUCCA(TOSCANA) " _
        & "MACERATA(MARCHE) " _
        & "MANTOVA(LOMBARDIA) " _
        & "MASSA-CARRARA(TOSCANA)) " _
        & "MATERA(BASILICATA) " _
        & "MESSINA(SICILIA) " _
        & " " _
        & "MODENA(EMILIA - ROMAGNA) " _
        & "MONZA() " _
        & "NAPOLI(CAMPANIA) " _
        & "NOVARA(PIEMONTE) " _
        & "NUORO(SARDEGNA) " _
        & "OLBIA(-TEMPIO(SARDEGNA)) " _
        & "ORISTANO(SARDEGNA) " _
        & "PADOVA(VENETO)" _
        & "PALERMO(SICILIA) " _
        & "PARMA(EMILIA - ROMAGNA) " _
        & "PAVIA(LOMBARDIA) " _
        & "PERUGIA(UMBRIA) " _
& "PESARO E URBINO (MARCHE) " _
   & "PESCARA(ABRUZZO) " _
      & "  PIACENZA(EMILIA - ROMAGNA) " _
        & "PISA(TOSCANA) " _
        & "PISTOIA(TOSCANA) " _
& "PORDENONE (FRIULI-VENEZIA GIULIA) " _
        & "POTENZA(BASILICATA) " _
        & "PRATO(TOSCANA) " _
        & "RAGUSA(SICILIA) " _
        & "RAVENNA(EMILIA - ROMAGNA) " _
        & "REGGIO CALABRIA(CALABRIA)) " _
        & "REGGIO EMILIA(EMILIA - ROMAGNA)) " _
        & "RIETI(LAZIO)  " _
        & "RIMINI(EMILIA - ROMAGNA) " _
        & "ROMA(LAZIO) " _
        & "ROVIGO(VENETO) " _
        & "SALERNO(CAMPANIA) " _
        & "MEDIO CAMPIDANO(SARDEGNA))  " _
        & "SASSARI(SARDEGNA)  " _
        & "SAVONA(LIGURIA) " _
        & "SIENA(TOSCANA) " _
        & "SIRACUSA(SICILIA) " _
        & "SONDRIO(LOMBARDIA) " _
        & "TARANTO(PUGLIA) " _
        & "TERAMO(ABRUZZO) " _
        & "TERNI(UMBRIA) " _
        & "TORINO(PIEMONTE) " _
        & "OGLIASTRA(SARDEGNA) " _
        & "TRAPANI(SICILIA) " _
& "TRENTO (TRENTINO-ALTO ADIGE) " _
        & "TREVISO(VENETO) " _
& "TRIESTE (FRIULI-VENEZIA GIULIA) " _
& "UDINE (FRIULI-VENEZIA GIULIA) " _
   & "     OSSOLA() " _
      & "  VARESE(LOMBARDIA) " _
        & "CUSIO() " _
        & "VENEZIA(VENETO) " _
        & "VERBANIA() " _
        & "VERBANO() " _
        & "VERCELLI(PIEMONTE) " _
        & "VERONA(VENETO) " _
        & "VIBO VALENTIA(CALABRIA)) " _
        & "VICENZA(VENETO) " _
        & "VITERBO(LAZIO)"
    End Function

    Private Function CaricaDati()

        Dim FREQ_VAR_ISTAT As String = ""
        Dim PERC_ISTAT As Double = 0
        Dim StatoRapporto As String = "LEGIT" 'legittimo o illegittimo

        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            CaricaCompContratto(lIdContratto)
            'CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Clear()
            'CType(Tab_Conduttore1.FindControl("lstComponenti"), ListBox).Items.Clear()

            'Dim INTESTAT As String = ""

            ''CARICO INTESTATARI
            'par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & lIdContratto
            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Do While myReader2.Read
            '    If par.IfNull(myReader2("RAGIONE_SOCIALE"), "") <> "" Then
            '        CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("RAGIONE_SOCIALE"), ""), 37) & " " & par.MiaFormat(par.IfNull(myReader2("PARTITA_IVA"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader2("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio"), "")), 10) & " " & par.FormattaData(par.IfNull(myReader2("data_fine"), "")), myReader2("ID")))
            '        INTESTAT = par.IfNull(myReader2("RAGIONE_SOCIALE"), "")
            '    Else
            '        CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("COGNOME"), ""), 20) & " " & par.MiaFormat(par.IfNull(myReader2("NOME"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader2("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader2("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio"), "")), 10) & " " & par.FormattaData(par.IfNull(myReader2("data_fine"), "")), myReader2("ID")))
            '        INTESTAT = par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
            '    End If

            'Loop
            'myReader2.Close()
            'CType(Tab_Comunicazioni1.FindControl("txtPresso"), TextBox).Text = INTESTAT

            ''CARICO COINTESTATARI
            'par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE='COINT' AND ID_CONTRATTO=" & lIdContratto
            'myReader2 = par.cmd.ExecuteReader()
            'Do While myReader2.Read
            '    If par.IfNull(myReader2("RAGIONE_SOCIALE"), "") <> "" Then
            '        CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("RAGIONE_SOCIALE"), ""), 37) & " " & par.MiaFormat(par.IfNull(myReader2("PARTITA_IVA"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader2("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio"), "")), 10) & " " & par.FormattaData(par.IfNull(myReader2("data_fine"), "")), myReader2("ID")))
            '    Else
            '        CType(Tab_Conduttore1.FindControl("lstIntestatari"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("COGNOME"), ""), 20) & " " & par.MiaFormat(par.IfNull(myReader2("NOME"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader2("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader2("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio"), "")), 10) & " " & par.FormattaData(par.IfNull(myReader2("data_fine"), "")), myReader2("ID")))
            '    End If
            'Loop
            'myReader2.Close()





            ''CARICAMENTO OCCUPANTI
            'par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.data_inizio,SOGGETTI_CONTRATTUALI.data_fine,TIPOLOGIA_PARENTELA.DESCRIZIONE AS ""PARENTE"",ANAGRAFICA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_PARENTELA.COD=SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND  COD_TIPOLOGIA_OCCUPANTE<>'INTE' AND COD_TIPOLOGIA_OCCUPANTE<>'COINT' AND ID_CONTRATTO=" & lIdContratto & " order by anagrafica.cognome asc,anagrafica.nome asc,SOGGETTI_CONTRATTUALI.data_inizio"
            'myReader2 = par.cmd.ExecuteReader()
            'Do While myReader2.Read
            '    If par.IfNull(myReader2("RAGIONE_SOCIALE"), "") <> "" Then
            '        CType(Tab_Conduttore1.FindControl("lstComponenti"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("RAGIONE_SOCIALE"), ""), 37) & " " & par.MiaFormat(par.IfNull(myReader2("PARTITA_IVA"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader2("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio"), "")), 10) & " " & par.FormattaData(par.IfNull(myReader2("data_fine"), "")), myReader2("ID")))
            '    Else
            '        CType(Tab_Conduttore1.FindControl("lstComponenti"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("COGNOME"), ""), 20) & " " & par.MiaFormat(par.IfNull(myReader2("NOME"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader2("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(UCase(par.IfNull(myReader2("PARENTE"), "")), 20) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio"), "")), 10) & " " & par.FormattaData(par.IfNull(myReader2("data_fine"), "")), myReader2("ID")))
            '    End If

            'Loop
            'myReader2.Close()

            ''CARICAMENTO OSPITI
            'CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Items.Clear()

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.OSPITI WHERE ID_CONTRATTO=" & lIdContratto & " ORDER BY DATA_AGG DESC"
            'myReader2 = par.cmd.ExecuteReader()
            'Do While myReader2.Read
            '    CType(Tab_Conduttore1.FindControl("lstOspiti"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("nominativo"), ""), 30) & " " & par.MiaFormat(par.IfNull(myReader2("cod_fiscale"), ""), 16) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_inizio_ospite"), "")), 15) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_fine_ospite"), "")), 15), myReader2("ID")))
            'Loop
            'myReader2.Close()



            'RICARICO IL CONDUTTORE NEL TAB GENERALE PERCHè POTREBBE ESSERE CAMBIATO
            Dim Conduttore As String = ""
            Dim Conduttore1 As String = ""

            par.cmd.CommandText = "SELECT anagrafica.id,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & lIdContratto & " AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' OR SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read
                Conduttore = Conduttore & par.IfNull(myReader1("INTESTATARIO"), "") & ", "
                'Conduttore1 = Conduttore1 & "<a href='#' onclick=" & Chr(34) & "window.open('anagrafica/Inserimento.aspx?DAC=1&ID=" & par.IfNull(myReader1("id"), "-1") & "','Anagrafe','height=500,top=0,left=0,width=500,scroll=no,status=no');" & Chr(34) & ">" & par.IfNull(myReader1("INTESTATARIO"), "") & "</a>, "
                Conduttore1 = Conduttore1 & "<a href='#' onclick=" & Chr(34) & "document.getElementById('Generale1_hAnagrafica').value='" & par.IfNull(myReader1("id"), "-1") & "';myOpacity10.toggle();" & Chr(34) & ">" & par.IfNull(myReader1("INTESTATARIO"), "") & "</a>, "

                txtCodAffittuario.Value = par.IfNull(myReader1("id"), "-1")
            Loop
            myReader1.Close()

            If Len(Conduttore) > 40 Then
                CType(Generale1.FindControl("lblConduttore"), Label).Font.Size = 8
            End If
            CType(Generale1.FindControl("lblConduttore"), Label).Text = Conduttore1
            txtconduttore.Value = Replace(Conduttore, "<br />", ", ")

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"



        Catch EX1 As Oracle.DataAccess.Client.OracleException
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Function

    Public Property sNuovoCodiceRapporto() As String
        Get
            If Not (ViewState("par_sNuovoCodiceRapporto") Is Nothing) Then
                Return CStr(ViewState("par_sNuovoCodiceRapporto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNuovoCodiceRapporto") = value
        End Set

    End Property

    Public Property lNuovoIdRapporto() As Long
        Get
            If Not (ViewState("par_lNuovoIdRapporto") Is Nothing) Then
                Return CLng(ViewState("par_lNuovoIdRapporto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lNuovoIdRapporto") = value
        End Set

    End Property

    Private Sub CanoniAttivazAbus(ByVal dataProssimoPeriodo As String, ByRef equoAnnuo As Decimal, ByRef canone2 As Decimal, ByRef descrizione As String, ByRef descrizioneEqc As String, ByRef descrizioneC2 As String)

        Dim giorniEquoCan As Integer = 0
        Dim giorniCanone2 As Integer = 0
        Dim giorni As Integer = 0

        Select Case Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 4, 2)
            Case "01", "03", "05", "07", "08", "10", "12"
                giorni = DateDiff("d", CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, "31" & Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 3, 8))
            Case "02"
                giorni = DateDiff("d", CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, "28" & Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 3, 8)) + 3
            Case Else
                giorni = DateDiff("d", CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, "30" & Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 3, 8)) + 1
        End Select

        If CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text) < CDate("30/11/2007") Then
            'equo
            giorniEquoCan = giorni + 30 * DateDiff("m", DateAdd(DateInterval.Month, 1, CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text)), "28/11/2007")
            equoAnnuo = par.CalcolaEQCAbusivi(UnitaContratto, Right(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 4))
            equoAnnuo = (equoAnnuo * 120) / 100
            equoAnnuo = Format((equoAnnuo / 360) * giorniEquoCan, "0.00")
            descrizioneEqc = "dal " & CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text & " al 28/11/2007"

            giorniCanone2 = 30 * DateDiff("m", DateAdd(DateInterval.Month, 1, CDate("28/11/2007")), "31/12/2008")
            par.CalcolaCanoneAbusivi(UnitaContratto, canone2, 1)
            canone2 = Format((canone2 / 360) * giorniCanone2, "0.00")
            descrizioneC2 = "dal 28/11/2007 al 31/12/2008"
        End If

        If CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text) > CDate("30/11/2007") And CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text) < CDate("31/12/2008") Then
            giorniCanone2 = giorni + 30 * DateDiff("m", DateAdd(DateInterval.Month, 1, CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text)), "31/12/2008")

            par.CalcolaCanoneAbusivi(UnitaContratto, canone2, 1)
            canone2 = Format((canone2 / 360) * giorniCanone2, "0.00")
            descrizioneC2 = "dal " & CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text & " al 31/12/2008"
        End If

        If descrizioneEqc <> "" Or descrizioneC2 <> "" Then
            descrizione = "dal 01/01/2009 al " & Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(dataProssimoPeriodo))), "dd/MM/yyyy")
        Else
            descrizione = "dal " & CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text & " al " & Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(dataProssimoPeriodo))), "dd/MM/yyyy")
        End If

    End Sub


    Protected Sub controllaContrColl()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim ContrTemp As String = ""
            Dim DOMANDACONTR As Integer = 0

            par.cmd.CommandText = "SELECT rapporti_utenza.id_domanda_abus, rapporti_utenza.cod_contratto from siscom_mi.rapporti_utenza where ID=" & lIdContratto
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("id_domanda_abus"), -1) <> -1 Then
                    CType(Generale1.FindControl("ContrColl"), Object).style.add("display", "block")
                End If

                If par.IfNull(myReader1("cod_contratto"), "") <> "" Then
                    ContrTemp = par.IfNull(myReader1("cod_contratto"), "")
                End If

            End If
            myReader1.Close()

            par.cmd.CommandText = "select domande_bando_vsa.cod_contratto_bozza from domande_bando_vsa, siscom_mi.rapporti_utenza where rapporti_utenza.cod_contratto=domande_bando_vsa.COD_CONTRATTO_BOZZA and domande_bando_vsa.fl_autorizzazione='1' AND COD_CONTRATTO_BOZZA IN (SELECT COD_CONTRATTO_BOZZA FROM DOMANDE_BANDO_VSA WHERE CONTRATTO_NUM = '" & ContrTemp & "')"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("cod_contratto_bozza"), "") <> "" Then
                    CType(Generale1.FindControl("ContrColl"), Object).style.add("display", "block")
                End If
            End If
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub VisualizzaPreventivo()

        Dim TestoBollettino1 As String = ""
        Dim TotPreventivoBollettino1 As Double = 0

        Dim TestoBollettino2 As String = ""
        Dim TotPreventivoBollettino2 As Double = 0
        Dim Buono As Boolean = True
        Dim impCauzione As Decimal = 0
        Dim SPESE_ISTRUTTORIA As Double = 0
        Dim TotPreventivo As Double = 0
        Dim SPESE_BOLLO As Double = 0
        Dim StringaTabella As String = ""

        CType(Tab_Bollette1.FindControl("lblPreventivo"), Label).Text = ""
        CType(Tab_Bollette1.FindControl("lblPreventivo2"), Label).Text = ""

        '31-03-2015 Recupero importo cauzione vecchio contratto
        par.cmd.CommandText = "SELECT * FROM siscom_mi.GESTIONE_CAMBIO_TIPO_CONTR WHERE ID_CONTRATTO_NUOVO=" & lIdContratto & ""
        Dim myReaderUA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderUA.Read Then
            impCauzione = CDbl(par.PuntiInVirgole(par.IfNull(myReaderUA("IMPORTO_CAUZIONE"), 0)))
        End If
        myReaderUA.Close()
        '31-03-2015 Fine

        If CType(Generale1.FindControl("lblStato"), Label).Text = "BOZZA" Then
            par.cmd.CommandText = "select * from SISCOM_MI.BOL_BOLLETTE where NOTE IN ('ATTIVAZIONE CONTRATTO BOLLETTINO N.1-VARIE','ATTIVAZIONE CONTRATTO BOLLETTINO N.1-DEP.CAUZIONALE') AND  ID_BOLLETTA_STORNO IS NULL AND ID_TIPO IN (10,9) AND ID_contratto=" & lIdContratto
            Dim myReaderQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderQ.HasRows = False Then

                'INIZIO BOLLETTINO N.1


                Select Case TipoContratto
                    Case "1", "2", "8", "10", "12"
                        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=22"
                    Case "5", "6", "7"
                        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=23"
                    Case "3"
                        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=24"
                End Select

                Dim myReaderJJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderJJ.Read Then
                    SPESE_ISTRUTTORIA = (CDbl(par.PuntiInVirgole(par.IfNull(myReaderJJ("VALORE"), 0))) * CDbl(par.PuntiInVirgole(CType(Tab_Canone1.FindControl("txtCanoneCorrente"), TextBox).Text))) / 100
                End If
                myReaderJJ.Close()
                TotPreventivo = TotPreventivo + SPESE_ISTRUTTORIA
                If SPESE_ISTRUTTORIA > 0 Then
                    StringaTabella = "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">SPESE ISTRUTTORIA</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESE_ISTRUTTORIA, "0.00") & "</td></tr>"
                End If
                If CDbl(par.VirgoleInPunti(CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text)) > 0 Then
                    TotPreventivo = TotPreventivo + CDbl(par.PuntiInVirgole(CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text))
                    StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">REGISTRAZIONE CONTRATTO</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & CType(Tab_Registrazione1.FindControl("lblRegCond"), Label).Text & "</td></tr>"
                End If
                If CDbl(par.VirgoleInPunti(CType(Tab_Canone1.FindControl("txtImportoAnticipo"), TextBox).Text)) > 0 Then
                    TotPreventivo = TotPreventivo + CDbl(par.PuntiInVirgole(CType(Tab_Canone1.FindControl("txtImportoAnticipo"), TextBox).Text))
                    StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">ANTICIPO</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & CType(Tab_Canone1.FindControl("txtImportoAnticipo"), TextBox).Text & "</td></tr>"
                End If
                If CDbl(par.VirgoleInPunti(CType(Tab_Canone1.FindControl("txtImportoCauzione"), TextBox).Text)) > 0 And impCauzione <> -1 Then
                    TotPreventivo = TotPreventivo + CDbl(par.PuntiInVirgole(CType(Tab_Canone1.FindControl("txtImportoCauzione"), TextBox).Text))
                    StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">CAUZIONE</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & CType(Tab_Canone1.FindControl("txtImportoCauzione"), TextBox).Text & "</td></tr>"
                End If

                '31-03-2015 aggiungo nuova riga per la restituzione cauzione nel preventivo
                If impCauzione > 0 Then
                    StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">RESTITUZIONE CAUZIONE A SEGUITO CAMBIO TIPOLOGIA CONTRATTUALE</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format((impCauzione * -1), "0.00") & "</td></tr>"
                    TotPreventivo = TotPreventivo + (impCauzione * -1)
                End If
                '31-03-2015 FINE aggiungo nuova riga per la restituzione cauzione nel preventivo


                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">Totale</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(TotPreventivo, "0.00") & "</td></tr>"
                CType(Tab_Bollette1.FindControl("lblPreventivo"), Label).Text = "<table><tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">IMPORTO PRESUNTO BOLLETTINO DI ATTIVAZIONE N.1</td><td></td></tr>" & StringaTabella & "</table>"




            Else
                CType(Tab_Bollette1.FindControl("lblPreventivo"), Label).Text = ""
            End If
            myReaderQ.Close()

            StringaTabella = ""
            TotPreventivo = 0

            'INIZIO BOLLETTINO N.2
            par.cmd.CommandText = "select * from SISCOM_MI.BOL_BOLLETTE where NOTE IN ('ATTIVAZIONE CONTRATTO BOLLETTINO N.2-AFF. E SPESE','ATTIVAZIONE CONTRATTO BOLLETTINO N.2-BOLLO') AND  ID_BOLLETTA_STORNO IS NULL AND ID_TIPO IN (1,10) AND ID_contratto=" & lIdContratto
            myReaderQ = par.cmd.ExecuteReader()
            If myReaderQ.HasRows = False Then

                par.cmd.CommandText = "select bollo from siscom_mi.rapporti_utenza where id=" & lIdContratto
                Dim myReaderJJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderJJ.Read Then
                    SPESE_BOLLO = CDbl(par.PuntiInVirgole(par.IfNull(myReaderJJ("BOLLO"), 0)))
                End If
                myReaderJJ.Close()
                If SPESE_BOLLO = 0 Then


                    Dim importoBolloParam As String = ""
                    Dim Pagine As Integer = 1
                    par.cmd.CommandText = "select valore from SISCOM_MI.PARAMETRI_BOLLETTA where ID = 18"
                    Dim myReaderZ1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderZ1.Read Then
                        importoBolloParam = par.IfNull(myReaderZ1("VALORE"), "0")
                    End If
                    myReaderZ1.Close()

                    Dim NumCopie As Integer = 1
                    par.cmd.CommandText = "select valore from SISCOM_MI.PARAMETRI_BOLLETTA where ID = 45"
                    myReaderZ1 = par.cmd.ExecuteReader()
                    If myReaderZ1.Read Then
                        NumCopie = par.IfNull(myReaderZ1("VALORE"), "0")
                    End If
                    myReaderZ1.Close()

                    Select Case TipoContratto
                        Case "1", "2", "8", "10", "12", "6"
                            Pagine = 5
                            SPESE_BOLLO = Ceiling((CDec(Pagine) / 4)) * CDec(par.PuntiInVirgole(importoBolloParam)) * NumCopie
                            StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">BOLLO SU CONTRATTO (STIMATO)</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESE_BOLLO, "0.00") & "</td></tr>"
                        Case "3"
                            Pagine = 6
                            SPESE_BOLLO = Ceiling((CDec(Pagine) / 4)) * CDec(par.PuntiInVirgole(importoBolloParam)) * NumCopie
                            If CType(Tab_Contratto1.FindControl("cmbDestUso"), DropDownList).SelectedItem.Value = "B" Then
                                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">BOLLO SU CONTRATTO (STIMATO)</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESE_BOLLO, "0.00") & "</td></tr>"
                            Else
                                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">BOLLO SU CONTRATTO (STIMATO)</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESE_BOLLO, "0.00") & "</td></tr>"
                            End If
                        Case "5"
                            Pagine = 6
                            SPESE_BOLLO = Ceiling((CDec(Pagine) / 4)) * CDec(par.PuntiInVirgole(importoBolloParam)) * NumCopie
                            StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">BOLLO SU CONTRATTO (STIMATO)</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESE_BOLLO, "0.00") & "</td></tr>"

                        Case "7"
                            SPESE_BOLLO = 0
                    End Select
                    TotPreventivo = TotPreventivo + SPESE_BOLLO
                Else
                    StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">BOLLO SU CONTRATTO</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(SPESE_BOLLO, "0.00") & "</td></tr>"
                    TotPreventivo = TotPreventivo + SPESE_BOLLO
                End If



                If CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text <> "" And CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text <> "" And CType(Tab_Canone1.FindControl("txtCANONECORRENTE"), TextBox).Text <> "" Then

                    Dim dataProssimoPeriodo As String = ""

                    Dim numerorata As Integer = par.ProssimaRata(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedValue, par.AggiustaData(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text), dataProssimoPeriodo)
                    par.cmd.CommandText = "select * from siscom_mi.PARAMETRI_BOLLETTA WHERE id=5"
                    Dim myReaderW1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderW1.Read = True Then
                        Select Case Format(Mid(par.IfNull(myReaderW1("VALORE"), 0), 5, 2), "00")
                            Case "01", "03", "05", "07", "08", "10", "12"
                                numerorata = par.ProssimaRata(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedValue, par.IfNull(myReaderW1("VALORE"), "0000") & "31", dataProssimoPeriodo)
                            Case "02"
                                numerorata = par.ProssimaRata(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedValue, par.IfNull(myReaderW1("VALORE"), "0000") & "28", dataProssimoPeriodo)
                            Case Else
                                numerorata = par.ProssimaRata(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedValue, par.IfNull(myReaderW1("VALORE"), "0000") & "30", dataProssimoPeriodo)
                        End Select
                    End If
                    myReaderW1.Close()

                    Dim giorni As Integer = 0

                    Select Case Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 4, 2)
                        Case "01", "03", "05", "07", "08", "10", "12"
                            giorni = DateDiff("d", CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, "31" & Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 3, 8)) + 1
                            If giorni = 31 Then giorni = 30
                        Case "02"
                            giorni = DateDiff("d", CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, "28" & Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 3, 8)) + 1
                            If giorni = 28 Then giorni = 30
                        Case Else
                            giorni = DateDiff("d", CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, "30" & Mid(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text, 3, 8)) + 1
                    End Select
                    giorni = giorni + 30 * DateDiff("m", DateAdd(DateInterval.Month, 1, CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text)), par.FormattaData(dataProssimoPeriodo))
                    Dim ImportoResiduo As Double = Format((par.IfEmpty(CType(Tab_Canone1.FindControl("txtCANONECORRENTE"), TextBox).Text, 0) / 12) * (giorni / 30), "0.00")
                    If ImportoResiduo < 0 Then ImportoResiduo = 0
                    If ImportoResiduo > 0 Then
                        TotPreventivo = TotPreventivo + ImportoResiduo
                        Dim sDESCRIZIONE = "dal " & CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text & " al " & Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(dataProssimoPeriodo))), "dd/MM/yyyy")
                        StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">CANONE " & sDESCRIZIONE & "</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(ImportoResiduo, "0.00") & "</td></tr>"
                    End If
                    Dim dataProssimoPeriodo1 As String = ""
                    Dim ImportoResiduo1 As Double = 0
                    Dim ImportoResiduo_2 As Double = 0

                    If ImportoResiduo < 0 Then
                        ImportoResiduo_2 = ImportoResiduo * -1
                        ImportoResiduo = 0
                    End If

                    Select Case Mid(dataProssimoPeriodo, 5, 2)
                        Case "02", "04", "06", "08", "10", "12"
                            numerorata = par.ProssimaRata(CType(Tab_Canone1.FindControl("cmbFreqCanone"), DropDownList).SelectedValue, par.AggiustaData(DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(dataProssimoPeriodo)))), dataProssimoPeriodo1)
                            ImportoResiduo1 = Format((par.IfEmpty(CType(Tab_Canone1.FindControl("txtCANONECORRENTE"), TextBox).Text, 0) / 12) - ImportoResiduo_2, "0.00")
                            giorni = (giorni + 30) / 30
                        Case Else

                    End Select
                    If ImportoResiduo1 < 0 Then ImportoResiduo1 = 0
                    If ImportoResiduo1 > 0 Then
                        TotPreventivo = TotPreventivo + ImportoResiduo1
                        StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">CANONE " & Mid(dataProssimoPeriodo, 5, 2) & "/" & Mid(dataProssimoPeriodo, 1, 4) & "</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(ImportoResiduo1, "0.00") & "</td></tr>"
                    End If

                    Dim IMP300 As Double = 0
                    Dim IMP301 As Double = 0
                    Dim IMP302 As Double = 0
                    Dim IMP303 As Double = 0
                    par.cmd.CommandText = "select IMPORTO,IMPORTO_SINGOLA_RATA FROM SISCOM_MI.BOL_SCHEMA WHERE ANNO=" & Year(Now) & " AND ID_VOCE=300 AND ID_CONTRATTO=" & lIdContratto
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        If giorni > 0 Then
                            IMP300 = ((par.IfNull(myReaderA("IMPORTO_SINGOLA_RATA"), 0) / 1) * giorni)
                        End If
                    End If
                    myReaderA.Close()
                    par.cmd.CommandText = "select IMPORTO,IMPORTO_SINGOLA_RATA FROM SISCOM_MI.BOL_SCHEMA WHERE  ANNO=" & Year(Now) & " AND  ID_VOCE=301 AND ID_CONTRATTO=" & lIdContratto
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        If giorni > 0 Then
                            IMP301 = ((par.IfNull(myReaderA("IMPORTO_SINGOLA_RATA"), 0) / 1) * giorni)
                        End If
                    End If
                    myReaderA.Close()
                    par.cmd.CommandText = "select IMPORTO,IMPORTO_SINGOLA_RATA FROM SISCOM_MI.BOL_SCHEMA WHERE  ANNO=" & Year(Now) & " AND  ID_VOCE=302 AND ID_CONTRATTO=" & lIdContratto
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        If giorni > 0 Then
                            IMP302 = ((par.IfNull(myReaderA("IMPORTO_SINGOLA_RATA"), 0) / 1) * giorni)
                        End If
                    End If
                    myReaderA.Close()
                    par.cmd.CommandText = "select IMPORTO,IMPORTO_SINGOLA_RATA FROM SISCOM_MI.BOL_SCHEMA WHERE  ANNO=" & Year(Now) & " AND ID_VOCE=303 AND ID_CONTRATTO=" & lIdContratto
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        If giorni > 0 Then
                            IMP303 = ((par.IfNull(myReaderA("IMPORTO_SINGOLA_RATA"), 0) / 1) * giorni)
                        End If
                    End If
                    myReaderA.Close()

                    If IMP300 > 0 Or IMP301 > 0 Or IMP302 > 0 Or IMP303 > 0 Then
                        TotPreventivo = TotPreventivo + IMP300 + IMP301 + IMP302 + IMP303
                        'StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">SPESE dal " & par.FormattaData(DataPartenza) & " al " & par.FormattaData(CDate(par.FormattaData(dataProssimoPeriodo)).AddDays(-1)) & "</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(IMP300 + IMP301 + IMP302 + IMP303, "0.00") & "</td></tr>"
                        StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold" & Chr(34) & ">SPESE (STIMATO)</td><td style=" & Chr(34) & "font-family: Arial; font-size: 8px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(IMP300 + IMP301 + IMP302 + IMP303, "0.00") & "</td></tr>"
                    End If
                End If
                StringaTabella = StringaTabella & "<tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">Totale</td><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold;text-align: right" & Chr(34) & ">" & Format(TotPreventivo, "0.00") & "</td></tr>"
                CType(Tab_Bollette1.FindControl("lblPreventivo2"), Label).Text = "<table><tr><td style=" & Chr(34) & "font-family: Arial; font-size: 10px; font-weight: bold" & Chr(34) & ">IMPORTO PRESUNTO BOLLETTINO DI ATTIVAZIONE N.2</td><td></td></tr>" & StringaTabella & "</table>"

            Else
                CType(Tab_Bollette1.FindControl("lblPreventivo2"), Label).Text = ""
            End If
            myReaderQ.Close()



        Else
            CType(Tab_Bollette1.FindControl("lblPreventivo"), Label).Text = ""
            CType(Tab_Bollette1.FindControl("lblPreventivo2"), Label).Text = ""
        End If
    End Sub

    Public Property SaldoContratto() As Double
        Get
            If Not (ViewState("par_SaldoContratto") Is Nothing) Then
                Return CDbl(ViewState("par_SaldoContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_SaldoContratto") = value
        End Set

    End Property
End Class
