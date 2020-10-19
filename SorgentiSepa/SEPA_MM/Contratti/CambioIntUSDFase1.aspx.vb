Imports System.Collections.Generic

Partial Class Contratti_CambioIntUSDFase1
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                IDC.Value = Request.QueryString("IDC")
                IDT.Value = Request.QueryString("IDT")
                DATARIC.Value = Request.QueryString("DATARIC")
                RESTDEP.Value = Request.QueryString("RESTDEP")
                NEWDEP.Value = Request.QueryString("NEWDEP")
                IMPC.Value = Replace(Request.QueryString("IMPC"), ".", ",")

                txtDataOp.Text = Format(Now, "dd/MM/yyyy")
                txtDataOp.Enabled = False
                par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_MOD_RESTITUZIONE ORDER BY DESCRIZIONE ASC", cmbTipo, "ID", "DESCRIZIONE", False)
                CaricaDati()
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Private Function CaricaDati()
        Try
            Dim Errore As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            txtCredito.Text = "0,00"
            txtCredito.Enabled = False
            par.cmd.CommandText = " select rapporti_utenza.*,SISCOM_MI.GETINTESTATARI(ID) AS INTESTATARIO,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.ID=" & IDC.Value
            Dim flInteressi As Integer = 0
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                lblIntestatario.Text = Mid(par.IfNull(myReader3("INTESTATARIO"), ""), 1, Len(par.IfNull(myReader3("INTESTATARIO"), "")) - 1)
                txtCredito.Text = Format(par.IfNull(myReader3("IMP_DEPOSITO_CAUZ"), "0,00"), "0.00")
                IDA.Value = par.IfNull(myReader3("ID_ANAGRAFICA"), "")
                flInteressi = par.IfNull(myReader3("interessi_cauzione"), 0)
                DECORRENZA.Value = par.IfNull(myReader3("DATA_DECORRENZA"), Format(Now, "yyyyMMdd"))
            End If
            myReader3.Close()

            If flInteressi = 1 Then
                txtInteressi.Text = Format(CalcolaInteressi(), "0.00")
                INTERESSI.Value = "1"
            Else
                txtInteressi.Text = "0,00"
                INTERESSI.Value = "0"
            End If

            'MAX 28/08/2017 RESTITUISCO IL DEPOSITO SOLO SE E' STATO PAGATO UN DEPOSITO DA QUESTO CONDUTTORE
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_TIPO=9 AND ID_BOLLETTA_STORNO IS NULL AND NVL(IMPORTO_PAGATO,0)>0 and id_contratto=" & IDC.Value & " AND COD_AFFITTUARIO=" & IDA.Value
            myReader3 = par.cmd.ExecuteReader()
            If myReader3.HasRows = False Then
                Errore = "Attenzione..Nessun DEPOSITO CAUZIONALE PAGATO in partita contabile da questo conduttore! Non sarà emessa la bolletta di restituzione del deposito e dei relativi interessi. Premere il pulsante procedi per continuare."
                txtInteressi.Text = "0,00"
                INTERESSI.Value = "0"
                txtCredito.Text = "0"
            End If
            myReader3.Close()

            'par.cmd.CommandText = " select RAPPORTI_UTENZA_DEP_CAUZ.* FROM SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ WHERE ID_CONTRATTO=" & IDC.Value
            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    txtNote.Text = par.IfNull(myReader2("NOTE_PAGAMENTO"), "")
            '    txtDataOp.Text = par.FormattaData(par.IfNull(myReader2("DATA_OPERAZIONE"), ""))
            '    cmbTipo.SelectedValue = par.IfNull(myReader2("TIPO_PAGAMENTO"), "-1")
            '    txtEstremi.Text = par.IfNull(myReader2("IBAN_CC"), "")
            '    txtCredito.Enabled = False
            '    AssegnaLabel()
            '    Errore = "\nFunzione già attivata"
            'End If
            'myReader2.Close()

            If txtCredito.Text = "0" Then
                lblCREDITO0.Visible = True
                txtNote.Enabled = False
                cmbTipo.Enabled = False
                txtEstremi.Enabled = False
            End If
            If Errore <> "" Then
                txtNote.Enabled = False
                cmbTipo.Enabled = False
                txtEstremi.Enabled = False
                lblErrore.Visible = True
                lblErrore.Text = Errore

            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Private Sub AssegnaLabel()
        Select Case cmbTipo.SelectedItem.Value
            Case "2", "3"
                txtEstremi.Visible = True
                lblEstremi.Text = "IBAN"
                lblEstremi.Visible = True
                lblAvviso.Visible = True
                lblAvviso.Text = "L'intestatario dell'IBAN deve coincidere l'intestatario del contratto"
            Case "1"
                txtEstremi.Visible = False
                lblEstremi.Text = ""
                lblEstremi.Visible = False
                lblAvviso.Visible = False
        End Select
    End Sub

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Protected Sub cmbTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        AssegnaLabel()
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        lblErrore.Visible = False
        If cmbTipo.SelectedItem.Value = "2" Or cmbTipo.SelectedItem.Value = "3" Then
            If par.ControllaIBAN(txtEstremi.Text) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "IBAN non corretto!"
            End If
        End If
        If lblErrore.Visible = False Then
            If lblCREDITO0.Visible = False Then
                Session.Add("DATIRIMB", IDA.Value & "#" & lblIntestatario.Text & "#-" & txtCredito.Text & "#" & txtDataOp.Text & "#" & cmbTipo.SelectedItem.Value & "#" & txtEstremi.Text & "#" & txtNote.Text & "#" & txtInteressi.Text)
            Else
                Session.Add("DATIRIMB", "")
            End If
            'Response.Redirect("CambioIntUSD.aspx?IDT=" & IDT.Value & "&IDC=" & IDC.Value & "&DATARIC=" & DATARIC.Value & "&IMPC=" & IMPC.Value & "&RESTDEP=" & RESTDEP.Value & "&NEWDEP=" & NEWDEP.Value)
            'Response.Write("<script>location.href='" & "CambioIntUSD.aspx?IDT=" & IDT.Value & "&IDC=" & IDC.Value & "&DATARIC=" & DATARIC.Value & "&IMPC=" & IMPC.Value & "&RESTDEP=" & RESTDEP.Value & "&NEWDEP=" & NEWDEP.Value & "';</script>")
            Response.Write("<script>window.open('CambioIntUSD.aspx?IDT=" & IDT.Value & "&IDC=" & IDC.Value & "&DATARIC=" & DATARIC.Value & "&IMPC=" & IMPC.Value & "&RESTDEP=" & RESTDEP.Value & "&NEWDEP=" & NEWDEP.Value & "','Cambio','height=598,top=0,left=0,width=800,scrollbars=no');</script>")
            'window.open('CambioIntUSD.aspx?IDT=<%= lIdConnessione %>&IDC=<%= lIdContratto %>&DATARIC=' + f.RDataRicez + '&IMPC=' + f.RImpCanone + '&RESTDEP=' + restDepCauz + '&NEWDEP=' + newDepCauz + '','Cambio','height=598,top=0,left=0,width=800,scrollbars=no');
        End If
    End Sub

    Private Function CalcolaInteressi() As Double
        Session.Remove("calcoloAdeguamentoInteressi")
        Dim listaDaEseguire As New Generic.List(Of String)
        listaDaEseguire.Clear()
        Dim Interessi As New SortedDictionary(Of Integer, Double)
        Dim DataCalcolo As String = ""
        Dim DataInizio As String = ""

        Dim tasso As Double = 0
        Dim baseCalcolo As Double = 0

        Dim Giorni As Integer = 0
        Dim GiorniAnno As Integer = 0
        Dim dataPartenza As String = ""

        Dim Totale As Double = 0
        Dim TotalePeriodo As Double = 0
        Dim indice As Long = 0
        Dim DataFine As String = ""

        par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legali order by anno desc"
        Interessi.Clear()
        Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReaderC.Read
            Interessi.Add(myReaderC("anno"), myReaderC("tasso"))
        Loop
        myReaderC.Close()
        If Interessi.ContainsKey(Year(Now)) = False Then
            par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legaLI order by anno desc"
            Dim myReaderC1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderC1.Read Then
                Interessi.Add(Year(Now), myReaderC1("tasso"))
            End If
            myReaderC1.Close()
        End If


        DataCalcolo = par.AggiustaData(txtDataOp.Text)

        baseCalcolo = txtCredito.Text
        If baseCalcolo > 0 Then

            par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & IDC.Value & " order by id desc"
            Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderZ.HasRows = False Then
                DataInizio = DECORRENZA.Value
            End If
            If myReaderZ.Read Then
                DataInizio = Format(DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(myReaderZ("data")))), "yyyyMMdd")
            End If

            myReaderZ.Close()

            Giorni = 0
            GiorniAnno = 0
            dataPartenza = DataInizio
            Totale = 0
            TotalePeriodo = 0

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ADEGUAMENTO_INTERESSI.NEXTVAL FROM DUAL"
            Dim idInt As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi (id,id_contratto,data,fl_applicato,ID_ANAGRAFICA) values (" & idInt & "," & IDC.Value & ",'" & DataCalcolo & "',1," & IDA.Value & ")"
            listaDaEseguire.Add(par.cmd.CommandText.ToString) 'par.cmd.ExecuteNonQuery()
            'par.cmd.CommandText = "select siscom_mi.seq_adeguamento_interessi.currval from dual"
            'myReaderZ = par.cmd.ExecuteReader()
            indice = idInt
            'If myReaderZ.Read Then
            'indice = myReaderZ(0)
            'End If

            For I = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                If I = CInt(Mid(DataCalcolo, 1, 4)) Then
                    DataFine = par.FormattaData(DataCalcolo)
                Else
                    DataFine = "31/12/" & I

                End If

                GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & I), CDate("31/12/" & I)) + 1

                Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                If I < 1990 Then
                    tasso = 5
                Else
                    If Interessi.ContainsKey(I) = True Then
                        tasso = Interessi(I)
                    End If
                End If

                TotalePeriodo = Format((((baseCalcolo * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                Totale = Totale + TotalePeriodo

                par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi_voci (id_adeguamento,dal,al,giorni,tasso,importo) values (" & indice & ",'" & dataPartenza & "','" & Format(CDate(DataFine), "yyyyMMdd") & "'," & Giorni & "," & par.VirgoleInPunti(tasso) & "," & par.VirgoleInPunti(TotalePeriodo) & ")"
                listaDaEseguire.Add(par.cmd.CommandText.ToString) 'par.cmd.ExecuteNonQuery()

                dataPartenza = I + 1 & "0101"

            Next
            par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set importo=" & par.VirgoleInPunti(Format(Totale, "0.00")) & " where id=" & indice
            listaDaEseguire.Add(par.cmd.CommandText.ToString) 'par.cmd.ExecuteNonQuery()
            CalcolaInteressi = Totale
        Else
            CalcolaInteressi = 0
        End If
        Session.Item("calcoloAdeguamentoInteressi") = listaDaEseguire
    End Function

End Class
