Imports Telerik.Web.UI

Partial Class Gestione_locatari_PatrimonioImmobComponenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            idImmob.Value = Request.QueryString("IDIMMOB")
            iddich.Value = Request.QueryString("IDD")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            operazione.Value = Request.QueryString("O")

            par.caricaComboTelerik("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & iddich.Value & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", False)
            par.caricaComboTelerik("SELECT * FROM TIPO_PIENA_PATR_IMMOB ORDER BY ID ASC", cmbTipoPropr, "ID", "DESCRIZIONE", False)

            RiempiCatCatastale()
            If Not String.IsNullOrEmpty(idImmob.Value.ToString) Then
                VisualizzaImmob()
            End If
        End If
    End Sub

    Private Sub VisualizzaImmob()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If
            cmbTipoPropr.ClearSelection()
            cmbTipoImm.ClearSelection()
            cmbComponente.ClearSelection()
            cmbTipo.ClearSelection()
            cmbComune.ClearSelection()
            Dim catCatastale As String = ""
            par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID=" & idImmob.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                cmbComponente.SelectedValue = par.IfNull(lettore("ID_COMPONENTE"), -1)
                cmbTipo.SelectedValue = par.IfNull(lettore("ID_TIPO"), -1)
                TxtMutuo.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("MUTUO").ToString.Replace(".", ""), 0)))
                txtValore.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("VALORE").ToString.Replace(".", ""), 0)))
                txtNumVani.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("N_VANI").ToString.Replace(".", ""), 0)))
                txtPerc.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("PERC_PATR_IMMOBILIARE").ToString.Replace(".", ""), 0)))
                txtRendita.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("REND_CATAST_DOMINICALE").ToString.Replace(".", ""), 0)))
                cmbTipoPropr.SelectedValue = par.IfNull(lettore("ID_TIPO_PROPRIETA"), -1)
                txtValoreMercato.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("VALORE_MERCATO").ToString.Replace(".", ""), 0)))
                catCatastale = par.IfNull(lettore("CAT_CATASTALE"), "A2")
                If par.IfNull(lettore("COMUNE"), "") <> "" Then
                    cmbComune.SelectedItem.Text = par.IfNull(lettore("COMUNE"), "MILANO")
                End If
                txtSupUtile.Text = par.VirgoleInPunti(CStr(par.IfNull(lettore("SUP_UTILE").ToString.Replace(".", ""), 0)))

                If Mid(par.IfNull(lettore("CAT_CATASTALE"), ""), 2, 1) = "0" Then
                    catCatastale = Replace(par.IfNull(lettore("CAT_CATASTALE"), "---"), Mid(par.IfNull(lettore("CAT_CATASTALE"), "---"), 2, 1), "")
                End If
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from T_TIPO_CATEGORIE_IMMOBILE where descrizione = '" & catCatastale & "'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                cmbTipoImm.SelectedValue = par.IfNull(lettore("COD"), 0)
            End If
            lettore.Close()

            If ApertaNow Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub cmbTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbTipo.SelectedIndexChanged

        RiempiCatCatastale()

    End Sub

    Private Sub RiempiCatCatastale()
        Try
            Select Case cmbTipo.SelectedValue
                Case "0"
                    cmbTipoImm.Enabled = True
                    par.caricaComboTelerik("SELECT * FROM T_TIPO_CATEGORIE_IMMOBILE ORDER BY descrizione ASC", cmbTipoImm, "COD", "DESCRIZIONE", False)
                Case "1"
                    cmbTipoImm.Enabled = False
                    cmbTipoImm.Items.Clear()
                Case "2"
                    cmbTipoImm.Enabled = False
                    cmbTipoImm.Items.Clear()
            End Select

            par.caricaComboTelerik("select * from comuni_nazioni where sigla<>'E' and sigla<>'C' and sigla<>'I' order by nome asc", cmbComune, "ID", "NOME", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            ScriviPatrImmobiliare()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Function CalcolaIMU() As Decimal
        Dim renditaCat As Double = 0
        Dim iciImu As Double = 0
        Dim rivalutaz As Double = 0
        Dim percPropr As Double = 0

        connData.apri(False)

        Select Case cmbTipo.SelectedValue
            Case 0
                rivalutaz = CDbl((txtRendita.Text / 100) * 5)
                renditaCat = CDbl(txtRendita.Text) + rivalutaz

                par.cmd.CommandText = "SELECT * FROM T_TIPO_CATEGORIE_IMMOBILE WHERE COD='" & cmbTipoImm.SelectedValue & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If Request.QueryString("ANNOREDD") <> "2012" Then
                        iciImu = renditaCat * par.IfNull(myReader("COEFF_ICI"), 0)
                    Else
                        iciImu = renditaCat * par.IfNull(myReader("COEFF_IMU"), 0)
                    End If
                End If
                myReader.Close()
                iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100
                txtValore.Text = Format(iciImu, "0")
            Case 1
                rivalutaz = CDbl((txtRendita.Text / 100) * 25)
                renditaCat = CDbl(txtRendita.Text) + rivalutaz
                iciImu = renditaCat
                iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100
                iciImu = iciImu * 75
                txtValore.Text = Format(iciImu, "0")
            Case 2
                renditaCat = CDbl(txtRendita.Text)
                iciImu = renditaCat
                iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100
                txtValore.Text = Format(iciImu, "0")
        End Select

        connData.chiudi(False)

        Return iciImu

    End Function

    Private Sub ScriviPatrImmobiliare()

        connData.apri(True)

        Dim tipoPropr As String = ""
        Dim FL_70KM As String = ""

        tipoPropr = par.PulisciStrSql(cmbTipoPropr.SelectedValue)

        If tipoPropr = "-1" Then
            tipoPropr = "NULL"
        End If

        FL_70KM = "0"
        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(cmbComune.SelectedItem.Text) & "'"
        Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderCC.Read() Then
            If myReaderCC("DISTANZA_KM") <= 70 Then
                FL_70KM = "1"
            End If
            If myReaderCC("DISTANZA_KM") = "0" Then
                If cmbTipo.SelectedValue = "0" Then
                    If cmbTipoImm.SelectedValue >= 0 And cmbTipoImm.SelectedValue < 11 Then
                        Response.Write("<script>alert('Attenzione...per il comune selezionato non è stata specificata la distanza in km. Il calcolo del canone potrebbe essere ERRATO. Contattare il responsabile');</script>")
                    End If
                End If
            End If
        End If
        myReaderCC.Close()

        If operazione.Value = "0" Then
            par.cmd.CommandText = "INSERT INTO COMP_PATR_IMMOB_VSA (ID, ID_COMPONENTE, ID_TIPO, PERC_PATR_IMMOBILIARE, VALORE, MUTUO, F_RESIDENZA, CAT_CATASTALE, COMUNE, N_VANI, SUP_UTILE, FL_70KM, REND_CATAST_DOMINICALE, ID_TIPO_PROPRIETA,VALORE_MERCATO) " _
                & " VALUES (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & cmbTipo.SelectedValue & "," & par.VirgoleInPunti(par.IfEmpty(txtPerc.Text, 0)) & "," & par.IfEmpty(txtValore.Text, 0) & "," & par.IfEmpty(TxtMutuo.Text, 0) & "," _
                & " '0','" & cmbTipoImm.SelectedItem.Text & "','" & par.PulisciStrSql(cmbComune.SelectedItem.Text) & "','" & txtNumVani.Text & "','" & txtSupUtile.Text & "','" & FL_70KM & "'," & par.IfEmpty(txtRendita.Text, 0) & "," & tipoPropr & "," & par.IfEmpty(txtValoreMercato.Text, "NULL") & ")"
            par.cmd.ExecuteNonQuery()
        Else
            par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID=" & idImmob.Value
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderI.Read Then
                par.cmd.CommandText = "UPDATE COMP_PATR_IMMOB_VSA" _
                    & " SET " _
                    & " ID_COMPONENTE=" & cmbComponente.SelectedValue & "," _
                    & " ID_TIPO= " & cmbTipo.SelectedValue & "," _
                    & " PERC_PATR_IMMOBILIARE=" & par.VirgoleInPunti(txtPerc.Text) & "," _
                    & " VALORE= " & txtValore.Text & "," _
                    & " MUTUO=" & TxtMutuo.Text & "," _
                    & " CAT_CATASTALE='" & cmbTipoImm.SelectedItem.Text & "'," _
                    & " COMUNE='" & par.PulisciStrSql(cmbComune.SelectedItem.Text) & "'," _
                    & " N_VANI='" & txtNumVani.Text & "'," _
                    & " SUP_UTILE='" & txtSupUtile.Text & "'," _
                    & " FL_70KM='" & FL_70KM & "'," _
                    & " REND_CATAST_DOMINICALE=" & par.IfEmpty(txtRendita.Text, 0) & "," _
                    & " ID_TIPO_PROPRIETA=" & tipoPropr & "," _
                    & " VALORE_MERCATO= " & par.IfEmpty(txtValoreMercato.Text, "NULL") & "" _
                    & " WHERE ID=" & idImmob.Value
                par.cmd.ExecuteNonQuery()
            End If
            myReaderI.Close()
        End If

        connData.chiudi(True)

        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)


    End Sub

    Protected Sub btnCalcolaICI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCalcolaICI.Click
        Try
            CalcolaIMU()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
